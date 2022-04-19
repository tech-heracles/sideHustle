using DbCore;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{
    public partial class LogeSistemi : MyPageBase
    {
        private string Komponente => clsFunksione.GetKomponente(Page.Request);

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ShtoMenuControlsDheMsgFrame();
            if (!IsPostBack)
            {
                dataNga.Date = DateTime.Today;
                dataDeri.Date = DateTime.Today;
                ConfigureAspxComboBox.KonfiguroComboBoxNivelVerbosity(cbVerbosity);
                ConfigureAspxComboBox.KonfiguroComboBoxLlojLogesh(cbModuli);
            }
        }
        
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, Komponente, this, menuInfo, null, null, null, null, false, true, false, Meme, false);
        }
    }
}