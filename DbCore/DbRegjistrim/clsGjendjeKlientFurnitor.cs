using System;
using System.Data;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.DataBase;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    public class clsGjendjeKlientFurnitor
    {
        #region Konstruktoret

        public clsGjendjeKlientFurnitor()
        {
        }

        public clsGjendjeKlientFurnitor(DataRow rreshti)
        {
            MbushGjendjeKf(rreshti);
        }

        #endregion

        #region Properties

        public int IdGjendjeKf { get; set; }

        public int IdDok { get; set; }

        public string NrDok { get; set; }

        public DateTime DateDok { get; set; }

        public double VlMinus { get; set; }

        public double VlMinusMonedheBaze { get; set; }

        public double VlPlus { get; set; }

        public double VlPlusMonedheBaze { get; set; }

        public int NivelDok { get; set; }

        public int IdMonedhaDok { get; set; }

        public double KursiDok { get; set; }

        public DateTime DateRegj { get; set; }

        public int IdKlientGjendjeKf { get; set; }

        public int IdStatusGjendjeKf { get; set; }

        public DateTime DtKrijimi { get; private set; }

        public DateTime DtModifikimi { get; private set; }

        #endregion

        #region Metoda Publike
        
        public clsMesazh Ruaj(DbData dbdata)
        {
            var data = new clsDatabaseRegjistrim(dbdata);
            int idGjendja;
            var mesazhi = data.ruajGjendjeKF(out idGjendja, IdDok, NrDok, DateDok, VlMinus, VlPlus, NivelDok, IdMonedhaDok, KursiDok, DateRegj, VlMinusMonedheBaze, VlPlusMonedheBaze, IdKlientGjendjeKf);
            IdGjendjeKf = idGjendja;
            return mesazhi;
        }

        public clsMesazh Modifiko(DbData dbData)
        {
            var data = new clsDatabaseRegjistrim(dbData);
            return data.modifikoGjendjeKF(IdGjendjeKf, IdDok, NrDok, DateDok, VlMinus, VlPlus, NivelDok, IdMonedhaDok, KursiDok, DateRegj, VlMinusMonedheBaze, VlPlusMonedheBaze, IdKlientGjendjeKf, IdStatusGjendjeKf);
        }

        public static clsGjendjeKlientFurnitor KrijoGjendjeKlientFurnitor(int idKlientFurnitor, int idNivel, string nrDok, DateTime dateDok, DateTime dateRegj, clsTrupiFleteKontabel trupiFleteKontabel, string rreshtiKf)
        {
            var gjendje = new clsGjendjeKlientFurnitor
            {
                IdGjendjeKf = 0,
                IdDok = 0,
                NivelDok = idNivel,
                NrDok = nrDok,
                IdKlientGjendjeKf = idKlientFurnitor,
                DateDok = dateDok,
                DateRegj = dateRegj,
                IdMonedhaDok = trupiFleteKontabel.IdMonedha,
                KursiDok = trupiFleteKontabel.Kursi
            };


            if (rreshtiKf == "KL")
            {
                gjendje.VlPlus = trupiFleteKontabel.VleftaDebiTrupiFleteKontabel;
                gjendje.VlMinus = trupiFleteKontabel.VleftaKrediTrupiFleteKontabel;
                gjendje.VlPlusMonedheBaze = trupiFleteKontabel.VleftaDebiMonBazeTrupiFleteKontabel;
                gjendje.VlMinusMonedheBaze = trupiFleteKontabel.VleftaKrediMonBazeTrupiFleteKontabel;
            }
            else if (rreshtiKf == "FR")
            {
                gjendje.VlPlus = trupiFleteKontabel.VleftaKrediTrupiFleteKontabel;
                gjendje.VlMinus = trupiFleteKontabel.VleftaDebiTrupiFleteKontabel;
                gjendje.VlPlusMonedheBaze = trupiFleteKontabel.VleftaKrediMonBazeTrupiFleteKontabel;
                gjendje.VlMinusMonedheBaze = trupiFleteKontabel.VleftaDebiMonBazeTrupiFleteKontabel;
            }

            return gjendje;
        }

        public double[] MerrGjendje(string kodkf, int idndermarje, DateTime datedok, int iddok, int idnivel)
        {
            using (var data = new clsDatabaseRegjistrim())
                return data.merrGjendje(kodkf, datedok, idndermarje, iddok, idnivel);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush gjendjen e klient furnitorit nga databaza
        /// </summary>
        /// <param name="dbDataRowGjendjeKf">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushGjendjeKf(DataRow dbDataRowGjendjeKf)
        {
            if (dbDataRowGjendjeKf != null)
            {
                try
                {
                    IdGjendjeKf = int.Parse(dbDataRowGjendjeKf["IDGJENDJEKF"].ToString());
                    IdDok = int.Parse(dbDataRowGjendjeKf["IDODOKGJENDJEKF"].ToString());
                    NrDok = dbDataRowGjendjeKf["NRDOKGJENDJEKF"].ToString();
                    DateDok = DateTime.Parse(dbDataRowGjendjeKf["DATEDOKGJENDJEKF"].ToString());
                    VlMinus = double.Parse(dbDataRowGjendjeKf["VLMINUSGJENDJEKF"].ToString());
                    VlPlus = double.Parse(dbDataRowGjendjeKf["VLPLUSGJENDJEKF"].ToString());
                    NivelDok = int.Parse(dbDataRowGjendjeKf["IDNIVELIGJENDJEKF"].ToString());
                    IdMonedhaDok = int.Parse(dbDataRowGjendjeKf["MONEDHAGJENDJEKF"].ToString());
                    KursiDok = double.Parse(dbDataRowGjendjeKf["KURSIGJENDJEKF"].ToString());
                    DateRegj = DateTime.Parse(dbDataRowGjendjeKf["DTREGJGJENDJEKF"].ToString());
                    IdKlientGjendjeKf = int.Parse(dbDataRowGjendjeKf["IDKLIENTGJENDJEKF"].ToString());
                    IdStatusGjendjeKf = int.Parse(dbDataRowGjendjeKf["IDSTATUSDOK"].ToString());
                    VlMinusMonedheBaze = double.Parse(dbDataRowGjendjeKf["VLMINUSMONEDHEBAZE"].ToString());
                    VlPlusMonedheBaze = double.Parse(dbDataRowGjendjeKf["VLPLUSMONEDHBAZE"].ToString());
                    DtKrijimi = !IsDBNull(dbDataRowGjendjeKf["DTKRIJIMI"])
                        ? ToDateTime(dbDataRowGjendjeKf["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dbDataRowGjendjeKf["DTMODIFIKIMI"])
                        ? ToDateTime(dbDataRowGjendjeKf["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se gjendjes se klient furnitorit nga db-ja");
                }
            }
        }

        #endregion
    }
}