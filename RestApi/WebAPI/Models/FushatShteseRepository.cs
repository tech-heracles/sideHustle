using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.WebAPI.Models
{
    /// <summary>
    /// TE PERDORET VETEM PER veprimet me fushat shtese
    /// </summary>
    public class FushatShteseRepository
    {
        public static IEnumerable<string> KtheDataAktivizimi(int idLidhese, int idModeli)
        {
            return colVleraFushaShtese.MerrDataAktivizmi(idLidhese, idModeli).Select(x=>x.ToShortDateString());
        }

    }
}
