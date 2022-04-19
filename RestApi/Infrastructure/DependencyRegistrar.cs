using AlphaWeb.Core.Configuration;
using AlphaWeb.Core.Infrastructure.DependencyManagement;
using AlphaWeb.Core.Interfaces.Infrastructure;
using Autofac;
using RestApi.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 10;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AlphaWebConfig config)
        {
            RegisterControllers(builder);
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterType<TestController>().InstancePerRequest();
        }
    }
}
