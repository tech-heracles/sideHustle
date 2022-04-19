using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
  public  class colDrejtaTabi : List<clsDrejtaTabi>
    {
        public static string keyFieldName = "idDrejta";
        public static string parentFieldName = "idPrindi";
        public static int rootPrindi = 0;

        #region Konstruktoret

        /// <summary>
        /// konstruktori bosh
        /// </summary>
        public colDrejtaTabi()
        {

        }

        //public colDrejtaTabi(int roli, int idNdermarrje, int idviti)
        //{
        //    clsDatabaseAdmin db = new clsDatabaseAdmin();
        //    DataTable dt = db.merrtedrejta.merrTeDrejtaTabesh(roli, idNdermarrje, idviti);
        //    if (!mbushTeDrejtat(dt))
        //    {
        //        db.Dispose();
        //        throw new Exception("ERROR: Gabim gjate mbushjes se collectionit nga db-ja");
        //    }
        //    db.Dispose();
        //}

        #endregion

        /// <summary>
        /// get dhe set sipas indexit te dhene te nje elementi te colection-it
        /// </summary>
        /// <param name="index">indeksi i dhene qe duhet te jete me i madh baraz se 0 dhe me i vogel se list.Count</param>
        /// <returns>elementin ne rastin kur eshte get</returns>
        public new clsDrejtaTabi this[int index]
        {
            get { return ((clsDrejtaTabi)base[index]); }
        }

        public colDrejtaTabi filtroTeDrejtaRoliSipasPrindit(int idPrindi)
        {
            colDrejtaTabi teDrejtat = new colDrejtaTabi();
            foreach (clsDrejtaTabi eDrejte in this)
                if (eDrejte.IdPrindi == idPrindi)
                    teDrejtat.Add(eDrejte);
            return teDrejtat;
        }

        /// <summary>
        /// gjen dhe kthen clsTeDrejtaRoli
        /// </summary>
        /// <param name="idDrejta">id-ja e te drejtes qe po kerkojme</param>
        /// <returns>te drejten e gjetur ose null</returns>
        public clsDrejtaTabi merrTeDrejten(int idDrejta)
        {
            foreach (clsDrejtaTabi edrejte in this)
                if (edrejte.IdDrejta == idDrejta)
                    return edrejte;
            return null; //not found
        }




    }
}