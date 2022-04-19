using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    public class colServerConnectionStrings : List<clsServerConnectionString>, IDataBaseReader
    {

        public colServerConnectionStrings()
        {

        }
        /// <summary>
        /// lexon nga db e zgjedhur si kryesore te gjtihe connectionstring-et
        /// </summary>
        /// <param name="connectionName"></param>
        public void ReadAll(string connectionName)
        {
            using (var db = new clsDatabaseAdmin(connectionName))
            {
                db.MerrGjitheConnectionStrings(this);
            }
        }
        /// <summary>
        /// konverton listen e connection string ne Dictionary<ConnectionNameKey,ConnectionString>
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetDictionary()
        {
            var dic = new Dictionary<string, string>(Count);
            ForEach(connString =>
            {
                dic.Add(connString.Name, connString.ConnectionString);
            });
            return dic;
        }


        public static IDictionary<string, string> GetAllConnectionStringsAsDictionary()
        {
            var connStrings = new colServerConnectionStrings();
            connStrings.ReadAll(IMBUtils.DataBase.MyConnectionsManager.ConnStringNameDefault);
            return connStrings.GetDictionary();
        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsServerConnectionString(record));
        }
    }
}
