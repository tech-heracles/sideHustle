using System;
using System.Data;

namespace DbCore.DbCRM
{
    /// <summary>
    /// eshte nje klase model e cila ben map ne db tabelen TAKIMEPERKONTROLL
    /// </summary>
    public class clsTakimePerKontroll
    {

        public static clsTakimePerKontroll Krijo(IDataRecord rekord)
        {
            return new clsTakimePerKontroll
            {
                IdTakimPerKontroll = !Convert.IsDBNull(rekord["IDTAKIMPERKONTROLL"]) ? Convert.ToInt32(rekord["IDTAKIMPERKONTROLL"]) : 0,
                KoordinataFillimTakimi = !Convert.IsDBNull(rekord["KOORDINATAFILLIMTAKIMI"]) ? Convert.ToString(rekord["KOORDINATAFILLIMTAKIMI"]) : String.Empty,
                Perdorues = !Convert.IsDBNull(rekord["PERDORUES"]) ? Convert.ToString(rekord["PERDORUES"]) : string.Empty,
                KodiKlientit = !Convert.IsDBNull(rekord["KODIKLIENTIT"]) ? Convert.ToString(rekord["KODIKLIENTIT"]) : String.Empty,
                RegID = !Convert.IsDBNull(rekord["REGID"]) ? Convert.ToString(rekord["REGID"]) : String.Empty,
                Uuid = !Convert.IsDBNull(rekord["UUID"]) ? Convert.ToString(rekord["UUID"]) : String.Empty,
                DtKrijimi = !Convert.IsDBNull(rekord["DTKRIJIMI"]) ? Convert.ToDateTime(rekord["DTKRIJIMI"]) : (DateTime?)null,
                Lexuar = !Convert.IsDBNull(rekord["LEXUAR"]) ? Convert.ToBoolean(rekord["LEXUAR"]) : false,
                DtLeximi = !Convert.IsDBNull(rekord["DTLEXIMI"]) ? Convert.ToDateTime(rekord["DTLEXIMI"]) : (DateTime?)null,
                KodNdermarrje = !Convert.IsDBNull(rekord["KODNDERMARJE"]) ? Convert.ToString(rekord["KODNDERMARJE"]) : String.Empty,
                BrendaRrezes = !Convert.IsDBNull(rekord["BRENDARREZES"]) ? Convert.ToBoolean(rekord["BRENDARREZES"]) : false,
                IdPerdoruesi = !Convert.IsDBNull(rekord["IDPERDORUESI"]) ? Convert.ToInt32(rekord["IDPERDORUESI"]) : 0,
                IdAgjentShitje = !Convert.IsDBNull(rekord["IDAGJENTSHITJE"]) ? Convert.ToInt32(rekord["IDAGJENTSHITJE"]) : 0,
            };
        }

        private int idTakimPerKontroll;

        public int IdTakimPerKontroll { get { return idTakimPerKontroll; } set { idTakimPerKontroll = value; } }

        public string KoordinataFillimTakimi { get; set; }

        public string Perdorues { get; set; }

        public string KodiKlientit { get; set; }

        public string RegID { get; set; }

        public string Uuid { get; set; }

        public Nullable<DateTime> DtKrijimi { get; set; }

        public bool Lexuar { get; set; }

        public Nullable<DateTime> DtLeximi { get; set; }

        public string KodNdermarrje { get; set; }

        public bool BrendaRrezes { get; set; }

        public int IdAgjentShitje { get; set; }

        public int IdPerdoruesi { get; set; }

        public clsMesazh UpdateStatusLexuar()
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = false;
            try
            {
                dbCRM.beginTransaksion();

                sukses = dbCRM.UpdateStatusLexuarTakimePerKontroll(IdTakimPerKontroll);
                dbCRM.commitTransaksion();
                return new clsMesazh(true, "Statusi u update-ua me sukses!");
            }
            catch (Exception)
            {
                dbCRM.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate update-imit te statusit per nje takim");
            }
        }

        public clsMesazh Ruaj()
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            try
            {
                dbCRM.beginTransaksion();

                dbCRM.shtoTakimTeKontrolluar(out this.idTakimPerKontroll,Perdorues, KodNdermarrje, Lexuar, Uuid, RegID);


                dbCRM.commitTransaksion();
                return new clsMesazh(true, "Ruajtja u krye me sukses!");
            }
            catch (Exception)
            {
                dbCRM.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se nje takimi te kontrolluar!");
            }
        }
    }
}