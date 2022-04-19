using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAnalizeBuxheti
{
    public class colTrupiPasqyraOrganike:List<clsTrupiPasqyraOrganike>
    {

        /// <summary>
        /// per te mundesuar kalimin e collectionit si  parameter SP-je
        /// </summary>
        /// <returns></returns>
        //IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        //{

        //    /*
        //public int IdTrupiDok { get; set; }
        //public int IdKokaDok { get; set; }
        //public int IdProfesioni { get; set; }
        //public decimal VleraFakt { get; set; }
        //public decimal VleraPlan { get; set; }
        //public int IdStatusDok { get; set; }
        //public int IdKrijuesi { get; set; }
        //public int IdModifikuesi { get; set; }
        //public DateTime? DtKrijimi { get; set; }
        //public DateTime? DtModifikimi { get; set; }
        //*/
        //    var sqlRow = new SqlDataRecord(
        //          new SqlMetaData("IdTrupiDok", SqlDbType.Int, 50),
        //          new SqlMetaData("IdKokaDok", SqlDbType.Int, 50),
        //          new SqlMetaData("IdProfesioni", SqlDbType.Int, 50),
        //          new SqlMetaData("VleraFakt", SqlDbType.Float, 100),
        //          new SqlMetaData("VleraPlan", SqlDbType.Float, 100),
        //          new SqlMetaData("IdStatusDok", SqlDbType.Int, 10),
        //          new SqlMetaData("IdKrijuesi", SqlDbType.Int, 10),
        //          new SqlMetaData("IdModifikuesi", SqlDbType.Int, 10),
        //          new SqlMetaData("DtKrijimi", SqlDbType.DateTime, 10),
        //          new SqlMetaData("DtModifikimi", SqlDbType.DateTime, 10)


        //          );
        //    foreach (clsTrupiPasqyraOrganike item in this)
        //    {
        //        sqlRow.SetInt32(0, item.IdTrupiDok);
        //        sqlRow.SetInt32(1, item.IdKokaDok);
        //        sqlRow.SetInt32(2, item.IdProfesioni);
        //        sqlRow.SetDecimal(3, item.VleraFakt);
        //        sqlRow.SetDecimal(4, item.VleraPlan);
        //        sqlRow.SetInt32(5, item.IdStatusDok);

        //        sqlRow.SetInt32(6, item.IdKrijuesi);
        //        sqlRow.SetInt32(7, item.IdModifikuesi);
        //        sqlRow.SetDateTime(8, item.DtKrijimi.Value);

        //        sqlRow.SetDateTime(9, item.DtModifikimi.Value);
        //        yield return sqlRow;
        //    }
        //}

        public void Add(clsTrupiPasqyraOrganike trup)
        {
            trup.IdTrupiDok = base.Count + 1;
            base.Add(trup);

        }
        public colTrupiPasqyraOrganike(int idKoka, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrTrupinPasqyraOrganike(idKoka))
        {

        }
        public colTrupiPasqyraOrganike(int idKoka)
            : base(new clsDatabaseAnalizeBuxheti().MerrTrupinPasqyraOrganike(idKoka))
        {

        }

        public colTrupiPasqyraOrganike(IEnumerable<clsTrupiPasqyraOrganike> trupi):base(trupi)
        {

        }
        public colTrupiPasqyraOrganike()
        {

        }


    }
}
