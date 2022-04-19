using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbAnalizeBuxheti
{
    public class colRealizimProkurimeshKonfig:List<clsRealizimProkurimesh>
    {
        public colRealizimProkurimeshKonfig(int idNdermarrje)
            : base(new clsDatabaseAnalizeBuxheti().MerrRealizimProkurimesh(idNdermarrje))
        {
        }

        public colRealizimProkurimeshKonfig(IEnumerable<clsRealizimProkurimesh> source) : base(source)
        {
        }

        public colRealizimProkurimeshKonfig(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrRealizimProkurimesh(idNdermarrje))
        {
        }

        public clsMesazh PerditesoNivelet(clsDatabaseAnalizeBuxheti dbAB)
        {
            clsMesazh mesazhi = null;
            try
            {
                dbAB.beginTransaksion();
                DataTable dt = this.ToDataTable("RpkId", "Niveli");
                mesazhi = dbAB.PerditesoNiveletEZeraveTeProkurimit(dt);
                if (mesazhi.Status)
                {
                    dbAB.commitTransaksion();
                    mesazhi = new clsMesazh(true, "Modifikimi u krye me sukses!\n");
                }
                else
                    dbAB.rollbackTransaksion();
            }
            catch (Exception ex)
            {
                dbAB.rollbackTransaksion();
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit!\n" + ex.Message);
            }
            return mesazhi;
        }
    }
}
