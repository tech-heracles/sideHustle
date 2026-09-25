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

        //    /*
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


        //        yield return sqlRow;

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
