using System;
using System.Collections.Generic;
using System.Data;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    public class clsTrupiMbylljeKF
    {
        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiMbylljeKF()
        {
        }

        public clsTrupiMbylljeKF(Dictionary<string, object> rreshtDokuKlient, int idndermarje, int idperdoruesi)
        {
            var kodi = rreshtDokuKlient["txtKodi"].ToString();
            var pershkrimi = rreshtDokuKlient["txtPershkrim"].ToString();
            var dk = rreshtDokuKlient["cmbDebiKredi"].ToString();
            var kursi = rreshtDokuKlient["txtKursi"].ToString();
            var gjendja = rreshtDokuKlient["txtGjendja"].ToString();
            var gjendjamon = rreshtDokuKlient["txtGjendjaMon"].ToString();
            var llogkunder = rreshtDokuKlient["txtLlogKunderparti"].ToString();

            if (kodi != null && kodi != "null" && kodi != "")
            {
                KodiKlientFurnitor = kodi;
                if (kodi != "" && !DbKontabiliteti.clsKlientFurnitor.EkzistonKlientFurnitor(kodi, idndermarje))
                    throw new Exception("Klient/Furnitori nuk ekziston!");

                var kf = new DbCore.DbKontabiliteti.clsKlientFurnitor();
                kf.mbushKlientFurnitorSipasKodit(kodi, idndermarje, idperdoruesi);
                if (kf.IdKlientFurnitor < 1)
                    throw new Exception("Ju nuk keni autorizime per kete klient/furnitor!");

                if (kodi != "" && !kf.AktivKF)
                    throw new Exception("Klient/Furnitori nuk është aktive!");

                IdKf = kf.IdKlientFurnitor;
                if (dk != null && dk != "null" && dk != "")
                {
                    DebiKredi = dk == "Debi" ? 1 : 2;
                }

                if (gjendja != null && gjendja != "null" && gjendja != "")
                {
                    GjendjaLlog = double.Parse(gjendja);
                }

                if (gjendjamon != null && gjendjamon != "null" && gjendjamon != "")
                {
                    GjendjaMonBaze = double.Parse(gjendjamon);
                }

                var llogari = new DbKontabiliteti.clsLlogari(llogkunder, idndermarje);
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

                IdTrupi = 0;
            }
            else
                IdKf = -1;
        }

        public clsTrupiMbylljeKF(DataRow rreshti)
        {
            MbushMbylljeKfTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne  e dokumentit, gjenerohet automatikisht nga databaza.
        /// </summary>
        public int IdTrupi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne  e Klient\Furnitorit per te cilin po behet azhornimi
        /// </summary>
        public int IdKf { get; set; }

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
        public int DebiKredi { get; set; }

        /// <summary>
        /// Kthen/Vendos vleften qe eshte gjeneruar si pasoje e azhornimit
        /// </summary>
        public double GjendjaLlog { get; set; }

        public double GjendjaMonBaze { get; set; }

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
        public int IdKoka { get; set; }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e azhornimit te KF-se nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushMbylljeKfTrupi(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    DebiKredi = !IsDBNull(dbDataRow["DEBIKREDI"])
                        ? ToInt32(dbDataRow["DEBIKREDI"])
                        : 0;
                    IdTrupi = !IsDBNull(dbDataRow["IDTRUPI"])
                        ? ToInt32(dbDataRow["IDTRUPI"])
                        : 0;
                    IdKf = !IsDBNull(dbDataRow["IDKF"])
                        ? ToInt32(dbDataRow["IDKF"])
                        : 0;
                    IdLlogariKp = !IsDBNull(dbDataRow["IDLLOGKUNDERPARTI"])
                        ? ToInt32(dbDataRow["IDLLOGKUNDERPARTI"])
                        : 0;
                    KodiKlientFurnitor = dbDataRow["KODKLIENTFURNITOR"].ToString();
                    NrLlogariKp = dbDataRow["NRLLOGARI"].ToString();
                    Pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    GjendjaLlog = !IsDBNull(dbDataRow["GJENDJALLOG"])
                        ? ToDouble(dbDataRow["GJENDJALLOG"])
                        : 0;
                    GjendjaMonBaze = !IsDBNull(dbDataRow["GJENDJAMONBAZE"])
                        ? ToDouble(dbDataRow["GJENDJAMONBAZE"])
                        : 0;
                    EmerKlientFurnitor = dbDataRow["EMERTIMIKF"].ToString();
                    Kursi = !IsDBNull(dbDataRow["KURSI"])
                        ? ToDouble(dbDataRow["KURSI"])
                        : 0;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se mbyllje te trupit te KF-se nga db-ja");
                }
            }
        }

        #endregion
    }
}
