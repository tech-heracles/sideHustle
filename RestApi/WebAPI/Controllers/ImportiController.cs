using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    [Authorize]
    public class ImportiController : ApiController, IRequiresSessionState
    {
        private HttpSessionState Session { get { return HttpContext.Current.Session; } }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheDataSourceDheFormatImporti(JObject param)
        {
            try
            {
                int idKategoria = param["idKategoria"].ToObject<int>();
                string kategoria = param["kategoria"].ToObject<string>();
                int formati = param["formati"].ToObject<int>();
                string lloji = param["lloji"].ToObject<string>();
                string emerTabeleKoke = param["emerTabeleKoke"].ToObject<string>();
                string emerTabeleTrupi = param["emerTabeleTrupi"].ToObject<string>();
                string emerTabeleRec = param["emerTabeleRec"].ToObject<string>();
                bool transferoFatura = param["transferoFatura"].ToObject<bool>();
                bool tePaImportuara = param["tePaImportuara"].ToObject<bool>();
                string emerSheet = param["emerSheet"].ToObject<string>();
                bool rimerrTeImportuara = param["rimerrTeImportuara"].ToObject<bool>();
                int? nrDokumentash = param["nrDokumentash"].ToObject<int?>();

                return Request.KthePergjigje(ImportiRepository.KtheDataSourceDheFormatImporti(idKategoria, kategoria, formati, lloji, emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, transferoFatura, tePaImportuara, emerSheet, Session, rimerrTeImportuara, nrDokumentash));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ModifikoDokumentaNeTabeleTemporale(JObject param)
        {
            try
            {
                object objRowsKokaDheRecepturaModifikim = param["objRowsKokaDheRecepturaModifikim"].ToObject<object>();
                object objRowsTrupiModifikim = param["objRowsTrupiModifikim"].ToObject<object>();
                object objectRowsFshi = param["objectRowsFshi"].ToObject<object>();
                int idKategoria = param["idKategoria"].ToObject<int>();
                int formati = param["formati"].ToObject<int>();
                string emerTabeleKoke = param["emerTabeleKoke"].ToObject<string>();
                string emerTabeleTrupi = param["emerTabeleTrupi"].ToObject<string>();
                string emerTabeleRec = param["emerTabeleRec"].ToObject<string>();

                return Request.KthePergjigje(ImportiRepository.ModifikoDokumentaNeTabeleTemporale(objRowsKokaDheRecepturaModifikim, objRowsTrupiModifikim, objectRowsFshi, formati, idKategoria, emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KontrolloImportoTeDhenaGridImporti(JObject param)
        {
            try
            {
                object rreshtaImporti = param["rreshtaImporti"].ToObject<object>();
                int idKonfig = param["idKonfig"].ToObject<int>();
                int idKategoria = param["idKategoria"].ToObject<int>();
                string kategoria = param["kategoria"].ToObject<string>();
                int formati = param["formati"].ToObject<int>();
                string lloji = param["lloji"].ToObject<string>();
                bool transferoFatura = param["transferoFatura"].ToObject<bool>();
                bool tePaImportuara = param["tePaImportuara"].ToObject<bool>();
                string emerTabeleKoke = param["emerTabeleKoke"].ToObject<string>();
                string emerTabeleTrupi = param["emerTabeleTrupi"].ToObject<string>();
                string emerTabeleRec = param["emerTabeleRec"].ToObject<string>();
                object mbishkruajVleratEMeparshme = param["mbishkruajVleratEMeparshme"].ToObject<object>();
                bool permbledhese = param["permbledhese"].ToObject<bool>();
                bool importo = param["importo"].ToObject<bool>();

                return Request.KthePergjigje(ImportiRepository.KontrolloImportoTeDhenaGridImporti(rreshtaImporti, idKonfig, idKategoria, kategoria, formati, lloji, transferoFatura, tePaImportuara, permbledhese, emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, mbishkruajVleratEMeparshme, importo, Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage PastroTabelatTemporare(JObject param)
        {
            try
            {
                string emerTabeleTrupi = param["tabelaTrupi"].ToString();
                string emerTabeleKoka = param["tabelaKoka"].ToString(); 
                string emerTabeleKokaHistorik = param["tabelaKokaHistorik"].ToString();
                string emerTabeleTrupiHistorik = param["tabelaTrupiHistorik"].ToString();
                return Request.KthePergjigje(ImportiRepository.PastroTabelatTemporare(emerTabeleKoka, emerTabeleTrupi, emerTabeleKokaHistorik, emerTabeleTrupiHistorik));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}
