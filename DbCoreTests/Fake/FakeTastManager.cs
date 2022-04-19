using AlphaWeb.Core.Interfaces;
using System;

namespace DbCoreTests.Fake
{
    public class FakeTastManager : ITaskManager
    {
        public void CreateNewTask(Action functionToExec, Action functionToClean)
        {
            functionToExec();
            functionToClean();
        }
    }
}
