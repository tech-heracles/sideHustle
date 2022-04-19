using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;
using NLog;
using System.Web.SessionState;
using DbCore.DbShare;

namespace RestApi.WebAPI.Models
{
    public class CmimeRepository
    {
        private static Logger _logu = LogManager.GetCurrentClassLogger();
        private static readonly string cmbCmimeArtikulliRritje = MessagesResource.Messages["cmbCmimeArtikulliRritje"];
        private static readonly string cmbCmimeArtikulliZbritje = MessagesResource.Messages["cmbCmimeArtikulliZbritje"];
        private static readonly string cmbCmimeArtikulliBarazim = MessagesResource.Messages["cmbCmimeArtikulliBarazim"];
        private static readonly string cmbCmimeArtikulliVlere = MessagesResource.Messages["cmbCmimeArtikulliVlere"];
        private static readonly string cmbCmimeArtikulliPerqidje = MessagesResource.Messages["cmbCmimeArtikulliPerqidje"];
        private static readonly string cmbCmimeArtikulliKosto = MessagesResource.Messages["cmbCmimeArtikulliKosto"];

        internal static object KtheDataSourceKolonash(int idNdermarrje, int idPerdoruesi, int shitjeApoBlerje)
        {
            var niveleCmimi = colNiveleCmimesh.GetNiveleCmimeshLookupSimpleTable(idNdermarrje, shitjeApoBlerje, idPerdoruesi);     
            var monedha = colMonedhat.GetMonedhaLookupSimpleTable(idNdermarrje, idPerdoruesi);
            var kodifikimeArtikulli = colKodifikimeArtikulli.GetKodifikimeArtikulliLookupSimpleTable(idNdermarrje);
            var detajimeArtikulli = colDetajimeArtikulli.GetDetajimeLookupSimpleTable(idNdermarrje, idPerdoruesi);
            var niveleTvsh = colTaksa.GetTaksaLookupSimpleTable(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
            var newRow = niveleTvsh.NewRow(); newRow["IdTaksa"] = -1; newRow["KodTaksa"] = MessagesResource.Messages["txtPaTVSH"]; newRow["NormaPerqindje"] = 0;
            niveleTvsh.Rows.InsertAt(newRow, 0);
            return new { niveleCmimi, niveleTvsh, monedha, kodifikimeArtikulli, detajimeArtikulli };
        }

        internal static colCmimeArtikujsh KtheListeCmimesh(int idNdermarrje, int idPerdoruesi, int shitjeApoBlerje, bool lupe, bool merrKosto, int idNivelCmimi)
        {
            return new colCmimeArtikujsh(idNdermarrje, idPerdoruesi, shitjeApoBlerje, merrKosto, lupe, idNivelCmimi);
        }

        internal static object KtheCmimArtikujshSipasNivelit(HttpSessionState session, string artIds, int idNivelCmimi)
        {
            return colCmimeArtikujsh.KtheCmimArtikujshSipasNivelit(artIds, idNivelCmimi);
        }

        internal static clsMesazh RuajListeCmimesh(int idNdermarrje, int idPerdoruesi, object cmimeObject, int shitjeApoBlerje, bool lupe, bool merrKosto, int idNivelCmimi)
        {
            colCmimeArtikujsh cmimeAll = KtheListeCmimesh(idNdermarrje, idPerdoruesi, shitjeApoBlerje, lupe, merrKosto, idNivelCmimi);
            colCmimeArtikujsh cmimeTeNdryshuar = JsonConvert.DeserializeObject<colCmimeArtikujsh>(cmimeObject.ToString());
            cmimeTeNdryshuar.ForEach(item =>
            {
                item.DateFillimi = item.DateFillimi.ToLocalTime();
                item.DateMbarimi = item.DateMbarimi.ToLocalTime();
                item.KoheFillimi = item.KoheFillimi.ToLocalTime();
                item.KoheMbarimi = item.KoheMbarimi.ToLocalTime();
            });
            cmimeTeNdryshuar.ShtoCmimeRetailNeKoleksion(cmimeAll);
            return colCmimeArtikujsh.RuajRreshtaTeModifikuar(cmimeTeNdryshuar);
        }

        internal static object RuajFilterNivelCmimi(HttpSessionState session, int idKonfigAmbjente, int idNivelCmimi, int idNdermarrje, int idViti, int idPerdoruesi)
        {
            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
            clsMesazh mesazh = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
            if (!tedrejtaInfo.DAmb)
                return mesazh;
            clsKusht kusht = new clsKusht(idKonfigAmbjente, "NCDNC");
            kusht.Vlera = idNivelCmimi;
            mesazh = kusht.modifiko();
            if(!mesazh)
                return mesazh;
            return new MesazhSuksesi(MessagesResource.Messages["msgRuajFilterNivelCmimi"]);
        }
    }
}
