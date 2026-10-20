using AlphaWeb.Core.Configuration;
using AlphaWeb.Core.Infrastructure.DependencyManagement;
using AlphaWeb.Core.Interfaces.Infrastructure;
using Autofac;
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

        // kontrolleret e API-se nuk kane varesi te regjistruara ketu (i vetmi ishte TestController, i hequr)
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AlphaWebConfig config)
        {
        }
    }
}
