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

    public class AccessController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetAllConnections()
        {
            try
            {
                return Request.KthePergjigje(AccessRepository.GetAllConnections());
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }
    }
}
