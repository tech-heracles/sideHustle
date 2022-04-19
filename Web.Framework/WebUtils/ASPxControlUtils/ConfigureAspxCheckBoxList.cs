using DbCore;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;

namespace PlatinumWeb.ApplicationUtils.ASPxControlUtils
{
    public static class ConfigureAspxCheckBoxList
    {
        public static void KonfiguroCheckBoxListMarreveshje(ASPxCheckBoxList checkBoxList)
        {
            checkBoxList.ConfigureAndFill(() =>
            {
                var marreveshjet = clsFunksione.KtheGjitheLlojeMarreveshjesh();
                return marreveshjet;
            }, "PERSHKRIMI", "IDLLOJMARREVESHJE");
        }
    }
}