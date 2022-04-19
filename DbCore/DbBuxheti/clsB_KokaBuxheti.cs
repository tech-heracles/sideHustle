using System;
using System.Data;
using System.Linq;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Validation;
using DbCore.DbShare;
using DbCore.DbAdmin;
using System.Collections.Generic;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;
using DbCore.DbArkaBanka;
using DbCore.DbKontabiliteti;
using DbCore.DbAsete;
using DbCore.DbInventari;
using Newtonsoft.Json;

namespace DbCore.DbBuxheti
{
    public class ClsBKokaBuxheti : IDataBase
    {
        #region Atributes
        public int IdBuxhetiKoka { get; set; }
        public int IdNivel { get; set; }
        public int IdKonfigAmbjente { get; set; }
        public string NrDok { get; set; }
        public DateTime? DtDok { get; set; }
        public decimal TotaliPlanifikuar { get; set; }
        public decimal Totali { get; set; }
        public int IdStatusDok { get; set; }
        public int IdNderm { get; set; }
        public int IdNdermVit { get; set; }
        public string Shenime { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        public int IdPerdoruesi { get; set; }
        public int IdKrijuesi { get; set; }
        public int Viti { get; set; }
        public int IdRaportDesign { get; set; }
        public int IdKokaKonvertimiNga { get; set; }
        public string LlojKonfigKonvertimiNga { get; set; }
        public ColBTrupiBuxheti OColBTrupiBuxheti { get; set; }
        public int IdKatDok { get; set; }
        public int IdNdermPostuesi { get; set; }
        public string KodNdermPostuesi { get; set; }
        public int IdDokPostuesi { get; set; }
        public int IdEntiteti { get; set; }
        public int LlojVeprimiGjenerimi { get; set; }
        public decimal TotaliPaTvsh { get; set; }
        public string Entiteti { get; set; }
        public int IdBurimi { get; set; }
        public StatusAprovimi StatusAprovimi { get; set; }
        public clsEtapeAprovimi EtapeAprovimi { get; set; }
        public bool VjenNgaImportSql { get; set; }
        public string TabeleKokeImporti { get; set; }
        public string IdDokImporti { get; set; }
        public int Doktori { get; set; }
        #endregion

        #region Konstruktoret
        public ClsBKokaBuxheti()
        {
        }

        public ClsBKokaBuxheti(int idBuxhetiKoka, int idNivel, int idKonfigAmbjente, string nrDok, DateTime dtDok, decimal totaliPlanifikuar, decimal totali, int idStatusDok, int idNderm, int idNdermVit, string shenime, DateTime dtKrijimi, DateTime dtModifikimi, int idPerdoruesi, int idKrijuesi, int viti, int idRaportDesign, int idKokaKonvertimiNga, string llojKonfigKonvertimiNga, int idNdermPostuesi, string kodNdermPostuesi, int idDokPostuesi, int idKatDok, int idEntiteti, int llojVeprimi, decimal totaliPaTvsh, StatusAprovimi statusAprovimi, string entiteti, int idBurimi, int doktori)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi,  idBuxhetiKoka,  idNivel,  idKonfigAmbjente, nrDok, dtDok, totaliPlanifikuar, totali,  idStatusDok,  idNderm,  idNdermVit, shenime, dtKrijimi, dtModifikimi,  idPerdoruesi,  idKrijuesi,  viti,  idRaportDesign,  idKokaKonvertimiNga, llojKonfigKonvertimiNga,  idNdermPostuesi, kodNdermPostuesi,  idDokPostuesi,  idKatDok,  idEntiteti,  llojVeprimi, totaliPaTvsh,  statusAprovimi, entiteti,  idBurimi);
            
