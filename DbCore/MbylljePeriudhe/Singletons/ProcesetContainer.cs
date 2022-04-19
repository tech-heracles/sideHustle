using DbCore.MbylljePeriudhe.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.MbylljePeriudhe
{
    public sealed class ProcesetContainer : IProcesetContainer
    {
        private static ProcesetContainer instance;
        private static object syncRoot = new object();
        //private List<ProcesiAnalitik> procesetAnalitike = null;
        private static List<IProcess> proceset;
        private static BlockingCollection<IClosedPeriod> closedPeriods;

        private ProcesetContainer()
        {
            proceset = new List<IProcess>();
            closedPeriods = new BlockingCollection<IClosedPeriod>();
        }

        public static ProcesetContainer Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            instance = new ProcesetContainer();
                        }
                    }
                }

                return instance;
            }
        }


        public void SetProcess(IProcess procesi)
        {
            proceset.Add(procesi);
            AddClosedPeriodIfNotExists(procesi.LastClosedPeriod);
        }

        public void DeleteProcess(IProcess procesi)
        {
            proceset.Remove(procesi);
            AddClosedPeriodIfNotExists(procesi.LastClosedPeriod);
        }

        public IProcess GetProcesi(string serverName, int idNdermarrje)
        {
            return proceset.FirstOrDefault(x => x.ServerName == serverName && x.IdNdermarrje == idNdermarrje);
        }

        private void AddClosedPeriodIfNotExists(IClosedPeriod closedPeriod)
        {
            IClosedPeriod existentClosedPeriod = closedPeriods.FirstOrDefault(x => x.ServerName == closedPeriod.ServerName && x.CompanyId == closedPeriod.CompanyId && x.Modul == closedPeriod.Modul);
            if (existentClosedPeriod != null)
            {
                existentClosedPeriod.LastClosedDate = closedPeriod.LastClosedDate;
                return;
            }

            AddClosedPeriod(closedPeriod);
        }

        private void AddClosedPeriod(IClosedPeriod closedPeriod)
        {
            closedPeriods.Add(closedPeriod);
        }

        public bool IsPeriodClosed(IClosedPeriod closedPeriod)
        {
            var existentClosedPeriod = closedPeriods.FirstOrDefault(x => x.ServerName == closedPeriod.ServerName && x.CompanyId == closedPeriod.CompanyId && x.Modul == closedPeriod.Modul);
            if (existentClosedPeriod != null)
                return existentClosedPeriod.LastClosedDate >= closedPeriod.LastClosedDate;
            
            var lastClosedDate = new DateTime();
            using (var db = new DatabasePeriodClosingCommon(new IMBUtils.DataBase.DbData()))
            {
                closedPeriod.ServerName = db.MyScopeDbManager.ConnectionName;
                lastClosedDate = db.GetLastClosedDate(closedPeriod.CompanyId, closedPeriod.Modul);
            }
            var isPeriodClosed = lastClosedDate >= closedPeriod.LastClosedDate;
            closedPeriod.LastClosedDate = lastClosedDate;
            AddClosedPeriod(closedPeriod);
            return isPeriodClosed;            
        }
    }
}
