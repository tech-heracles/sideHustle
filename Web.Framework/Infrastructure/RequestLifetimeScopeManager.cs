using AlphaWeb.Core.Infrastructure.DependencyManagement;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Autofac.Integration.WebApi;
using Autofac.Integration.Web;
using System.Web.Http;

namespace Web.Framework.Infrastructure
{
    public class RequestLifetimeScopeManager : IRequestLifetimeScopeManager
    {
        public ILifetimeScope GetCurrentRequestLifetimeScope()
        {
            if (HttpContext.Current != null)
            {
                //Web Api Request
                if (IsApiRequest())
                {
                    return GlobalConfiguration.Configuration.DependencyResolver.GetRequestLifetimeScope();
                    
                }
                //Page Request
                return ((IContainerProviderAccessor)HttpContext.Current.ApplicationInstance).ContainerProvider.RequestLifetime;
            }
            return null;
        }

        private bool IsApiRequest()
        {
            return HttpContext.Current.Request.Url.LocalPath.IndexOf("/api/", StringComparison.InvariantCultureIgnoreCase) != -1;
        }
    }
}