            IdBuxhetiKoka = idBuxhetiKoka;
            IdNivel = idNivel;
            IdKonfigAmbjente = idKonfigAmbjente;
            NrDok = nrDok;
            DtDok = dtDok;
            TotaliPlanifikuar = totaliPlanifikuar;
            Totali = totali;
            IdStatusDok = idStatusDok;
            IdNderm = idNderm;
            IdNdermVit = idNdermVit;
            Shenime = shenime;
            DtKrijimi = dtKrijimi;
            DtModifikimi = dtModifikimi;
            IdPerdoruesi = idPerdoruesi;
            IdKrijuesi = idKrijuesi;
            Viti = viti;
            IdRaportDesign = idRaportDesign;
            IdKokaKonvertimiNga = idKokaKonvertimiNga;
            LlojKonfigKonvertimiNga = llojKonfigKonvertimiNga;
            IdNdermPostuesi = IdNdermPostuesi;
            KodNdermPostuesi = kodNdermPostuesi;
            IdDokPostuesi = idDokPostuesi;
            IdKatDok = idKatDok;
            IdEntiteti = idEntiteti;
            LlojVeprimiGjenerimi = llojVeprimi;
            TotaliPaTvsh = totaliPaTvsh;
            StatusAprovimi = statusAprovimi;
            Entiteti = entiteti;
            IdBurimi = idBurimi;
            Doktori = doktori;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka, idNivel, idKonfigAmbjente, nrDok, dtDok, totaliPlanifikuar, totali, idStatusDok, idNderm, idNdermVit, shenime, dtKrijimi, dtModifikimi, idPerdoruesi, idKrijuesi, viti, idRaportDesign, idKokaKonvertimiNga, llojKonfigKonvertimiNga, idNdermPostuesi, kodNdermPostuesi, idDokPostuesi, idKatDok, idEntiteti, llojVeprimi, totaliPaTvsh, statusAprovimi, entiteti, idBurimi);
        }

        public ClsBKokaBuxheti(int idKoka)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.ktheKokaBuxhetiSipasID(idKoka, this);

            OColBTrupiBuxheti = new ColBTrupiBuxheti(idKoka);
        }

        public ClsBKokaBuxheti(IDataRecord record)
        {
            Mbush(record);
        }
        #endregion

        #region Metoda Publike
        public void Mbush(IDataRecord record)
        {
            IdBuxhetiKoka = !Convert.IsDBNull(record["IDBUXHETIKOKA"]) ? Convert.ToInt32(record["IDBUXHETIKOKA"]) : 0;
            IdNivel = !Convert.IsDBNull(record["IDNIVEL"]) ? Convert.ToInt32(record["IDNIVEL"]) : 0;
            IdKonfigAmbjente = !Convert.IsDBNull(record["IDKONFIGAMBJENTE"]) ? Convert.ToInt32(record["IDKONFIGAMBJENTE"]) : 0;
            NrDok = !Convert.IsDBNull(record["NRDOK"]) ? Convert.ToString(record["NRDOK"]) : String.Empty;
            DtDok = !Convert.IsDBNull(record["DTDOK"]) ? Convert.ToDateTime(record["DTDOK"]) : (DateTime?)null;
            TotaliPlanifikuar = !Convert.IsDBNull(record["TOTALIPLANIFIKUAR"]) ? Convert.ToDecimal(record["TOTALIPLANIFIKUAR"]) : 0;
            Totali = !Convert.IsDBNull(record["TOTALI"]) ? Convert.ToDecimal(record["TOTALI"]) : 0;
            IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0;
            IdNderm = !Convert.IsDBNull(record["IDNDERM"]) ? Convert.ToInt32(record["IDNDERM"]) : 0;
            IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0;
            Shenime = !Convert.IsDBNull(record["SHENIME"]) ? Convert.ToString(record["SHENIME"]) : String.Empty;
            DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null;
            DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null;
            IdPerdoruesi = !Convert.IsDBNull(record["IDPERDORUESI"]) ? Convert.ToInt32(record["IDPERDORUESI"]) : 0;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            Viti = !Convert.IsDBNull(record["VITI"]) ? Convert.ToInt32(record["VITI"]) : 0;
            IdRaportDesign = !Convert.IsDBNull(record["IDRAPORTDESIGN"]) ? Convert.ToInt32(record["IDRAPORTDESIGN"]) : 0;
            IdKokaKonvertimiNga = !Convert.IsDBNull(record["IDKOKAKONVERTIMINGA"]) ? Convert.ToInt32(record["IDKOKAKONVERTIMINGA"]) : 0;
            LlojKonfigKonvertimiNga = !Convert.IsDBNull(record["LLOJKONFIGKONVERTIMINGA"]) ? Convert.ToString(record["LLOJKONFIGKONVERTIMINGA"]) : String.Empty;
            IdKatDok = !Convert.IsDBNull(record["IDKATDOK"]) ? Convert.ToInt32(record["IDKATDOK"]) : 0; 
            IdNdermPostuesi = !Convert.IsDBNull(record["IDNDERMPOSTUESI"]) ? Convert.ToInt32(record["IDNDERMPOSTUESI"]) : 0; 
            KodNdermPostuesi = !Convert.IsDBNull(record["KODNDERMPOSTUESI"]) ? Convert.ToString(record["KODNDERMPOSTUESI"]) : String.Empty; 
            IdDokPostuesi = !Convert.IsDBNull(record["IDDOKPOSTUESI"]) ? Convert.ToInt32(record["IDDOKPOSTUESI"]) : 0;
            IdEntiteti = !Convert.IsDBNull(record["IDENTITETI"]) ? Convert.ToInt32(record["IDENTITETI"]) : 0;
            Entiteti = !Convert.IsDBNull(record["ENTITETI"]) ? Convert.ToString(record["ENTITETI"]) : "";
            IdBurimi = !Convert.IsDBNull(record["IDBURIMI"]) ? Convert.ToInt32(record["IDBURIMI"]) : 0;
            LlojVeprimiGjenerimi = !Convert.IsDBNull(record["LLOJVEPRIMIGJENERIMI"]) ? Convert.ToInt32(record["LLOJVEPRIMIGJENERIMI"]) : 0;
            TotaliPaTvsh = !Convert.IsDBNull(record["TOTALIPATVSH"]) ? Convert.ToDecimal(record["TOTALIPATVSH"]) : 0;
            StatusAprovimi = (StatusAprovimi)(!Convert.IsDBNull(record["STATUSAPROVIMI"]) ? Convert.ToInt32(record["STATUSAPROVIMI"]) : 0);
            Doktori = !Convert.IsDBNull(record["DOKTORI"]) ? Convert.ToInt32(record["DOKTORI"]) : 0;
        }

        public clsMesazh Valido()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            var validim = new ClsBValidimBuxheti();
            clsMesazh msg = validim.Valido(this);
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return msg;
        }

        public clsMesazh Ruaj()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

                var dbData = new DbData();
                using (var scope = new MyTransactionScope(dbData))
                {
                    var mesazh = RuajRegjistrimNeDb(dbData);
                    if (!mesazh)
                        return mesazh;

                    mesazh = KaloNeHistorik();
                    if (!mesazh)
                        return mesazh;

                    if (VjenNgaImportSql)
                    {
                        mesazh = UpdateStatusImporti(dbData);
                        if (!mesazh)
                            return mesazh;
                    }

                    scope.Complete();

                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return new MesazhSuksesi("Dokumenti u ruajt me sukses.");
                }
            } catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi($"Ndodhi nje gabim gjate ruajtjes se dokumentit te buxhetit me id {IdBuxhetiKoka}. \n" + ex.Message);
            }
        }

        public clsMesazh Modifiko()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
                var dbData = new DbData();
                using (var scope = new MyTransactionScope(dbData))
                {
                    bool aprovuar = false;
                    #region dokumentaPerAprovim
                    clsMesazh mesazh = AprovoDokumentBuxheti(dbData, out aprovuar);
                    if (!mesazh.Status)
                        return mesazh;

                    if (aprovuar)
                    {
                        scope.Complete();
                        return mesazh;
                    }
                    #endregion

                    mesazh = Valido();
                    if (!mesazh.Status)
                        return mesazh;

                    //Modifikojme koken 
                    mesazh = ModifikoKokaBuxheti();
                    if (!mesazh)
                        return mesazh;

                    if (IdKatDok == 175 || IdKatDok == 177 || IdKatDok == 179 || IdKatDok == 181)
                    {
                        mesazh = FshiTrupDokumentBuxheti();
                        if (!mesazh)
                            return mesazh;
                    }

                    //Modifikojme trupin
                    OColBTrupiBuxheti.ForEach(x => x.IdBuxhetiKoka = IdBuxhetiKoka);
                    mesazh = OColBTrupiBuxheti.Ruaj();
                    if (!mesazh)
                        return mesazh;

                    mesazh = GjeneroDokumentVartes(dbData);
                    if (!mesazh)
                        return mesazh;

                    //Dokumentin e modifikuar bashke me turpat e kalojme ne historik
                    mesazh = KaloNeHistorik();
                    if (!mesazh)
                        return mesazh;

                    //Dokumentin e Alokimit/Rialokimit e postojme tek bijat kur kalon me status Ruajtur 1/Refuzuar 8
                    mesazh = this.PostoTeBija(IdPerdoruesi);
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();

                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return new MesazhSuksesi("Dokumenti u ruajt me sukses.");
                }
            } catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi($"Ndodhi nje gabim gjate modifikimit te dokumentit me id {IdBuxhetiKoka}. \n" + ex.Message);
            }
        }
        
        public clsMesazh Fshi()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
                var dbData = new DbData();
                using (var scope = new MyTransactionScope())
                {
                    var mesazh = ClsBValidimBuxheti.validoVeprimPerStatus(this, 2);
                    if (!mesazh)
                        return mesazh;

                    IdStatusDok = 2;
                    mesazh = FshiDokumentGjeneruar(dbData);
                    if (!mesazh)
                        return mesazh;

                    mesazh = KaloNeHistorik();
                    if (!mesazh)
                        return mesazh;

                    mesazh = FshiRegjistrim();
                    if (!mesazh)
                        return mesazh;


                    mesazh = FshiKomponenteVlereGjeneruar();
                    if (!mesazh)
                        return mesazh;

                    scope.Complete();

                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return new MesazhSuksesi("Dokumenti u fshi me sukses.");
                }
            } catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi($"Dokumenti me id {IdBuxhetiKoka} nuk u fshi.");
            }
        }

        public static int ktheIdRaportDesign(int idbuxhetikoka)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.ktheIdRaportDesignKokaBuxheti(idbuxhetikoka);
        }

        public ClsBKokaBuxheti MbushKokePaTrup(int idBuxhetiKoka)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.ktheKokaBuxhetiSipasID(idBuxhetiKoka, this);
            return this;
        }

        public static clsMesazh EshteKonvertuarDokMPBuxhetiMeId(int id)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, id);

            var eshteKonvertuar = false;
            using (var db = new ClsDatabaseBuxheti())
                eshteKonvertuar = db.eshteKonvertuarDokMPBuxhetiMeId(id);
            if (eshteKonvertuar)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ky dokument eshte konvertuar njehere dhe nuk mund te konvertohet serish.");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, id);
            return new MesazhSuksesi();
        }

        public static clsMesazh EshteKonvertuarPlotesishtDokBuxhetiMeId(int id)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, id);

            var eshteKonvertuar = false;
            using (var db = new ClsDatabaseBuxheti())
                eshteKonvertuar = db.eshteKonvertuarPlotesishtDokBuxhetiMeId(id);
            if (eshteKonvertuar)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ky dokument eshte konvertuar njehere plotesisht dhe nuk mund te konvertohet serish.");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, id);
            return new MesazhSuksesi();
        }

        public clsMesazh PostoDokumentBuxheti(int idPerdoruesi)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idPerdoruesi);
                using (var scope = new MyTransactionScope())
                {
                    int idDokPostuar = 0;
                    var mesazh = ValidoPostim();
                    if (!mesazh)
                        return mesazh;

                    if (IdKatDok != 170)
                        mesazh = UpdateStatusDok(idPerdoruesi, 9);
                    else
                        mesazh = EshtePostuarMiratimi();
                    if (!mesazh)
                        return mesazh;

                    switch (IdKatDok)
                    {
                        case 170:
                            mesazh = PostoDokumentMiratimi(idPerdoruesi);
                            break;
                        case 172:
                            mesazh = RuajDokumentPostimAlokimi(idPerdoruesi);
                            break;
                        case 175:
                            mesazh = PostoDokBuxhetiSipasDestinacionitINS(idPerdoruesi);
                            break;
                        case 179:
                            switch (clsNivelRegjistrimi.ktheKodNivelRegjistrimi(IdNivel))
                            {
                                case "PIB":
                                    mesazh = PostoDokBuxhetiSipasDestinacionitINSOutId(idPerdoruesi, out idDokPostuar);
                                    break;
                                case "PEB":
                                    var nivelNderm = new clsNdermarrje(IdNderm).Nivelstrukture;
                                    if (nivelNderm == 3)
                                        mesazh = PostoDokBuxhetiSipasDestinacionitINS(idPerdoruesi);
                                    else
                                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Kjo nenkategori dokumenti nuk mund te postohet!");
                                    break;
                            }
                            break;
                        default:
                            return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Nuk ka dokument per postim");
                    }
                    if (!mesazh)
                        return mesazh;

                    using (var db = new ClsDatabaseBuxheti())
                    {
                        mesazh = RuajEtapeAprovimi(db, idDokPostuar);
                        if (!mesazh)
                            return mesazh;

                        mesazh = db.hidhNeHistorikBKokaBuxheti(IdBuxhetiKoka, 9, idPerdoruesi, IdNderm);
                        if (!mesazh)
                            return mesazh;
                    }

                    scope.Complete();
                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idPerdoruesi);
                    return new MesazhSuksesi("Dokumenti u postua me sukses.");
                }
            } catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi($"Ndodhi nje gabim gjate postimit te dokumentit me id {IdBuxhetiKoka} te prindi. \n" + ex.Message);
            }
        }

        public clsMesazh PostoTeBija(int idPerdoruesi)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idPerdoruesi);
                if (String.IsNullOrEmpty(KodNdermPostuesi) || (IdStatusDok != 1 && IdStatusDok != 8) || (IdKatDok != 172 && IdKatDok != 175 && IdKatDok != 179))
                {
                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idPerdoruesi);
                    return new MesazhSuksesi();
                }

                clsMesazh mesazh = new clsMesazh();

                switch (IdKatDok)
                {
                    case 172:
                        mesazh = this.ModifikoDokAlokimiTeBija(idPerdoruesi);
                        break;
                    case 175:
                        if (IdStatusDok == 8)
                            mesazh = this.UpdateStatusRefuzuar(idPerdoruesi, IdKonfigAmbjente);
                        else
                            mesazh = this.ModifikoDokRialokimiTeBijaDheMbesa(idPerdoruesi);
                        break;
                    case 179:
                        switch (clsNivelRegjistrimi.ktheKodNivelRegjistrimi(IdNivel))
                        {
                            case "PIB":
                                if (IdStatusDok == 8)
                                    mesazh = this.UpdateStatusRefuzuar(idPerdoruesi, IdKonfigAmbjente);
                                else
                                    mesazh = this.ModifikoDokRialokimiTeBijaDheMbesa(idPerdoruesi);
                                break;
                            case "PEB":
                                mesazh = this.ModifikoDokAlokimiTeBija(idPerdoruesi);
                                break;
                        }
                        break;
                    default:
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Nuk ka dokument per postim");
                }

                if (!mesazh)
                    return mesazh;

                using (var db = new ClsDatabaseBuxheti())
                    mesazh = db.hidhNeHistorikBKokaBuxheti(IdDokPostuesi, IdStatusDok, idPerdoruesi, IdNderm);
                if (!mesazh)
                    return mesazh;

                switch (IdKatDok)
                {
                    case 172:
                        EmailComposer.DergoEmailNjoftuesPerAlokimBuxheti(idPerdoruesi, IdNderm, IdNdermPostuesi, Viti, ""); 
                        break;
                    case 175:
                        EmailComposer.DergoEmailAprovimRefuzimRialokimBuxheti(idPerdoruesi, IdNderm, IdNdermPostuesi, DtDok, IdKonfigAmbjente, IdDokPostuesi, IdStatusDok, NrDok);
                        break;
                    case 179:
                        switch (clsNivelRegjistrimi.ktheKodNivelRegjistrimi(IdNivel))
                        {
                            case "PIB":
                                EmailComposer.DergoEmailAprovimRefuzimPlanifikimInvestimBuxheti(idPerdoruesi, IdNderm, IdNdermPostuesi, DtDok, IdKonfigAmbjente, IdDokPostuesi, IdStatusDok, NrDok);
                                break;
                            case "PEB":
                                EmailComposer.DergoEmailPlanifikimEkzekutimBuxheti(idPerdoruesi, IdNderm, IdNdermPostuesi, Viti, IdStatusDok, "");
                                break;
                        }
                        break;
                }

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idPerdoruesi);
                return new MesazhSuksesi("Dokumenti u modifikua me sukses te qendra.");
            } catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi($"Ndodhi nje gabim gjate modifikimit te dokumentit me id {IdDokPostuesi} te qendra. \n" + ex.Message);
            }
        }

        public clsMesazh ModifikoDokAlokimiTeBija(int idPerdoruesi)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.ModifikoAlokimTeBija(idPerdoruesi, IdBuxhetiKoka);
        }

        public clsMesazh ModifikoDokRialokimiTeBijaDheMbesa(int idPerdoruesi)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.ModifikoRialokimTeBijaDheMbesa(idPerdoruesi, IdBuxhetiKoka);
        }

        public clsMesazh UpdateStatusDok(int idPerdoruesi, int idStatusDok)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.updateStatusDokBuxheti(IdBuxhetiKoka, idPerdoruesi, idStatusDok);
        }

        public clsMesazh RuajDokumentPostimAlokimi(int idPerdoruesi)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.PostoAlokim(IdBuxhetiKoka, idPerdoruesi);
        }

        public clsMesazh PostoDokBuxhetiSipasDestinacionitINS(int idPerdoruesi)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.PostoDokBuxhetiSipasDestinacionitINS(IdBuxhetiKoka, IdKonfigAmbjente, idPerdoruesi);
        }

        public clsMesazh PostoDokBuxhetiSipasDestinacionitINSOutId(int idPerdoruesi, out int idDokPostuar)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.PostoDokBuxhetiSipasDestinacionitINSOutId(IdBuxhetiKoka, IdKonfigAmbjente, idPerdoruesi, out idDokPostuar);
        }

        public clsMesazh PostoDokumentMiratimi(int idPerdoruesi)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.PostoBuxhetim(IdBuxhetiKoka, idPerdoruesi);
        }

        public clsMesazh UpdateStatusRefuzuar(int idPerdoruesi, int idKonfigAmbjente)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.UpdateStatusRefuzuar(IdBuxhetiKoka, idPerdoruesi, idKonfigAmbjente);
        }

        public static int ktheIdStatusDok(int idkoka)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.ktheIdStatusDokKokaBuxheti(idkoka);
        }

        public clsMesazh ValidoPostim()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            var validim = new ClsBValidimBuxheti();
            clsMesazh msg = validim.ValidoPostim(this);
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return msg;
        }

        #endregion

        #region Metoda Private

        private clsMesazh FshiKomponenteVlereGjeneruar()
        {
            if (IdKatDok != 171)
                return new MesazhSuksesi();

            ColBKomponenteVlere vleratGjeneruar = new ColBKomponenteVlere(IdBuxhetiKoka, true);
            return vleratGjeneruar.Fshi();
        }

        private clsMesazh AprovoDokumentBuxheti(DbData dbData, out bool aprovuar)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            aprovuar = false; //true - vetem kur dokumenti eshte me status aprovimi dhe aprovohet ose refuzohet, false - rastet e tjera
            if (IdKatDok != 179 || clsNivelRegjistrimi.ktheKodNivelRegjistrimi(IdNivel) != "PIB")
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi();
            }

            clsMesazh mesazh = RuajEtapeAprovimi(dbData, 0);
            if (!mesazh)
                return mesazh;

            int statusAprovimiUpdatuar = KtheStatusAprovimiDokumenti(IdBuxhetiKoka);

            if ((StatusAprovimi)statusAprovimiUpdatuar == StatusAprovimi.Aprovuar)
            {
                this.IdStatusDok = 1;
                this.StatusAprovimi = StatusAprovimi.Aprovuar;
                mesazh = this.ModifikoPaTrans(dbData);
                if (!mesazh)
                    return mesazh;
                aprovuar = true;
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi("Dokumenti u aprovua me sukses.");

            }
            if ((StatusAprovimi)statusAprovimiUpdatuar == StatusAprovimi.Refuzuar)
            {
                this.IdStatusDok = 8;
                this.StatusAprovimi = StatusAprovimi.Refuzuar;
                mesazh = this.ModifikoPaTrans(dbData);
                if (!mesazh)
                    return mesazh;
                aprovuar = true;
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return new MesazhSuksesi("Dokumenti u refuzua me sukses.");
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi("Dokumenti nuk ishte per aprovim.");
        }

        private clsMesazh RuajRegjistrimNeDb(DbData dbData)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            var mesazh = Valido();
            if (!mesazh.Status)
                return mesazh;

            mesazh = RuajKokaBuxheti();
            if (!mesazh.Status)
                return mesazh;

            OColBTrupiBuxheti.ForEach(x => x.IdBuxhetiKoka = IdBuxhetiKoka);
            
            mesazh = OColBTrupiBuxheti.Ruaj();
            if (!mesazh.Status)
                return mesazh;

            mesazh = GjeneroDokumentVartes(dbData);
            if (!mesazh)
                return mesazh;

            mesazh = RuajEtapeAprovimi(dbData, 0);
            if (!mesazh)
                return mesazh;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        private clsMesazh RuajEtapeAprovimi(DbData dbData, int idDokPasPostimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDokPasPostimi);
            var nivelStrukture = new clsNdermarrje(IdNderm).Nivelstrukture;
            if (clsNivelRegjistrimi.ktheKodNivelRegjistrimi(IdNivel) != "PIB" || nivelStrukture == 3 || IdStatusDok == 8)
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPasPostimi);
                return new MesazhSuksesi();
            }
            
            if (IdBuxhetiKoka > 0 && (StatusAprovimi)EtapeAprovimi.StatusAprovimi == StatusAprovimi.Undefined && nivelStrukture == 2)
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPasPostimi);
                return new MesazhSuksesi();
            }

            clsEtapeAprovimi etape = new clsEtapeAprovimi();
            string serverUrl = "";
            clsMesazh mesazh;
            if (idDokPasPostimi > 0)
            {
                var dokPasPostimi = new ClsBKokaBuxheti(idDokPasPostimi);
                mesazh = etape.ruaj(dokPasPostimi.IdPerdoruesi, serverUrl, EtapeAprovimi.IdSkema, new clsDatabaseRegjistrim(dbData), dokPasPostimi.IdBuxhetiKoka, dokPasPostimi.IdPerdoruesi, (StatusAprovimi)EtapeAprovimi.StatusAprovimi, EtapeAprovimi.IdEtapa, dokPasPostimi.IdKatDok, dokPasPostimi.IdStatusDok, dokPasPostimi.IdNderm, dokPasPostimi.StatusAprovimi, 0, dokPasPostimi.IdBuxhetiKoka, dokPasPostimi.IdPerdoruesi, (double)dokPasPostimi.Totali, dokPasPostimi.DtKrijimi != null ? (DateTime)dokPasPostimi.DtKrijimi : (DateTime)dokPasPostimi.DtDok, dokPasPostimi.IdKonfigAmbjente, dokPasPostimi.NrDok, (DateTime)dokPasPostimi.DtDok);
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPasPostimi);
                return mesazh;
            }

            mesazh = etape.ruaj(IdPerdoruesi, serverUrl, EtapeAprovimi.IdSkema, new clsDatabaseRegjistrim(dbData), IdBuxhetiKoka, IdPerdoruesi, (StatusAprovimi)EtapeAprovimi.StatusAprovimi, EtapeAprovimi.IdEtapa, IdKatDok, IdStatusDok, IdNderm, StatusAprovimi, 0, IdBuxhetiKoka, IdPerdoruesi, (double)Totali, DtKrijimi != null ? (DateTime)DtKrijimi : (DateTime)DtDok, IdKonfigAmbjente, NrDok, (DateTime)DtDok);
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPasPostimi);
            return mesazh;
        }
        
        private clsMesazh RuajKokaBuxheti()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajKokaBuxheti(this);
        }

        private clsMesazh ModifikoKokaBuxheti()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.ModifikoKokaBuxheti(this);
        }

        private clsMesazh KaloNeHistorik()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.hidhNeHistorikBKokaBuxheti(IdBuxhetiKoka, IdStatusDok, IdPerdoruesi, IdNderm);

        }

        private clsMesazh FshiRegjistrim()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.fshiDokBuxheti(IdBuxhetiKoka);
        }

        private clsMesazh FshiTrupDokumentBuxheti()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.fshiTrupDokumentBuxheti(IdBuxhetiKoka);
        }

        public static int KtheIdGjeneruarDokumenti(int idBuxhetiKoka)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.KtheIdGjeneruarDokumenti(idBuxhetiKoka);
        }

        public static int KtheStatusAprovimiDokumenti(int idBuxhetiKoka)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.KtheStatusAprovimiDokumenti(idBuxhetiKoka);
        }

        private clsMesazh EshtePostuarMiratimi()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            bool postuar;
            using (var db = new ClsDatabaseBuxheti())
                postuar = db.eshtePostuarMiratimBuxhetiMeId(IdBuxhetiKoka);
            if (postuar)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti eshte postuar nje here.");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi("Dokumenti nuk eshte postuar me pare.");
        }

        private clsMesazh GjeneroDokumentVartes(DbData dbData)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            clsMesazh mesazh = new MesazhSuksesi();
            if ((IdKatDok == 177 || IdKatDok == 181) && IdStatusDok == 1 && clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "GJDV") == "Po")
            {
                mesazh = FshiDokumentGjeneruar(dbData);
                if (!mesazh)
                    return mesazh;
                switch (LlojVeprimiGjenerimi)
                {
                    case (int)EnumBLlojeVeprimi.Shitje:
                        mesazh = GjeneroShitje(dbData);
                        break;
                    case (int)EnumBLlojeVeprimi.Blerje:
                        mesazh = GjeneroShitje(dbData);
                        break;
                    case (int)EnumBLlojeVeprimi.Arketim:
                        mesazh = GjeneroArketim(dbData);
                        break;
                    case (int)EnumBLlojeVeprimi.Pagese:
                        mesazh = GjeneroArketim(dbData);
                        break;
                }
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        private clsMesazh FshiDokumentGjeneruar(DbData dbData)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            if (IdKatDok != 177 && IdKatDok != 181)
                return new MesazhSuksesi();

            clsMesazh mesazh = new MesazhSuksesi();
            int idGjeneruar = KtheIdGjeneruarDokumenti(IdBuxhetiKoka);
            if (idGjeneruar > 0)
            {
                switch (LlojVeprimiGjenerimi)
                {
                    case (int)EnumBLlojeVeprimi.Shitje:
                    case (int)EnumBLlojeVeprimi.Blerje:
                        var kokaShitje = new clsKokaShitje(idGjeneruar);
                        var dokumentaLidhes = kokaShitje.merrIdsDokLidhur();
                        if (dokumentaLidhes.Select("[tipi]='dlidhesbanka'").Count() > 0)
                            return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Modifikimi/Fshirja nuk mund te kryhet pasi dokumenti i gjeneruar eshte i lidhur me dokument arketimi/pagese");
                        mesazh = kokaShitje.fshiShitje(IdPerdoruesi, kokaShitje, true, new clsDatabaseRegjistrim(dbData), false, false, 2);
                        break;
                    case (int)EnumBLlojeVeprimi.Arketim:
                    case (int)EnumBLlojeVeprimi.Pagese:
                        var kokaArkaBanka = new clsVeprimBankaKoka(idGjeneruar);
                        mesazh = kokaArkaBanka.fshi(new clsDatabaseArkaBanka(dbData));
                        break;
                }
                if (!mesazh)
                    return mesazh;
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        public clsMesazh GjeneroShitje(DbData dbData)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            bool isShitje; int idKonfigDokShitje = 0;
            if(LlojVeprimiGjenerimi == (int)EnumBLlojeVeprimi.Blerje)
            {
                idKonfigDokShitje = Convert.ToInt32(clsKusht.kthevlereSipasKushtitDheIdKonfig(IdKonfigAmbjente, "ZKDB"));
                isShitje = false;
            }
            else
            {
                idKonfigDokShitje = Convert.ToInt32(clsKusht.kthevlereSipasKushtitDheIdKonfig(IdKonfigAmbjente, "ZKDSH"));
                isShitje = true;
            }

            var konfigDokShitje = new clsKonfigurimAmbjenti(idKonfigDokShitje);
            var kushtLLD = new clsKusht(idKonfigDokShitje, "LLD");
            var llojeRreshti = (new clsKonfLlojRreshti(kushtLLD.IdKushtTemplate, "Shitje")).ColKonfLlojRreshtiVlere.Select(x => x.IdLlojRreshti).ToArray();
            Dictionary<string, string> kontrollVlereDefault = new Dictionary<string, string>();
            kontrollVlereDefault = clsAtributeTrupi.merrVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL(idKonfigDokShitje);
            var viti = new clsNdermarrjeViti(IdNdermVit);
            var dtDok = (DateTime)DtDok;
            #region vleredhenia e parametrave te krijimit/ruajtes se shitjes nga dok buxheti
            bool gjeneromeme = false, eshtememe = false, gjenerobij = false, faturepermbledhese = false, kontrollEkzistence = false,
                 tollona = false, autoklient = false, ngaImporti = false, gjeneruar = false, tollonkastrati = false, tollonakastratielektronik = false, blerengadealeri = false,
                 krijoartri = false, kontrolloGjendje = false, eshteDokKthimi = false, meKontabilizim = true, eshteOwn = false, eshteMeme = false,
                 zbritjeNeVlere = false, 
                 dogana = bool.TryParse(kontrollVlereDefault["cmbDogana"], out dogana),
                 kase = bool.TryParse(kontrollVlereDefault["cbKasa"], out kase),
                 kupon = bool.TryParse(kontrollVlereDefault["cbKupon"], out kupon),
                 shpenzJoTeZbritshme = bool.TryParse(kontrollVlereDefault["cbShpenzimeJoTeZbritshme"], out shpenzJoTeZbritshme),
                 kartaPaPagese = bool.TryParse(kontrollVlereDefault["cbKartaPaPagese"], out kartaPaPagese)
                 ;

            clsKonfigurimAmbjenti konfigMagazine = new clsKonfigurimAmbjenti();
            bool gjeneroDokMag = clsAlternativaKushti.getAlternativa(idKonfigDokShitje, "GJDM") == "Po";
            if (gjeneroDokMag)
                konfigMagazine = new clsKonfigurimAmbjenti(Convert.ToInt32(clsKusht.kthevlereSipasKushtitDheIdKonfig(idKonfigDokShitje, "ZKDM")));

            int idtempl = 0, idProj = 0, idkokaekzistueserezervim = 0, idtransferimi = 0, idkonfigtransferimi = 0, idGjuha = 0, idStatusVjeter = 0, idDokTransferimNga = 0,
                skemaWorkFlow = 0, idEtapa = 0,
                idMonedha = clsMonedha.ktheIdMonedhenENdermarrjes(IdNderm),
                llojKursi = int.TryParse(kontrollVlereDefault["txtKursi"], out llojKursi) ? llojKursi : 1,
                idMenyreTrans = int.TryParse(kontrollVlereDefault["btnMenyreTransporti"], out idMenyreTrans) ? idMenyreTrans : 0,
                idKushtDerg = int.TryParse(kontrollVlereDefault["btnKushtDergimi"], out idKushtDerg) ? idKushtDerg : 0,
                idAgj = int.TryParse(kontrollVlereDefault["btnAgjenti"], out idAgj) ? idAgj : 0,
                idMenPag = int.TryParse(kontrollVlereDefault["cmbMenyrePagese"], out idMenPag) ? idMenPag : 0,
                idKushtPag = int.TryParse(kontrollVlereDefault["btnKushtPagese"], out idKushtPag) ? idKushtPag : 0,
                idDegeAdmin = int.TryParse(kontrollVlereDefault["cmbDegeAdministrative"], out idDegeAdmin) ? idDegeAdmin : 0,
                idPikeShitjFurn = int.TryParse(kontrollVlereDefault["cmbPikeShitjeFurnizimi"], out idPikeShitjFurn) ? idPikeShitjFurn : 0,
                idRapDesign = int.TryParse(kontrollVlereDefault["cmbFormatiPrintimit"], out idRapDesign) ? idRapDesign : 0,
                idPeriudhaKont = new clsPeriudhaKontabel((DateTime)DtDok, IdNderm).IdPeriudha,
                idMag = int.TryParse(kontrollVlereDefault["btnMagazina"], out idMag) ? idMag : 0,
                idgrup1 = int.TryParse(kontrollVlereDefault["cmbGrup1"], out idgrup1) ? idgrup1 : 0,
                idgrup2 = int.TryParse(kontrollVlereDefault["cmbGrup2"], out idgrup2) ? idgrup2 : 0,
                idgrup3 = int.TryParse(kontrollVlereDefault["cmbGrup3"], out idgrup3) ? idgrup3 : 0,
                idStsAprv = int.TryParse(kontrollVlereDefault["lblStatusAprovimi"], out idStsAprv) ? idStsAprv : 0,
                idAgj2 = int.TryParse(kontrollVlereDefault["btnAgjenti2"], out idAgj2) ? idAgj2 : 0,
                idAgj3 = int.TryParse(kontrollVlereDefault["btnAgjenti3"], out idAgj3) ? idAgj3 : 0,
                idTransportues = int.TryParse(kontrollVlereDefault["btnTransportues"], out idTransportues) ? idTransportues : 0,
                idArka = int.TryParse(kontrollVlereDefault["btneArka"], out idArka) ? idArka : 0,
                idMuajRaportimi = dtDok.Month,
                idVitRaportimi = viti.IdViti > 0 ? viti.IdViti : 0,
                idKarta = int.TryParse(kontrollVlereDefault["cmbKarta"], out idKarta) ? idKarta : 0,
                pike = int.TryParse(kontrollVlereDefault["txtPike"], out pike) ? pike : 0,
                idFaza = int.TryParse(kontrollVlereDefault["cmbFaza"], out idFaza) ? idFaza : 0,
                idKlFurnvartes = int.TryParse(kontrollVlereDefault["btneKlientfurnitorVartes"], out idKlFurnvartes) ? idKlFurnvartes : 0,
                idKategSerial = int.TryParse(kontrollVlereDefault["cmbKategoriSeriali"], out idKategSerial) ? idKategSerial : 0,
                idLlojMarrvesh = int.TryParse(kontrollVlereDefault["cmbLlojMarreveshje"], out idLlojMarrvesh) ? idLlojMarrvesh : 0
                ;

            var colKlienteFurnitoreVartes = new colKlienteFurnitore();
            if (idKlFurnvartes != 0)
                colKlienteFurnitoreVartes.Add(new clsKlientFurnitor(idKlFurnvartes));

            string shfaqmesazhapolupe = "", mesazhinformues = "", emertimTr = "", llojzevendesimi = "", koordinata = "", marrveshje = "", mesazhMeVonshem,
                   kodMonedha = clsMonedha.ktheMonedhenENdermarrjes(IdNderm),
                   kodMenyreTrans = idMenyreTrans > 0 ? new clsMenyreTransporti(idMenyreTrans).KodiMenyreTransporti : "",
                   kodKushtDerg = idKushtDerg > 0 ? new clsKushtDergimi(idKushtDerg).KodiKushtDergimi : "",
                   kodAgj = idAgj > 0 ? new clsAgjentShitje(idAgj).KodiAgjentShitje : "",
                   kodKushtPag = idKushtPag > 0 ? new DbKontabiliteti.clsKushtPageseKoka(idKushtPag).KodiKushtPagese : "",
                   adrFaturim = kontrollVlereDefault["txtAdresaFaturimit"],
                   adrDergim = kontrollVlereDefault["txtAdresaDergimit"],
                   pershkrimi = kontrollVlereDefault["txtPershkrimi"],
                   kodDegeAdmin = idDegeAdmin > 0 ? new clsDegeAdministrative(idDegeAdmin).Kodi : "",
                   kodPikeShitjFurn = idPikeShitjFurn > 0 ? new clsPikeShitjeFurnizimi(idPikeShitjFurn).Kodi : "",
                   kodMag = idMag > 0 ? new clsNjesiAdministrative(idMag).Kodi : "",
                   kodgrup1 = idgrup1 > 0 ? new clsGrupimDokumentiKoka(idgrup1).Kodi : "",
                   kontakti = kontrollVlereDefault["txtKontakti"],
                   targa = kontrollVlereDefault["txtTarga"],
                   kodAgj2 = idAgj2 > 0 ? new clsAgjentShitje(idAgj2).KodiAgjentShitje : "",
                   kodAgj3 = idAgj3 > 0 ? new clsAgjentShitje(idAgj3).KodiAgjentShitje : "",
                   marresi = kontrollVlereDefault["txtMarresi"],
                   shoferi = kontrollVlereDefault["txtShoferi"],
                   targashoferit = kontrollVlereDefault["txtTarga2"],
                   niptKlienti = kontrollVlereDefault["txtNipt"],
                   qytetiK = kontrollVlereDefault["txtQytetiK"],
                   shenime2 = kontrollVlereDefault["txtShenime2"],
                   kerkuarNga = kontrollVlereDefault["txtKerkuarNga"]
                   ;

            double perqindjeZbritje = 0, total = 0, totalPaTvsh = 0, tvsh = 0,
                   zbritje = double.TryParse(kontrollVlereDefault["txtTotalMeZbritje1"], out zbritje) ? zbritje : 0,
                   cash = double.TryParse(kontrollVlereDefault["txtCash"], out cash) ? cash : 0,
                   perqindAgj = double.TryParse(kontrollVlereDefault["txtPerqindjeAgjent"], out perqindAgj) ? perqindAgj : 0,
                   perqindAgj2 = double.TryParse(kontrollVlereDefault["txtPerqindjeAgjent2"], out perqindAgj2) ? perqindAgj2 : 0,
                   perqindAgj3 = double.TryParse(kontrollVlereDefault["txtPerqindjeAgjent3"], out perqindAgj3) ? perqindAgj3 : 0,
                   kmAuto = double.TryParse(kontrollVlereDefault["txtKilometra"], out kmAuto) ? kmAuto : 0
                   ;

            MenyrePagese menyrePagese = (MenyrePagese)idMenPag;
            StatusAprovimi statusAprovimi = (StatusAprovimi)idStsAprv;
            StatusTrasferimi statustrasferimi = StatusTrasferimi.PaTransferuar;

            DateTime dtKrijimiPajisje = DateTime.Today,
                     dtMat = DateTime.TryParse(kontrollVlereDefault["dateMaturimi_DateEdit"], out dtMat) ? dtMat : DateTime.Today,
                     dtTrans = DateTime.TryParse(kontrollVlereDefault["dateTransportimi_DateEdit"], out dtTrans) ? dtTrans : DateTime.Today,
                     dtRegj = DateTime.TryParse(kontrollVlereDefault["dateRegjistrimi_DateEdit"], out dtRegj) ? dtRegj : DateTime.Today,
                     afatikohor = DateTime.TryParse(kontrollVlereDefault["dteAfatiKohor"], out afatikohor) ? afatikohor : DateTime.Today,
                     dtfill = DateTime.TryParse(kontrollVlereDefault["DtFillimi_DateEdit"], out dtfill) ? dtfill : DateTime.Today,
                     dtmb = DateTime.TryParse(kontrollVlereDefault["DtMbarimi_DateEdit"], out dtmb) ? dtmb : DateTime.Today,
                     dtfat = DateTime.TryParse(kontrollVlereDefault["DtFature_DateEdit"], out dtfat) ? dtfat : DateTime.Today,
                     dateKerkes = DateTime.TryParse(kontrollVlereDefault["DtKerkese_DateEdit"], out dateKerkes) ? dateKerkes : DateTime.Today
                     ;


            double kursi = 1;
            if (IdEntiteti < 1)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Lloji i veprimit dhe entiteti duhet te plotesohen sepse dokumenti i buxhetit gjeneron dokument vartes.");
            var klient = new DbCore.DbKontabiliteti.clsKlientFurnitor(IdEntiteti);
            string monedha = klient.Monedha;
            clsMonedha monedhaKF = new clsMonedha();
            monedhaKF.mbushMonedhenSipasLlogari(klient.IdLlogari);
            if(idMonedha != monedhaKF.IdMonedha)
            {
                idMonedha = monedhaKF.IdMonedha;
                clsKurset clsKursi = new clsKurset(idMonedha, dtDok, llojKursi);
                if (clsKursi.IdKursi <= 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Nuk eshte percaktuar asnje kurs per llojin {llojKursi} per monedhen {monedhaKF.KodiMonedha}.");
                kursi = clsKursi.VleraKursi;
            }
            #endregion

            var mesazh = new clsMesazh();
            colTrupiShitje trupiShitje = new colTrupiShitje();

            foreach (var rresht in OColBTrupiBuxheti)
            {
                if (rresht.IdObjekti <= 0)
                    continue;
                string kodObjekti = String.Empty;
                int idNjesia = 0;
                var objekti = new object();
                switch (rresht.LlojObjekti)
                {
                    case 1:
                        if (!llojeRreshti.Contains(1))
                            return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti nuk mund te permbaje artikuj ne trup.");
                        objekti = new clsArtikulli(rresht.IdObjekti);
                        kodObjekti = ((clsArtikulli)objekti).KodArtikulli;
                        idNjesia = ((clsArtikulli)objekti).Njesi1Artikulli;
                        break;
                    case 3:
                        if (!llojeRreshti.Contains(3))
                            return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti nuk mund te permbaje llogari ne trup.");
                        objekti = new clsLlogari(rresht.IdObjekti);
                        kodObjekti = ((clsLlogari)objekti).NrLlogari;
                        break;
                }
                int nrRreshti = 1;

                clsTrupiShitje trupi = new clsTrupiShitje(IdNderm, IdPerdoruesi, isShitje, false, eshteMeme, false, kodgrup1, false, eshteOwn, gjeneroDokMag, false, eshteDokKthimi, isShitje ? "shitje" : "blerje", tollona, tollonkastrati, false, tollonkastrati, false, blerengadealeri, kodObjekti, Enum.GetName(typeof(llojRreshtiShitje), rresht.LlojObjekti), rresht.IdObjekti, rresht.IdBuxhetiTrupi, 0, 0, 0, 0, 0, "", "", "", idNjesia, (double)rresht.Sasia, 0, 0, 0, 0, (double)rresht.Cmimi / kursi, 0, 0, 0, (double)rresht.VleraPaTvsh / kursi, rresht.IdTvsh, (double)rresht.Vlera / kursi, idMag, "", "", dtfill, dtmb, "", 0, 0, "", nrRreshti, true, dtDok, false);
                trupi.Element = objekti;
                nrRreshti++;
                totalPaTvsh += ((double)rresht.VleraPaTvsh / kursi);
                total += ((double)rresht.Vlera / kursi);
                tvsh = total - totalPaTvsh;

                if (string.IsNullOrEmpty(trupi.Kodi))
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti nuk mund te permbaje rreshta pa zera!");

                trupiShitje.Add(trupi);
            }

            clsKokaShitje kokaShitje = new clsKokaShitje();
            mesazh = kokaShitje.krijoShitje(ref gjeneroDokMag, konfigDokShitje.IdNivel, idtempl, idKonfigDokShitje, IdEntiteti, klient.KodKlientFurnitor, idProj, kontrollVlereDefault["txtNumerProjekti"], (DateTime)DtDok, NrDok, kontrollVlereDefault["txtNumerSerial"], (DateTime)DtDok, idMonedha, kodMonedha, kursi, idMenyreTrans, kodMenyreTrans, dtMat, idKushtDerg, kodKushtDerg, idAgj, kodAgj, idMenPag, menyrePagese.ToString(), idKushtPag, kodKushtPag, zbritje, (double)total, tvsh, dtRegj, IdStatusDok, IdNderm, IdNdermVit, IdNivel, IdKonfigAmbjente, IdBuxhetiKoka, 0, adrFaturim, adrDergim, pershkrimi, dogana, idDegeAdmin, kodDegeAdmin, idPikeShitjFurn, kodPikeShitjFurn, IdPerdoruesi > 0 ? IdPerdoruesi : IdKrijuesi, idRapDesign, trupiShitje, konfigDokShitje.IdKategori == 1, konfigDokShitje.KodKonfigAmbjente, idPeriudhaKont, konfigMagazine, idMag, kodMag, meKontabilizim, idgrup1, idgrup2, idgrup3, afatikohor, cash, statusAprovimi, IdPerdoruesi > 0 ? IdPerdoruesi : IdKrijuesi, perqindAgj, out shfaqmesazhapolupe, null, new DbQendraKosto.colTrupiQendraKosto(), idkokaekzistueserezervim, out mesazhinformues, gjeneromeme, new clsKokaShitje(), idtransferimi, idkonfigtransferimi, eshtememe, gjenerobij, statustrasferimi, klient.EmertimiKF, kontakti, kase, kupon, kodgrup1, dtfill, dtmb, Doktori, kmAuto, targa, idAgj2, perqindAgj2, kodAgj2, idAgj3, perqindAgj3, kodAgj3, marresi, idTransportues, emertimTr, faturepermbledhese, new clsKokaShitje(), shpenzJoTeZbritshme, idArka, kontrollEkzistence, tollona, autoklient, ngaImporti, dtfat, gjeneruar, tollonkastrati, tollonakastratielektronik, idMuajRaportimi, idVitRaportimi, shoferi, targashoferit, zbritjeNeVlere, perqindjeZbritje, idKarta, pike, new colFazaKontrate(), idFaza, colKlienteFurnitoreVartes, dtKrijimiPajisje, dbData, llojzevendesimi, koordinata, blerengadealeri, krijoartri, idGjuha, new clsKonfigurimAmbjenti(), idStatusVjeter, niptKlienti, qytetiK, kontrolloGjendje, idKategSerial, new colSerialeUnikeMagazina(), shenime2, kartaPaPagese, idDokTransferimNga, eshteDokKthimi, idLlojMarrvesh, marrveshje, StatusMarreveshje.Inaktive, kerkuarNga, "shtim", dateKerkes, false, NrDok, "", "", 0,"","","",0,0,"");

            if (!mesazh)
                return mesazh;

            clsVeprimBankaKoka banka = new clsVeprimBankaKoka();
            bool kontrollodisponibel = clsAlternativaKushti.getAlternativa(kokaShitje.IdKonfigAmbjente, "KD", new clsDatabaseShare(dbData)) == "Po";
            mesazh = kokaShitje.ruajShitje("", kokaShitje, isShitje, false, idPeriudhaKont, new clsDatabaseRegjistrim(dbData), new colKonvertimi(), gjeneroDokMag, skemaWorkFlow, StatusAprovimi.Undefined, idEtapa, out shfaqmesazhapolupe, new DbQendraKosto.colTrupiQendraKosto(), kontrollodisponibel, eshteOwn, new DbAsete.colSerialetMagazine(), new clsKonfigurimAmbjenti(), new colAmortizimiKoka(), meKontabilizim, new DbQendraKosto.colTrupiQendraKosto(), out shfaqmesazhapolupe, konfigDokShitje.KodKonfigAmbjente, 0, 0, kokaShitje.IdShitjeKoka, idGjuha, 0, tollona, false, false, tollonakastratielektronik, false, false, false, false, false, false, false, out mesazhMeVonshem, false,new clsKokaShitje());

            if (!mesazh)
                return mesazh;

            bool booleanFalse = false;
            string stringBosh = String.Empty;
            mesazh = kokaShitje.krijoDokArkeBanke(kokaShitje, ref booleanFalse, ref banka, isShitje, ref stringBosh, ref stringBosh, idPeriudhaKont, new clsDatabaseRegjistrim(dbData), new clsDatabaseShare(dbData));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        public clsMesazh GjeneroArketim(DbData dbData)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            string krediDebi, veprimi; int idKonfigArketim = 0;
            if (LlojVeprimiGjenerimi == (int)EnumBLlojeVeprimi.Pagese)
            {
                idKonfigArketim = Convert.ToInt32(clsKusht.kthevlereSipasKushtitDheIdKonfig(IdKonfigAmbjente, "ZKDPAG"));
                krediDebi = "Debi";
                veprimi = "Pagese";
            }
            else
            {
                idKonfigArketim = Convert.ToInt32(clsKusht.kthevlereSipasKushtitDheIdKonfig(IdKonfigAmbjente, "ZKDA"));
                krediDebi = "Kredi";
                veprimi = "Arketim";
            }

            var konfigArketim = new clsKonfigurimAmbjenti(idKonfigArketim);
            Dictionary<string, string> kontrollVlereDefault = new Dictionary<string, string>();
            kontrollVlereDefault = clsAtributeTrupi.merrVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL(idKonfigArketim);

            var arka = new clsBanka(IdEntiteti);
            var dtDok = (DateTime)DtDok;

            bool meKontabilizim = true,
                 kase = bool.TryParse(kontrollVlereDefault["cbKasa"], out kase);

            double totali = 0,
                   komisioniBankar = double.TryParse(kontrollVlereDefault["komision_TextBox"], out komisioniBankar) ? komisioniBankar : 0;

            int idDokNga = 0, idKokaEksistuese = 0, idDokAnullimi = 0,
                nrReference = int.TryParse(kontrollVlereDefault["referenca_TextBox"], out nrReference) ? nrReference : 0,
                idMenyrePagese = int.TryParse(kontrollVlereDefault["menyrePagese_ComboBox"], out idMenyrePagese) ? idMenyrePagese : 0,
                idDegeAdmin = int.TryParse(kontrollVlereDefault["cmbDegeAdministrative"], out idDegeAdmin) ? idDegeAdmin : 0,
                idLlogKrediti = int.TryParse(kontrollVlereDefault["kredite_ButtonEdit"], out idLlogKrediti) ? idLlogKrediti : 0,
                idPeriudhaKont = new clsPeriudhaKontabel(dtDok, IdNderm).IdPeriudha,
                idRap = int.TryParse(kontrollVlereDefault["cmbFormatiPrintimit"], out idRap) ? idRap : 0,
                idStatusAprovimi = 0,
                idGrup1 = int.TryParse(kontrollVlereDefault["cmbGrup1"], out idGrup1) ? idGrup1 : 0,
                idGrup2 = int.TryParse(kontrollVlereDefault["cmbGrup2"], out idGrup2) ? idGrup2 : 0,
                idGrup3 = int.TryParse(kontrollVlereDefault["cmbGrup3"], out idGrup3) ? idGrup3 : 0,
                llojKursi = int.TryParse(kontrollVlereDefault["kursi_TextBox"], out llojKursi) ? llojKursi : 1;

            string shfaqmesazhapolupe = "", shfaqmesazhapolupeVDK = "",
                   nrSerial = kontrollVlereDefault["nrSerial_TextBox"],
                   kodDegeAdmin = idDegeAdmin > 0 ? new clsDegeAdministrative(idDegeAdmin).Kodi : "",
                   shoqeria = kontrollVlereDefault["txtShoqeria"],
                   customerNumber = kontrollVlereDefault["txtCustomerNr"],
                   targa = kontrollVlereDefault["txtTarga"],
                   financieri = kontrollVlereDefault["txtFinancieri"],
                   dhenesiMarresi = kontrollVlereDefault["txtDhenesiMarresi"],
                   arketari = kontrollVlereDefault["txtArketari"],
                   arsyeAnullimi = kontrollVlereDefault["txtArsye"],
                   nrLlogari = kontrollVlereDefault["txtNrLlogari"],
                   nrKredite = idLlogKrediti > 0 ? new clsLlogari(idLlogKrediti).NrLlogari : "";

            MenyrePagese kodMenyrePagese = (MenyrePagese)idMenyrePagese;
            StatusAprovimi statusAprovimi = (StatusAprovimi)idStatusAprovimi;

            var nivelRegjArk = new clsNivelRegjistrimi();
            nivelRegjArk.IdNivel = konfigArketim.IdNivel;
            nivelRegjArk.merrNivelRegjSipasId();

            colVeprimBankaTrupi trupatArketim = new colVeprimBankaTrupi();

            clsMonedha monedhaArka = new clsMonedha(arka.IdMonedhaBanka);
            int idMonedha = clsMonedha.ktheIdMonedhenENdermarrjes(IdNderm);
            double kursi = 1;
            if (idMonedha != monedhaArka.IdMonedha)
            {
                idMonedha = monedhaArka.IdMonedha;
                clsKurset clsKursi = new clsKurset(idMonedha, dtDok, llojKursi);
                if (clsKursi.IdKursi <= 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Nuk eshte percaktuar asnje kurs per llojin {llojKursi} per monedhen {monedhaArka.KodiMonedha}.");
                kursi = clsKursi.VleraKursi;
            }

            foreach (var rresht in OColBTrupiBuxheti)
            {
                if (rresht.IdObjekti <= 0)
                    continue;
                int idllogaria = 0;
                clsLlogari llogaria;
                switch (rresht.LlojObjekti)
                {
                    case 1:
                        clsArtikulli artikulli = new clsArtikulli(rresht.IdObjekti);
                        idllogaria = LlojVeprimiGjenerimi == (int)EnumBLlojeVeprimi.Pagese ? (artikulli.LlojiArt ? artikulli.IdLlogariTeTrete : artikulli.IdLlogariBlerje) : artikulli.IdLlogariShitje;
                        break;
                    case 3:
                        idllogaria = rresht.IdObjekti;
                        break;
                }

                llogaria = new clsLlogari(idllogaria);
                var trupArka = new clsVeprimBankaTrupi("Llogari", idllogaria, "", krediDebi, 0, 0, 0, (double)rresht.VleraPaTvsh / kursi, (double)rresht.VleraPaTvsh, (double)rresht.VleraPaTvsh / kursi, kursi, IdNivel, 0, "", "", llogaria.NrLlogari, 0, 0, "", false);

                if (trupArka.IdSubjekti == -1 || trupArka.IdSubjekti == 0)
                    return new MesazhGabimi(MessagesResource.Messages["msgLlogariaNukEzistonOseJoAktive"]);

                totali += (double)rresht.Vlera;
                trupatArketim.Add(trupArka);
            }

            var kokaArka = new clsVeprimBankaKoka();
            var konfLidhes = new clsKonfigurimAmbjenti(konfigArketim.IdKonfigAmbjente, new clsDatabaseShare());
            object[] nivele = { IdNivel };

            var mesazh = kokaArka.krijoVeprimeBanke(IdEntiteti, arka.KodiBanka, kursi, dtDok, DateTime.Today, NrDok, nrReference, nrSerial, String.IsNullOrEmpty(Shenime) ? "" : Shenime, idMenyrePagese, kodMenyrePagese.ToString(), totali / kursi, totali, komisioniBankar, komisioniBankar, veprimi, IdPerdoruesi > 0 ? IdPerdoruesi : IdKrijuesi, konfigArketim.IdKategori, IdStatusDok, IdNdermVit, konfigArketim.IdKonfigAmbjente, IdNivel, IdKonfigAmbjente, IdBuxhetiKoka, konfigArketim.IdNivel, idDokNga, idDegeAdmin, kodDegeAdmin, IdNderm, idLlogKrediti, trupatArketim, meKontabilizim, idPeriudhaKont, arka.IdMonedhaBanka, nrKredite, idGrup1, idGrup2, idGrup3, konfLidhes, nivele, new clsDatabaseArkaBanka(dbData), new clsKokaShitje(), out shfaqmesazhapolupe, out shfaqmesazhapolupeVDK, new DbQendraKosto.colTrupiQendraKosto(), idKokaEksistuese, shoqeria, customerNumber, Doktori, targa, idRap, financieri, dhenesiMarresi, arketari, kase, statusAprovimi, arsyeAnullimi, idDokAnullimi, nrLlogari, null, konfigArketim.IdKategori, IdPerdoruesi > 0 ? IdPerdoruesi : IdKrijuesi, IdKrijuesi);

            if (!mesazh)
                return mesazh;

            mesazh = kokaArka.ruaj(null, new clsDatabaseArkaBanka(dbData), 0, StatusAprovimi.Undefined, 0, string.Empty, false);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        private clsMesazh ModifikoPaTrans(DbData dbData)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            var mesazh = Valido();
            if (!mesazh.Status)
                return mesazh;

            //Modifikojme koken 
            mesazh = ModifikoKokaBuxheti();
            if (!mesazh)
                return mesazh;

            if (IdKatDok == 175 || IdKatDok == 177 || IdKatDok == 179 || IdKatDok == 181)
            {
                mesazh = FshiTrupDokumentBuxheti();
                if (!mesazh)
                    return mesazh;
            }

            //Modifikojme trupin
            OColBTrupiBuxheti.ForEach(x => x.IdBuxhetiKoka = IdBuxhetiKoka);
            mesazh = OColBTrupiBuxheti.Ruaj();
            if (!mesazh)
                return mesazh;

            mesazh = GjeneroDokumentVartes(dbData);
            if (!mesazh)
                return mesazh;

            //Dokumentin e modifikuar bashke me turpat e kalojme ne historik
            mesazh = KaloNeHistorik();
            if (!mesazh)
                return mesazh;

            //Dokumentin e Alokimit/Rialokimit e postojme tek bijat kur kalon me status Ruajtur 1/Refuzuar 8
            mesazh = this.PostoTeBija(IdPerdoruesi);
            if (!mesazh)
                return mesazh;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi();
        }

        private clsMesazh UpdateStatusImporti(DbData dbData)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim(dbData))
                return db.updateDokTabeleTemportal(IdDokImporti, IdNderm, 1, TabeleKokeImporti, "Id Koka Buxheti", "Kod Ndermarrje");
        }
        #endregion

        #region Internal

        internal void krijoKokeDokumentBuxhetiPerImport(int idNdermarrje, int idPerdorues, int idkatdok, int idNdermVit, string nenkategoria, string llojDok, string nrDok, DateTime dtDok, string llojVeprimi, string entiteti, int viti, decimal buxhetiMiratuar, string shenime, ColBTrupiBuxheti colTrupi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdorues, idkatdok, idNdermVit, nenkategoria, llojDok, nrDok, dtDok, llojVeprimi, entiteti, viti, buxhetiMiratuar, shenime, colTrupi);
            #region Exception nese fushat e detyrueshme jane bosh
            if (String.IsNullOrWhiteSpace(nenkategoria) || String.IsNullOrEmpty(nenkategoria)) throw new Exception("Fusha Nenkategoria nuk duhet te jete bosh!");
            if (String.IsNullOrWhiteSpace(llojDok) || String.IsNullOrEmpty(llojDok)) throw new Exception("Fusha Lloj dok nuk duhet te jete bosh!");
            if (String.IsNullOrWhiteSpace(nrDok) || String.IsNullOrEmpty(nrDok)) throw new Exception("Fusha Nr dok nuk duhet te jete bosh!");
            if (dtDok == DateTime.MinValue) throw new Exception("Fusha Dt dok nuk duhet te jete bosh!");
            if (idkatdok == 172 || idkatdok == 170)
                if (dtDok.Year != viti) throw new Exception("Viti dhe Dt dok nuk perputhen.");
            if (idkatdok != 172 && idkatdok != 175 && idkatdok != 179 && idkatdok != 170)
            {
                if (String.IsNullOrWhiteSpace(llojVeprimi) || String.IsNullOrEmpty(llojVeprimi)) throw new Exception("Fusha Lloj veprimi nuk duhet te jete bosh!");
                if (String.IsNullOrWhiteSpace(entiteti) || String.IsNullOrEmpty(entiteti)) throw new Exception("Fusha Entiteti nuk duhet te jete bosh!");
            }
            #endregion

            clsNivelRegjistrimi niveli = new clsNivelRegjistrimi(nenkategoria, idNdermarrje);
            if (niveli == null) throw new Exception("Nenkategoria me kod " + nenkategoria + " nuk ekziston.");
            if (niveli.IdKategori != idkatdok) throw new Exception("Nenkategoria me kod " + nenkategoria + " nuk i perket kategorise se zgjedhur per import.");
            this.IdNivel = niveli.IdNivel;

            this.IdKonfigAmbjente = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(llojDok, idNdermarrje);
            if (clsKonfigurimAmbjenti.ktheIdNiveliSipasIdKonfigurimi(this.IdKonfigAmbjente) != this.IdNivel) throw new Exception("Lloji i dok nuk i perket Nenkategorise.");
            
            if (idkatdok != 172 && idkatdok != 175 && idkatdok != 179 && idkatdok != 170)
            {
                if (llojVeprimi == "Arketim" || llojVeprimi == "Pagese")
                {
                    this.LlojVeprimiGjenerimi = llojVeprimi == "Arketim" ? 1 : 3;
                    if (!clsBanka.ekziston(entiteti, idNdermarrje)) throw new Exception("Arka me kod " + entiteti + " nuk ekziston!");
                    this.IdEntiteti = clsBanka.ktheIdBanka(entiteti, idNdermarrje);
                }
                else if (llojVeprimi == "Shitje" || llojVeprimi == "Blerje")
                {
                    this.LlojVeprimiGjenerimi = llojVeprimi == "Shitje" ? 2 : 4;
                    if (!clsKlientFurnitor.EkzistonKlientFurnitor(entiteti, idNdermarrje)) throw new Exception("Klienti/Furnitori me kod " + entiteti + " nuk ekziston!");
                    this.IdEntiteti = clsKlientFurnitor.MerrIdKlientFurnitor(entiteti, idNdermarrje);
                }
                else throw new Exception($"Lloji i veprimit '{llojVeprimi}' nuk eshte i vlefshem.");
            }

            this.NrDok = nrDok;
            this.DtDok = dtDok;
            this.IdNderm = idNdermarrje;
            this.IdNdermVit = idNdermVit;
            this.IdKrijuesi = idPerdorues;
            this.Viti = (idkatdok == 172 || idkatdok == 170) ? viti : dtDok.Year;
            this.IdKatDok = idkatdok;
            this.Shenime = shenime;
            this.OColBTrupiBuxheti = colTrupi;
            this.IdStatusDok = 0;

            if (idkatdok == 170)
            {
                this.Totali = buxhetiMiratuar;
                this.TotaliPaTvsh = buxhetiMiratuar;
                return;
            }

            decimal totali = 0, totaliPaTvsh = 0, totaliMiratuar = 0;

            foreach (ClsBTrupiBuxheti t in colTrupi)
            {
                if (idkatdok == 172)
                {
                    ClsBKategoriBuxhetimi kategori = new ClsBKategoriBuxhetimi( MessagesResource.Messages, t.IdKategoriBuxhetimi);
                    totali += (kategori.IdPrindi == 0) ? t.Vlera : 0;
                    totaliPaTvsh += (kategori.IdPrindi == 0) ? t.VleraPaTvsh : 0;
                }
                else
                {
                    totali += t.Vlera;
                    totaliPaTvsh += t.VleraPaTvsh;
                }
                totaliMiratuar += t.VleraPlanifikuar;
            }
            this.Totali = totali;
            this.TotaliPaTvsh = totaliPaTvsh;

            if (idkatdok == 172)
            {
                this.TotaliPlanifikuar = 0;
                if (buxhetiMiratuar != 0 && buxhetiMiratuar != totaliMiratuar/12) throw new Exception("Buxheti i miratuar nuk eshte i barabarte me shumen e miratimeve per cdo kategori.");
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdorues, idkatdok, idNdermVit, nenkategoria, llojDok, nrDok, dtDok, llojVeprimi, entiteti, viti, buxhetiMiratuar, shenime, colTrupi);
        }

        #endregion        
    }
}