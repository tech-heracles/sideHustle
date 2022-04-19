using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
   public class MobileController: ApiController,IReadOnlySessionState
    {
        public HttpResponseMessage login(JObject param)
        {
            try
            {
                var username = param["username"].Value<string>();
                var password = param["password"].ToObject<string>();
                var data = param["data"].ToObject<string>();

                return Request.KthePergjigje(MobileRepository.login(username, password, data));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        public HttpResponseMessage kontrolloLogin(JObject param)
        {
            try
            {
                var username = param["username"].Value<string>();
                var password = param["password"].ToObject<string>();
                var data = param["data"].ToObject<string>();

                return Request.KthePergjigje(MobileRepository.kontrolloLogin(username, password, data));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage Ndermarrjet(JObject param)
        {
            try
            {
                var username = param["username"].Value<string>();
                var password = param["password"].ToObject<string>();
                var data = param["data"].ToObject<string>();

                return Request.KthePergjigje(MobileRepository.Ndermarrjet(username, password, data));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        public HttpResponseMessage Objekte(JObject param)
        {
            try
            {
               
                return Request.KthePergjigje(MobileRepository.Objekte());
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }


        public HttpResponseMessage HelloWorld(JObject param)
        {
            try
            {

                return Request.KthePergjigje(MobileRepository.HelloWorld());
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
     


    }
}
