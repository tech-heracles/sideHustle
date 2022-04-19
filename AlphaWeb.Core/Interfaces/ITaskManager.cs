using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaWeb.Core.Interfaces
{
    public interface ITaskManager
    {
        void CreateNewTask(Action functionToExec, Action functionToClean);
    }
}
