using DbCore.IMBUtils.DataBase;
using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    public class colKonfigurimFtp : List<clsKonfigurimFtp>, IDataBaseReader
    {
        public colKonfigurimFtp(int idNdermarrje)
        {
            using (var db = new clsDatabaseAdmin())
                db.mbushKonfigurimFtpSipasIdNdermarrje(idNdermarrje, this);
        }

        public colKonfigurimFtp(int idNdermarrje, int idMetoda)
        {
            using (var db = new clsDatabaseAdmin())
                db.mbushKonfigurimFtpSipasIdNdermarrjeDheMetode(idNdermarrje, idMetoda, this);
        }

        public colKonfigurimFtp(int idNdermarrje, int idMetoda, string kategoria)
        {
            using (var db = new clsDatabaseAdmin())
                db.mbushKonfigurimFtpSipasIdNdermarrjeDheMetode(idNdermarrje, idMetoda, kategoria, this);
        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsKonfigurimFtp(record));
        }

    }
}