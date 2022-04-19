using System;
using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbBuxheti
{
    public class ColBLlojBuxheti:List<ClsBLlojBuxheti>,IDataBaseReader
    {
        public ColBLlojBuxheti() { }

        public ColBLlojBuxheti(ClsBLlojBuxheti llojBuxheti)
        {
            this.Add(llojBuxheti);
        }

        public static ColBLlojBuxheti KtheSipasNdermarrjes(int idNdermarrje)
        {
            var colBLlojBuxheti = new ColBLlojBuxheti();
            using (var db = new ClsDatabaseBuxheti())
                db.MerrLlojBuxhetiSipasNdermarrjes(idNdermarrje, colBLlojBuxheti);
            return colBLlojBuxheti;
        }

        public static ColBLlojBuxheti KtheSipasNdermarrjesDheBijave(int idNdermarrje)
        {
            var colBLlojBuxheti = new ColBLlojBuxheti();
            using (var db = new ClsDatabaseBuxheti())
                db.MerrLlojBuxhetiSipasNdermarrjesDheBijave(idNdermarrje, colBLlojBuxheti);
            return colBLlojBuxheti;
        }

        public static ColBLlojBuxheti KtheSipasNdermarrjesDheLlogarise(int idNdermarrje, int idLlogaria)
        {
            var colBLlojBuxheti = new ColBLlojBuxheti();
            using (var db = new ClsDatabaseBuxheti())
                db.MerrLlojBuxhetiSipasNdermarrjesDheLlogarise(idNdermarrje, idLlogaria, colBLlojBuxheti);
            return colBLlojBuxheti;
        }

        public static ColBLlojBuxheti KtheSipasBuxheteve(string idBuxhete)
        {
            var colBLlojBuxheti = new ColBLlojBuxheti();
            using (var db = new ClsDatabaseBuxheti())
                db.MerrLlojBuxhetiSipasBuxheteve(idBuxhete, colBLlojBuxheti);
            return colBLlojBuxheti;
        }

        public static ColBLlojBuxheti KtheSipasKategoriBuxhetimi(int idKategoriBuxhetimi)
        {
            var colBLlojBuxheti = new ColBLlojBuxheti();
            using (var db = new ClsDatabaseBuxheti())
                db.MerrLlojBuxhetiSipasKategoriBuxhetimi(idKategoriBuxhetimi, colBLlojBuxheti);
            return colBLlojBuxheti;
        }

        public static DataTable merrLlojeBuxhetiLikeKodOsePershkDT(int idNdermarrje, string infixText)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.merrLlojeBuxhetiLikeKodOsePershkDT(idNdermarrje, infixText);
        }

        public static DataTable merrLlojeBuxhetiLikeKodOsePershkDTSipasLlogarise(int idNdermarrje, int idLlogaria, string infixText)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.merrLlojeBuxhetiLikeKodOsePershkDTSipasLlogarise(idNdermarrje, idLlogaria, infixText);
        }

        public void Mbush(IDataRecord record) => Add(new ClsBLlojBuxheti(record));

    }
}
