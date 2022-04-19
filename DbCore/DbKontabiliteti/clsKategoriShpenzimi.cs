using System;
using System.Data;
using DbCore.IMBUtils.Messages;
using static System.Convert;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje kategori shpenzimi
    ///  (Te dhenat  merren nga tabela : T_KATEGORISHPENZIMI)
    /// </summary>
    public class clsKategoriShpenzimi
    {
        /// <summary>
        /// konstante kur nuk ruhet aktiviteti
        /// </summary>
        private const string StrAktivitetiNukURuajt = "Kategoria nuk u ruajt!";

        /// <summary>
        /// konstante kur nuk ruhet nje nga rreshtat e trupit
        /// </summary>
        private const string StrNjeNgaRreshtatETrupitNukURuajt = "Nje nga rreshtat e buxhetet nuk u ruajt!";

        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk modifikohet aktiviteti
        /// </summary>
        private const string StrAktivitetiNukUModifikua = "Kategoria nuk u modifikua!";

        /// <summary>
        ///  kur aktiviteti mbushet me sukses
        /// </summary>
        public static string MbushjeSukses = "Kategoria u mbush me sukses";

        /// <summary>
        ///  kur ndodh gabim ne marrjen e te dhenave
        /// </summary>
        public static string GabimNeTeDhena = "ERROR: Gabim gjate marrjes se kategorise nga db-ja";

        /// <summary>
        ///  kur nuk kthen gje db
        /// </summary>
        public static string Drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKategoriShpenzimi()
        {
        }

        /// <summary>
        /// Konstruktori me parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kodi"></param>
        /// <param name="pershkrimi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarje"></param>
        /// <param name="idkrijuesi"></param>
        /// <param name="idStatusDok"></param>
        /// <param name="buxheti"></param>
        /// <param name="shtim"></param>
        /// <param name="idPrindi"></param>
        /// <param name="nivelKategoriShpenzimi"></param>
        /// <param name="kodPrindi"></param>
        /// <param name="kategoriAktive"></param>
        public clsKategoriShpenzimi(int id, string kodi, string pershkrimi, int idPerdoruesi, int idNdermarje, int idkrijuesi, int idStatusDok, colBuxhetet buxheti, bool shtim, int idPrindi, int nivelKategoriShpenzimi, string kodPrindi, bool kategoriAktive)
        {
            Id = id;
            Kodi = kodi;
            Pershkrimi = pershkrimi;
            IdPerdoruesi = idPerdoruesi;
            IdNdermarje = idNdermarje;
            Idkrijuesi = idkrijuesi;
            IdStatusDok = idStatusDok;
            IdPrindi = idPrindi;
            NivelKategorie = nivelKategoriShpenzimi;
            KodPrindi = kodPrindi;
            KategoriAktive = kategoriAktive;
            OColBuxhetet = buxheti;

            Kontrollokategori(shtim);
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i kategorise</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsKategoriShpenzimi(string kodi, int idNderm)
        {
            var db = new clsDatabaseKontabilitet();
            if (!MbushKategori(db.ktheKategoriShpenzimiSipasKodit(kodi, idNderm)).Status)
                Id = -1;
            db.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kategorise</param>
        public clsKategoriShpenzimi(int id)
        {
            using (var db = new clsDatabaseKontabilitet())
                MbushKategori(db.ktheKategoriShpenzimi(id));
        }

        public clsKategoriShpenzimi(DataRow rreshti)
        {
            MbushKategori(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// id e kategorise
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// kodi i kategorise
        /// </summary>
        public string Kodi { get; set; }


        /// <summary>
        /// pershkrimi
        /// </summary>
        public string Pershkrimi { get; set; }


        /// <summary>
        /// id e perdoruesit qe ka kryer veprimin
        /// </summary>
        public int IdPerdoruesi { get; set; }

        /// <summary>
        /// id e ndermarjes 
        /// </summary>
        public int IdNdermarje { get; set; }

        /// <summary>
        /// id e krijuesit
        /// </summary>
        public int Idkrijuesi { get; set; }

        /// <summary>
        /// id e status te dok
        /// <example> 0 draft, 1-ruajtur,2 -fshire</example>
        /// </summary>
        public int IdStatusDok { get; set; }

        /// <summary>
        /// data e krijimit te burimit
        /// </summary>
        public DateTime DtKrijimi { get; private set; }

        /// <summary>
        /// data e modifikimi te fundit te burimit
        /// </summary>
        public DateTime DtModifikimi { get; private set; }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsBuxheti"/>
        /// </summary>
        public colBuxhetet OColBuxhetet { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e prindit.
        /// </summary>
        public int IdPrindi { get; set; }

        /// <summary>
        /// Kthen/Vendos nivelin e kodifikimit.
        /// </summary>
        public int NivelKategorie { get; set; }

        /// <summary>
        /// Kthen kodin e prindit
        /// </summary>
        public string KodPrindi { get; }

        /// <summary>
        /// Kthen kodin e prindit
        /// </summary>
        public bool KategoriAktive { get; set; }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// ruan kategori shpenzimi
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo kategoria</returns>
        public clsMesazh Ruaj(int idNderVit, bool eshteProjektBuxhet)
        {
            var db = new clsDatabaseKontabilitet();
            db.beginTransaksion();
            int idKoka;
            var mesazh = db.ruajKategoriShpenzimi(out idKoka, Kodi, Pershkrimi, Idkrijuesi, IdPerdoruesi, IdNdermarje, IdStatusDok, IdPrindi, NivelKategorie, KategoriAktive);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, StrAktivitetiNukURuajt);
            }

            Id = idKoka;
            var idLlojBuxheti = clsLlojBuxheti.mbushIDLlojBuxheti("KategoriShpenzimi", db);

            foreach (var o in OColBuxhetet)
            {
                o.IdLidhese = Id;
                int idB;
                mesazh = db.ruajBuxhet(out idB, idLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, idNderVit, eshteProjektBuxhet, o.Shenime, o.DtAktivizimi, o.IdKonfigUrdherPagese);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return new clsMesazh(false, StrNjeNgaRreshtatETrupitNukURuajt);
                }
            }

            if (mesazh.Status)
            {
                if (!eshteProjektBuxhet)
                {
                    mesazh = colBuxhetet.krijoBuxhetetPerGjitheVitetENdermPerKategorine(idLlojBuxheti, IdNdermarje, Id, db);
                    if (mesazh.Status)
                        db.commitTransaksion();
                    else
                        db.rollbackTransaksion();
                }
                else
                    db.commitTransaksion();
            }
            else
                db.rollbackTransaksion();

            return mesazh;
        }

        /// <summary>
        /// modifikon skemen
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo skemen</returns>   
        public clsMesazh Modifiko(int idNderVit, bool eshteProjektBuxhet)
        {
            var db = new clsDatabaseKontabilitet();
            db.beginTransaksion();

            var mesazh = db.modifikoKategoriShpenzimi(Id, Kodi, Pershkrimi, IdPerdoruesi, IdNdermarje, IdStatusDok, IdPrindi, NivelKategorie, KategoriAktive);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, StrAktivitetiNukUModifikua);
            }
            int idLlojBuxheti = clsLlojBuxheti.mbushIDLlojBuxheti("KategoriShpenzimi", db);
            if (OColBuxhetet.Count != 0)
            {
                if (!clsBuxheti.ekzistonProjektBuxhetiPerKategorine(Id, idLlojBuxheti, idNderVit, eshteProjektBuxhet, OColBuxhetet[0].DtAktivizimi))
                {
                    foreach (var o in OColBuxhetet)
                    {
                        o.IdLidhese = Id;
                        o.IdLlojBuxheti = idLlojBuxheti;
                        int idB;
                        mesazh = db.ruajBuxhet(out idB, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, idNderVit, eshteProjektBuxhet, o.Shenime, o.DtAktivizimi, o.IdKonfigUrdherPagese);
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return new clsMesazh(false, StrNjeNgaRreshtatETrupitNukURuajt);
                        }
                    }
                }
                else
                {
                    foreach (var o in OColBuxhetet)
                    {
                        mesazh = db.modifikoBuxhet(o.IdBuxheti, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, idNderVit, eshteProjektBuxhet, o.Shenime, o.DtAktivizimi, o.IdKonfigUrdherPagese);
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return new clsMesazh(false, StrNjeNgaRreshtatETrupitNukURuajt);
                        }
                    }
                }
            }

            if (mesazh.Status)
                db.commitTransaksion();
            else
                db.rollbackTransaksion();

            return mesazh;
        }

        /// <summary>
        /// therret funksionin fshi te kesaj klase
        /// </summary>
        /// <returns>mesazh: true nese fshirja eshte kryer me sukses, false perndryshe.</returns>
        public clsMesazh Fshi()
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.fshiKategoriShpenzimiStatus(Id, IdPerdoruesi);
        }

        /// <summary>
        /// kontrollon nese ekziston kategori me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool EkzistonKategori(string kodi, int idndermarje)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.ekzistonKategoriShpenzimi(kodi, idndermarje);
        }

        public static bool KaVeprimeKategoriShpenzimi(int id)
        {
            using (var db = new clsDatabaseKontabilitet())
            {
                return db.kaVeprimeKategoriShpenzimi(id);
            }
        }

        /// <summary>
        /// kontrollon nese kategoria me kete id eshte prind
        /// </summary>
        /// <param name="id"></param>
        /// <returns>kthen true ne rast se kategoria me id eshte prind, false perndryshe</returns>
        public static bool KontrolloEshtePrindKategoriaShpenzimit(int id)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.kontrolloEshtePrindKategoriaShpenzimit(id);
        }

        public static int KtheIdSipasKodit(string kodi, int idNdermarrje)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.ktheIdKategoriShpenzimiSipasKodit(kodi, idNdermarrje);
        }

        public static clsKategoriShpenzimi KrijoKategoriShpenzimiNgaImporti(string kodi, string pershkrimi, bool aktiv, string kodPrindi, int idNdermarrja, int idPerdoruesi)
        {
            var idPrindi = 0;
            var niveli = 1;
            if (kodPrindi != "")
            {
                idPrindi = KtheIdSipasKodit(kodPrindi, idNdermarrja);
                if (idPrindi == 0)
                    throw new MyException(MessagesResource.Messages["msgPrindiNukEkziston"]);
                niveli = KtheNivelKategoriShpenzimi(idPrindi) + 1;
            }

            return new clsKategoriShpenzimi(0, kodi, pershkrimi, idPerdoruesi, idNdermarrja, idPerdoruesi, 1, colBuxhetet.KrijoBuxhetetFillestare(new DateTime(DateTime.Now.Year, 01, 01), false), true, idPrindi, niveli, kodPrindi, aktiv);
        }

        #endregion

        #region Metoda Private

        private static int KtheNivelKategoriShpenzimi(int idKategoriShpenzimi)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.KtheNivelKategoriShpenzimi(idKategoriShpenzimi);
        }

        /// <summary>
        /// kontrollon nese ka nje prind te ndryshem nga ky qe po i caktohet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idPrindi"></param>
        /// <returns></returns>
        private static bool KontrolloEshtePrindKategoriaShpenzimit(int id, int idPrindi)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.kontrolloEshtePrindKategoriaShpenzimit(id, idPrindi);
        }

        private static bool KaVeprimePrindiKategoriShpenzimi(int id)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.kaVeprimePrindiKategoriShpenzimi(id);
        }

        /// <summary>
        /// kontroll nese objekti i kategorise i ka te dhenat e sakta 
        /// </summary>
        /// <param name="shtim">tregon nese eshte shtim apo modifikim</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private void Kontrollokategori(bool shtim)
        {
            if (Kodi == "")
                throw new MyException(MessagesResource.Messages["msgPlotesoniKodinKategorise"]);

            if (Pershkrimi == "")
                throw new MyException(MessagesResource.Messages["msgPlotesoniEmertiminEAktivitetit"]);

            if (!shtim && KontrolloEshtePrindKategoriaShpenzimit(Id, IdPrindi) && KodPrindi != string.Empty)
                throw new MyException("Kategorise nuk mund t`i caktohet prind sepse eshte e detajuar!");

            if (KodPrindi != string.Empty)
            {
                if (!EkzistonKategori(KodPrindi, IdNdermarje))
                    throw new MyException(MessagesResource.Messages["msgPrindiNukEkziston"]);

                if (KaVeprimePrindiKategoriShpenzimi(IdPrindi))
                    throw new MyException("Kategoria prind eshte e lidhur me llogari. Ju lutem zgjidhni nje kategori prind tjeter!");
            }

            if (!shtim && Id == IdPrindi)
                throw new MyException("Kategoria nuk mund te zgjidhet prind i saj. Ju lutem, zgjidhni nje kategori prind tjeter!");

            if (shtim && EkzistonKategori(Kodi, IdNdermarje))
                throw new MyException(MessagesResource.Messages["msgEkzistonNjeAktivitetMeKeteKod"]);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kategorine me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh MbushKategori(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    Id = !IsDBNull(dbDataRow["ID"]) ? ToInt32(dbDataRow["ID"]) : 0;
                    Kodi = dbDataRow["KODI"].ToString();
                    Pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    IdPerdoruesi = !IsDBNull(dbDataRow["IDPERDORUESI"]) ? ToInt32(dbDataRow["IDPERDORUESI"]) : 0;
                    IdNdermarje = !IsDBNull(dbDataRow["IDNDERMARJE"]) ? ToInt32(dbDataRow["IDNDERMARJE"]) : 0;
                    Idkrijuesi = !IsDBNull(dbDataRow["IDKRIJUESI"]) ? ToInt32(dbDataRow["IDKRIJUESI"]) : 0;
                    IdStatusDok = !IsDBNull(dbDataRow["IDSTATUSDOK"]) ? ToInt32(dbDataRow["IDSTATUSDOK"]) : 0;
                    KategoriAktive = !IsDBNull(dbDataRow["KATEGORIAKTIVE"]) && ToBoolean(dbDataRow["KATEGORIAKTIVE"]);
                    DtKrijimi = !IsDBNull(dbDataRow["DTKRIJIMI"]) ? ToDateTime(dbDataRow["DTKRIJIMI"]) : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dbDataRow["DTMODIFIKIMI"]) ? ToDateTime(dbDataRow["DTMODIFIKIMI"]) : DateTime.MinValue;
                    NivelKategorie = !IsDBNull(dbDataRow["NIVELKATEGORIE"]) ? ToInt32(dbDataRow["NIVELKATEGORIE"]) : 0;
                    IdPrindi = !IsDBNull(dbDataRow["IDPRINDI"]) ? ToInt32(dbDataRow["IDPRINDI"]) : 0;
                    OColBuxhetet = new colBuxhetet();

                    return new clsMesazh(true, MbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(GabimNeTeDhena);
                }
            }

            return new clsMesazh(false, Drbosh);
        }

        #endregion
    }
}
