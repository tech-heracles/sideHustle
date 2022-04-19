using DbCore;
using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb
{
    public partial class B_KomponenteBuxhetiVlere : MyPageBase
    {
        private string Komponente => clsFunksione.GetKomponente(Page.Request);
        private int IdKomponente => clsKomponente.MerrIdKomponenteSipasEmrit(Komponente);
        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            base.ShtoMenuControlsDheMsgFrame();
            if (!IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                PercaktoTeDrejtaNeHiddenField();
                VendosTipinNeHfState();
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, Komponente, this, menuInfo, null, null, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        private void PercaktoTeDrejtaNeHiddenField()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void VendosTipinNeHfState()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var list = new List<object>();
            list.Add(new { text = "", value = (int?)null });
            foreach (EnumBLlojKufizimi item in Enum.GetValues(typeof(EnumBLlojKufizimi)))
            {
                list.Add(new { text = item.ToString(), value = Convert.ToInt32(item) });
            }
            hfState.Set("llojeKufizimi", Newtonsoft.Json.JsonConvert.SerializeObject(list));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
    }
}