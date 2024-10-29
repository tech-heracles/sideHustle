using System;
using System.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbAsete
{
	public abstract class clsDatabazeAseteAbstract : DbData
	{
		#region Konstruktore

		public clsDatabazeAseteAbstract() { }

		public clsDatabazeAseteAbstract(DbData db) : base(db) { }
		public clsDatabazeAseteAbstract(string connectionName) : base(connectionName)
		{

		}

		#endregion

		#region Krijues instance

		public static clsDatabazeAseteAbstract krijoInstance(enumObjekteAmortizimi objekti)
		{
			if (objekti == enumObjekteAmortizimi.REZERVA)
				return new clsDatabazeAseteRezerva();
			else
				return new clsDatabazeAsete();
		}
		public static clsDatabazeAseteAbstract krijoInstance(enumObjekteAmortizimi objekti, DbData dbData)
		{
			if (objekti == enumObjekteAmortizimi.REZERVA)
				return new clsDatabazeAseteRezerva(dbData);
			else
				return new clsDatabazeAsete(dbData);
		}

		#endregion

		#region Rezerva Norma Amortizimi

		public abstract clsMesazh ruajArtikulliNormaAmortizimi(out int idLidhjeArtikullLlojAmort, int idArtikulli, int idLlojAmortizimi, int idStandartAmortizimi, bool normeMagazine, double norme, DateTime dtaktivizimi);

		public abstract clsMesazh modifikimiArtikulliNormaAmortizimi(int idLidhjeArtikullLlojAmort, int idLlojAmortizimi, bool normeMagazine, double norme);

		internal abstract clsMesazh fshiArtikullNormaAmortizimi(int idLidhjeArtikullLlojAmort);

		internal abstract DataTable ktheArtikullNormaAmortizimiTeGjitha(int idArtikulli, int idndermarje, DateTime dtaktivizimi);

		internal abstract DataTable ktheArtikullNormaAmortizimiFillestare(int idndermarje, DateTime data);
		internal abstract DataTable ktheArtikullNormaAmortizimiSipasIdKodifikimit(int idkodifikimi, int idndermarje, DateTime data);
		internal abstract DataTable merrDataNdryshimiNormaAmortizimi(int idkoka);

		internal abstract DataRow ktheArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data);

		internal abstract string merrNormenDheMetodenAmortizimitSipasIdArtikullStandartit(int idArtikulli, int idStandarti, DateTime data);

		public abstract int ktheIDArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data);

		internal abstract int ktheIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data);

		internal abstract int ktheIDLlojAmortizimiNormaAmortizimiSipasID(int idArtikullLlojAmort);

		internal abstract bool ekzistonArtikullNormaAmortizimit(int idArtikulli, int idStandarti, DateTime data);

		internal abstract DataTable ktheArtikujNormaAmortizimiDTExport(int idnderm);

		internal abstract DataTable ktheArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi);
		internal abstract bool ktheBoolArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi);
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
		internal abstract clsMesazh ruajAmortizimiTrupi(out int idAmortizimiTrupi, int idAmortizimKoka, int nrRendor, int idArtikulli, int idArtikull_LlojAmortizimi, DateTime dateAmortizimi, DateTime dateMePareAmortizimi, DateTime dateMagazineInaktive, int idNjesiAdministrative,
			double normaAmortizimi, double amortizimiShtese, double amortizimiGjithsej, double amortizimiVjetor, double vleftaPlusMinus, double hdAmortizimGjithsej, double hdAmortizimVjetor, double vleftaGjendje, double diteAmortizimi, int idAQTSeriali, double vleftaShteseRivleresim);

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
		internal abstract clsMesazh modifikoAmortizimiTrupiPerRillogaritje(int idAmortizimiTrupi, int idAmortizimKoka, DateTime dateMePareAmortizimi, DateTime dateMagazineInaktive, int idNjesiAdministrative,
			double normaAmortizimi, double amortizimiShtese, double amortizimiGjithsej, double amortizimiVjetor, double vleftaPlusMinus, double hdAmortizimGjithsej, double hdAmortizimVjetor, double vleftaGjendje, double diteAmortizimi, double vleftaShteseRivleresim);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA nje DataTable me te gjithe trupin e kokes se dokumentit qe ne kerkojme.
		/// </summary>
		/// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
		/// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
		internal abstract DataTable ktheAmortizimTrupiSipasIdKokaAmortizimi(int idAmortizimKoka);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKAGrupSipasArtikullit nje DataTable me te gjithe trupin e kokes se dokumentit qe ne kerkojme sipas artikujve.
		/// </summary>
		/// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
		/// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
		internal abstract DataTable ktheAmortizimTrupiSipasIdKokaAmortizimiGrupSipasArtikullit(int idAmortizimKoka);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDKOKA_IDNJESI nje DataTable me te gjithe trupin e kokes se dokumentit ne magazinen qe ne kerkojme.
		/// </summary>
		/// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
		/// <param name="idNjesiAdministrative">(int) Id e njesi administrative ku po ndodh ndryshimi i statusit.</param>
		/// <returns>Kthen nje DataTable me trupin e nje koke te nje dokumenti amortizimi ne magazinen qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
		internal abstract DataTable ktheAmortizimTrupiSipasIdKokaAmortizimiIdNjesiAdministrative(int idAmortizimKoka, int idNjesiAdministrative);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT nje DataTable me te gjithe amortizimet e fundit per secilin serial ne ndermarrje.
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <returns>Kthen nje DataTable me trupin e amortizimit te fundit per cdo serial te magazines qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
		internal abstract DataTable ktheAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje);
		internal abstract DataTable ktheAmortizimTrupiAmortVeprimePas(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje);
		internal abstract DataTable ktheAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje, int idDok);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE nje DataTable me te gjithe trupat te mundur per rillogaritje ne varesi te dates vetem per artikujt qe kane serial.
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <returns>Kthen nje DataTable me trupin e amortizimit te per rillogaritje ne varesi te dates nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
		internal abstract DataTable ktheAmortizimTrupiPerRillogaritje(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASSTANDART_IDNDERMARRJE_ARTIKUJPASERIAL nje DataTable me te gjithe trupat te mundur per rillogaritje ne varesi te dates vetem per artikujt qe s'kane seriale.
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <returns>Kthen nje DataTable me trupin e amortizimit te per rillogaritje ne varesi te dates nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
		internal abstract DataTable ktheAmortizimTrupiPerRillogaritjeArtikujPaSerial(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_VEPRIMETEFUNDIT_TESERIALIT nje DataTable me te gjithe amortizimet e fundit per serialin specifik ne ndermarrje.
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <param name="idSeriali">(int) Id e serialit qe kerkojme veprimin paraardhes.</param>
		/// <returns>Kthen nje DataTable me trupin e amortizimit te fundit per serialin specifik qe kerkojme nga tabela T_ASETE_AMORTIZIMI_TRUPI.</returns>
		internal abstract DataRow ktheAmortizimTrupiAmortFunditSipasSerialit(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int idSeriali, int renditje);

		/// <summary>
		/// MODULI ASETE:
		/// Kthen nga databaza nepermjet procedures PRC_T_ASETE_AMORTIZIMI_TRUPI_SEL_SIPASIDSERIAL_DATEAMORT nje DataTable me rreshtat qe plotesojne kushtet.
		/// </summary>
		/// <param name="idSeriali">(int) Id e serialit te aqt-se qe po amortizohet.</param>
		/// <param name="dataAmortizimi">(DateTime) Data e amortizimit te rreshtit te trupit.</param>
		/// <param name="idStandartAmortizimi">(int) Id e standartit te amortizimit.</param>
		/// <returns>Kthen int numrin e rekordeve te gjetur nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.</returns>
		internal abstract int ktheNrAmortizimTrupiSipasIdSerialiDateAmortizimi(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi);

		internal abstract bool kaVeprimePasPerKeteSerial(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi);
		#endregion
	}
}
