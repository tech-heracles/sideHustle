using DbCore.DbRegjistrim;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbAdmin
{
    public abstract class clsDokumentAbstract : IDataBase
    {
        public LlojObjekti llojObjekti;
        public abstract clsMesazh Fshi();

        public abstract void Mbush(IDataRecord record);


        public abstract clsMesazh Modifiko();

        public abstract clsMesazh Ruaj();

        public abstract clsMesazh Ekziston();
        public abstract clsMesazh Valido();

        public virtual clsMesazh HidhNeHistorik(int idDok)
        {

            switch (llojObjekti)
            {
                case LlojObjekti.Shitje:
                    break;

                default:
                    throw new MyException($"LLoji i objektit {llojObjekti.ToString()} eshte i pa percaktuar!");
            }
            return new MesazhSuksesi();
        }
    }
}
