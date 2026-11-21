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
