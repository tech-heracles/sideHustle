using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAnalizeBuxheti
{
    public class colPlanifikimRealizim:List<clsPlanifikimRealizim>
    {
        public colPlanifikimRealizim(int idNdermarrje, int idNdermVit, int idKrijuesi, bool raportuese)
        {
            string suffix = raportuese ? "_raportuese" : string.Empty;
            using(clsDatabaseAnalizeBuxheti dbAb = new clsDatabaseAnalizeBuxheti())
            {
                this.AddRange(dbAb.MerrListePlanifikimRealizim(idNdermarrje, idNdermVit, idKrijuesi, suffix));
            }
        }

        public clsMesazh Ruaj()
        {
            try
            {
                using (var scope=new MyTransactionScope())
                {
                    var dt = this.ToDataTable("PrId", "IdArtikulli", "SasiaMiratuar",  "IdKrijuesi", "IdModifikuesi","IdNdermarrje", "IdNdermVit");

                    clsDatabaseAnalizeBuxheti dbAb = new clsDatabaseAnalizeBuxheti();
                    dbAb.ruajPlanifikimRealizimDt(dt);
                    scope.Complete();

                    return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                }
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }
    }
}
