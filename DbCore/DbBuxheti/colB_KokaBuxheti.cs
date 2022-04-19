using System;
using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbBuxheti
{
    public class ColBKokaBuxheti:List<ClsBKokaBuxheti>,IDataBaseReader
    {
        public ColBKokaBuxheti()
        {
        }

        public ColBKokaBuxheti(int idNdermarrje, int viti, int idPerdoruesi, int idKatDok, bool gjitheDokumentat)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.MerrKokaBuxheti(idNdermarrje, viti, idPerdoruesi, idKatDok, gjitheDokumentat, this);
        }

        public static DataTable ktheDokumentBuxhetiPerEksport(int idNdermarrje, int idKatDok)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.ktheDokumentBuxhetiPerEksport(idNdermarrje, idKatDok);
        }

        public static DataTable ktheDokAlokimBuxhetiPerEksport(int idNdermarrje)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.ktheDokAlokimBuxhetiPerEksport(idNdermarrje);
        }
        public static DataTable ktheDokMiratimBuxhetiPerEksport(int idNdermarrje)
        {
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                return db.ktheDokMiratimBuxhetiPerEksport(idNdermarrje);
        }

        public static DataTable merrDokumentaPerImport(string emerTabKoka, string emerTabTrupi, string kodNdermFusha, string ndermarjeKodi, string primaryKey, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, emerTabKoka, emerTabTrupi, kodNdermFusha, ndermarjeKodi, primaryKey, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);

            ClsDatabaseBuxheti db = new ClsDatabaseBuxheti();
            DataTable dt = db.merrDokumentaPerImport(emerTabKoka, emerTabTrupi, kodNdermFusha, ndermarjeKodi, primaryKey, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);
            db.Dispose();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, emerTabKoka, emerTabTrupi, kodNdermFusha, ndermarjeKodi, primaryKey, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);
            return dt;
        }

        public void Mbush(IDataRecord record) => Add(new ClsBKokaBuxheti(record));
    }
}
