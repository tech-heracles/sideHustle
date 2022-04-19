using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaWeb.Core.Infrastructure.DependencyManagement
{
    public interface IRequestLifetimeScopeManager
    {
        ILifetimeScope GetCurrentRequestLifetimeScope();
    }
}
