using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using Newtonsoft.Json.Linq;
using System.Web.SessionState;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Newtonsoft.Json;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using DbCore.DbShare;
using DbCore.DbIntegrime;

namespace RestApi.WebAPI.Controllers
{
    public class NodeApiController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage getlastmessagedate()
        {
            try
            {
                return Request.KthePergjigje(NodeApiRepository.GetLastModifiedDateMessage());
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }

        // public HttpResponseMessage RuajMesazhe(JObject param)
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajmesazhe(JObject param)
        {
            try
            {
                colNjoftime nj = param["messages"].ToObject<colNjoftime>();
                return Request.KthePergjigje(NodeApiRepository.RuajMesazhet(nj));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ruajmesazheperdoeus(clsNjoftimePerdorues njoftime)
        {
            try
            {
                return Request.KthePergjigje(NodeApiRepository.RuajMesazhetPerdoruesHistorik(njoftime));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(njoftime, ex);
            }
        }


        [HttpGet]
        public HttpResponseMessage getJobetAutomatike()
        {
            try
            {
                return Request.KthePergjigje(NodeApiRepository.MerrJobetAutomatike());
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }


        [HttpPost]
        public HttpResponseMessage getJobetAutomatikeParametra(JObject param)
        {
            try
            {
                int idskeduleri = param.Value<int>("IdSkeduleri");
                return Request.KthePergjigje(NodeApiRepository.MerrJobAutomatikeParametra(idskeduleri));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }

        [HttpPost]
        public HttpResponseMessage updateJobAutomatike(clsJobAutomatike paramJobAutomatike)
        {
            try
            {
                return Request.KthePergjigje(NodeApiRepository.ModifikoJobAutomatike(paramJobAutomatike));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }

        }
    }
}
