using AlphaWeb.Core.Configuration;
using AlphaWeb.Core.Infrastructure.DependencyManagement;
using AlphaWeb.Core.Interfaces.Infrastructure;
using AlphaWeb.Core.Logging;
using AlphaWeb.Infrastructure.Logging;
using AlphaWeb.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatinumWeb.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AlphaWebConfig config)
        {
            builder.RegisterType<AlphaWebLogger>().As<IAlphaWebLogger>().SingleInstance();
            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
        }
    }
}