using System;
using System.Collections.Generic;
using System.Data;
using DbCore.DbKontabiliteti;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne dokumentat e trupit te azhornimit te Klient\Furnitoreve
    /// </summary>
    public class clsAzhornimKFTrupi
    {
        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsAzhornimKFTrupi()
        {
        }

        public clsAzhornimKFTrupi(Dictionary<string, object> rreshtDokuKlient, int idndermarje, int idperdoruesi)
        {
            var kodi = rreshtDokuKlient["txtKodi"].ToString();
            var pershkrimi = rreshtDokuKlient["txtPershkrim"].ToString();
            var dk = rreshtDokuKlient["cmbDebiKredi"].ToString();
            var vlefta = rreshtDokuKlient["txtVlefta"].ToString();
            var kursi = rreshtDokuKlient["txtKursi"].ToString();
            var llogkunder = rreshtDokuKlient["txtLlogKunderparti"].ToString();

            if (kodi != null && kodi != "null" && kodi != "")
            {
                KodiKlientFurnitor = kodi;
                if (kodi != "" && !clsKlientFurnitor.EkzistonKlientFurnitor(kodi, idndermarje))
                    throw new Exception("Klient/Furnitori nuk ekziston!");

                var kf = new clsKlientFurnitor();
                kf.mbushKlientFurnitorSipasKodit(kodi, idndermarje, idperdoruesi);
                if (kf.IdKlientFurnitor < 1)
                    throw new Exception("Ju nuk keni autorizime per kete klient/furnitor!");

                if (kodi != "" && !kf.AktivKF)
                    throw new Exception("Klient/Furnitori nuk është aktive!");

                IdKlientFurnitor = kf.IdKlientFurnitor;

                if (dk != null && dk != "null" && dk != "")
                {
                    DebiKredi = dk == "Debi";
                }

                if (vlefta != null && vlefta != "null" && vlefta != "")
                {
                    Vlefta = double.Parse(vlefta);
                }

                var llogari = new clsLlogari(llogkunder, idndermarje);

                if (llogari.IdLlogari > 0 && llogari.Aktiv)
                {
                    IdLlogariKp = llogari.IdLlogari;
                    NrLlogariKp = llogari.NrLlogari;
                }
                else
                {
                    IdLlogariKp = -1;
                    NrLlogariKp = "";
                }

                if (pershkrimi != null && pershkrimi != "null")
                {
                    Pershkrimi = pershkrimi;
                }

                if (kursi != null && kursi != "null" && kursi != "")
                {
                    Kursi = double.Parse(kursi);
                }

                IdAzhornimKfTrupi = 0;

            }
            else
                IdKlientFurnitor = -1;
        }

        public clsAzhornimKFTrupi(DataRow rreshti)
        {
            MbushAzhornimKfTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne  e dokumentit, gjenerohet automatikisht nga databaza.
        /// </summary>
        public int IdAzhornimKfTrupi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne  e Klient\Furnitorit per te cilin po behet azhornimi
        /// </summary>
        public int IdKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e Klient\Furnitorit per te cilin po behet azhornimi
        /// </summary>
        public string KodiKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos emrin e Klient\Furnitorit per te cilin po behet azhornimi
        /// </summary>
        public string EmerKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos pershkrimin qe eshte vendosur ne rreshtin perkates te grides
        /// </summary>
        public string Pershkrimi { get; set; }

        /// <summary>
        /// Kthen/Vendos vleren qe tregon nese llogari e klientit do prekete ne debi apo ne kredi
        /// </summary>
        public bool DebiKredi { get; set; }

        /// <summary>
        /// Kthen/Vendos vleften qe eshte gjeneruar si pasoje e azhornimit
        /// </summary>
        public double Vlefta { get; set; }

        /// <summary>
        /// Kthen/Vendos kursin e azhornimit
        /// </summary>
        public double Kursi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne  e llogarise kunderparti, qe do preket ne kah te kundert me llogarine e klient/furnitorit
        /// </summary>
        public int IdLlogariKp { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin  e llogarise kunderparti, qe do preket ne kah te kundert me llogarine e klient/furnitorit
        /// </summary>
        public string NrLlogariKp { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne  e kokes se dokumentit qe eshte gjeneruar nga veprimi i azhornimit
        /// </summary>
        public int IdAzhornimKfKoka { get; set; }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e azhornimit te KF-se nga databaza
        /// </summary>
        /// <param name="dbDataRowAzhornimKfTrupi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushAzhornimKfTrupi(DataRow dbDataRowAzhornimKfTrupi)
        {
            if (dbDataRowAzhornimKfTrupi != null)
            {
                try
                {
                    DebiKredi = !IsDBNull(dbDataRowAzhornimKfTrupi["DEBIKREDI"]) && ToBoolean(dbDataRowAzhornimKfTrupi["DEBIKREDI"]);
                    IdAzhornimKfTrupi = !IsDBNull(dbDataRowAzhornimKfTrupi["IDAZHORNIMTRUPI"])
                        ? ToInt32(dbDataRowAzhornimKfTrupi["IDAZHORNIMTRUPI"])
                        : 0;
                    IdKlientFurnitor = !IsDBNull(dbDataRowAzhornimKfTrupi["IDKLIENTFURNITOR"])
                        ? ToInt32(dbDataRowAzhornimKfTrupi["IDKLIENTFURNITOR"])
                        : 0;
                    IdLlogariKp = !IsDBNull(dbDataRowAzhornimKfTrupi["LLOGARIKP"])
                        ? ToInt32(dbDataRowAzhornimKfTrupi["LLOGARIKP"])
                        : 0;
                    KodiKlientFurnitor = dbDataRowAzhornimKfTrupi["KODKLIENTFURNITOR"].ToString();
                    NrLlogariKp = dbDataRowAzhornimKfTrupi["LLOGARIKP"].ToString();
                    Pershkrimi = dbDataRowAzhornimKfTrupi["PERSHKRIMI"].ToString();
                    Vlefta = !IsDBNull(dbDataRowAzhornimKfTrupi["VLEFTA"])
                        ? ToDouble(dbDataRowAzhornimKfTrupi["VLEFTA"])
                        : 0;
                    EmerKlientFurnitor = dbDataRowAzhornimKfTrupi["EMERTIMIKF"].ToString();
                    Kursi = !IsDBNull(dbDataRowAzhornimKfTrupi["KURSI"])
                        ? ToDouble(dbDataRowAzhornimKfTrupi["KURSI"])
                        : 0;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se azhornimit te trupit te KF-se nga db-ja");
                }
            }
        }

        #endregion
    }
}
