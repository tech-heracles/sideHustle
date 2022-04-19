using AlphaWeb.Services;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestApi.WebAPI.Controllers
{
    public class TestController:ApiController
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [AllowAnonymous]
        public string Index()
        {

            var srv = AlphaWeb.Core.Infrastructure.EngineContext.Current.Resolve<ITestService>();
            return srv.GetTestData();
        }
    }
}
