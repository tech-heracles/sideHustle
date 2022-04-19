using AlphaWeb.Core.Common;
using AlphaWeb.Core.Messages;
using DbCore;
using DbCore.DbAdmin;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.MbylljePeriudhe;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.SessionState;

namespace RestApi.WebAPI.Models
{
    internal class MbylljePeriudheRepository
    {
        private static List<object> GetActions ()            
        {
            return new List<object>(){
                new { IdAction = PcAction.MbyllPeriudhe, ActionText = MessagesResource.Messages["MP_lblMbyllPeriudhe"] },
                new { IdAction =  PcAction.AnulloMbyllje, ActionText = MessagesResource.Messages["MP_lblAnulloMbyllje"] },
                new { IdAction = PcAction.FshiHistorik, ActionText = MessagesResource.Messages["MP_lblFshiHistorik"] }
            };
        }

        private static object GetMessages()
        {
            return new
            {
                MP_lblNextButton = MessagesResource.Messages["MP_lblNextButton"],
                MP_lblPreviousButton = MessagesResource.Messages["MP_lblPreviousButton"],
                MP_lblCancelButton = MessagesResource.Messages["MP_lblCancelButton"],
                MP_lblFinishButton = MessagesResource.Messages["MP_lblFinishButton"],
                MP_lblCloseButton = MessagesResource.Messages["MP_lblCloseButton"],
                MP_lblStopButton = MessagesResource.Messages["MP_lblStopButton"],
                MP_lblYear = MessagesResource.Messages["MP_lblYear"],
                MP_lblEntitety = MessagesResource.Messages["MP_lblEntitety"],
                MP_lblPcAction = MessagesResource.Messages["MP_lblPcAction"],
                MP_lblPeriod = MessagesResource.Messages["MP_lblPeriod"],
                MP_lblProgress = MessagesResource.Messages["MP_lblProgress"],
                MP_msgStartProcess = MessagesResource.Messages["MP_msgStartProcess"],
                MP_msgStopProcess = MessagesResource.Messages["MP_msgStopProcess"],
                MP_colEntiteti = MessagesResource.Messages["MP_colEntiteti"],
                MP_colDataMbylljes = MessagesResource.Messages["MP_colDataMbylljes"],
                MP_colKrijuesi = MessagesResource.Messages["MP_colKrijuesi"],
                MP_colViti = MessagesResource.Messages["MP_colViti"],
                MP_colMuaji = MessagesResource.Messages["MP_colMuaji"]
            };
        }


        internal static object GetPageConfigurations(HttpSessionState session)
        {
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            var periodSummaries = GetPeriodSummaries(session);
            var enumEntities = Enum.GetNames(typeof(Modul)).ToList();
            var entities = (new colModulet(idGjuha)).FindAll(x => enumEntities.Contains(x.KodiModuli));

            return new
            {
                PeriodSummaries = periodSummaries,
                Entities = entities,
                Months = PeriodClosing.GetMonths(),
                Messages = GetMessages()
            };
        }

        internal static object GetWizardConfigurations(HttpSessionState session)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            string serverName = MyConnectionsManager.GetSelectedConNameServer();
            IProcess procesi = ProcesetContainer.Instance.GetProcesi(serverName, idNdermarrje);
            if (procesi != null)
                return GetRunningProcess(procesi);
            return GetWizardConfigurations(session, idNdermarrje);
        }

        private static object GetWizardConfigurations(HttpSessionState session, int idNdermarrje)
        {
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            var enumEntities = Enum.GetNames(typeof(Modul)).ToList();
            var entities = (new colModulet(idGjuha)).FindAll(x => enumEntities.Contains(x.KodiModuli));

            colNdermarrjeVitet years = new colNdermarrjeVitet();
            years.mbushGjitheViteELidhuraMeNdermarrje(idNdermarrje);
            List<object> actions = GetActions();

            return new
            {
                ProcessRunning = false,
                Entities = entities,
                Actions = actions,
                Years = years
            };
        }

