using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAsete
{
    public class clsDatabazeAseteRezerva : clsDatabazeAseteAbstract
    {
        public clsDatabazeAseteRezerva() { }
        public clsDatabazeAseteRezerva(DbData db) : base(db) { }
        public clsDatabazeAseteRezerva(string connectionName) : base(connectionName)
        {

        }
        #region Rezerva Norma Amortizimi

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_LIDHJE_REZERVE_LLOJAMORT objektin e normave te amortizimit te artikullit nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_INSERT.
        /// </summary>
        /// <param name="idLidhjeArtikullLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idLlojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="idStandartAmortizimi">Id e standartit qe po i regjistrohet norma.</param>
        /// <param name="normeMagazine">(bool) Nese amortizimi do te varet nga norma ne magazine ose nga ajo artikullit.</param>
        /// <param name="norme">(float) Norma e amortizimit te artikullit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public override clsMesazh ruajArtikulliNormaAmortizimi(out int idLidhjeArtikullLlojAmort, int idArtikulli, int idLlojAmortizimi, int idStandartAmortizimi, bool normeMagazine, double norme, DateTime dtaktivizimi)
        {
            idLidhjeArtikullLlojAmort = -1;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID_REZERVA_LLOJAMORTIZIMI", idLidhjeArtikullLlojAmort, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJAMORTIZIMI", idLlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLLOJSTANDARTI", idStandartAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NORMEMAGAZINE", normeMagazine, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NORMA", norme, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DTAKTIVIZIMI", dtaktivizimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_INSERT");
            idLidhjeArtikullLlojAmort = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, "Ruatja perfundoi me sukses!");
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_LIDHJE_REZERVE_LLOJAMORT objektin e normave te amortizimit te artikullit nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_UPDATE.
        /// </summary>
        /// <param name="idLidhjeArtikullLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <param name="idLlojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="normeMagazine">(bool) Nese amortizimi do te varet nga norma ne magazine ose nga ajo artikullit.</param>
        /// <param name="norme">(float) Norma e amortizimit te artikullit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public override clsMesazh modifikimiArtikulliNormaAmortizimi(int idLidhjeArtikullLlojAmort, int idLlojAmortizimi, bool normeMagazine, double norme)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@ID_REZERVA_LLOJAMORTIZIMI", idLidhjeArtikullLlojAmort, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJAMORTIZIMI", idLlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NORMEMAGAZINE", normeMagazine, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NORMA", norme, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_UPDATE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e normave te amortizimit te artikullit ne tabelen T_ASETE_LIDHJE_REZERVE_LLOJAMORT nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_DELETE.
        /// </summary>
        /// <param name="idLidhjeArtikullLlojAmort">(int) Id automatike e normas se grupit dhe standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal override clsMesazh fshiArtikullNormaAmortizimi(int idLidhjeArtikullLlojAmort)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_REZERVA_LLOJAMORTIZIMI", idLidhjeArtikullLlojAmort, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_DELETE");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL nje DataTable me te gjitha normat e standarteve te artikullit qe kerkohet.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen nje DataTable me te gjitha normat e standarteve te artikullit nga tabela T_ASETE_LIDHJE_REZERVE_LLOJAMORT.</returns>
        internal override DataTable ktheArtikullNormaAmortizimiTeGjitha(int idArtikulli, int idndermarje, DateTime dtaktivizimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_ARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", dtaktivizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL");
            return ds.Tables[0];
        }
        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_METODEAMORTIZIMI_SEL_SIPASIDART_IDSTANDART_DATE 
        /// nje DataTable me normat e amortizimeve dhe metoden perkatese per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        ///  /// <param name="data">(int) data e amortizimit.</param>
        /// <returns>Kthen nje DataTable me normat e amortizimeve te artikullit nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.</returns>
        internal override string merrNormenDheMetodenAmortizimitSipasIdArtikullStandartit(int idArtikulli, int idStandarti, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_METODEAMORTIZIMI_SEL_SIPASIDART_IDSTANDART_DATE");
            if (ds == null)
                return null;
            return ds.Tables[0].Rows[0]["LLOJ_AMORTIZIMI"].ToString();
        }
        internal override DataTable ktheArtikullNormaAmortizimiSipasIdKodifikimit(int idkodifikimi, int idndermarje, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKODIFIKIMARTIKULLI", idkodifikimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SELSiKodifikimi");
            return ds.Tables[0];
        }
        internal override DataTable ktheArtikullNormaAmortizimiFillestare(int idndermarje, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SELFillestare");
            return ds.Tables[0];
        }
        internal override DataTable merrDataNdryshimiNormaAmortizimi(int idkoka)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            using (DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_merrGjitheDatatSipasIdKoka"))
            {
                return ds.Tables[0];
            }

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen nje DataRow me normat e amortizimeve te artikullit nga tabela T_ASETE_LIDHJE_REZERVE_LLOJAMORT.</returns>
        internal override DataRow ktheArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_ARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_DATE");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e automatike te normes se artikullit per standartin e kerkuar nga tabela T_ASETE_LIDHJE_REZERVE_LLOJAMORT.</returns>
        public override int ktheIDArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idArtikullNormaAmortizim;
            int.TryParse(ds.Tables[0].Rows[0]["ID_REZERVA_LLOJAMORTIZIMI"].ToString(), out idArtikullNormaAmortizim);
            return idArtikullNormaAmortizim;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e llojit te amortizimit per standartin dhe artikullin e kerkuar nga tabela T_ASETE_LIDHJE_REZERVE_LLOJAMORT.</returns>
        internal override int ktheIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID_ARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_DATE");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idLlojAmortizimi;
            int.TryParse(ds.Tables[0].Rows[0]["ID_REZERVA_LLOJAMORTIZIMI"].ToString(), out idLlojAmortizimi);
            return idLlojAmortizimi;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASID nje DataRow me normat e amortizimeve per id e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e llojit te amortizimit per id e kerkuar nga tabela T_ASETE_LIDHJE_REZERVE_LLOJAMORT.</returns>
        internal override int ktheIDLlojAmortizimiNormaAmortizimiSipasID(int idArtikullLlojAmort)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID_ARTIKULL_LLOJAMORTIZIM", idArtikullLlojAmort, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASID");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;

            int idLlojAmortizimi;
            int.TryParse(ds.Tables[0].Rows[0]["IDLLOJAMORTIZIMI"].ToString(), out idLlojAmortizimi);
            return idLlojAmortizimi;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART nje DataRow me normat e amortizimeve per standartin dhe artikullin e kerkuar.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen True nese ekziston njehere specifikimet per normen e artikullit dhe standartit dhe False ne te kundert.</returns>
        internal override bool ekzistonArtikullNormaAmortizimit(int idArtikulli, int idStandarti, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@data", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART");
            if (ds == null)
                return true;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        internal override DataTable ktheArtikujNormaAmortizimiDTExport(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idndermarje", idnderm, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SELDTExport");

            return ds.Tables[0];
        }

        internal override DataTable ktheArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEFILLIMI", dateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEMBARIMI", datePerfundimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_MIDISDATEVE");
            return ds.Tables[0];
        }
        internal override bool ktheBoolArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJSTANDARTI", idStandarti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATEFILLIMI", dateFillimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATEMBARIMI", datePerfundimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_LIDHJE_REZERVE_LLOJAMORT_SEL_SIPASIDART_IDSTANDART_MIDISDATEVE");
            return ds.Tables[0].Rows.Count > 1;
        }
        #endregion

        #region Amortizimi Trupi

        /// <summary>
        /// MODULI ASETE:
        /// Ruan ne tabelen T_ASETE_AMORTIZIMI_TRUPI objektin e trupit te amortizimit nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_INSERT.
        /// </summary>
        /// <param name="idAmortizimiTrupi">(int) Id automatike e amortizimit te trupit.</param>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <param name="nrRendor">(int) Nr rendor i rreshtit te trupit.</param>
        /// <param name="idArtikulli">(int) Id e artikullit qe po amortizohet.</param>
        /// <param name="idArtikull_LlojAmortizimi">(int) Id se per cfare lloj amortizimi eshte rreshti qe po amortizohet.</param>
        /// <param name="dateAmortizimi">(DateTime) Data e amortizimit te rreshtit te trupit.</param>
        /// <param name="dateMePareAmortizimi">(DateTime) Data e meparshme e amortizimit e serialit qe ndodhet te rreshti.</param>
        /// <param name="dateMagazineInaktive">(DateTime) Data e nderrimit te statusit te magazines per kete serial. Perdoret per te llogaritur 3 muajt ne nje magazine para se te filloj te amortizoje,</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po ndodh amortizimi.</param>
        /// <param name="normaAmortizimi">(float) Norma e amortizimit me te cilen po llogaritet rreshti i amortizimit.</param>
        /// <param name="amortizimiShtese">(float) Amortizimi shtese i llogaritur per ditet e pallogaritura te amortizimit.</param>
        /// <param name="amortizimiGjithsej">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare ne kete vit deri ne ditet aktuale.</param>
        /// <param name="amortizimiVjetor">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare deri ne ditet aktuale</param>
        /// <param name="vleftaPlusMinus">(float) Vlera e amortizimit shtese qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimGjithsej">(float) Vlera e amortizimit gjithsej qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimVjetor">(float) Vlera e amortizimit vjetor qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="vleftaGjendje">(float) Vlefta totale e blerjes se serialit qe po amortizojme.</param>
        /// <param name="diteAmortizimi">(float) Ditet e amortizimit aktual.</param>
        /// <param name="idAQTSeriali">(int) Id e serialit te aqt-se qe po amortizohet.</param>
        /// <returns>Kthen True nese ruajtja kryhet me sukses ose False ne te kundert.</returns>
        internal override clsMesazh ruajAmortizimiTrupi(out int idAmortizimiTrupi, int idAmortizimKoka, int nrRendor, int idArtikulli, int idArtikull_LlojAmortizimi, DateTime dateAmortizimi, DateTime dateMePareAmortizimi, DateTime dateMagazineInaktive, int idNjesiAdministrative,
            double normaAmortizimi, double amortizimiShtese, double amortizimiGjithsej, double amortizimiVjetor, double vleftaPlusMinus, double hdAmortizimGjithsej, double hdAmortizimVjetor, double vleftaGjendje, double diteAmortizimi, int idAQTSeriali, double vleftaShteseRivleresim)
        {
            idAmortizimiTrupi = 0;

            dbManager.Open();
            dbManager.CreateParameters(20);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI_REZERVA_TRUPI", idAmortizimiTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRRENDOR", nrRendor, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDARTIKULL_LLOJAMORT", idArtikull_LlojAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATE_AMORTIZIMI", dateAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DATE_MEPARE_AMORTIZIMI", dateMePareAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DATE_NDRYSHIM_STATUSMAGAZINE", dateMagazineInaktive, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NORMAAMORTIZIMI", normaAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@AMORTIZIMISHTESE", amortizimiShtese, ParameterDirection.Input);
            dbManager.AddParameters(11, "@AMORTIZIMIGJITHSEJ", amortizimiGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(12, "@AMORTIZIMIVJETOR", amortizimiVjetor, ParameterDirection.Input);
            dbManager.AddParameters(13, "@VLEFTAPLUSMINUS", vleftaPlusMinus, ParameterDirection.Input);
            dbManager.AddParameters(14, "@HD_AMORTGJITHSEJ", hdAmortizimGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(15, "@HD_AMORTVJETOR", hdAmortizimVjetor, ParameterDirection.Input);
            dbManager.AddParameters(16, "@VLEFTAGJENDJE", vleftaGjendje, ParameterDirection.Input);
            dbManager.AddParameters(17, "@DITEAMORTIZIMI", diteAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(18, "@IDSERIALI", idAQTSeriali, ParameterDirection.Input);
            dbManager.AddParameters(19, "@VLEFTASHTESERIVLERESIM", vleftaShteseRivleresim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_INSERT");
            idAmortizimiTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon ne tabelen T_ASETE_AMORTIZIMI_TRUPI objektin e trupit te amortizimit nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_MODIFIKIM.
        /// </summary>
        /// <param name="idAmortizimiTrupi">(int) Id automatike e amortizimit te trupit.</param>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <param name="dateMePareAmortizimi">(DateTime) Data e meparshme e amortizimit e serialit qe ndodhet te rreshti.</param>
        /// <param name="dateMagazineInaktive">(DateTime) Data e nderrimit te statusit te magazines per kete serial. Perdoret per te llogaritur 3 muajt ne nje magazine para se te filloj te amortizoje,</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku po ndodh amortizimi.</param>
        /// <param name="normaAmortizimi">(float) Norma e amortizimit me te cilen po llogaritet rreshti i amortizimit.</param>
        /// <param name="amortizimiShtese">(float) Amortizimi shtese i llogaritur per ditet e pallogaritura te amortizimit.</param>
        /// <param name="amortizimiGjithsej">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare ne kete vit deri ne ditet aktuale.</param>
        /// <param name="amortizimiVjetor">(float) Amortizimi gjithsej qe kur eshte llogaritur per here te pare deri ne ditet aktuale</param>
        /// <param name="vleftaPlusMinus">(float) Vlera e amortizimit shtese qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimGjithsej">(float) Vlera e amortizimit gjithsej qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="hdAmortizimVjetor">(float) Vlera e amortizimit vjetor qe i shtohet ose i hiqet rreshtit gjate levizjeve nga magazina aktuale.</param>
        /// <param name="vleftaGjendje">(float) Vlefta totale e blerjes se serialit qe po amortizojme.</param>
        /// <param name="diteAmortizimi">(float) Ditet e amortizimit aktual.</param>
        /// <returns>Kthen True nese modifikimi kryhet me sukses ose False ne te kundert.</returns>
        internal override clsMesazh modifikoAmortizimiTrupiPerRillogaritje(int idAmortizimiTrupi, int idAmortizimKoka, DateTime dateMePareAmortizimi, DateTime dateMagazineInaktive, int idNjesiAdministrative,
            double normaAmortizimi, double amortizimiShtese, double amortizimiGjithsej, double amortizimiVjetor, double vleftaPlusMinus, double hdAmortizimGjithsej, double hdAmortizimVjetor, double vleftaGjendje, double diteAmortizimi, double vleftaShteseRivleresim)
        {
            dbManager.Open();
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@ID_AMORTIZIMI_REZERVA_TRUPI", idAmortizimiTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATE_MEPARE_AMORTIZIMI", dateMePareAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATE_NDRYSHIM_STATUSMAGAZINE", dateMagazineInaktive, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NORMAAMORTIZIMI", normaAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@AMORTIZIMISHTESE", amortizimiShtese, ParameterDirection.Input);
            dbManager.AddParameters(7, "@AMORTIZIMIGJITHSEJ", amortizimiGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(8, "@AMORTIZIMIVJETOR", amortizimiVjetor, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VLEFTAPLUSMINUS", vleftaPlusMinus, ParameterDirection.Input);
            dbManager.AddParameters(10, "@HD_AMORTGJITHSEJ", hdAmortizimGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(11, "@HD_AMORTVJETOR", hdAmortizimVjetor, ParameterDirection.Input);
            dbManager.AddParameters(12, "@VLEFTAGJENDJE", vleftaGjendje, ParameterDirection.Input);
            dbManager.AddParameters(13, "@DITEAMORTIZIMI", diteAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(14, "@VLEFTASHTESERIVLERESIM", vleftaShteseRivleresim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_MODIFIKIM");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA nje DataTable me te gjithe trupin e kokes se dokumentit qe ne kerkojme.
        /// </summary>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiSipasIdKokaAmortizimi(int idAmortizimKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_SIPASIDKOKA");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKAGrupSipasArtikullit nje DataTable me te gjithe trupin e kokes se dokumentit qe ne kerkojme sipas artikujve.
        /// </summary>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiSipasIdKokaAmortizimiGrupSipasArtikullit(int idAmortizimKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_SIPASIDKOKAGrupSipasArtikullit");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA_IDNJESI nje DataTable me te gjithe trupin e kokes se dokumentit ne magazinen qe ne kerkojme.
        /// </summary>
        /// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative ku po ndodh ndryshimi i statusit.</param>
        /// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi ne magazinen qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiSipasIdKokaAmortizimiIdNjesiAdministrative(int idAmortizimKoka, int idNjesiAdministrative)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAMORTIZIMIKOKA", idAmortizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNJESIADMINISTRATIVE", idNjesiAdministrative, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_SIPASIDKOKA_IDNJESI");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT nje DataTable me te gjithe amortizimet e fundit per secilin serial ne ndermarrje.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te fundit per cdo serial te magazines qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_RENDITJE", nrRenditje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_VEPRIMETEFUNDIT");
            return ds.Tables[0];

        }
     internal override DataTable ktheAmortizimTrupiAmortVeprimePas(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_RENDITJE", nrRenditje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_VEPRIMEPas");
            return ds.Tables[0];

        }
        internal override DataTable ktheAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje, int idDok)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NR_RENDITJE", nrRenditje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDDOK", idDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_VEPRIMETEFUNDIT_idDok");
            return ds.Tables[0];

        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE nje DataTable me te gjithe trupat te mundur per rillogaritje ne varesi te dates vetem per artikujt qe kane serial.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te per rillogaritje ne varesi te dates nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiPerRillogaritje(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataFillimRillogaritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE").Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE_ARTIKUJPASERIAL nje DataTable me te gjithe trupat te mundur per rillogaritje ne varesi te dates vetem per artikujt qe s'kane seriale.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te per rillogaritje ne varesi te dates nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataTable ktheAmortizimTrupiPerRillogaritjeArtikujPaSerial(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataFillimRillogaritje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE_ARTIKUJPASERIAL");
            return ds.Tables[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT_TESERIALIT nje DataTable me te gjithe amortizimet e fundit per serialin specifik ne ndermarrje.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <param name="idSeriali">(int) Id e serialit qe kerkojme veprimin paraardhes.</param>
        /// <returns>Kthen nje DataTable me trupin e amortizimit te fundit per serialin specifik qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
        internal override DataRow ktheAmortizimTrupiAmortFunditSipasSerialit(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int idSeriali, int renditje)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataMaksimaleZgjedhje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJSTANDARTI", idLlojStandarti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSERIALI", idSeriali, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NR_RENDITJE", renditje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_VEPRIMETEFUNDIT_TESERIALIT");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDSERIAL_DATEAMORT nje DataTable me rreshtat qe plotesojne kushtet.
        /// </summary>
        /// <param name="idSeriali">(int) Id e serialit te aqt-se qe po amortizohet.</param>
        /// <param name="dataAmortizimi">(DateTime) Data e amortizimit te rreshtit te trupit.</param>
        /// <param name="idStandartAmortizimi">(int) Id e standartit te amortizimit.</param>
        /// <returns>Kthen int numrin e rekordeve te gjetur nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
        internal override int ktheNrAmortizimTrupiSipasIdSerialiDateAmortizimi(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSeriali, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTANDARTAMORTIZIMI", idStandartAmortizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_SEL_SIPASIDSERIAL_DATEAMORT");
            if (ds == null)
                return 0;
            return ds.Tables[0].Rows.Count;

        }

        internal override bool kaVeprimePasPerKeteSerial(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSERIALI", idSeriali, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_AMORTIZIMI", dataAmortizimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSTANDARTAMORTIZIMI", idStandartAmortizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_ASETE_AMORTIZIMI_REZERVA_TRUPI_kaAmortizimePas");
            if (ds == null)
                return false;
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;


        }

        #endregion

    }
}