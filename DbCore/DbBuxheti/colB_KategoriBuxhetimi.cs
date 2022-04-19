using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbBuxheti
{
    public class ColBKategoriBuxhetimi:List<ClsBKategoriBuxhetimi>,IDataBaseReader
    {
        public ColBKategoriBuxhetimi()
        {
        }

        public ColBKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            this.Add(kategoriBuxhetimi);
        }
        public ColBKategoriBuxhetimi(int idNdermarrje)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.MerrKategoriBuxhetimi(idNdermarrje, this);
        }

        public static ColBKategoriBuxhetimi merrKategoriBuxhetimiAktiveSipasNdermarrjes(int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            ColBKategoriBuxhetimi kategorite = new ColBKategoriBuxhetimi(idNdermarrje);
            ColBKategoriBuxhetimi kategoriAktive = new ColBKategoriBuxhetimi();
            kategoriAktive.AddRange(kategorite.FindAll(x => x.Aktive == true));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
            return kategoriAktive;
        }

        public void MbushKategoriBuxhetimiSipasNdermarrjesDheBijave(int idNdermarrje)
        {
            using (var db = new ClsDatabaseBuxheti())
                db.MbushKategoriBuxhetimiSipasNdermarrjesDheBijave(idNdermarrje, this);
        }

        public static DataTable merrKategoriBuxhetimiLikeKodOsePershkDT(int idNdermarrje, string infixText)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.merrKategoriBuxhetimiLikeKodOsePershkDT(idNdermarrje, infixText);
        }


        public void Mbush(IDataRecord record) => Add(new ClsBKategoriBuxhetimi(record));
    }
}
