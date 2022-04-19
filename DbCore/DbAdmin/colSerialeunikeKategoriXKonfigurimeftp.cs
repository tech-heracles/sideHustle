using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    public class colSerialeunikeKategoriXKonfigurimeftp : List<clsSerialeunikeKategoriXKonfigurimeftp>, IDataBaseReader
    {
        public colSerialeunikeKategoriXKonfigurimeftp()
        {
        }
        public colSerialeunikeKategoriXKonfigurimeftp(int id)
        {
            using (var db = new clsDatabaseAdmin())
                db.mbushLidhjeKategoriserialiKonfigurimftpSipasIdKategori(id, this);
        }
        public void Mbush(IDataRecord record)
        {
            Add(new clsSerialeunikeKategoriXKonfigurimeftp(record));
        }

    }
}