        internal static object GetPeriodSummaries(HttpSessionState session)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            return PeriodClosing.GetAllPeriodSummaryByIdNdermarrje(new DbBuilder(new DbData()), idNdermarrje);
        }

        private static object GetRunningProcess(IProcess procesi)
        {
            int progressBarSize = procesi.Months.Count;
            int progressBarValue = procesi.Months.IndexOf(procesi.CurrentMonth);
            string currentMonth = MessagesResource.Messages[$"muaji_" + procesi.CurrentMonth];
            string text = currentMonth;
            switch (procesi.Action)
            {
                case PcAction.MbyllPeriudhe:
                    text = $"{MessagesResource.Messages["MP_msgClosingMonth"]} {currentMonth}";
                    break;
                case PcAction.AnulloMbyllje:
                    text = $"{MessagesResource.Messages["MP_msgDeletingMonth"]} {currentMonth}";
                    break;
                case PcAction.FshiHistorik:
                    text = $"{MessagesResource.Messages["MP_msgDeletingHistoric"]} {currentMonth}";
                    break;
            }

            return new
            {
                ProcessRunning = true,
                ProgressBarSize = progressBarSize,
                ProgressBarValue = progressBarValue,
                Text = text
            };
        }


        //id: action.IdAction, KodiMuaji: action.ActionText
        internal static object GetMonths(HttpSessionState session, int idNdermarrjeVit, int idModuli, int idAction)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);

            return new
            {
                Months = PeriodClosing.GetMonths(idNdermarrje, idNdermarrjeVit, idModuli, (PcAction)idAction)
            };
        }

        internal static object StartActionMbylljePeriudhe(HttpSessionState session, int idModuli, int idAction, int idNdermarrjeVit, object muajtObj)
        {
            string serverName = MyConnectionsManager.GetSelectedConNameServer();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int year = clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNdermarrjeVit);
            clsModuli moduliObj = new clsModuli(idGjuha, idModuli);
            Modul moduli = (Modul)Enum.Parse(typeof(Modul), moduliObj.KodiModuli);
            DbBuilder dbBuilder = new DbBuilder(new DbData(serverName), moduli);
            ILogger logger = ImbLogger.MbylljePeriudheLogger;
            PcAction action = (PcAction)idAction;

            List<int> months = JsonConvert.DeserializeObject<List<int>>(muajtObj.ToString());
            if(action == PcAction.AnulloMbyllje)
                months.Reverse();

            DateTime lastClosedDate = new DateTime(year, months.Last(), DateTime.DaysInMonth(year, months.Last()), 23, 59, 59).AddMonths(action == PcAction.AnulloMbyllje ? -1 : 0);

            try
            {
                Process procesi = new Process()
                {
                    Messages = MessagesResource.Messages,
                    TaskManager = new TaskManager(),
                    ServerName = serverName,
                    DbBuilder = dbBuilder,
                    MesazhBuilder = new MesazhBuilder(),
                    Logger = logger,
                    PeriodSummaryFactory = PeriodClosing.CreateFactory(moduli, dbBuilder),
                    Modul = moduli,
                    Months = months,
                    IdNdermarrje = idNdermarrje,
                    IdModuli = idModuli,
                    IdKrijuesi = idPerdoruesi,
                    IdNdermarrjeVit = idNdermarrjeVit,
                    Statusi = Statusi.InProcess,
                    DtFillimi = DateTime.Now,
                    Action = action,
                    PeriodSummaries = new List<PeriodSummary>(),
                    CurrentMonth = months.First(),
                    CurrentYear = year,
                    LastClosedPeriod = new ClosedPeriod() { ServerName = serverName, CompanyId = idNdermarrje, Modul = moduli, LastClosedDate = lastClosedDate }
                };
                var runningProcess = GetRunningProcess(procesi);
                IMesazh mesazh = procesi.Start();

                return new { runningProcess, mesazh };
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error Procesi - (IdNdermarrje: {idNdermarrje}, IdPerdoruesi: {idPerdoruesi}, Veprimi: {((PcAction)idAction).ToString()}) - {ex.Message}";
                logger.Error(errorMsg);
                return new MesazhGabimi(errorMsg);
            }
        }
        
        internal static IMesazh StopActionMbylljePeriudhe(HttpSessionState session)
        {
            string serverName = MyConnectionsManager.GetSelectedConNameServer();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            IProcess procesi = ProcesetContainer.Instance.GetProcesi(serverName, idNdermarrje);
            if (procesi == null)
                return new MesazhSuksesi(MessagesResource.Messages["MP_msgProcessFinished"]);
            return procesi.Cancel();
        }

        internal static object CheckRunningProcess(HttpSessionState session)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            string serverName = MyConnectionsManager.GetSelectedConNameServer();
            IProcess procesi = ProcesetContainer.Instance.GetProcesi(serverName, idNdermarrje);
            if(procesi != null)
                return GetRunningProcess(procesi);

            return new
            {
                ProcessRunning = false,
                ProgressBarSize = 1,
                ProgressBarValue = 1,
                Text = MessagesResource.Messages["MP_msgProcessFinished"]
            };
        }

        internal static object GetPeriodSummaryDetailsReport(HttpSessionState session, string kodiModuli, int viti, int muaji)
        {
            string serverName = MyConnectionsManager.GetSelectedConNameServer();
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);

            Modul moduli = (Modul)Enum.Parse(typeof(Modul), kodiModuli);
            DbBuilder dbBuilder = new DbBuilder(new DbData(serverName), moduli);

            PeriodSummaryFactory periodSummaryFactory = PeriodClosing.CreateFactory(moduli, dbBuilder);
            return new
            {
                columnNames = GetReportColumnNames(moduli),
                dataSource = periodSummaryFactory.GetPeriodSummaryDetailsReport(idNdermarrje, viti, muaji)
            };
        }

        private static object GetReportColumnNames(Modul moduli)
        {
            switch (moduli)
            {
                case Modul.M_KONTABILITETI:
                    return GetReportColumnNamesKontabiliteti();
                default:
                    return null;
            }
        }

        private static object GetReportColumnNamesKontabiliteti()
        {
            return new
            {
                nrLlogarie = MessagesResource.Messages["filterRaportiNumerLlogarie"],
                emerLlogarie = MessagesResource.Messages["filterRaportEmerLlogarie"],
                monedha = MessagesResource.Messages["labelMonedha"],
                kredi = MessagesResource.Messages["labelRaportiKredi"],
                debi = MessagesResource.Messages["labelRaportiDebi"],
                gjendjaMonBaze = MessagesResource.Messages["labelRaportiGjendMonBaze"],
                gjendjaMonLlogarie = MessagesResource.Messages["labelRaportiGjendMonLlog"]
            };
        }

    }
}
