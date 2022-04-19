using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using NLog;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kartat e klientit
    ///  (Te dhenat  merren nga tabela : T_Karta)
    /// </summary>
    public class clsKarta
    {
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKarta { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e kartes.
        /// </summary>
        public string Kodi { get; set; }

        /// <summary>
        /// Kthen/Vendos email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Kthen/Vendos emrin e personit.
        /// </summary>
        public string Emri { get; set; }

        /// <summary>
        /// Kthen/Vendos kontaktin e personit.
        /// </summary>
        public string Kontakt { get; set; }

        public string Shoferi { get; set; }

        public string Targa { get; set; }

        /// <summary>
        /// Kthen/Vendos idStatusDok
        /// </summary>
        public int IdStatusDok { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e krijuesit.
        /// </summary>
        public int IdKrijues { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes.
        /// </summary>
        public int IdNdermarrje { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdModifikues { get; set; }

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi { get; private set; }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit.
        /// </summary>
        public DateTime DtModifikimi { get; private set; }

        public int IdPolitike { get; set; }

        public int IdKategoriZB { get; set; }

        public colKlienteFurnitore OColKlientFurnitor { get; set; }

        public ICollection<clsLimitKarta> Limitet { get; set; }

        /// <summary>
        /// datelindja
        /// </summary>
        public DateTime Datelindja { get; set; }

        /// <summary>
        /// id e  qytetit
        /// </summary>
        public int IdQyteti { get; set; }

        /// <summary>
        /// adresa
        /// </summary>
        public string Adresa { get; set; }

        public bool Aktiv { get; set; }

        public Dhurata Dhurata { get; private set; }

        public string Departamenti { get; set; }

        public string Modeli { get; set; }

        public string Id { get; set; }

        public int IdMenyrePagese { get; set; }
        #endregion
        public int GjendjePikesh { get; set; }
        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKarta() { }

        /// <summary>
        /// konstrukotr me 1 parameter
        /// </summary>
        /// <param name="idKarta">id e kartes</param>
        public clsKarta(int idKarta)
        {
            using (var data = new clsDatabaseRegjistrim())
                Mbush(data.MerrKartaSipasId(idKarta));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsKarta"/> class.
        /// </summary>
        /// <param name="kodi">Kodi.</param>
        /// <param name="idNdermarje">The identifier ndermarje.</param>
        /// <param name="data">The data.</param>
        public clsKarta(string kodi, int idNdermarje, clsDatabaseRegjistrim data)
        {
            Mbush(data.TransCache.getKarta(kodi, idNdermarje, data));
        }

        public clsKarta(int idKarta, string kodi, string emri, string email, string kontakt, string shoferi, string targa, string klient, string politike, string kategoriZbritje, int idKrijues, int idNdermarrje, int idModifikues, bool aktiv, bool shtim, bool lejoMeTenjejtinKlient, DateTime datelindja, string qyteti, string adresa, IEnumerable<DataRow> limitet, Dhurata dhurata, string departamenti, string modeli, string id, int idMenyrePagese,int gjendjePikesh)
        {
            IdKarta = idKarta;
            Kodi = kodi;
            Emri = emri;
            Email = email;
            Kontakt = kontakt;
            IdStatusDok = 1;
            IdKrijues = idKrijues;
            IdNdermarrje = idNdermarrje;
            IdModifikues = idModifikues;
            Targa = targa;
            Shoferi = shoferi;
            Aktiv = aktiv;
            Datelindja = datelindja;
            Adresa = adresa;
            Dhurata = dhurata;
            Departamenti = departamenti;
            Modeli = modeli;
            Id = id;
            IdMenyrePagese = idMenyrePagese;
            GjendjePikesh = gjendjePikesh;

            var qytet = new clsQyteti(qyteti, idNdermarrje);
            IdQyteti = qytet.IdQyteti;

            var politika = new clsPolitikeKarta();
            if (!string.IsNullOrEmpty(politike))
            {
                politika = new clsPolitikeKarta(politike, idNdermarrje);
                IdPolitike = politika.IdPolitike;
            }

            if (!string.IsNullOrEmpty(kategoriZbritje))
            {
                if (politika.Lloji == 1)
                    throw new MyException(string.Format(MessagesResource.Messages["KartaKlienti.PolitikeMePikeValidim"], politika.Kodi));

                IdKategoriZB = clsKokaKategoriZbritje.ktheIdKokaKategoriZbritje(kategoriZbritje, idNdermarrje);
            }

            OColKlientFurnitor = new colKlienteFurnitore();
            if (!string.IsNullOrEmpty(klient))
            {
                var kodet = klient.Split(',');

                for (var index = 0; index < kodet.Length; index++)
                {
                    var kodKlientFurnitori = kodet[index];
                    kodKlientFurnitori = kodKlientFurnitori.RemoveSpaces();
                    if (string.IsNullOrEmpty(kodKlientFurnitori))
                        continue;

                    var klientFurnitor = new clsKlientFurnitor(kodKlientFurnitori, idNdermarrje, idKrijues);
                    if (klientFurnitor.IdKlientFurnitor == 0)
                        throw new MyException("Klienti me kod " + kodKlientFurnitori + " nuk ekziston!");

                    OColKlientFurnitor.Add(klientFurnitor);
                }
            }

            Limitet = new List<clsLimitKarta>();
            foreach (var row in limitet)
            {
                Limitet.Add(new clsLimitKarta(IdKarta, ToInt32(row["Lloji"]), row["Kodi"].ToString(),
                        ToDouble(row["LimitSasi"]), ToDouble(row["LimitVlere"]), ToDateTime(row["DtFillimi"]),
                        ToDateTime(row["DtMbarimi"]), row["ZbritjaAnalitike"].ToString(), idNdermarrje));
            }
            
            KontrolloKarta(shtim, lejoMeTenjejtinKlient, qyteti, kategoriZbritje, politike);
        }

        public clsKarta(DataRow rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Metoda Publike

        public static clsKarta KrijoKartePerImoprt(string kodi, string emri, string email, string kontakt, string politike, string kategoriZbritje, string klienti, string targa, string shoferi, string qyteti, string adresa, bool aktiv, DateTime ditelindja, int idPerdoruesi, int idNdermarrje, bool lejoMeTeNjejtinKlient, string departamenti, string modeli, string id, string menyrePagese, int gjendjePikesh)
        {
            int idMenyrePagese = clsFunksione.ktheMenyrePageseSipasLlojit(menyrePagese);
            return new clsKarta(0, kodi, emri, email, kontakt, shoferi, targa, klienti, politike, kategoriZbritje, idPerdoruesi, idNdermarrje, 0, aktiv, true, lejoMeTeNjejtinKlient, ditelindja, qyteti, adresa, new List<DataRow>(), new Dhurata(), departamenti, modeli, id, idMenyrePagese, gjendjePikesh);
        }

        /// <summary>
        /// kthe nje datarow me karten e marre nga db sipas id.
        /// </summary>
        /// <param name="idKarte"></param>
        /// <returns>datarow</returns>
        public static DataRow MerrKarteSipasId(int idKarte)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKartaSipasId(idKarte);
        }

        /// <summary>
        /// kthe nje datatable me karten e marre nga db sipas id.
        /// </summary>
        /// <param name="idKarte"></param>
        /// <returns>datatable</returns>
        public static DataTable MerrKarteSipasIdDt(int idKarte)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKartaSipasIdDt(idKarte);
        }

        public static DataTable MerrKarteSipasIdDheKlientitDt(int idKarte, int idKlient)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrKarteSipasIdDheKlientitDt(idKarte, idKlient);
        }
        public void Ruaj()
        {
            using (var scope = new MyTransactionScope())
            {
                using (var db = new clsDatabaseRegjistrim())
                {
                    try
                    {
                        IdKarta = db.RuajKarta(Kodi, Emri, Email, Kontakt, IdStatusDok, IdNdermarrje, IdKrijues, IdModifikues, IdPolitike, IdKategoriZB, Aktiv, Targa, Shoferi, Datelindja, IdQyteti, Adresa, Departamenti, Modeli, Id, IdMenyrePagese);

                        foreach (var klientFurnitor in OColKlientFurnitor)
                        {
                            if (klientFurnitor.IdKlientFurnitor == 0)
                                continue;

                            db.RuajLidhjeKarta(IdKarta, klientFurnitor.IdKlientFurnitor);
                        }

                        if (new clsPolitikeKarta(IdPolitike).Lloji != 0)
                        {
                            Dhurata.IdKarta = IdKarta;
                            Dhurata.IdKategoriDhurate = new colTrupiPolitikeKarta(IdPolitike)[0].IdKategoria;
                            Dhurata.IdNdermarrje = Dhurata.IdNdermarrje == 0 ? IdNdermarrje : Dhurata.IdNdermarrje;
                            Dhurata.IdKrijues = Dhurata.IdKrijues == 0 ? IdKrijues : Dhurata.IdKrijues;
                            Dhurata.IdNdermarrjeVit = Dhurata.IdNdermarrjeVit == 0 ? clsNdermarrjeViti.ktheIdNdermarrjeVitiSipasNdermarjesDheKodVitit(IdNdermarrje, DateTime.Today.Year) : Dhurata.IdNdermarrjeVit;
                            Dhurata.PikeDebit = 0 - GjendjePikesh;
                            Dhurata.Ruaj(db);
                        }

                        if (Limitet != null)
                        {
                            foreach (var limitKarta in Limitet)
                            {
                                limitKarta.IdKarta = IdKarta;
                                limitKarta.Ruaj(db);
                            }
                        }

                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
                        throw;
                    }
                }
            }
        }

        public void Modifiko()
        {
            using (var scope = new MyTransactionScope())
            {
                using (var db = new clsDatabaseRegjistrim())
                {
                    db.ModifikoKarta(IdKarta, Kodi, Emri, Email, Kontakt, IdStatusDok, IdNdermarrje, IdModifikues, IdPolitike, IdKategoriZB, Aktiv, Targa, Shoferi, Datelindja, IdQyteti, Adresa, Departamenti, Modeli, Id, IdMenyrePagese);

                    db.FshiLidhjeKarta(IdKarta);

                    foreach (var klientFurnitor in OColKlientFurnitor)
                    {
                        db.RuajLidhjeKarta(IdKarta, klientFurnitor.IdKlientFurnitor);
                    }

                    if (!EshteLidhurKarta(IdKarta, clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("KK", IdNdermarrje)))
                    {
                        Dhurata.ModifikoPikeFillestareKarte(db);
                    }

                    clsLimitKarta.FshiSipasKartes(db, IdKarta);

                    foreach (var limitKarta in Limitet)
                    {
                        limitKarta.IdKarta = IdKarta;
                        limitKarta.Ruaj(db);
                    }

                    scope.Complete();
                }
            }
        }

        public void Fshi(int idPerdorues)
        {
            using (var scope = new MyTransactionScope())
            {
                using (var db = new clsDatabaseRegjistrim())
                {
                    clsLimitKarta.FshiSipasKartes(db, IdKarta);
                    db.FshiKartaMeStatusDok(IdKarta, idPerdorues);

                    scope.Complete();
                }
            }
        }

        public static int KthePikeKarte(int idKarta, int idNdermarrje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrPikeKartaSipasId(idKarta, idNdermarrje);
        }

        public static bool EshteLidhurKarta(int idKarta, int idNivel)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.eshteDokumentiILidhurCelje(idKarta.ToString(), idNivel.ToString());
        }

        #endregion

        #region Metoda Private

        private bool EkzistonKarte(clsDatabaseRegjistrim db)
        {
            return db.EkzistonKarta(Kodi, IdNdermarrje);
        }

        private void KontrolloKarta(bool shtim, bool lejoMeTenjejtinKlient, string qyteti, string kategoriZbritje, string politike)
        {
            if (string.IsNullOrEmpty(Kodi))
                throw new MyException(MessagesResource.Messages["KartaKlienti.PlotesoniKodin"]);

            if (string.IsNullOrEmpty(politike))
                throw new MyException(MessagesResource.Messages["KartaKlienti.PlotesoniPolitiken"]);

            if (IdQyteti <= 0 && !string.IsNullOrEmpty(qyteti))
                throw new MyException(string.Format(MessagesResource.Messages["KartaKlienti.QytetiNukEkziston"], qyteti));

            if (IdKategoriZB <= 0 && !string.IsNullOrEmpty(kategoriZbritje))
                throw new MyException(string.Format(MessagesResource.Messages["KartaKlienti.KategoriaZbritjeNukEkziston"], kategoriZbritje));

            if (IdPolitike <= 0 && !string.IsNullOrEmpty(politike))
                throw new MyException(string.Format(MessagesResource.Messages["KartaKlienti.PolitikaNukEkziston"], politike));

            using (var db = new clsDatabaseRegjistrim())
            {
                if (shtim && EkzistonKarte(db))
                    throw new MyException(string.Format(MessagesResource.Messages["KartaKlienti.EkzistonKarte"], Kodi));

                foreach (var klientFurnitor in OColKlientFurnitor)
                {
                    if (shtim && !lejoMeTenjejtinKlient && db.EkzistonKartePerKlient(klientFurnitor.IdKlientFurnitor, IdNdermarrje))
                    {
                        throw new MyException(string.Format(MessagesResource.Messages["KartaKlienti.EkzistonKarteKlient"], klientFurnitor.KodKlientFurnitor));
                    }
                }
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Metode per mbushjen e kartes se klientit
        /// </summary>
        /// <param name="dbDataRow">The database data row.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">ERROR: Gabim gjate marrjes se automjetit nga db-ja</exception>
        internal void Mbush(DataRow dbDataRow)
        {
            if (dbDataRow == null)
                return;
            try
            {
                IdKarta = !IsDBNull(dbDataRow["IDKARTA"])
                    ? ToInt32(dbDataRow["IDKARTA"])
                    : 0;
                Kodi = dbDataRow["KODI"].ToString();
                Emri = dbDataRow["EMRI"].ToString();
                Email = dbDataRow["EMAIL"].ToString();
                Kontakt = dbDataRow["KONTAKT"].ToString();
                Aktiv = !IsDBNull(dbDataRow["AKTIV"]) && ToBoolean(dbDataRow["AKTIV"]);
                IdPolitike = !IsDBNull(dbDataRow["IDPOLITIKE"])
                    ? ToInt32(dbDataRow["IDPOLITIKE"])
                    : 0;
                IdKategoriZB = !IsDBNull(dbDataRow["IDKATEGORIZB"])
                    ? ToInt32(dbDataRow["IDKATEGORIZB"])
                    : 0;
                IdStatusDok = !IsDBNull(dbDataRow["IDSTATUSDOK"])
                    ? ToInt32(dbDataRow["IDSTATUSDOK"])
                    : 0;
                IdNdermarrje = !IsDBNull(dbDataRow["IDNDERMARRJE"])
                    ? ToInt32(dbDataRow["IDNDERMARRJE"])
                    : 0;
                IdKrijues = !IsDBNull(dbDataRow["IDKRIJUES"])
                    ? ToInt32(dbDataRow["IDKRIJUES"])
                    : 0;
                IdModifikues = !IsDBNull(dbDataRow["IDMODIFIKUES"])
                    ? ToInt32(dbDataRow["IDMODIFIKUES"])
                    : 0;
                DtKrijimi = !IsDBNull(dbDataRow["DTKRIJIMI"])
                    ? ToDateTime(dbDataRow["DTKRIJIMI"])
                    : DateTime.MinValue;
                DtModifikimi = !IsDBNull(dbDataRow["DTMODIFIKIMI"])
                    ? ToDateTime(dbDataRow["DTMODIFIKIMI"])
                    : DateTime.MinValue;
                Targa = dbDataRow["Targa"].ToString();
                Shoferi = dbDataRow["Shoferi"].ToString();
                Datelindja = !IsDBNull(dbDataRow["DATELINDJA"])
                    ? ToDateTime(dbDataRow["DATELINDJA"])
                    : DateTime.MinValue;
                IdQyteti = !IsDBNull(dbDataRow["IDQYTETI"])
                    ? ToInt32(dbDataRow["IDQYTETI"])
                    : 0;
                Adresa = dbDataRow["ADRESA"].ToString();
                Departamenti = dbDataRow["DEPARTAMENTI"].ToString();
                Modeli = dbDataRow["MODELI"].ToString();
                Id = dbDataRow["ID"].ToString();
                IdMenyrePagese = !IsDBNull(dbDataRow["IDMENYREPAGESE"])
                    ? ToInt32(dbDataRow["IDMENYREPAGESE"])
                    : 0;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se automjetit nga db-ja");
            }
        }

        private void Mbush(clsKarta karta)
        {
            IdKarta = karta.IdKarta;
            Kodi = karta.Kodi;
            Emri = karta.Emri;
            Email = karta.Email;
            Kontakt = karta.Kontakt;
            Aktiv = karta.Aktiv;
            IdPolitike = karta.IdPolitike;
            IdKategoriZB = karta.IdKategoriZB;
            IdStatusDok = karta.IdStatusDok;
            IdNdermarrje = karta.IdNdermarrje;
            IdKrijues = karta.IdKrijues;
            IdModifikues = karta.IdModifikues;
            DtKrijimi = karta.DtKrijimi;
            DtModifikimi = karta.DtModifikimi;
            Targa = karta.Targa;
            Shoferi = karta.Shoferi;
            Datelindja = karta.Datelindja;
            IdQyteti = karta.IdQyteti;
            Adresa = karta.Adresa;
            Departamenti = karta.Departamenti;
            Modeli = karta.Modeli;
            Id = karta.Id;
            IdMenyrePagese = karta.IdMenyrePagese;
        }

        #endregion
    }
}