using DbCore.DbInventari;
using DbCore.DbProdhimi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;

namespace RestApi.WebAPI.Models
{
    public class PlanifikimProdhimiRepository
    {
        internal static List<Object> MerrDokumentPlanifikimProdhimi(int idDokumenti, HttpSessionState Session)
        {
            colTrupiPlanifikim col = new colTrupiPlanifikim(idDokumenti, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            List<Object> result = new List<Object>();
            foreach (clsTrupiPlanifikim trp in col)
            {
                clsArtikulli art = new clsArtikulli(trp.IdArtikulli);
                clsDetajimArtikulli d1 = new clsDetajimArtikulli(trp.Detajim1);
                clsDetajimArtikulli d2 = new clsDetajimArtikulli(trp.Detajim2);

                result.Add(new { trupiPlanifikim = trp, artikulli = art, detajim1 = d1, detajim2 = d2});
            }
            return result;
        }
    }
}
