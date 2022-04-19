using AlphaWeb.Core.Interfaces;
using AlphaWeb.Core.Interfaces.Localization;
using AlphaWeb.Core.Messages;
using DbCore.IMBUtils.DataBase;
using DbCore.MbylljePeriudhe.Interfaces;
using System;
using System.Collections.Generic;
using NLog;

namespace DbCore.MbylljePeriudhe
{
    public interface IProcess
    {
        string ServerName { get; set; }
        IMessagesResource Messages { get; set; }
        ILogger Logger { get; set; }
        IDbBuilder DbBuilder { get; set; }
        IMesazhBuilder MesazhBuilder { get; set; }
        ICollection<PeriodSummary> PeriodSummaries { get; set; }
        Modul Modul { get; set; }
        IList<int> Months { get; set; }
        PeriodSummaryFactory PeriodSummaryFactory { get; set; }
        int CurrentMonth { get; set; }
        int CurrentYear { get; set; }
        int IdProcess { get; set; }
        int IdModuli { get; set; }
        int IdNdermarrje { get; set; }
        int IdNdermarrjeVit { get; set; }
        int IdKrijuesi { get; set; }
        DateTime DtFillimi { get; set; }
        DateTime DtMbarimi { get; set; }
        Statusi Statusi { get; set; }
        PcAction Action { get; set; }
        IClosedPeriod LastClosedPeriod { get; set; }
        IMesazh Start();
        IMesazh Cancel();
    }
}
