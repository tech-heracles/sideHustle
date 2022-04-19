using AlphaWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaWeb.Core.Common
{
    public class TaskManager : ITaskManager
    {
        public void CreateNewTask(Action functionToExec, Action functionToClean)
        {
            Task task = Task.Factory.StartNew(() => functionToExec());
            task.ContinueWith((Task) => { Cleanup(Task, functionToClean); });
        }

        private void Cleanup(Task task, Action functionToClean)
        {
            if (null != task && task.IsFaulted)
            {
                System.Diagnostics.Debug.WriteLine("Error encountered while running task");
            }
            functionToClean();
        }
    }
}
