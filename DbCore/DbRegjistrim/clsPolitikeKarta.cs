using DbCore.IMBUtils.Messages;
using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// Klasa e politikave te kartes
    /// </summary>
    public class clsPolitikeKarta
    {
        #region Properties

        public string Kodi { get; set; }

        public int IdPolitike { get; set; }

        public double VleraPikes { get; set; }

        /// <summary>
        /// 0 = me zbritje
        /// 1 = me pike
        /// 2 = me zbritje dhe pike
        /// </summary>
        public int Lloji { get; set; }

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi { get; set; }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit.
        /// </summary>
        public DateTime DtModifikimi { get; set; }

        public int IdPerdoruesi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes.
        /// </summary>
        public int IdNdermarrje { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne status dok.
        /// </summary>
        public int IdStatusDok { get; set; }

        public colTrupiPolitikeKarta OcolTrupiPolitikeKarta { get; set; }

        #endregion

        #region Konstruktoret

        public clsPolitikeKarta() { }

        public clsPolitikeKarta(int idPolitike)
        {
            using (var data = new clsDatabaseRegjistrim())
                Mbush(data.merrPolitikeSipasId(idPolitike));
        }

        /// <summary>
        /// konstrukotr me 2 parameter
        /// </summary>
        public clsPolitikeKarta(string kodi, int idNdermarje)
        {
            using (var data = new clsDatabaseRegjistrim())
                Mbush(data.merrPolitikeSipasEmertimit(kodi, idNdermarje));
        }

        public clsPolitikeKarta(int idPolitike, string kodi, int lloji, double vlerapike, int idKrijues, int idNdermarrje, colTrupiPolitikeKarta ocolTrupi, bool shtim)
        {
            IdPolitike = idPolitike;
            Kodi = kodi;
            Lloji = lloji;
            VleraPikes = vlerapike;
            IdPerdoruesi = idKrijues;
            IdNdermarrje = idNdermarrje;
            IdStatusDok = 1;
            OcolTrupiPolitikeKarta = ocolTrupi;

            Kontrollo(shtim);
        }

        public clsPolitikeKarta(DataRow rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Metoda Publike

        public static DataTable MerrPolitikeKarteSipasNdermarrjes(int idNdermarrje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrPolitikeSipasNdermarrjes(idNdermarrje);
        }

        /// <summary>
        /// Metode qe perdoret per te shtuar ose modifikuar nje rresht ne gride.
        /// </summary>
        /// <param name="idPolitike">Identifikuesi i politikes.</param>
        /// <returns></returns>
        public static DataRow MerrPolitikeSipasId(int idPolitike)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.merrPolitikeSipasIdGrid(idPolitike);
        }

        public void Ruaj()
        {
            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabaseRegjistrim();

                IdPolitike = db.RuajPolitikeKarta(Kodi, Lloji, VleraPikes, IdStatusDok, IdNdermarrje, IdPerdoruesi);

                foreach (var trupiPolitikeKarta in OcolTrupiPolitikeKarta)
                {
                    trupiPolitikeKarta.IdPolitike = IdPolitike;
                    trupiPolitikeKarta.Ruaj(db);
                }

                scope.Complete();
            }
        }

        public void Modifiko()
        {
            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabaseRegjistrim();

                db.ModifikoPolitikeKarta(IdPolitike, Kodi, Lloji, VleraPikes, IdStatusDok, IdNdermarrje, IdPerdoruesi);

                clsTrupiPolitikeKarta.FshiTrupiPolitikeKartaSipasIdPolitike(db, IdPolitike);

                foreach (var trupiPolitikeKarta in OcolTrupiPolitikeKarta)
                {
                    trupiPolitikeKarta.IdPolitike = IdPolitike;
                    trupiPolitikeKarta.Ruaj(db);
                }

                scope.Complete();
            }
        }

        public clsMesazh Fshi(int idPerdorues)
        {
            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabaseRegjistrim();
                var mesazh = new clsMesazh();
                try
                {
                    mesazh = db.fshiPolitikeMeStatusDok(IdPolitike, idPerdorues);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                    scope.Complete();
                    return mesazh;
                }
                catch
                {
                    return mesazh;
                }
            }
        }

        /// <summary>
        /// Kontrollon nqs politika eshte e lidhur me karte.
        /// </summary>
        /// <param name="idPolitike">Identifikuesi i politikes.</param>
        /// <param name="idNdermarrje">Identifikuesi i ndermarrjes.</param>
        /// <returns></returns>
        public static bool EkzistonPolitikeLidhurMeKarte(int idPolitike, int idNdermarrje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.ekzistonPolitikaLidhurMeKarte(idPolitike, idNdermarrje);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Kontrollon nqs politika eshte e vlefshme.
        /// </summary>
        /// <param name="shtim">if set to <c>true</c> [shtim].</param>
        /// <exception cref="MyException">
        /// Plotesoni kodin e politikes!
        /// or
        /// Ekziston nje politike me kete kod!
        /// or
        /// Duhet te celni kategorite e pikeve!
        /// </exception>
        private void Kontrollo(bool shtim)
        {
            if (Kodi == "")
                throw new MyException(MessagesResource.Messages["PolitikeKartaKlienti.ValidimKodi"]);

            if (shtim && EkzistonPolitika())
                throw new MyException(MessagesResource.Messages["PolitikeKartaKlienti.EkzistonPolitika"]);

            if (Lloji != 0 && OcolTrupiPolitikeKarta.Count == 0)
                throw new MyException(MessagesResource.Messages["PolitikeKartaKlienti.CelniKategorine"]);
        }

        /// <summary>
        /// Kontrollon per ekzistencen e politikes.
        /// </summary>
        /// <returns></returns>
        private bool EkzistonPolitika()
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.ekzistonPolitika(Kodi, IdNdermarrje);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Metode per mbushjen e automjetit nga databaza. Thirret nga metoda mbushAutomjetet e colAutomjete.cs
        /// </summary>
        /// <param name="dataRow">Si parameter merr nje DataRow.</param>
        internal void Mbush(DataRow dataRow)
        {
            if (dataRow == null)
                return;

            IdPolitike = !IsDBNull(dataRow["IDPOLITIKE"])
                ? ToInt32(dataRow["IDPOLITIKE"])
                : 0;
            Kodi = dataRow["KODI"].ToString();
            Lloji = !IsDBNull(dataRow["LLOJI"])
                ? ToInt32(dataRow["LLOJI"])
                : 0;
            VleraPikes = !IsDBNull(dataRow["VLERAPIKES"])
                ? ToDouble(dataRow["VLERAPIKES"])
                : 0;
            IdStatusDok = !IsDBNull(dataRow["IDSTATUSDOK"])
                ? ToInt32(dataRow["IDSTATUSDOK"])
                : 0;
            IdNdermarrje = !IsDBNull(dataRow["IDNDERMARRJE"])
                ? ToInt32(dataRow["IDNDERMARRJE"])
                : 0;
            IdPerdoruesi = !IsDBNull(dataRow["IDPERDORUESI"])
                ? ToInt32(dataRow["IDPERDORUESI"])
                : 0;
            DtKrijimi = !IsDBNull(dataRow["DTKRIJIMI"])
                ? ToDateTime(dataRow["DTKRIJIMI"])
                : DateTime.MinValue;
            DtModifikimi = !IsDBNull(dataRow["DTMODIFIKIMI"])
                ? ToDateTime(dataRow["DTMODIFIKIMI"])
                : DateTime.MinValue;
        }

        #endregion
    }
}
