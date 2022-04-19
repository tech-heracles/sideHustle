using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbAnalizeBuxheti
{
    public class colShpenzimeOperativeKonfig : List<clsShpenzimeOperativeKonfig>
    {
        public colShpenzimeOperativeKonfig(int idNdermarrje)
            : base(new clsDatabaseAnalizeBuxheti().MerrShpenzimeOperativeKonfig(idNdermarrje))
        {
        }

        public colShpenzimeOperativeKonfig(IEnumerable<clsShpenzimeOperativeKonfig> source) : base(source)
        {
        }

        public colShpenzimeOperativeKonfig(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrShpenzimeOperativeKonfig(idNdermarrje))
        {
        }

        /// <summary>
        /// modifikon te gjitha nivelet e shpenzimeve operative,pasi eshte bere nje shtim ose modifikim ne konfigurimin e tyre
        /// </summary>
        /// <param name="dbAB"></param>
        /// <returns></returns>

        public clsMesazh PerditesoNiveletEShpenzimeveOperative(clsDatabaseAnalizeBuxheti dbAB)
        {
            clsMesazh mesazhi = null;
            //TODO te updatohen vetem ato qe duhen
            try
            {
                dbAB.beginTransaksion();
                DataTable dt = this.ToDataTable("ShokId", "Niveli");
                mesazhi = dbAB.PerditesoNiveletEShpenzimeveOperative(dt);
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