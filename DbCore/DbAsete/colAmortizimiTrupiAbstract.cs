using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using EO.Web;

namespace DbCore.DbAsete
{
	/// <summary>
	/// MODULI ASETE:
	/// Mban nje list objektesh clsAmortizimiTrupi (objekte per ruajtjen e trupave te dokumentave te amortizimit) ne modulin e Aseteve.
	/// Te dhenat merret nga tabela T_ASETE_AMORTIZIMI_TRUPI.
	/// </summary>
	public abstract class colAmortizimiTrupiAbstract : List<clsAmortizimiTrupiAbstract>
	{
		public virtual enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ABSTRACT;

		#region Konstruktore

		/// <summary>
		/// MODULI ASETE:
		/// Krijon nje list objektesh bosh te klases clsAmortizimiTrupi per trupat e dokumentave te amortizimit.
		/// </summary>
		public colAmortizimiTrupiAbstract()
		{
		}

		#endregion

		#region Krijues instance

		public static colAmortizimiTrupiAbstract krijoInstance(enumObjekteAmortizimi objektiKod)
		{
			if (objektiKod == enumObjekteAmortizimi.REZERVA)
				return new colAmortizimiTrupiRezerva();
			else
				return new colAmortizimiTrupi();
		}

		#endregion

		#region Metoda Publike

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe koleksionin e artikujve nga trupi.
		/// </summary>
		/// <returns>Kthen objektin DbInventari.colArtikujt nga trupi.</returns>
		public DbInventari.colArtikujt ktheColArtikuj()
		{
			DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
			foreach (clsAmortizimiTrupiAbstract trupMag in this)
			{
				colArt.Add(new DbInventari.clsArtikulli(trupMag.IdArtikulli));
			}
			return colArt;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe koleksionin e magazinave sipas perdoruesit nga trupi.
		/// </summary>
		/// <param name="idPerdorues">(int) Id e perdoruesit.</param>
		/// <returns>Kthen objektin colNjesiAdministrative nga trupi.</returns>
		public colNjesiAdministrative ktheColMag(int idPerdorues)
		{
			colNjesiAdministrative colMag = new colNjesiAdministrative();
			foreach (clsAmortizimiTrupiAbstract trupMag in this)
			{
				clsNjesiAdministrative mag = new clsNjesiAdministrative(trupMag.IdNjesiAdministrative);
				colMag.Add(mag);
			}
			return colMag;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe koleksionin e serialeve nga trupi.
		/// </summary>
		/// <returns>Kthen objektin colAQTSeriale nga trupi.</returns>
		public colAQTSeriale ktheColSeriale(int idndermarje)
		{
			colAQTSeriale col = new colAQTSeriale();
			foreach (clsAmortizimiTrupiAbstract trupMag in this)
			{
				clsAQTSeriale serial = new clsAQTSeriale();
				if (trupMag.IdAQTSeriali == 0)
				{
					// colAQTSeriale seriale = new colAQTSeriale();
					// seriale.merrAQTSerialSipasIDArtikulliIDMagazine(trupMag.IdArtikulli, trupMag.IdNjesiAdministrative, idndermarje);
					// serial.AmortizimiFillestar = seriale.Sum(x => x.AmortizimiFillestar);
					serial.AqtSerialKod = "";
				}
				else serial.merrAQTSerialSipasID(trupMag.IdAQTSeriali);
				col.Add(serial);
			}
			return col;
		}
		public colAmortizimiFillestar ktheColAmortizimFillestar(int idndermarje, int idllojstandarti, DateTime data)
		{
			colAmortizimiFillestar col = new colAmortizimiFillestar();
			foreach (clsAmortizimiTrupiAbstract trupMag in this)
			{
				clsAmortizimiFillestar serial = new clsAmortizimiFillestar();
				if (trupMag.IdAQTSeriali == 0)
				{
					colAQTSeriale seriale = new colAQTSeriale();
					seriale.merrAQTSerialSipasIDArtikulliIDMagazine(trupMag.IdArtikulli, trupMag.IdNjesiAdministrative, idndermarje, data);

					foreach (clsAQTSeriale ser in seriale)
					{
						clsAmortizimiFillestar amort = new clsAmortizimiFillestar();
						amort.merrAmortizimFillestarSipasSerialitDheStandartit(ser.IdAQTSerial, idllojstandarti, trupMag.IdAmortizimKoka);
						serial.AmortizimiFillestar += amort.AmortizimiFillestar;
					}

				}
				else

					serial.merrAmortizimFillestarSipasSerialitDheStandartit(trupMag.IdAQTSeriali, idllojstandarti, trupMag.IdAmortizimKoka);
				col.Add(serial);
			}
			return col;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe trupin e id se kokes se dokumentit te amortizimit.
		/// </summary>
		/// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
		/// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se trupit te dokumentit te amortizimit ose False ne te kundert.</returns>
		public bool merrAmortizimTrupiSipasIdKokaAmortizimi(int idAmortizimKoka)
		{
			clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
			bool pergjigja = mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiSipasIdKokaAmortizimi(idAmortizimKoka));
			moduliAsete.Dispose();
			return pergjigja;
		}

		public bool merrAmortizimTrupiSipasIdKokaAmortizimi(int idAmortizimKoka, clsDatabazeAsete moduliAsete)
		{

			bool pergjigja = mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiSipasIdKokaAmortizimi(idAmortizimKoka));

			return pergjigja;
		}

		/// <summary>
		/// perdoret per regjistrimet permbledhese qe jane ne nivel artikulli dhe jo seriali
		/// </summary>
		/// <param name="idAmortizimKoka"></param>
		/// <returns></returns>
		public bool ktheAmortizimTrupiSipasIdKokaAmortizimiGrupSipasArtikullit(int idAmortizimKoka)
		{
			clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
			bool pergjigja = mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiSipasIdKokaAmortizimiGrupSipasArtikullit(idAmortizimKoka));
			moduliAsete.Dispose();
			return pergjigja;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe trupin e id se kokes se dokumentit te amortizimit.
		/// </summary>
		/// <param name="idAmortizimKoka">(int) Id automatike e kokes qe po gjeneron kete trup.</param>
		/// <param name="idNjesiAdministrative">(int) Id e njesi administrative ku po ndodh ndryshimi i statusit.</param>
		/// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se trupit te dokumentit te amortizimit ose False ne te kundert.</returns>
		//public bool merrAmortizimTrupiSipasIdKokaAmortizimiIdNjesiAdministrative(int idAmortizimKoka, int idNjesiAdministrative)
		//{
		//    clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
		//    bool pergjigja = mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiSipasIdKokaAmortizimiIdNjesiAdministrative(idAmortizimKoka, idNjesiAdministrative));
		//    moduliAsete.Dispose();
		//    return pergjigja;
		//}

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe trupin e amortizimit sipas cdo seriali ne ndermarrje
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <param name="nrRenditje">(int) Numri renditjes.</param>
		/// <param name="idDok">(int) Id e dokumentit te magazines.</param>
		/// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se amortizimeve te fundit ose False ne te kundert.</returns>
		public bool merrAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje, int idDok)
		{
			clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
			return mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiAmortFunditSipasRenditjes(idNdermarrje, dataMaksimaleZgjedhje, idLlojStandarti, nrRenditje, idDok));
		}

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe trupin e amortizimit sipas cdo seriali ne ndermarrje
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <param name="nrRenditje">(int) Numri renditjes.</param>
		/// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se amortizimeve te fundit ose False ne te kundert.</returns>
		public bool merrAmortizimTrupiAmortFunditSipasRenditjes(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje)
		{
			clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
			return mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiAmortFunditSipasRenditjes(idNdermarrje, dataMaksimaleZgjedhje, idLlojStandarti, nrRenditje));
		}

		public DataTable merrAmortizimTrupiAmortFunditSipasRenditjesDT(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje)
		{
			clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
			return moduliAsete.ktheAmortizimTrupiAmortFunditSipasRenditjes(idNdermarrje, dataMaksimaleZgjedhje, idLlojStandarti, nrRenditje);
		}
		public bool ktheAmortizimTrupiAmortVeprimePas(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int nrRenditje)
		{
			clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
			return mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiAmortVeprimePas(idNdermarrje, dataMaksimaleZgjedhje, idLlojStandarti, nrRenditje));
		}
		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe trupin e amortizimit sipas cdo seriali ne ndermarrje vetem per artikujt me serial
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se amortizimeve te fundit ose False ne te kundert.</returns>
		public bool merrAmortizimTrupiPerRillogaritje(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti)
		{
			using (var moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod))
			{
				return mbushAmortizimiTrupiList(moduliAsete.ktheAmortizimTrupiPerRillogaritje(idNdermarrje, dataFillimRillogaritje, idLlojStandarti));
			}
		}

		/// <summary>
		/// MODULI ASETE:
		/// Merr te gjithe trupin e amortizimit sipas cdo seriali ne ndermarrje vetem per artikujt pa serial
		/// </summary>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
		/// <param name="dataFillimRillogaritje">(DateTime) Data minimale nga fillon rillogaritja.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
		/// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se amortizimeve te fundit ose False ne te kundert.</returns>
		public bool merrAmortizimTrupiPerRillogaritjeArtikujPaSerial(int idNdermarrje, DateTime dataFillimRillogaritje, int idLlojStandarti)
		{
			using (var moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod))
			{
				bool pergjigja = mbushAmortizimiTrupiListPerRillogaritjeArtikujPaSerial(moduliAsete.ktheAmortizimTrupiPerRillogaritjeArtikujPaSerial(idNdermarrje, dataFillimRillogaritje, idLlojStandarti));
				return pergjigja;
			}
		}

		#region Krijimi i trupit te dokumentit te amortizimit per raste te ndryshme

		/// <summary>
		/// MODULI ASETE:
		/// Krijon trupin e dokumentit te amortizimit nga dokumenti i blerjes.
		/// </summary>
		/// <param name="seriale">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
		/// <param name="dataAmortizimi">(DateTime) Data e ndryshimit te statusit te ri.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		public bool krijoTrupiDokAmortizimiNgaVeprimeMagazineBlerje(colSerialetMagazine seriale, DateTime dataAmortizimi, int idLlojStandarti)
		{
			//Llogarit amortizimin per secilin serial te trupit.
			bool pergjigja = llogaritAmortizimiNgaVeprimeMagazineBlerje(seriale, dataAmortizimi, idLlojStandarti);
			return pergjigja;
		}

		#region Krijimi i dokumetave per transferim magazine

		/// <summary>
		/// MODULI ASETE:
		/// Krijon trupin e dokumentit te amortizimit nga dokumenti i daljes.
		/// </summary>
		/// <param name="trupiPerHyrje">(colAmortizimiTrupi) Trupi i ri qe krijohet per dokumentat e hyrjes.</param>
		/// <param name="seriale">(colSerialetMagazine) Lista e serialeve qe po behen hyrje me kete dokument.</param>
		/// <param name="serialeTeNdashme">(colHistorikAQTSeriale) Lista e serialeve te rinj te gjeneruar nga ndarja e serialit prind.</param>
		/// <param name="dataAmortizimi">(DateTime) Data e ndryshimit te statusit te ri.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes qe po kryhet veprimi.</param>
		/// <param name="llojDokumentiAktual">(int) Id e llojit te dokumentit qe po kryhet. Nevojitet per te llogaritur +1 ne amortizim nese eshte nevoja.</param>
		/// <param name="mosllogaritAmortizimShtese"></param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		public clsMesazh krijoTrupiDokAmortizimiNgaVeprimeMagazineDaljaPerTransferim(ref colAmortizimiTrupiAbstract trupiPerHyrje, colSerialetMagazine seriale, colHistorikAQTSeriale serialeTeNdashme, DateTime dataAmortizimi, int idLlojStandarti, int idNdermarrje, int llojDokumentiAktual, bool mosllogaritAmortizimShtese, int nrRenditje, int idDokMag, out string mesazhmevonshem)
		{
			mesazhmevonshem = "";
			clsMesazh pergjigja = new clsMesazh(true);
			//Marrja e gjithe serialeve qe llogaritur me pare.
			colAmortizimiTrupiAbstract teGjitheSerialetLlogariturMePare = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			pergjigja.Status = teGjitheSerialetLlogariturMePare.merrAmortizimTrupiAmortFunditSipasRenditjes(idNdermarrje, dataAmortizimi, idLlojStandarti, nrRenditje, idDokMag);
			if (!pergjigja.Status)
				return pergjigja;
			colAmortizimiTrupiAbstract teGjitheSerialetLlogariturMePas = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			pergjigja.Status = teGjitheSerialetLlogariturMePas.ktheAmortizimTrupiAmortVeprimePas(idNdermarrje, dataAmortizimi, idLlojStandarti, nrRenditje);
			if (!pergjigja.Status)
				return pergjigja;

			//Llogarit amortizimin per secilin serial te trupit.
			return llogaritAmortizimiNgaVeprimeMagazineDalja(teGjitheSerialetLlogariturMePare, ref trupiPerHyrje, seriale, dataAmortizimi, idLlojStandarti, idNdermarrje, llojDokumentiAktual, mosllogaritAmortizimShtese, teGjitheSerialetLlogariturMePas, out mesazhmevonshem);
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon trupin e dokumentit te amortizimit nga dokumenti i daljes.
		/// </summary>
		/// <param name="amortizimiKrijuarNgaDalja">(clsAmortizimiKoka) Dokumenti i amortizimit i krijuar per hyrjen ne magazine.</param>
		/// <param name="idNjesiAdministrative">(int) Id e njesise administrative per te cilen do llogaritet norma dhe vlerat e trupit te amortizimit.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		public bool krijoTrupiDokAmortizimiNgaVeprimeMagazineHyrjePerTransferim(clsAmortizimiKoka amortizimiKrijuarNgaDalja, int idNjesiAdministrative, colHistorikAQTSeriale serialetendashem, clsKokaMagazina maghyrese)
		{
			DateTime dateNdryshimStatusMagazine;
			//int i = 0;
			int indexArtSerialeTeNdashme = 0; //mban indeksin per artikujt me seriale te ndashme
			int magazinatransf = 0;
			colAmortizimiTrupiAbstract amortizimDalje;
			if (this.objektiKod == enumObjekteAmortizimi.ASETE)
				amortizimDalje = amortizimiKrijuarNgaDalja.ColTrupi;
			else amortizimDalje = amortizimiKrijuarNgaDalja.ColTrupiRezerva;
			foreach (clsAmortizimiTrupiAbstract amortizimiTrupPerDalje in amortizimDalje)
			{
				DbInventari.clsArtikulli art = amortizimiTrupPerDalje.Artikull;

				clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(art.Kodifikimi1Artikulli), amortizimiKrijuarNgaDalja.IdLlojStandarti, amortizimiKrijuarNgaDalja.IdNdermarrje);

				if (karakteristikaStandartit == null)
					return false;

				///marrim magazinen ne trupin e dokumentit te trasferimit. nqs artikulli eshte me serial kemi nga nje rresht amortizimi dalje per cdo serial pra sa sasia e rreshtit te dokumentit te magazines. nqs artikulli eshte me seriale te ndashem kemi vetem nje rresht per cdo artikull.
				if (!art.MeSerial)
					indexArtSerialeTeNdashme++;
				magazinatransf = maghyrese.OcolTrupiMagazina[amortizimiTrupPerDalje.NrRendor].IdMag;

				//Kontrolli nese magazina eshte inaktive, duhet per te update DateNdryshimStatusMagazine me daten e hyrjes ne kete magazine inaktive.
				if (clsStatusMagazine_Asete.INAKTIVE == clsStatusMagazine_Asete.merrEmertimStatusMagazinesTeNdermarrjesIDStatus(clsHistorikStatusMagazine.merrIDStatusMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(magazinatransf), amortizimiKrijuarNgaDalja.IdNdermarrje))
					dateNdryshimStatusMagazine = karakteristikaStandartit.konvertoDatenSipasKerkesesKonfigurimit(amortizimiTrupPerDalje.DateAmortizimi, false, true);
				else
					dateNdryshimStatusMagazine = amortizimiTrupPerDalje.DateNdryshimStatusMagazine;

				clsAmortizimiTrupiAbstract amortizimiTrupPerHyrje = clsAmortizimiTrupiAbstract.krijoInstance(amortizimiTrupPerDalje.NrRendor, amortizimiTrupPerDalje.IdArtikulli, amortizimiTrupPerDalje.Artikull.PershkrimArtikulli, amortizimiTrupPerDalje.IdArtikull_LlojAmortizimi, amortizimiTrupPerDalje.DateAmortizimi, amortizimiTrupPerDalje.DateAmortizimi, dateNdryshimStatusMagazine, magazinatransf, art.MeSerial ? amortizimiTrupPerDalje.IdAQTSeriali : serialetendashem[indexArtSerialeTeNdashme - 1].IdAQTSeriale, amortizimiTrupPerDalje.Serial, amortizimiTrupPerDalje.Artikull, -amortizimiTrupPerDalje.VleftaShteseRivleresim, this.objektiKod);


				amortizimiTrupPerHyrje.VleftaPlusMinus = -amortizimiTrupPerDalje.VleftaPlusMinus;
				amortizimiTrupPerHyrje.HdAmortizimGjithsej = -amortizimiTrupPerDalje.HdAmortizimGjithsej;
				amortizimiTrupPerHyrje.HdAmortizimVjetor = -amortizimiTrupPerDalje.HdAmortizimVjetor;
				amortizimiTrupPerHyrje.NormaAmortizimi = llogaritNormenAmortizimit(amortizimiTrupPerDalje.IdArtikulli, amortizimiKrijuarNgaDalja.IdLlojStandarti, magazinatransf, amortizimiTrupPerDalje.DateAmortizimi, this.objektiKod, amortizimiTrupPerDalje.Artikull.KodArtikulli);

				//i++;
				Add(amortizimiTrupPerHyrje);
			}
			return true;
		}

		#endregion

		/// <summary>
		/// MODULI ASETE:
		/// Krijon trupin e dokumentit te amortizimit nga ambjenti i regjistrimit te amortizimit dhe nga ndryshimi i amortizimit.
		/// Metode qe perdoren nga metodat e : NDRYSHIMI I STATUSIT TE MAGAZINES, AMORTIZIMI NORMAL
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="nrreshti">(int) Merr rreshtin qe ndodhet aktualisht procesi.</param>
		/// <param name="idart">(List) Id e artikujve ne nje list.</param>
		/// <param name="e">(EO.Web.ProgressTaskEventArgs) Merr progresin e progres barit.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		public clsMesazh krijoTrupiDokAmortizimiPerLlogaritje(clsAmortizimiKoka kokaEAmortizimit, int idhistorikufundit, bool ndryshimgjendjemagazine, out int nrreshti, int nrRenditje, List<object> idart, EO.Web.ProgressTaskEventArgs e, out string mesazhmevonshem)
		{
			mesazhmevonshem = "";
			nrreshti = 0;
			clsMesazh pergjigja = new clsMesazh(true);
			//Marrja e gjithe serialeve qe llogaritur me pare.
			colAmortizimiTrupiAbstract teGjitheSerialetLlogariturMePare = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			pergjigja.Status = teGjitheSerialetLlogariturMePare.merrAmortizimTrupiAmortFunditSipasRenditjes(kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.DateDokumenti, kokaEAmortizimit.IdLlojStandarti, nrRenditje);
			if (!pergjigja.Status)
				return pergjigja;
			colAmortizimiTrupiAbstract teGjitheSerialetLlogariturMePas = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			pergjigja.Status = teGjitheSerialetLlogariturMePas.ktheAmortizimTrupiAmortVeprimePas(kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.DateDokumenti, kokaEAmortizimit.IdLlojStandarti, nrRenditje);
			if (!pergjigja.Status)
				return pergjigja;
			colAmortizimiTrupiAbstract amortizimetTrupi = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			colAmortizimiTrupiAbstract amortizimetTrupiPas = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			if (idart.Count > 0)
			{
				List<clsAmortizimiTrupiAbstract> amortizimetTrupiri = teGjitheSerialetLlogariturMePare.FindAll(p => idart.Any(p2 => int.Parse(p2.ToString()) == p.IdArtikulli));
				teGjitheSerialetLlogariturMePare.Clear();
				teGjitheSerialetLlogariturMePare.AddRange(amortizimetTrupiri);
				List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = teGjitheSerialetLlogariturMePas.FindAll(p => idart.Any(p2 => int.Parse(p2.ToString()) == p.IdArtikulli));
				teGjitheSerialetLlogariturMePas.Clear();
				teGjitheSerialetLlogariturMePas.AddRange(amortizimetTrupipas);
			}

			if (kokaEAmortizimit.IdNjesiAdministrative != 0)
			{
				//Marrja e nderprerjse se ketyre dy listave (colAmortizimiTrupi, colSerialetMagazine) dhe konvertimi ne list objektesh colAmortizimiTrupi.
				List<clsAmortizimiTrupiAbstract> amortizimetTrupiri = teGjitheSerialetLlogariturMePare.FindAll(x => x.IdNjesiAdministrative == kokaEAmortizimit.IdNjesiAdministrative);
				amortizimetTrupi.AddRange(amortizimetTrupiri);
				List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = teGjitheSerialetLlogariturMePas.FindAll(x => x.IdNjesiAdministrative == kokaEAmortizimit.IdNjesiAdministrative);
				amortizimetTrupiPas.AddRange(amortizimetTrupipas);
			}
			else
			{
				amortizimetTrupi = teGjitheSerialetLlogariturMePare;
				amortizimetTrupiPas = teGjitheSerialetLlogariturMePas;
			}

			//Llogarit amortizimin per secilin serial te trupit.
			pergjigja.Status = llogaritAmortizimiNgaAmbjentiTePergjithshme(kokaEAmortizimit, amortizimetTrupi, idhistorikufundit, ndryshimgjendjemagazine, out nrreshti, e, amortizimetTrupiPas, out mesazhmevonshem);
			return pergjigja;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon trupin e dokumentit te amortizimit fillestar ne transaksion.
		/// Metode qe perdoren nga metodat e : AMORTIZIMIT FILLESTAR
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		public clsMesazh krijoTrupiDokAmortizimiPerAmortizimFillestar(clsAmortizimiKoka kokaEAmortizimit, colAmortizimiFillestar seriale, List<double> amortizimifillestar, out string mesazhmevonshem)
		{
			//Llogarit amortizimin per secilin rresht te trupit.
			clsMesazh pergjigja = llogaritAmortizimiAmortizimFillestar(kokaEAmortizimit, seriale, amortizimifillestar, out mesazhmevonshem);
			return pergjigja;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon trupin e dokumentit te amortizimit per rivleresimin.
		/// Metode qe perdoren nga metodat e : RIVLERESIMI
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		public clsMesazh krijoTrupiDokAmortizimiPerRivleresim(int nrRenditje, clsAmortizimiKoka kokaEAmortizimit, bool rivlersimXStandart, out string mesazhmevonshem)
		{
			mesazhmevonshem = "";
			//Marrja e gjithe serialeve qe llogaritur me pare.
			colAmortizimiTrupiAbstract teGjitheSerialetLlogariturMePare = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			clsMesazh pergjigja = new clsMesazh()
			{
				Status = teGjitheSerialetLlogariturMePare.merrAmortizimTrupiAmortFunditSipasRenditjes(kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.DateDokumenti, kokaEAmortizimit.IdLlojStandarti, nrRenditje)
			};
			if (!pergjigja.Status)
				return pergjigja;
			colAmortizimiTrupiAbstract teGjitheSerialetLlogariturMePas = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			pergjigja.Status = teGjitheSerialetLlogariturMePas.ktheAmortizimTrupiAmortVeprimePas(kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.DateDokumenti, kokaEAmortizimit.IdLlojStandarti, nrRenditje);
			if (!pergjigja.Status)
				return pergjigja;
			//Llogarit amortizimin per secilin rresht te trupit.
			pergjigja = llogaritAmortizimiRivleresim(kokaEAmortizimit, teGjitheSerialetLlogariturMePare, rivlersimXStandart, teGjitheSerialetLlogariturMePas, out mesazhmevonshem);
			return pergjigja;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon trupin e dokumentit te amortizimit nga shperndarja e shpenzimeve.
		/// Metode qe perdoren nga metodat e : SHPERNDARJE SHPENZIME
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="trupiPerKetedokmag">trupat qe jane perdorur per kete dokument magazine per te mare id dokumentit te magazines qe eshte krijuar gjate hyrjes.</param> 
		/// <param name="kokamag">koka e magazines e krijuar gjate shperndarjes se shpenzimeve.</param> 
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
		public clsMesazh krijoTrupiDokAmortizimiPerShperndarjeShpenzimesh(clsAmortizimiKoka kokaEAmortizimit, List<clsShperndarjeShpenzimeTrupi> trupiPerKetedokmag, DbRegjistrim.clsKokaMagazina kokamag, colSerialetMagazine serialetMagazinesPerShperndarje)
		{
			clsMesazh pergjigja = new clsMesazh();
			bool llogaritvleraperserial = false;
			if (serialetMagazinesPerShperndarje.Count == 0)///meqe amortizimi perseritet per disa standarte colectioni i serialeve eshte i njejte per te gjithe keshtu qe nuk e marrim perseri
			{
				foreach (clsShperndarjeShpenzimeTrupi t in trupiPerKetedokmag)
				{
					clsKokaMagazina kokahyrje = new clsKokaMagazina();
					kokahyrje.mbushKokaMagazinaSipasID(t.IdFatura);
					pergjigja.Status = serialetMagazinesPerShperndarje.merrSerialetMagazineSipasIDDokumenti(t.IdFatura, kokaEAmortizimit.IdNdermarrje, kokahyrje.IdKonfigAmbjente);
					if (!pergjigja.Status)
						return pergjigja;
				}
				llogaritvleraperserial = true;
			}
			//Llogarit amortizimin per secilin rresht te trupit.
			pergjigja.Status = llogaritAmortizimiShperndarjeShpenzimesh(kokaEAmortizimit, serialetMagazinesPerShperndarje, kokamag, llogaritvleraperserial);
			return pergjigja;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Rillogarit trupin e amortizimit pas filtrimit te serialeve.
		/// </summary>
		/// <param name="gjitheKokat"></param>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
		/// <param name="dataFillimiRillogaritje">(DateTime) Data e fillimit te rillogaritjes. </param>
		/// <param name="idPerdoruesi">(int) Id e perdoruesit qe po ben rillogaritjen.</param>
		/// <param name="meSerial">True nese llogaritja do te behet per artikujt me serial dhe False nese llogaritja do te behet per artikujt pa serial</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese rillogaritja kryhet me sukses, ne te kundert False.</returns>
		public static clsMesazh rillogariTrupinAmortizimArtikuj(colAmortizimiKoka gjitheKokat, int idNdermarrje, int idLlojStandarti, string StandartiEmertim, DateTime dataFillimiRillogaritje, int idPerdoruesi, bool meSerial, EO.Web.ProgressTaskEventArgs e, ResourceManager rm, CultureInfo ci, colAmortizimiTrupiAbstract colAm, colAmortizimiTrupiAbstract colRez)
		{
			clsMesazh pergjigje = new clsMesazh();
			List<clsAmortizimiTrupiAbstract> lista = new List<clsAmortizimiTrupiAbstract>();
			lista.AddRange(colAm);
			lista.AddRange(colRez);
			colAmortizimiKoka kokaAmortizimiPerModifikim = new colAmortizimiKoka();
			//colAm.ktheArtikujNormaAmortizimiBrendaDatave
			bool serialIRi = true, tempSerialIRi = true;
			bool merrDateAmortizimiDokMePare = false;
			DateTime DateAmortizimiTemp = DateTime.Now;
			DataRow konfigurimeAmortizimi = clsKonfigurimAmbjenti.ktheIdKonfigurimiTeAmortizimeve(idNdermarrje);
			int nrPerqindjeMeSerial = 0, nrPerqindjePaSerial = 0, mykoleksionCount = lista.Count,
				FADT = Convert.ToInt32(konfigurimeAmortizimi["FADT"]),
				FADTK = Convert.ToInt32(konfigurimeAmortizimi["FADTK"]),
				FAD = Convert.ToInt32(konfigurimeAmortizimi["FAD"]),
				FAS = Convert.ToInt32(konfigurimeAmortizimi["FAS"]),
				FANS = Convert.ToInt32(konfigurimeAmortizimi["FANS"]),
				FRAanalitike = Convert.ToInt32(konfigurimeAmortizimi["FRAanalitike"]),
				FRAanalitikeRezerve = Convert.ToInt32(konfigurimeAmortizimi["FRAanalitikeRezerve"]),
				FRApermbledhese = Convert.ToInt32(konfigurimeAmortizimi["FRApermbledhese"]),
				FRApermblRezerve = Convert.ToInt32(konfigurimeAmortizimi["FRApermblRezerve"]),
				FA = Convert.ToInt32(konfigurimeAmortizimi["FA"]),
				FASS = Convert.ToInt32(konfigurimeAmortizimi["FASS"]),
				FAHT = Convert.ToInt32(konfigurimeAmortizimi["FAHT"]),
				FAHTK = Convert.ToInt32(konfigurimeAmortizimi["FAHTK"]),
				FAB = Convert.ToInt32(konfigurimeAmortizimi["FAB"]),
				FAFpermbledhese = Convert.ToInt32(konfigurimeAmortizimi["FAFpermbledhese"]),
				FAFpermblRezerve = Convert.ToInt32(konfigurimeAmortizimi["FAFpermblRezerve"]),
				FAFanalitike = Convert.ToInt32(konfigurimeAmortizimi["FAFanalitike"]),
				FAFanalitikeRezerve = Convert.ToInt32(konfigurimeAmortizimi["FAFanalitikeRezerve"]);
			string serialiAktual = "", tempSerialiAktual = "";//
			int maxRetry = 200;
			int.TryParse(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.MAXRETRY_TRANS), out maxRetry);

			try
			{
				colAmortizimiTrupiAbstract trupiPerSerial = colAmortizimiTrupiAbstract.krijoInstance(lista.Count > 0 ? lista[0].objektiKod : enumObjekteAmortizimi.ASETE);
				var dbData = new DbData();
				for (int objektiAktual = 0; objektiAktual < mykoleksionCount; objektiAktual++)
				{
					#region retryTrans
					clsRetryTrans retryTrans = new clsRetryTrans("RillogaritjeAmortizimi", maxRetry);
					do
					{
						using (var scope = new MyTransactionScope(dbData, 0))
						{
							try
							{
								clsAmortizimiTrupiAbstract objektTrupi = lista[objektiAktual].DeepClone();
								clsAseteNormaAmortizimiAbstract norma = clsAseteNormaAmortizimiAbstract.krijoInstance(objektTrupi.objektiKod);
								string llojAmortizimi = norma.merrNormenDheMetodenAmortizimitSipasIdArtikullStandartit(objektTrupi.IdArtikulli, idLlojStandarti, objektTrupi.DateAmortizimi);
								tempSerialIRi = serialIRi;
								tempSerialiAktual = serialiAktual;
								colAseteNormaAmortizimiAbstract aseteNorma = colAseteNormaAmortizimiAbstract.krijoInstance(objektTrupi.objektiKod);
								//nqs nuk eshte dok i pare ne list merr daten e dok paraardhes si data me pare amortizimi
								DateTime DateMepareAmortizimi = merrDateAmortizimiDokMePare ? DateAmortizimiTemp.AddDays(1) : objektTrupi.DateMePareAmortizimi.AddDays(1);
								if (aseteNorma.ktheBoolArtikujNormaAmortizimiBrendaDatave(objektTrupi.IdArtikulli, idLlojStandarti, DateMepareAmortizimi, objektTrupi.DateAmortizimi))
									return new clsMesazh(false, String.Format("Serialit {0} per standartin {1} i duhet llogaritur me pare amortizimi pasi ka me shume norma se 1 gjate intervalit {2}  dhe {3} te pallogaritur!", objektTrupi.Serial, StandartiEmertim, DateMepareAmortizimi.ToString("dd/MM/yyyy"), objektTrupi.DateAmortizimi.ToString("dd/MM/yyyy")));
								pergjigje = RillogaritTrupDokumentAmortizimi(dbData, objektTrupi, ref tempSerialiAktual, gjitheKokat, idLlojStandarti, idNdermarrje, idPerdoruesi, objektiAktual, ref tempSerialIRi, lista, trupiPerSerial, dataFillimiRillogaritje, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermbledhese, FRAanalitike, FAFpermbledhese, FAFanalitike, FRApermblRezerve, FRAanalitikeRezerve, FAFpermblRezerve, FAFanalitikeRezerve, FAHT, FAHTK, meSerial, kokaAmortizimiPerModifikim, nrPerqindjeMeSerial, nrPerqindjePaSerial, llojAmortizimi, e);
								merrDateAmortizimiDokMePare = true;
								if (!pergjigje.Status)
									return pergjigje;
								DateAmortizimiTemp = objektTrupi.DateAmortizimi;
								lista[objektiAktual] = objektTrupi;
								serialIRi = tempSerialIRi;
								serialiAktual = tempSerialiAktual;

								scope.Complete(out dbData);

								retryTrans.stopRetrying(); //dil se e bone
							}
							catch (SqlException sqlEx)
							{
								if (!retryTrans.checkRetry(sqlEx, "Rillogaritje amortzimi", maxRetry, sqlEx.Message))
									throw sqlEx;
							}
							catch (Exception ex)
							{
								throw ex;
							}
						}
					} while (retryTrans.isRetrying());
					#endregion
				}
			}
			catch (Exception)
			{
				throw new MyException("Ngeli te seriali: " + serialiAktual);
			}


			if (meSerial)
				e.UpdateProgress(50);
			else
				e.UpdateProgress(100);

			return kokaAmortizimiPerModifikim.modifikoKokatPerRillogaritje(rm, ci, idNdermarrje, lista, maxRetry);

		}

		private static clsMesazh RillogaritTrupDokumentAmortizimi(DbData dbData, clsAmortizimiTrupiAbstract amortTrupiKorrent, ref string serialiAktual, colAmortizimiKoka gjitheKokat, int idLlojStandarti, int idNdermarrje, int idPerdoruesi, int objektiAktual, ref bool serialIRi, List<clsAmortizimiTrupiAbstract> lista, colAmortizimiTrupiAbstract trupiPerSerial, DateTime dataFillimiRillogaritje, int FA, int FAB, int FAS, int FAD, int FADT, int FADTK, int FANS, int FASS, int FRApermbledhese, int FRAanalitike, int FAFpermbledhese, int FAFanalitike, int FRApermblRezerve, int FRAanalitikeRezerve, int FAFpermblRezerve, int FAFanalitikeRezerve, int FAHT, int FAHTK, bool meSerial, colAmortizimiKoka kokaAmortizimiPerModifikim, int nrPerqindjeMeSerial, int nrPerqindjePaSerial, string llojAmortizimi, ProgressTaskEventArgs e)
		{
			clsDatabaseInventari dbInv = new clsDatabaseInventari(dbData);
			clsDatabazeAsete dbAsete = new clsDatabazeAsete(dbData);

			clsMesazh pergjigje = new clsMesazh();
			serialiAktual = amortTrupiKorrent.Serial;

			clsAmortizimiKoka amortizimiKoka = gjitheKokat.Find(x => x.IdAmortizimi == amortTrupiKorrent.IdAmortizimKoka);
			clsAmortizimiTrupiAbstract trupiPerparaFillimit = clsAmortizimiTrupiAbstract.krijoInstance(amortTrupiKorrent.objektiKod);

			amortTrupiKorrent.Artikull = amortTrupiKorrent.Artikull ?? new clsArtikulli(amortTrupiKorrent.IdArtikulli, dbInv);

			int idPrindiFillestar = clsKodifikimArtikulli.ktheIdPrindiFillestar(amortTrupiKorrent.Artikull.Kodifikimi1Artikulli, dbInv);

			clsKarakteristikaStandarti karakteristikaStandartit = new clsKarakteristikaStandarti(idLlojStandarti, idPrindiFillestar, idNdermarrje, true, dbAsete);

			//Zbresim amortizimin shtese te serialit dhe do ja shtojme pasi ta kemi rillogaritur.
			if (amortTrupiKorrent.objektiKod == enumObjekteAmortizimi.ASETE)
				amortizimiKoka.AmortizimiShteseTotal = amortizimiKoka.AmortizimiShteseTotal - amortTrupiKorrent.AmortizimiShtese;

			amortizimiKoka.IdPerdoruesi = idPerdoruesi;

			//Duhet te kontrolloje per dokumenta te meparshem nga data qe ne fillojme rillogaritjen. 
			//Nese ka i referohet atij dokumenti, ne te kunder rreshti i pare per cdo serial nuk modifikohet.
			//Kjo procedure kryehet vetem kur indeksi eshte zero, pra i pari ne koleksion ose kur indeksi simbolizon nje serial te ri nga ata te meparshmit qe po llogariteshin.
			if (objektiAktual == 0 || serialIRi || amortTrupiKorrent.objektiKod != lista[objektiAktual - 1].objektiKod)
			{
				pergjigje = RillogaritSerialTeRiOseIndeksZero(pergjigje, ref serialIRi, amortTrupiKorrent, amortizimiKoka, karakteristikaStandartit, trupiPerSerial, dataFillimiRillogaritje, idNdermarrje, idLlojStandarti, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermbledhese, FRAanalitike, FAFpermbledhese, FAFanalitike, FRApermblRezerve, FRAanalitikeRezerve, FAFpermblRezerve, FAFanalitikeRezerve, meSerial, llojAmortizimi, dbAsete);

				if (!pergjigje.Status)
					return pergjigje;
			}
			else
			{
				int trupiParaIndex = -1;

				//Artikujt pa serial do te merren si peme qe nga prindi fillestar deri te gjethet. Grupi i pare i if do ti referohet gjithmone rreshtit te fundit te llogaritur sakte te atij seriali kurse grupi tjeter do i referohet gjithmone dokumentit te fundit te llogaritur sakte te prindit nga eshte gjeneruar per te marre te dhenat paraardhese.
				if (!meSerial)
				{
					bool kushtFaSeriali = amortizimiKoka.IdKonfigurimAmbjenti.EqualsAny(FADT, FADTK, FAD, FAS, FANS, FRAanalitike, FRApermbledhese, FRAanalitikeRezerve, FRApermblRezerve, FA, FASS);
					bool kushtFaSerialiPrind = amortizimiKoka.IdKonfigurimAmbjenti.EqualsAny(FAHT, FAHTK);

					pergjigje = MerrAmortizimTrupiFunditSipasSerialit(pergjigje, kushtFaSeriali, kushtFaSerialiPrind, ref trupiParaIndex, trupiPerparaFillimit, amortTrupiKorrent, amortizimiKoka.NrRenditje, trupiPerSerial, idNdermarrje, dataFillimiRillogaritje, idLlojStandarti);
				}

				if (meSerial)
					pergjigje = RillogaritAmortizimPerArtikujtMeOsePaSeriale(pergjigje, amortTrupiKorrent, lista[objektiAktual - 1], amortizimiKoka.IdKonfigurimAmbjenti, karakteristikaStandartit, idNdermarrje, idLlojStandarti, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermbledhese, FRAanalitike, FAFpermbledhese, FAFanalitike, FRApermblRezerve, FRAanalitikeRezerve, FAFpermblRezerve, FAFanalitikeRezerve, meSerial, llojAmortizimi, dbAsete);

				else
				{
					clsAmortizimiTrupiAbstract tempTrupAmortizimi = trupiParaIndex >= 0 ? trupiPerSerial[trupiParaIndex] : trupiPerparaFillimit;

					pergjigje = RillogaritAmortizimPerArtikujtMeOsePaSeriale(pergjigje, amortTrupiKorrent, tempTrupAmortizimi, amortizimiKoka.IdKonfigurimAmbjenti, karakteristikaStandartit, idNdermarrje, idLlojStandarti, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermbledhese, FRAanalitike, FAFpermbledhese, FAFanalitike, FRApermblRezerve, FRAanalitikeRezerve, FAFpermblRezerve, FAFanalitikeRezerve, meSerial, llojAmortizimi, dbAsete);
				}

				if (!pergjigje.Status)
					return pergjigje;

			}

			//Kontrollon nese rreshti i ri ne koleksionin e trupit i perket te njejtit serial ose jo.
			//Nese nuk i perket te njejtit serial duhet te filloj procedurat sikur te ishte objektiAktual = 0;
			//Kontrolli behet deri per serialin e parafundit te koleksionit.
			if (objektiAktual < lista.Count - 1)
			{
				if (meSerial)
					serialIRi = amortTrupiKorrent.IdAQTSeriali != lista[objektiAktual + 1].IdAQTSeriali;
				else
					serialIRi = amortTrupiKorrent.IdPrindFillestar != lista[objektiAktual + 1].IdPrindFillestar;
			}

			trupiPerSerial.Add((clsAmortizimiTrupiAbstract)amortTrupiKorrent.Clone());
			if (amortTrupiKorrent.objektiKod == enumObjekteAmortizimi.ASETE)
				//Shtimi i amortizimit shtese pas rillogaritjes.
				amortizimiKoka.AmortizimiShteseTotal = amortizimiKoka.AmortizimiShteseTotal + amortTrupiKorrent.AmortizimiShtese;

			kokaAmortizimiPerModifikim.AddIfNotExists(amortizimiKoka);

			if (meSerial)
				BejProgresTeRillogaritjes(0, objektiAktual, lista.Count, ref nrPerqindjeMeSerial, e);
			else
				BejProgresTeRillogaritjes(50, objektiAktual, lista.Count, ref nrPerqindjePaSerial, e);

			return pergjigje;
		}

		private static void BejProgresTeRillogaritjes(int koeficenti, int objektiAktual, int listCount, ref int nrPerqindje, ProgressTaskEventArgs e)
		{
			int nrPerqindjeMeSerialTmp = (int)koeficenti + (objektiAktual + 1) * 100 / (2 * listCount);
			if (nrPerqindje < nrPerqindjeMeSerialTmp)
			{
				nrPerqindje = nrPerqindjeMeSerialTmp;
				e.UpdateProgress(nrPerqindje);
			}
		}

		private static clsMesazh RillogaritAmortizimPerArtikujtMeOsePaSeriale(clsMesazh pergjigje, clsAmortizimiTrupiAbstract amortTrupiKorrent, clsAmortizimiTrupiAbstract trupiAmortizimiSerial, int idKonfigAmortizimiKoka, clsKarakteristikaStandarti karakteristikaStandartit, int idNdermarrje, int idLlojStandarti, int FA, int FAB, int FAS, int FAD, int FADT, int FADTK, int FANS, int FASS, int FRApermbledhese, int FRAanalitike, int FAFpermbledhese, int FAFanalitike, int FRApermblRezerve, int FRAanalitikeRezerve, int FAFpermblRezerve, int FAFanalitikeRezerve, bool meSerial, string llojAmortizimi, clsDatabazeAsete dbAsete)
		{
			if (amortTrupiKorrent.objektiKod == enumObjekteAmortizimi.ASETE)
				pergjigje.Status = llogaritAtributetPerRillogaritje(amortTrupiKorrent, trupiAmortizimiSerial, idKonfigAmortizimiKoka, idNdermarrje, idLlojStandarti, karakteristikaStandartit, meSerial, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermbledhese, FRAanalitike, FAFpermbledhese, FAFanalitike, llojAmortizimi, dbAsete);
			else
				pergjigje.Status = llogaritAtributetPerRillogaritje(amortTrupiKorrent, trupiAmortizimiSerial, idKonfigAmortizimiKoka, idNdermarrje, idLlojStandarti, karakteristikaStandartit, meSerial, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermblRezerve, FRAanalitikeRezerve, FAFpermblRezerve, FAFanalitikeRezerve, llojAmortizimi, dbAsete);

			if (!pergjigje.Status)
				return new clsMesazh(false, String.Format("Gabim gjate rillogaritjes se atributeve te trupit te amortizimit per serialin {0}!", amortTrupiKorrent.Serial));

			return pergjigje;
		}

		private static clsMesazh MerrAmortizimTrupiFunditSipasSerialit(clsMesazh pergjigje, bool kushtFaSeriali, bool kushtFaSerialiPrind, ref int trupiParaIndex, clsAmortizimiTrupiAbstract trupiPerparaFillimit, clsAmortizimiTrupiAbstract amortTrupiKorrent, int nrRenditje, colAmortizimiTrupiAbstract trupiPerSerial, int idNdermarrje, DateTime dataFillimiRillogaritje, int idLlojStandarti)
		{
			if (kushtFaSeriali)
			{
				trupiParaIndex = trupiPerSerial.FindLastIndex(x => x.IdAQTSeriali == amortTrupiKorrent.IdAQTSeriali);
				if (trupiParaIndex == -1)
					pergjigje.Status = trupiPerparaFillimit.merrAmortizimTrupiAmortFunditSipasSerialit(idNdermarrje, dataFillimiRillogaritje, idLlojStandarti, amortTrupiKorrent.IdAQTSeriali, nrRenditje);
				if (!pergjigje.Status)
					return new clsMesazh(false, String.Format("Gabim ne marrjen e dokumentave me pare sipas serialit ne serialin {0}!", amortTrupiKorrent.Serial));

			}
			else
				if (kushtFaSerialiPrind)
			{
				trupiParaIndex = trupiPerSerial.FindLastIndex(x => x.IdAQTSeriali == amortTrupiKorrent.IdPrind);
				if (trupiParaIndex == -1)
				{
					pergjigje.Status = trupiPerparaFillimit.merrAmortizimTrupiAmortFunditSipasSerialit(idNdermarrje, dataFillimiRillogaritje, idLlojStandarti, amortTrupiKorrent.IdPrind, nrRenditje);
					if (!pergjigje.Status)
						return new clsMesazh(false, String.Format("Gabim ne marrjen e dokumentave me pare sipas serialit ne serialin {0}!", amortTrupiKorrent.Serial));
				}
			}
			return pergjigje;
		}

		private static clsMesazh RillogaritSerialTeRiOseIndeksZero(clsMesazh pergjigje, ref bool serialIRi, clsAmortizimiTrupiAbstract amortTrupiKorrent, clsAmortizimiKoka amortizimiKoka, clsKarakteristikaStandarti karakteristikaStandartit, colAmortizimiTrupiAbstract trupiPerSerial, DateTime dataFillimiRillogaritje, int idNdermarrje, int idLlojStandarti, int FA, int FAB, int FAS, int FAD, int FADT, int FADTK, int FANS, int FASS, int FRApermbledhese, int FRAanalitike, int FAFpermbledhese, int FAFanalitike, int FRApermblRezerve, int FRAanalitikeRezerve, int FAFpermblRezerve, int FAFanalitikeRezerve, bool meSerial, string llojAmortizimi, clsDatabazeAsete dbAsete)
		{
			serialIRi = false;
			trupiPerSerial = colAmortizimiTrupiAbstract.krijoInstance(amortTrupiKorrent.objektiKod);
			clsAmortizimiTrupiAbstract trupiPerpara = clsAmortizimiTrupiAbstract.krijoInstance(amortTrupiKorrent.objektiKod);
			pergjigje.Status = trupiPerpara.merrAmortizimTrupiAmortFunditSipasSerialit(idNdermarrje, dataFillimiRillogaritje, idLlojStandarti, amortTrupiKorrent.IdAQTSeriali, amortizimiKoka.NrRenditje);

			if (trupiPerpara.IdAmortizimiTrupi > 0)
			{
				if (amortTrupiKorrent.objektiKod == enumObjekteAmortizimi.ASETE)
					pergjigje.Status = llogaritAtributetPerRillogaritje(amortTrupiKorrent, trupiPerpara, amortizimiKoka.IdKonfigurimAmbjenti, idNdermarrje, idLlojStandarti, karakteristikaStandartit, meSerial, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermbledhese, FRAanalitike, FAFpermbledhese, FAFanalitike, llojAmortizimi, dbAsete);
				else
					pergjigje.Status = llogaritAtributetPerRillogaritje(amortTrupiKorrent, trupiPerpara, amortizimiKoka.IdKonfigurimAmbjenti, idNdermarrje, idLlojStandarti, karakteristikaStandartit, meSerial, FA, FAB, FAS, FAD, FADT, FADTK, FANS, FASS, FRApermblRezerve, FRAanalitikeRezerve, FAFpermblRezerve, FAFanalitikeRezerve, llojAmortizimi, dbAsete);

				if (!pergjigje.Status)
					return new clsMesazh(false, $"Gabim gjate rillogaritjes se atributeve te trupit te amortizimit per serialin {amortTrupiKorrent.Serial}!");
			}
			return new MesazhSuksesi($"Rillogaritja e atributeve te trupit te amortizimit per serialin {amortTrupiKorrent.Serial} u krye ne rregull!");
		}

		#endregion

		#endregion

		#region Metoda Private

		#region Llogartija e amortizimet per raste te ndryshme

		/// <summary>
		/// MODULI ASETE:
		/// Krijon te dhenat e reja per trupin e ri qe do i llogaritet amortizimi nga ambjenti i amortizimit dhe i ndryshimit te statusit.
		/// Veprimet qe perdorin metoden: NDRYSHIMI I STATUSIT TE MAGAZINES, AMORTIZIMI NORMAL
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="colTrupiOld">(colAmortizimiTrupi) Trupi i amortizimit te vjeter qe duhet te llogaritim amortizimin e ri.</param>
		/// <param name="nrRreshtit">(int) Merr rreshtin qe ndodhet aktualisht procesi.</param>
		/// <param name="e">(EO.Web.ProgressTaskEventArgs) Merr progresin e progres barit.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
		private bool llogaritAmortizimiNgaAmbjentiTePergjithshme(clsAmortizimiKoka kokaEAmortizimit, colAmortizimiTrupiAbstract colTrupiOld, int idhistorikufundit, bool ndryshimstatusmagazine, out int nrRreshtit, EO.Web.ProgressTaskEventArgs e, colAmortizimiTrupiAbstract colTrupiOldpas, out string mesazhmevonshem)
		{
			mesazhmevonshem = "";
			bool pergjigje = true;
			nrRreshtit = 0;
			DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli();
			int FAFanalitike = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(colTrupiOld.objektiKod == enumObjekteAmortizimi.ASETE ? "FAFanalitike" : "FAFanalitikeRezerve", kokaEAmortizimit.IdNdermarrje),
				FAFpermbledhese = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(colTrupiOld.objektiKod == enumObjekteAmortizimi.ASETE ? "FAFpermbledhese" : "FAFpermblRezerve", kokaEAmortizimit.IdNdermarrje),
				FAB = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("FAB", kokaEAmortizimit.IdNdermarrje),
				FASS = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("FASS", kokaEAmortizimit.IdNdermarrje);
			try
			{
				int nrPerqindje = 0;
				clsKarakteristikaStandarti karakteristikaStandartit = new clsKarakteristikaStandarti();
				foreach (clsAmortizimiTrupiAbstract rreshtiVjeter in colTrupiOld)
				{
					if (mesazhmevonshem == "")
					{
						List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = colTrupiOldpas.FindAll(x => x.IdAQTSeriali == rreshtiVjeter.IdAQTSeriali);
						if (amortizimetTrupipas.Count() > 0)
							mesazhmevonshem = "Kujdes ka veprime te mevonshme me kete aset " + rreshtiVjeter.Serial;
					}
					clsAmortizimiTrupiAbstract rreshtiVjeterClone = (clsAmortizimiTrupiAbstract)rreshtiVjeter.Clone();
					colAseteNormaAmortizimiAbstract aseteNorma = colAseteNormaAmortizimiAbstract.krijoInstance(this.objektiKod);
					aseteNorma.ktheArtikujNormaAmortizimiBrendaDatave(rreshtiVjeter.IdArtikulli, kokaEAmortizimit.IdLlojStandarti, rreshtiVjeter.DateAmortizimi, kokaEAmortizimit.DateAmortizimi);
					int i = 0;
					do
					{
						DateTime dataERe = (i < aseteNorma.Count - 1) ? aseteNorma[i + 1].DtAktivizimi.AddDays(-1) : kokaEAmortizimit.DateAmortizimi;
						clsAmortizimiTrupiAbstract trupiSipasNormes = llogaritjaAmortizimitTrup(ref rreshtiVjeterClone, aseteNorma[i].IdLidhjeArtikullLlojAmort, karakteristikaStandartit, artikulli, kokaEAmortizimit, dataERe, nrRreshtit, idhistorikufundit, ndryshimstatusmagazine, FAFanalitike, FAFpermbledhese, FAB, FASS);
						if (trupiSipasNormes != null)
							Add(trupiSipasNormes);
						i++;
					} while (i < aseteNorma.Count);
					if (e != null)
					{
						int nrPerqindjeTmp = (nrRreshtit + 1) * 100 / colTrupiOld.Count;
						if (nrPerqindjeTmp > nrPerqindje)
						{
							nrPerqindje = nrPerqindjeTmp;
							e.UpdateProgress(nrPerqindje);
						}
					}
					nrRreshtit++;
				}
				return pergjigje;
			}
			catch (MyException ex)
			{
				throw;
			}
			catch (Exception)
			{
				throw new MyException(" U ndalua tek artikulli: " + artikulli.KodArtikulli);
			}
		}

		private clsAmortizimiTrupiAbstract llogaritjaAmortizimitTrup(ref clsAmortizimiTrupiAbstract rreshtiVjeter, int IdArtikull_LlojAmortizimi, clsKarakteristikaStandarti karakteristikaStandartit, DbInventari.clsArtikulli artikulli,
			clsAmortizimiKoka kokaEAmortizimit, DateTime dataAktualeRresht, int nrRreshtit, int idhistorikufundit, bool ndryshimstatusmagazine, int FAFanalitike, int FAFpermbledhese, int FAB, int FASS)
		{
			bool pergjigje = true;
			artikulli = rreshtiVjeter.Artikull != null ? rreshtiVjeter.Artikull : new DbInventari.clsArtikulli(rreshtiVjeter.IdArtikulli);
			karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(artikulli.Kodifikimi1Artikulli), kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdNdermarrje);

			clsAmortizimiTrupiAbstract clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(nrRreshtit, rreshtiVjeter.IdArtikulli, artikulli.PershkrimArtikulli, IdArtikull_LlojAmortizimi, rreshtiVjeter.DateAmortizimi,
				dataAktualeRresht, rreshtiVjeter.DateNdryshimStatusMagazine, rreshtiVjeter.IdNjesiAdministrative, rreshtiVjeter.IdAQTSeriali, rreshtiVjeter.Serial, artikulli, rreshtiVjeter.VleftaShteseRivleresim, this.objektiKod);

			//Llogarit vlerat e amortizimit.
			pergjigje = llogaritVleraAmortizimiAtributeTePergjithshme(clsTrupiAmortRi, rreshtiVjeter, kokaEAmortizimit.IdKonfigurimAmbjenti, karakteristikaStandartit, idhistorikufundit, ndryshimstatusmagazine, FAB, FASS, FAFpermbledhese, FAFanalitike, false, this.objektiKod);
			if (!pergjigje)
				return null;
			//Heq dhe rastet kur dalin shitjen ne amortizimin e rradhes.
			if ((clsTrupiAmortRi.AmortizimiShtese != 0 || rreshtiVjeter.DateAmortizimi < clsTrupiAmortRi.DateAmortizimi) && (clsTrupiAmortRi.VleftaGjendje != 0 || clsTrupiAmortRi.VleftaPlusMinus != 0))
			{
				rreshtiVjeter = (clsAmortizimiTrupiAbstract)clsTrupiAmortRi.Clone();
				return clsTrupiAmortRi;
			}
			return null;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon te dhenat e reja per trupin e ri qe do i llogaritet amortizimi per blerjen.
		/// </summary>
		/// <param name="seriale">(colSerialetMagazine) Lista e serialeve qe po behen blerje me kete dokument.</param>
		/// <param name="dataAmortizimiNew">(DateTime) Data e ndryshimit te statusit te ri.</param>
		/// <param name="idStandartAmort">(int) Id e llojit te standartit.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		private bool llogaritAmortizimiNgaVeprimeMagazineBlerje(colSerialetMagazine seriale, DateTime dataAmortizimiNew, int idStandartAmort)
		{
			foreach (clsSerialetMagazine seriali in seriale)
			{
				clsAQTSeriale ser = new clsAQTSeriale();
				ser.merrAQTSerialSipasID(seriali.IdAQTSeriali);
				int idArtikullAktual = ser.IdAQTArt;// clsAQTSeriale.merrIDArtikullAQTSerialSipasID(seriali.IdAQTSeriali, dbasete);     
				DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli(idArtikullAktual);
				clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(artikulli.Kodifikimi1Artikulli), idStandartAmort, seriali.IdNdermarrje);

				if (karakteristikaStandartit == null)
					return false;

				DateTime dataKonvertuarSipasKarakteristikave = karakteristikaStandartit.konvertoDatenSipasKerkesesKonfigurimit(dataAmortizimiNew, true, true);
				clsAmortizimiTrupiAbstract clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(seriali.NrRendor, idArtikullAktual, artikulli.PershkrimArtikulli, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(idArtikullAktual, idStandartAmort, dataKonvertuarSipasKarakteristikave, this.objektiKod),
					dataKonvertuarSipasKarakteristikave, dataKonvertuarSipasKarakteristikave, dataKonvertuarSipasKarakteristikave, seriali.IdNjesiAdministrative, seriali.IdAQTSeriali, ser.AqtSerialKod, artikulli, 0, this.objektiKod);
				//Llogarit vlerat e amortizimit.
				llogaritVleraAmortizimiNgaVeprimeMagazineBlerje(clsTrupiAmortRi, seriali, karakteristikaStandartit);

				Add(clsTrupiAmortRi);
			}
			return true;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon te dhenat e reja per trupin e ri qe do i llogaritet amortizimi per daljen.
		/// </summary>
		/// <param name="colTrupiOld">(colAmortizimiTrupi) Trupi i amortizimit te vjeter qe duhet te llogaritim amortizimin e ri.</param>
		/// <param name="trupiPerHyrje">(colAmortizimiTrupi) Trupi i amortizimit te ri per dokumentat e hyrjes.</param>
		/// <param name="seriale">(colSerialetMagazine) Lista e serialeve qe po behen dalje me kete dokument.</param>
		/// <param name="dataAmortizimiNew">(DateTime) Data e ndryshimit te statusit te ri.</param>
		/// <param name="idStandartAmort">(int) Id e llojit te standartit.</param>
		/// <param name="idNdermarrja">(int) Id e ndermarrjes qe po kryhet veprimi.</param>
		/// <param name="llojDokumentiAktual">(int) Id e llojit te dokumentit qe po kryhet. Nevojitet per te llogaritur +1 ne amortizim nese eshte nevoja.</param>
		/// <param name="mosLlogaritAmortizimShtese">(bool) Nese nuk duhet llogaritur amortizimi shtese gjate levizjeve ne magazina vjen True, ne te kundert false.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
		private clsMesazh llogaritAmortizimiNgaVeprimeMagazineDalja(colAmortizimiTrupiAbstract colTrupiOld, ref colAmortizimiTrupiAbstract trupiPerHyrje, colSerialetMagazine seriale, DateTime dataAmortizimiNew, int idStandartAmort, int idNdermarrja, int llojDokumentiAktual, bool mosLlogaritAmortizimShtese, colAmortizimiTrupiAbstract colTrupiOldPas, out string mesazhmevonshem)
		{
			mesazhmevonshem = "";
			int nrRreshtit = 0;
			clsMesazh pergjigje = new clsMesazh(true);
			trupiPerHyrje = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			int FAFanalitike = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(this.objektiKod == enumObjekteAmortizimi.ASETE ? "FAFanalitike" : "FAFanalitikeRezerve", idNdermarrja), FAFpermbledhese = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(this.objektiKod == enumObjekteAmortizimi.ASETE ? "FAFpermbledhese" : "FAFpermblRezerve", idNdermarrja), FAB = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("FAB", idNdermarrja), FASS = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("FASS", idNdermarrja);

			foreach (clsSerialetMagazine seriali in seriale)
			{
				DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(seriali.IdArtikulli);
				if (this.objektiKod == enumObjekteAmortizimi.REZERVA && !art.MeRezerveRivleresimi)
					continue;
				clsAmortizimiTrupiAbstract trupiVjeter = colTrupiOld.Find(x => x.IdAQTSeriali == seriali.IdAQTSeriali);
				clsAQTSeriale ser = new clsAQTSeriale();
				ser.merrAQTSerialSipasID(seriali.IdAQTSeriali);
				clsStandarteAmortizim standart = new clsStandarteAmortizim(idStandartAmort);
				if (trupiVjeter == null)
				{
					string mesazh = string.Format(MessagesResource.Messages["msgSeriale"], ser.AqtSerialKod, art.KodArtikulli);
					return new clsMesazh(false, mesazh);
				}
				if (mesazhmevonshem == "")
				{
					List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = colTrupiOldPas.FindAll(x => x.IdAQTSeriali == ser.IdAQTSerial);
					if (amortizimetTrupipas.Count() > 0)
						mesazhmevonshem = "Kujdes ka veprime te mevonshme me kete aset " + ser.AqtSerialKod;
				}
				int index = 0;
				double sasiadalje = 0;
				clsAmortizimiTrupiAbstract rreshtiVjeterClone = (clsAmortizimiTrupiAbstract)trupiVjeter.Clone();
				colAseteNormaAmortizimiAbstract aseteNorma = colAseteNormaAmortizimiAbstract.krijoInstance(this.objektiKod);
				DateTime DateAmortizimi = trupiVjeter.DateAmortizimi.AddDays(1);
				aseteNorma.ktheArtikujNormaAmortizimiBrendaDatave(trupiVjeter.IdArtikulli, idStandartAmort, DateAmortizimi, dataAmortizimiNew);
				if (aseteNorma.Count > 1)
					return new clsMesazh(false, String.Format("Serialit {0} per standartin {1} i duhet llogaritur me pare amortizimi pasi ka me shume norma se 1 gjate intervalit te pallogaritur!", ser.AqtSerialKod, standart.Pershkrimi));
				clsAmortizimiTrupiAbstract clsTrupiAmortRi = llogaritjaAmortizimitDaljeTrup(ref rreshtiVjeterClone, seriale, pergjigje, seriali, ser, dataAmortizimiNew, idStandartAmort, idNdermarrja, llojDokumentiAktual,
mosLlogaritAmortizimShtese, FAFanalitike, FAFpermbledhese, FAB, FASS, ref index, ref sasiadalje);
				trupiPerHyrje.Add(clsTrupiAmortRi);

				if (index < 0)
				{
					Add(clsTrupiAmortRi);
					nrRreshtit++;
				}
				else
				{
					clsAmortizimiTrupiAbstract trupiPerNdryshim = (clsAmortizimiTrupiAbstract)this[index].Clone();
					trupiPerNdryshim.HdAmortizimGjithsej += clsTrupiAmortRi.HdAmortizimGjithsej;
					trupiPerNdryshim.HdAmortizimVjetor += clsTrupiAmortRi.HdAmortizimVjetor;
					trupiPerNdryshim.VleftaPlusMinus += clsTrupiAmortRi.VleftaPlusMinus;
					this[index] = (clsAmortizimiTrupiAbstract)trupiPerNdryshim.Clone();
				}
			}

			return pergjigje;
		}

		private clsAmortizimiTrupiAbstract llogaritjaAmortizimitDaljeTrup(ref clsAmortizimiTrupiAbstract trupiVjeter, colSerialetMagazine seriale, clsMesazh pergjigje, clsSerialetMagazine seriali, clsAQTSeriale ser, DateTime dataAmortizimiNew, int idStandartAmort, int idNdermarrja, int llojDokumentiAktual,
	bool mosLlogaritAmortizimShtese, int FAFanalitike, int FAFpermbledhese, int FAB, int FASS, ref int index, ref double sasiadalje)
		{
			clsHistorikAQTSeriale historikSeriali = new clsHistorikAQTSeriale();
			historikSeriali.merrHistorikAQTSerialSipasIDAQT(seriali.IdAQTSeriali, idNdermarrja);
			int idArtikullAktual = ser.IdAQTArt;
			DbInventari.clsArtikulli artikulli = (trupiVjeter.Artikull == null) ? new DbInventari.clsArtikulli(idArtikullAktual) : trupiVjeter.Artikull;
			clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(artikulli.Kodifikimi1Artikulli), idStandartAmort, idNdermarrja);

			DateTime dataAmortizimit = (mosLlogaritAmortizimShtese == true ? trupiVjeter.DateAmortizimi : (trupiVjeter.DateAmortizimi > karakteristikaStandartit.konvertoDatenSipasKerkesesKonfigurimit(dataAmortizimiNew, false, true) ? trupiVjeter.DateAmortizimi : karakteristikaStandartit.konvertoDatenSipasKerkesesKonfigurimit(dataAmortizimiNew, false, true)));
			clsAmortizimiTrupiAbstract clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(seriali.NrRendor, idArtikullAktual, artikulli.PershkrimArtikulli, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(idArtikullAktual, idStandartAmort, dataAmortizimit, this.objektiKod), trupiVjeter.DateAmortizimi, dataAmortizimit, trupiVjeter.DateNdryshimStatusMagazine, seriali.IdNjesiAdministrative, seriali.IdAQTSeriali, ser.AqtSerialKod, artikulli, trupiVjeter.VleftaShteseRivleresim, this.objektiKod);

			//Nese seriali ekziston njehere brenda te njejtit dokument veprimet per daljen duhet te mblidhen, por jo per hyrjen
			index = this.FindIndex(x => x.IdAQTSeriali == clsTrupiAmortRi.IdAQTSeriali);
			sasiadalje = seriale.FindAll(x => x.IdAQTSeriali == clsTrupiAmortRi.IdAQTSeriali).Sum(item => item.Sasia);

			//Llogarit vlerat e amortizimit.
			pergjigje.Status = llogaritVleraAmortizimiNgaVeprimeMagazineDalje(clsTrupiAmortRi, trupiVjeter, llojDokumentiAktual, seriali, sasiadalje, historikSeriali, karakteristikaStandartit, FAB, FASS, FAFanalitike, FAFpermbledhese, this.objektiKod);

			if (!pergjigje.Status)
				return clsAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);

			trupiVjeter = (clsAmortizimiTrupiAbstract)clsTrupiAmortRi.Clone();
			return clsTrupiAmortRi;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon te dhenat e reja per trupin e ri qe do i llogaritet amortizimi per amortizimin fillestar ne transaksion.
		/// Veprimet qe perdorin metoden: AMORTIZIMI FILLESTAR
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese mbushja e objektit kryhet me sukses, ne te kundert False.</returns>
		private clsMesazh llogaritAmortizimiAmortizimFillestar(clsAmortizimiKoka kokaEAmortizimit, colAmortizimiFillestar serialeaqt, List<double> amortizimiFillestar, out string mesazhmevonshem)
		{
			mesazhmevonshem = "";
			int nrRreshtit = 0;
			int i = 0;
			clsMesazh pergjigja = new clsMesazh();
			colAmortizimiTrupiAbstract teGjitheSerialetLlogariturMePas = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			pergjigja.Status = teGjitheSerialetLlogariturMePas.ktheAmortizimTrupiAmortVeprimePas(kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.DateDokumenti, kokaEAmortizimit.IdLlojStandarti, 0);
			if (!pergjigja.Status)
				return pergjigja;
			colAmortizimiTrupiAbstract trupiRi = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			foreach (clsAmortizimiTrupiAbstract rreshtiAmortizimFillestar in this)
			{
				clsAmortizimiTrupiAbstract clsTrupiAmortRi;

				if (rreshtiAmortizimFillestar.IdAQTSeriali > 0)
				{
					if (mesazhmevonshem == "")
					{
						List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = teGjitheSerialetLlogariturMePas.FindAll(x => x.IdAQTSeriali == rreshtiAmortizimFillestar.IdAQTSeriali);
						clsAQTSeriale ser = new DbAsete.clsAQTSeriale();
						ser.merrAQTSerialSipasID(rreshtiAmortizimFillestar.IdAQTSeriali);
						if (amortizimetTrupipas.Count() > 0)
							mesazhmevonshem = "Kujdes ka veprime te mevonshme me kete aset " + ser.AqtSerialKod;
						else if (clsAmortizimiKoka.kaVeprimeMeKeteAset(kokaEAmortizimit.IdNiveli, rreshtiAmortizimFillestar.IdAQTSeriali, kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdAmortizimi))
							mesazhmevonshem = "Ekziston nje FAF me kete serial " + ser.AqtSerialKod;
					}
					if (rreshtiAmortizimFillestar.Artikull == null)
						rreshtiAmortizimFillestar.Artikull = new DbInventari.clsArtikulli(rreshtiAmortizimFillestar.IdArtikulli);
					clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(rreshtiAmortizimFillestar.Artikull.Kodifikimi1Artikulli), kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdNdermarrje);

					if (karakteristikaStandartit == null)
						return new clsMesazh(false, "Nuk ka rregull per kete standart!");


					clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(nrRreshtit, rreshtiAmortizimFillestar.IdArtikulli, rreshtiAmortizimFillestar.Artikull.PershkrimArtikulli, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(rreshtiAmortizimFillestar.IdArtikulli, kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.DateAmortizimi, this.objektiKod), kokaEAmortizimit.DateAmortizimi, kokaEAmortizimit.DateAmortizimi, kokaEAmortizimit.DateAmortizimi, rreshtiAmortizimFillestar.IdNjesiAdministrative, rreshtiAmortizimFillestar.IdAQTSeriali, rreshtiAmortizimFillestar.Serial, rreshtiAmortizimFillestar.Artikull, 0, this.objektiKod);
					if (amortizimiFillestar[i] != 0)
					{
						clsAmortizimiFillestar amort = new clsAmortizimiFillestar();
						amort.krijoObjekt(rreshtiAmortizimFillestar.IdAQTSeriali, karakteristikaStandartit.IdStandart, kokaEAmortizimit.IdAmortizimi, amortizimiFillestar[i]);

						serialeaqt.Add(amort);//serialit i vendosim vleren e amortizimit fillestar
					}
					//Llogarit vlerat e amortizimit.
					llogaritVleraAmortizimiAmortizimiFillestar(clsTrupiAmortRi, rreshtiAmortizimFillestar, karakteristikaStandartit, 1);
					//if (objektiKod==enumObjekteAmortizimi.ASETE&& clsTrupiAmortRi.NormaAmortizimi == 0.0)
					//    return new clsMesazh(false, "Mungon norma e amortizimit per artikull " + clsTrupiAmortRi.Artikull.KodArtikulli + "!"); ;
					if (objektiKod == enumObjekteAmortizimi.ASETE)
						clsTrupiAmortRi.VleftaGjendje = clsSerialetMagazine.merrSerialetMagazineSipasIDSerialDokFundit(clsTrupiAmortRi.IdAQTSeriali, karakteristikaStandartit.IdNdermarrje, kokaEAmortizimit.DateDokumenti, 0);
					else clsTrupiAmortRi.VleftaGjendje = rreshtiAmortizimFillestar.VleftaGjendje;

					trupiRi.Add(clsTrupiAmortRi);
					nrRreshtit++;
				}
				else
				{
					colAQTSeriale seriale = new colAQTSeriale();
					seriale.merrAQTSerialSipasIDArtikulliIDMagazine(rreshtiAmortizimFillestar.IdArtikulli, rreshtiAmortizimFillestar.IdNjesiAdministrative, kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.DateDokumenti);
					if (rreshtiAmortizimFillestar.Artikull == null)
						rreshtiAmortizimFillestar.Artikull = new DbInventari.clsArtikulli(rreshtiAmortizimFillestar.IdArtikulli);
					//DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli(rreshtiAmortizimFillestar.IdArtikulli, dbinv);
					if (seriale.Count == 0)
						return new clsMesazh(false, String.Format("Nuk ka gjendje per artikullin {0}!", rreshtiAmortizimFillestar.Artikull.KodArtikulli));
					foreach (clsAQTSeriale seriali in seriale)
					{
						if (mesazhmevonshem == "")
						{
							List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = teGjitheSerialetLlogariturMePas.FindAll(x => x.IdAQTSeriali == seriali.IdAQTSerial);
							if (amortizimetTrupipas.Count() > 0)
								mesazhmevonshem = "Kujdes ka veprime te mevonshme me kete aset " + seriali.AqtSerialKod;
							else if (clsAmortizimiKoka.kaVeprimeMeKeteAset(kokaEAmortizimit.IdNiveli, rreshtiAmortizimFillestar.IdAQTSeriali, kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdAmortizimi))
								mesazhmevonshem = "Ekziston nje FAF me kete serial " + seriali.AqtSerialKod;
						}
						clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(rreshtiAmortizimFillestar.Artikull.Kodifikimi1Artikulli), kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdNdermarrje);

						if (karakteristikaStandartit == null)
							return new clsMesazh(false, "Nuk ka rregull per kete standart!"); ;


						clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(nrRreshtit, rreshtiAmortizimFillestar.IdArtikulli, rreshtiAmortizimFillestar.Artikull.PershkrimArtikulli, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(rreshtiAmortizimFillestar.IdArtikulli, kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.DateAmortizimi, this.objektiKod), kokaEAmortizimit.DateAmortizimi, kokaEAmortizimit.DateAmortizimi, kokaEAmortizimit.DateAmortizimi, rreshtiAmortizimFillestar.IdNjesiAdministrative, seriali.IdAQTSerial, seriali.AqtSerialKod, rreshtiAmortizimFillestar.Artikull, 0, this.objektiKod);

						double nrSerialeve;

						if (!rreshtiAmortizimFillestar.Artikull.MeSerial)
						{
							double sasiaTotaleArtikull = clsHistorikAQTSeriale.merrIDHistorikuAQTSerialSipasIDAQT(rreshtiAmortizimFillestar.IdArtikulli, rreshtiAmortizimFillestar.IdNjesiAdministrative, kokaEAmortizimit.DateDokumenti);
							double sasiaPerSerial = clsHistorikAQTSeriale.merrHistorikuAQTSerialSasiSipasIDAQTSerialit(seriali.IdAQTSerial, kokaEAmortizimit.IdNdermarrje);

							if (sasiaPerSerial > 0)
								nrSerialeve = (double)sasiaTotaleArtikull / sasiaPerSerial;
							else
								nrSerialeve = 0;

						}
						else
							nrSerialeve = seriale.Count;
						if (amortizimiFillestar[i] != 0)
						{
							clsAmortizimiFillestar amort = new clsAmortizimiFillestar();
							amort.krijoObjekt(seriali.IdAQTSerial, karakteristikaStandartit.IdStandart, kokaEAmortizimit.IdAmortizimi, amortizimiFillestar[i] / nrSerialeve);
							serialeaqt.Add(amort);

						}
						//Llogarit vlerat e amortizimit.
						//ne parametrin e sasise do te vendoset sasiaTotaleArtikull/sasiaPerSerial qe duke qene se pjeston me pas amortizimet fillestare del formula
						//(amortizimet fillestare) * sasiaPerSerial / sasiaTotaleArtikull
						llogaritVleraAmortizimiAmortizimiFillestar(clsTrupiAmortRi, rreshtiAmortizimFillestar, karakteristikaStandartit, nrSerialeve);
						if (objektiKod == enumObjekteAmortizimi.ASETE)
						{
							if (rreshtiAmortizimFillestar.Artikull.MeSerial)
								clsTrupiAmortRi.VleftaGjendje = clsSerialetMagazine.merrSerialetMagazineSipasIDSerialDokFundit(clsTrupiAmortRi.IdAQTSeriali, karakteristikaStandartit.IdNdermarrje, kokaEAmortizimit.DateDokumenti, 0);
							else
								clsTrupiAmortRi.VleftaGjendje = clsHistorikAQTSeriale.merrHistorikuAQTSerialVlefteSipasIDAQTSerialit(clsTrupiAmortRi.IdAQTSeriali, karakteristikaStandartit.IdNdermarrje);
						}
						else clsTrupiAmortRi.VleftaGjendje = nrSerialeve > 0 ? rreshtiAmortizimFillestar.VleftaGjendje / nrSerialeve : rreshtiAmortizimFillestar.VleftaGjendje;
						trupiRi.Add(clsTrupiAmortRi);
						nrRreshtit++;
					}
				}
				i++;
			}

			Clear();
			AddRange(trupiRi);

			return new clsMesazh(true, "Trupi u krijua me sukses!");
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon te dhenat e reja per trupin e ri qe do i llogaritet amortizimi nga rivleresimi.
		/// Veprimet qe perdorin metoden: RIVLERESIMI
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="amortizimetEVjetraSeriale">(colAmortizimiTrupi) Te gjithe serialet e llogaritshem.</param> 
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
		private clsMesazh llogaritAmortizimiRivleresim(clsAmortizimiKoka kokaEAmortizimit, colAmortizimiTrupiAbstract amortizimetEVjetraSeriale, bool rivlersimXStandart, colAmortizimiTrupiAbstract amortizimetEVjetraSerialePas, out string mesazhmevonshem)
		{
			clsMesazh pergjigja = new clsMesazh(true, "Llogaritja e rivleresimit u krye me sukses!");
			mesazhmevonshem = "";
			int nrRreshtit = 0;
			colAmortizimiTrupiAbstract trupiRi = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
			//Merr te gjitha elementet e grides qe u eshte vendosur seriali pra jane analitike
			List<clsAmortizimiTrupiAbstract> trupRivleresimiMeSeriale = kokaEAmortizimit.ColTrupi.FindAll(x => x.IdAQTSeriali > 0);
			//Merr te gjitha elementet e grides qe nuk eshte vendosur seriali pra jane te pergjithshme dhe eshte vetem id e artikullit.
			List<clsAmortizimiTrupiAbstract> trupRivleresimiPaSeriale = kokaEAmortizimit.ColTrupi.FindAll(x => x.IdAQTSeriali == 0);

			int FAFanalitike = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(this.objektiKod == enumObjekteAmortizimi.ASETE ? "FAFanalitike" : "FAFanalitikeRezerve", kokaEAmortizimit.IdNdermarrje), FAFpermbledhese = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(this.objektiKod == enumObjekteAmortizimi.ASETE ? "FAFpermbledhese" : "FAFpermblRezerve", kokaEAmortizimit.IdNdermarrje), FAB = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("FAB", kokaEAmortizimit.IdNdermarrje), FASS = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("FASS", kokaEAmortizimit.IdNdermarrje);

			if (trupRivleresimiMeSeriale.Count > 0)
			{
				colAmortizimiTrupiAbstract trupRivleresimiOld = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
				trupRivleresimiOld.AddRange(amortizimetEVjetraSeriale.Where(p => trupRivleresimiMeSeriale.Any(p2 => p2.IdAQTSeriali == p.IdAQTSeriali)));
				if (trupRivleresimiOld.Count == 0)
					return new clsMesazh(false, "Nuk ka amortizim fillestar per serialet e dokumentit");


				foreach (clsAmortizimiTrupiAbstract rreshtRivleresimiOldRresht in trupRivleresimiOld)
				{
					if (mesazhmevonshem == "")
					{
						List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = amortizimetEVjetraSerialePas.FindAll(x => x.IdAQTSeriali == rreshtRivleresimiOldRresht.IdAQTSeriali);
						if (amortizimetTrupipas.Count() > 0)
							mesazhmevonshem = "Kujdes ka veprime te mevonshme me kete aset " + rreshtRivleresimiOldRresht.Serial;
					}
					double vleftaRePlusMinus = kokaEAmortizimit.ColTrupi.Find(x => x.IdAQTSeriali == rreshtRivleresimiOldRresht.IdAQTSeriali).VleftaPlusMinus;
					DbInventari.clsArtikulli artikulli = rreshtRivleresimiOldRresht.Artikull == null ? new DbInventari.clsArtikulli(rreshtRivleresimiOldRresht.IdArtikulli) : rreshtRivleresimiOldRresht.Artikull;
					clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(artikulli.Kodifikimi1Artikulli), kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdNdermarrje);
					DateTime dataAmortizimi = karakteristikaStandartit.konvertoDatenSipasKerkesesKonfigurimit(kokaEAmortizimit.DateAmortizimi, false, true);
					clsStandarteAmortizim standart = new clsStandarteAmortizim(kokaEAmortizimit.IdLlojStandarti);

					colAseteNormaAmortizimiAbstract aseteNorma = colAseteNormaAmortizimiAbstract.krijoInstance(this.objektiKod);
					aseteNorma.ktheArtikujNormaAmortizimiBrendaDatave(rreshtRivleresimiOldRresht.IdArtikulli, kokaEAmortizimit.IdLlojStandarti, rreshtRivleresimiOldRresht.DateAmortizimi, dataAmortizimi);
					if (aseteNorma.Count > 1)
						return new clsMesazh(false, String.Format("Serialit {0} per standartin {1} i duhet llogaritur me pare amortizimi pasi ka me shume norma se 1 gjate intervalit te pallogaritur!", artikulli.KodArtikulli, standart.Pershkrimi));

					clsAmortizimiTrupiAbstract clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(nrRreshtit, rreshtRivleresimiOldRresht.IdArtikulli, artikulli.PershkrimArtikulli,
						clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(rreshtRivleresimiOldRresht.IdArtikulli, kokaEAmortizimit.IdLlojStandarti, dataAmortizimi, this.objektiKod),
						rreshtRivleresimiOldRresht.DateAmortizimi, dataAmortizimi, dataAmortizimi, rreshtRivleresimiOldRresht.IdNjesiAdministrative,
						rreshtRivleresimiOldRresht.IdAQTSeriali, rreshtRivleresimiOldRresht.Serial, artikulli, 0, this.objektiKod);

					if (rivlersimXStandart)
					{
						clsTrupiAmortRi.VleftaPlusMinus = 0;
						clsTrupiAmortRi.VleftaShteseRivleresim = vleftaRePlusMinus;
					}
					else
					{
						clsTrupiAmortRi.VleftaPlusMinus = vleftaRePlusMinus;
						clsTrupiAmortRi.VleftaShteseRivleresim = 0;
					}

					//Llogarit vlerat e amortizimit.
					pergjigja.Status = llogaritVleraAmortizimiAtributeTePergjithshme(clsTrupiAmortRi, rreshtRivleresimiOldRresht, kokaEAmortizimit.IdKonfigurimAmbjenti, karakteristikaStandartit, 0, false, FAB, FASS, FAFpermbledhese, FAFanalitike, rivlersimXStandart, this.objektiKod);
					if (!pergjigja.Status)
						return pergjigja;
					trupiRi.Add(clsTrupiAmortRi);
					nrRreshtit++;
				}
			}

			if (trupRivleresimiPaSeriale.Count > 0)
			{
				colAmortizimiTrupiAbstract trupRivleresimiOld = colAmortizimiTrupiAbstract.krijoInstance(this.objektiKod);
				trupRivleresimiOld.AddRange(amortizimetEVjetraSeriale.Where(p => trupRivleresimiPaSeriale.Any(p2 => p2.IdArtikulli == p.IdArtikulli && p2.IdNjesiAdministrative == p.IdNjesiAdministrative)));
				if (trupRivleresimiOld.Count == 0)
					return new clsMesazh(false, "Nuk ka amortizim fillestar per serialet e dokumentit");
				foreach (clsAmortizimiTrupiAbstract rreshtRivleresimiOldRresht in trupRivleresimiOld)
				{

					double vleftaRePlusMinus = kokaEAmortizimit.ColTrupi.Find(x => x.IdArtikulli == rreshtRivleresimiOldRresht.IdArtikulli).VleftaPlusMinus;
					DbInventari.clsArtikulli artikulli = rreshtRivleresimiOldRresht.Artikull == null ? new DbInventari.clsArtikulli(rreshtRivleresimiOldRresht.IdArtikulli) : rreshtRivleresimiOldRresht.Artikull;
					if (mesazhmevonshem == "")
					{
						List<clsAmortizimiTrupiAbstract> amortizimetTrupipas = amortizimetEVjetraSerialePas.FindAll(x => x.IdArtikulli == rreshtRivleresimiOldRresht.IdArtikulli);
						if (amortizimetTrupipas.Count() > 0)
							mesazhmevonshem = "Kujdes ka veprime te mevonshme me kete aset " + artikulli.KodArtikulli;
					}

					clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(artikulli.Kodifikimi1Artikulli), kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdNdermarrje);
					clsStandarteAmortizim standart = new clsStandarteAmortizim(kokaEAmortizimit.IdLlojStandarti);
					DateTime dateAmortizimi = karakteristikaStandartit.konvertoDatenSipasKerkesesKonfigurimit(kokaEAmortizimit.DateAmortizimi, false, true);

					colAseteNormaAmortizimiAbstract aseteNorma = colAseteNormaAmortizimiAbstract.krijoInstance(this.objektiKod);
					aseteNorma.ktheArtikujNormaAmortizimiBrendaDatave(rreshtRivleresimiOldRresht.IdArtikulli, kokaEAmortizimit.IdLlojStandarti, rreshtRivleresimiOldRresht.DateAmortizimi, dateAmortizimi);
					if (aseteNorma.Count > 1)
						return new clsMesazh(false, String.Format("Serialit {0} per standartin {1} i duhet llogaritur me pare amortizimi pasi ka me shume norma se 1 gjate intervalit te pallogaritur!", artikulli.KodArtikulli, standart.Pershkrimi));

					clsAmortizimiTrupiAbstract clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(nrRreshtit, rreshtRivleresimiOldRresht.IdArtikulli,
						artikulli.PershkrimArtikulli, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(rreshtRivleresimiOldRresht.IdArtikulli, kokaEAmortizimit.IdLlojStandarti, dateAmortizimi, this.objektiKod),
						rreshtRivleresimiOldRresht.DateAmortizimi, dateAmortizimi, dateAmortizimi, rreshtRivleresimiOldRresht.IdNjesiAdministrative, rreshtRivleresimiOldRresht.IdAQTSeriali, rreshtRivleresimiOldRresht.Serial, artikulli, 0, this.objektiKod);

					//Nese eshte pa seriale atehere vlera qe kemi per rivleresim do te pjestohet me sa seriale ka per kete artikull
					colAQTSeriale seriale = new colAQTSeriale();
					seriale.merrAQTSerialSipasIDArtikulliIDMagazine(rreshtRivleresimiOldRresht.IdArtikulli, rreshtRivleresimiOldRresht.IdNjesiAdministrative, kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.DateDokumenti);

					if (!artikulli.MeSerial)
					{
						double sasiaTotaleArtikull = clsHistorikAQTSeriale.merrIDHistorikuAQTSerialSipasIDAQT(rreshtRivleresimiOldRresht.IdArtikulli, rreshtRivleresimiOldRresht.IdNjesiAdministrative, kokaEAmortizimit.DateDokumenti);
						double sasiaPerSerial = clsHistorikAQTSeriale.merrHistorikuAQTSerialSasiSipasIDAQTSerialit(rreshtRivleresimiOldRresht.IdAQTSeriali, kokaEAmortizimit.IdNdermarrje);
						if (rivlersimXStandart)
						{
							clsTrupiAmortRi.VleftaPlusMinus = 0;
							clsTrupiAmortRi.VleftaShteseRivleresim = vleftaRePlusMinus * sasiaPerSerial / sasiaTotaleArtikull;
						}
						else
						{
							clsTrupiAmortRi.VleftaPlusMinus = vleftaRePlusMinus * sasiaPerSerial / sasiaTotaleArtikull;
							clsTrupiAmortRi.VleftaShteseRivleresim = 0;
						}
					}
					else
					{
						if (rivlersimXStandart)
						{
							clsTrupiAmortRi.VleftaPlusMinus = 0;
							clsTrupiAmortRi.VleftaShteseRivleresim = vleftaRePlusMinus / seriale.Count;
						}
						else
						{
							clsTrupiAmortRi.VleftaPlusMinus = vleftaRePlusMinus / seriale.Count;
							clsTrupiAmortRi.VleftaShteseRivleresim = 0;
						}
					}


					//Llogarit vlerat e amortizimit.
					pergjigja.Status = llogaritVleraAmortizimiAtributeTePergjithshme(clsTrupiAmortRi, rreshtRivleresimiOldRresht, kokaEAmortizimit.IdKonfigurimAmbjenti, karakteristikaStandartit, 0, false, FAB, FASS, FAFpermbledhese, FAFanalitike, rivlersimXStandart, this.objektiKod);

					if (!pergjigja.Status)
						return pergjigja;

					trupiRi.Add(clsTrupiAmortRi);
					nrRreshtit++;
				}
			}

			Clear();
			AddRange(trupiRi);

			return pergjigja;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Krijon te dhenat e reja per trupin e ri qe do i llogaritet amortizimi nga shpendarja e shpenzimeve.
		/// Veprimet qe perdorin metoden: AMORTIZIMI FILLESTAR
		/// </summary>
		/// <param name="kokaEAmortizimit">(clsAmortizimiKoka) Koka e amortizimit qe po regjistrohet.</param> 
		/// <param name="seriale">(colSerialetMagazine) Lista e serialeve qe po behen dalje me kete dokument.</param> 
		/// <param name="kokamag"> dokumenti i magazines i krijuar gjate shperndarjes se shpenzimeve</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
		private bool llogaritAmortizimiShperndarjeShpenzimesh(clsAmortizimiKoka kokaEAmortizimit, colSerialetMagazine seriale, DbRegjistrim.clsKokaMagazina kokamag, bool llogaritvleraperserial)
		{
			bool pergjigja = true;
			int nrRendorFiks = 0;
			for (int i = 0; i < seriale.Count; i++)
			//foreach (clsSerialetMagazine seriali in seriale)
			{
				//Per rastet kur ka me shume se nje dokument ne shperndarje shpenzimet
				//Sa here qe njeh nje numerRendor == 0 do te thote qe eshte dokument i ri dhe ngrin numrin fiks per t'iu shtuar cdo numri rendor te dokumentit te ri.
				if (seriale[i].NrRendor == 0)
				{
					if (i == 0)
						nrRendorFiks = i;
					else if (seriale[i].IdDok != seriale[i - 1].IdDok)
						nrRendorFiks = nrRendorFiks + seriale[i - 1].NrRendor + 1;
				}

				clsAQTSeriale ser = new clsAQTSeriale();
				ser.merrAQTSerialSipasID(seriale[i].IdAQTSeriali);
				int idArtikullAktual = ser.IdAQTArt;// clsAQTSeriale.merrIDArtikullAQTSerialSipasID(seriali.IdAQTSeriali, dbasete);
				DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli(idArtikullAktual);
				clsKarakteristikaStandarti karakteristikaStandartit = clsKarakteristikaStandarti.merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(artikulli.Kodifikimi1Artikulli), kokaEAmortizimit.IdLlojStandarti, kokaEAmortizimit.IdNdermarrje);

				DateTime dateAmortizimi = karakteristikaStandartit.konvertoDatenSipasKerkesesKonfigurimit(kokaEAmortizimit.DateAmortizimi, true, true);
				clsAmortizimiTrupiAbstract clsTrupiAmortRi = clsAmortizimiTrupiAbstract.krijoInstance(seriale[i].NrRendor, idArtikullAktual, artikulli.PershkrimArtikulli, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(idArtikullAktual, kokaEAmortizimit.IdLlojStandarti, dateAmortizimi, this.objektiKod),
					dateAmortizimi, dateAmortizimi, dateAmortizimi, seriale[i].IdNjesiAdministrative, seriale[i].IdAQTSeriali, ser.AqtSerialKod, artikulli, 0, this.objektiKod);

				//float vleraShperndarje = (float)kokamag.OcolTrupiMagazina.Find(x => x.IdArtikulli == idArtikullAktual).Vlefta;
				float vleraShperndarje = (float)kokamag.OcolTrupiMagazina[nrRendorFiks + seriale[i].NrRendor].Vlefta;
				clsKokaMagazina dokMagazine = new clsKokaMagazina();
				pergjigja = dokMagazine.mbushKokaMagazinaSipasID(seriale[i].IdDok);
				dokMagazine.OcolTrupiMagazina.mbushGjitheTrupiMagazinaNgaKoka(dokMagazine.IdKokaMagazina);
				if (!pergjigja)
					return pergjigja;

				//float sasia = (float)dokMagazine.OcolTrupiMagazina.Find(x => x.IdArtikulli == idArtikullAktual).Sasia;
				float sasia = (float)dokMagazine.OcolTrupiMagazina[seriale[i].NrRendor].Sasia;
				//Llogarit vlerat e amortizimit.
				if (artikulli.MeSerial)
					llogaritVleraAmortizimiNgaShperndarjeShpenzime(clsTrupiAmortRi, dokMagazine.DtDok, kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.IdLlojStandarti, vleraShperndarje / sasia, seriale[i].NrRendDitor);
				else
					llogaritVleraAmortizimiNgaShperndarjeShpenzime(clsTrupiAmortRi, dokMagazine.DtDok, kokaEAmortizimit.IdNdermarrje, kokaEAmortizimit.IdLlojStandarti, vleraShperndarje, seriale[i].NrRendDitor);

				Add(clsTrupiAmortRi);
				if (llogaritvleraperserial)
				{
					//per te krijuar rreshtat e rinj me vlerat e reja te shperndarjes
					seriale[i].Vlefta = clsTrupiAmortRi.VleftaGjendje + clsTrupiAmortRi.VleftaPlusMinus;
					seriale[i].Cmimi = seriale[i].Vlefta / seriale[i].Sasia;
				}
			}
			return pergjigja;
		}

		#endregion

		#region Llogaritja e vlerave te amortizimet per raste te ndryshme

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e vlerave te reja te amortizimit.
		/// </summary>
		/// <param name="clsTrupiAmortRi">(clsAmortizimiTrupi) Trupi i amortizimit te ri qe po llogaritim amortizimin e ri.</param>
		/// <param name="trupAmortRreshtOld">(clsAmortizimiTrupi) Trupi i amortizimit te vjeter qe duhet te llogaritim amortizimin e ri.</param>
		/// <param name="llojDokumentiAktual">(int) Id e llojit te dokumentit qe po kryhet. Nevojitet per te llogaritur +1 ne amortizim nese eshte nevoja.</param>
		/// <param name="karakteristikaStandartit">(clsKarakteristikaStandarti) Karakteristikat e standartit qe po behet amortizimi.</param>
		/// <param name="idhistorikufundit">(int) </param>
		/// <param name="ndryshimstatusimagazine">(bool) </param>
		/// <param name="FAB"></param>
		/// <param name="FASS"></param>
		/// <param name="FAFpermbledhese"></param>
		/// <param name="FAFanalitike"></param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese llogaritja kryhet me sukses, ne te kundert False.</returns>
		private static bool llogaritVleraAmortizimiAtributeTePergjithshme(clsAmortizimiTrupiAbstract clsTrupiAmortRi, clsAmortizimiTrupiAbstract trupAmortRreshtOld, int llojDokumentiAktual, clsKarakteristikaStandarti karakteristikaStandartit, int idhistorikufundit, bool ndryshimstatusimagazine, int FAB, int FASS, int FAFpermbledhese, int FAFanalitike, bool rivleresimXStandarte, enumObjekteAmortizimi objektiKod)
		{
			bool pergjigje = true;
			clsTrupiAmortRi.HdAmortizimGjithsej = 0;
			clsTrupiAmortRi.HdAmortizimVjetor = 0;
			clsTrupiAmortRi.VleftaShteseRivleresim = clsTrupiAmortRi.VleftaShteseRivleresim;
			clsTrupiAmortRi.VleftaPlusMinus = clsTrupiAmortRi.VleftaPlusMinus;
			if (rivleresimXStandarte)
				clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje + trupAmortRreshtOld.VleftaPlusMinus + clsTrupiAmortRi.VleftaShteseRivleresim;
			else
				clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje + trupAmortRreshtOld.VleftaPlusMinus;
			clsTrupiAmortRi.AmortizimiGjithsej = trupAmortRreshtOld.AmortizimiGjithsej + trupAmortRreshtOld.AmortizimiShtese + trupAmortRreshtOld.HdAmortizimGjithsej;
			if (clsTrupiAmortRi.DateMePareAmortizimi.Year == clsTrupiAmortRi.DateAmortizimi.Year)
				clsTrupiAmortRi.AmortizimiVjetor = trupAmortRreshtOld.AmortizimiVjetor + trupAmortRreshtOld.HdAmortizimVjetor;
			else
				clsTrupiAmortRi.AmortizimiVjetor = trupAmortRreshtOld.AmortizimiGjithsej + trupAmortRreshtOld.AmortizimiShtese + +trupAmortRreshtOld.HdAmortizimGjithsej;

			//Llogarit ditet qe nuk eshte llogaritur amortizimi.
			//pergjigje = llogaritDitetAmortizimShtese(clsTrupiAmortRi, trupAmortRreshtOld, llojDokumentiAktual, karakteristikaStandartit, idhistorikufundit, ndryshimstatusimagazine, FAB, FASS, FAFanalitike, FAFpermbledhese);
			pergjigje = llogaritDitetAmortizimShtese(clsTrupiAmortRi, trupAmortRreshtOld, llojDokumentiAktual, karakteristikaStandartit, idhistorikufundit, ndryshimstatusimagazine, FAFanalitike, FAFpermbledhese, new clsDatabazeAsete());
			if (!pergjigje)
				return pergjigje;

			//Gjen normen e artikullit
			clsTrupiAmortRi.NormaAmortizimi = llogaritNormenAmortizimit(clsTrupiAmortRi.IdArtikulli, karakteristikaStandartit.IdStandart, clsTrupiAmortRi.IdNjesiAdministrative, clsTrupiAmortRi.DateAmortizimi, clsTrupiAmortRi.objektiKod, clsTrupiAmortRi.Artikull.KodArtikulli);

			string llojAmortizimi = clsAseteLlojAmortizimi.merrEmeritimiLlojAmortizimiSipasID(clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasID(clsTrupiAmortRi.IdArtikull_LlojAmortizimi, objektiKod));
			string llojAmortizimiVjeter = clsAseteLlojAmortizimi.merrEmeritimiLlojAmortizimiSipasID(clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasID(trupAmortRreshtOld.IdArtikull_LlojAmortizimi, objektiKod));

			if (llojAmortizimiVjeter == llojAmortizimi && llojAmortizimi == "Amortizim linear")
			{
				clsDatabazeAsete veprimePara = new clsDatabazeAsete();
				if (veprimePara.aKaVeprimeMeLlojiAmortizimiTeNdryshemMePara(trupAmortRreshtOld.IdArtikulli, trupAmortRreshtOld.DateAmortizimi, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasID(trupAmortRreshtOld.IdArtikull_LlojAmortizimi, objektiKod)))
				{
					if (rivleresimXStandarte) clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje - trupAmortRreshtOld.AmortizimiVjetor + clsTrupiAmortRi.VleftaShteseRivleresim + trupAmortRreshtOld.VleftaPlusMinus;
					else clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje - trupAmortRreshtOld.AmortizimiVjetor + trupAmortRreshtOld.VleftaPlusMinus;
				}
			}

			//Llogarit amortizimin shtese nga ditet e gjetura.
			if (rivleresimXStandarte)
			{
				if (llojAmortizimiVjeter == "Amortizim mbi vleren e mbetur" && llojAmortizimi == "Amortizim linear")
				{
					clsTrupiAmortRi.AmortizimiShtese = llogaritAmortizimShteseHeraEPare(clsTrupiAmortRi.VleftaGjendje, clsTrupiAmortRi.AmortizimiVjetor, clsTrupiAmortRi.NormaAmortizimi, (int)clsTrupiAmortRi.DiteAmortizimi, new DateTime(clsTrupiAmortRi.DateAmortizimi.Year, 12, 31).DayOfYear, llojAmortizimi);
					//Nese vlera absolute e diferences mids vleftes totale te asetit dhe vleres qe eshte amortizuar deri ne kete veprim eshte me e vogel se vlera qe normalisht duhet te amortizoje ne nje muaj
					//ath do amortizohet vetem sa diferenca e tyre.
					if (Math.Abs(clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej) < Math.Abs(clsTrupiAmortRi.AmortizimiShtese))
						clsTrupiAmortRi.AmortizimiShtese = clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej;
					clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje + trupAmortRreshtOld.VleftaPlusMinus;
				}
				else
				{
					clsTrupiAmortRi.AmortizimiShtese = llogaritAmortizimShtese(clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.VleftaShteseRivleresim, clsTrupiAmortRi.AmortizimiVjetor, clsTrupiAmortRi.NormaAmortizimi, (int)clsTrupiAmortRi.DiteAmortizimi, new DateTime(clsTrupiAmortRi.DateAmortizimi.Year, 12, 31).DayOfYear, llojAmortizimi);
					//Nese vlera absolute e diferences mids vleftes totale te asetit dhe vleres qe eshte amortizuar deri ne kete veprim eshte me e vogel se vlera qe normalisht duhet te amortizoje ne nje muaj
					//ath do amortizohet vetem sa diferenca e tyre.
					if (Math.Abs(clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.VleftaShteseRivleresim - clsTrupiAmortRi.AmortizimiGjithsej) < Math.Abs(clsTrupiAmortRi.AmortizimiShtese))
						clsTrupiAmortRi.AmortizimiShtese = clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej;

					if (llojAmortizimi == "Amortizim linear")
					{
						clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje + trupAmortRreshtOld.VleftaPlusMinus;
					}
				}
			}
			else
			{
				if (llojAmortizimiVjeter == "Amortizim mbi vleren e mbetur" && llojAmortizimi == "Amortizim linear")
				{
					clsTrupiAmortRi.AmortizimiShtese = llogaritAmortizimShteseHeraEPare(clsTrupiAmortRi.VleftaGjendje, clsTrupiAmortRi.AmortizimiVjetor, clsTrupiAmortRi.NormaAmortizimi, (int)clsTrupiAmortRi.DiteAmortizimi, new DateTime(clsTrupiAmortRi.DateAmortizimi.Year, 12, 31).DayOfYear, llojAmortizimi);
					//Nese vlera absolute e diferences mids vleftes totale te asetit dhe vleres qe eshte amortizuar deri ne kete veprim eshte me e vogel se vlera qe normalisht duhet te amortizoje ne nje muaj
					//ath do amortizohet vetem sa diferenca e tyre.
					if (Math.Abs(clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej) < Math.Abs(clsTrupiAmortRi.AmortizimiShtese))
						clsTrupiAmortRi.AmortizimiShtese = clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej;

					clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje + trupAmortRreshtOld.VleftaPlusMinus;
				}
				else
				{
					clsTrupiAmortRi.AmortizimiShtese = llogaritAmortizimShtese(clsTrupiAmortRi.VleftaGjendje, clsTrupiAmortRi.AmortizimiVjetor, clsTrupiAmortRi.NormaAmortizimi, (int)clsTrupiAmortRi.DiteAmortizimi, new DateTime(clsTrupiAmortRi.DateAmortizimi.Year, 12, 31).DayOfYear, llojAmortizimi);
					//Nese vlera absolute e diferences mids vleftes totale te asetit dhe vleres qe eshte amortizuar deri ne kete veprim eshte me e vogel se vlera qe normalisht duhet te amortizoje ne nje muaj
					//ath do amortizohet vetem sa diferenca e tyre.
					if (Math.Abs(clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej) < Math.Abs(clsTrupiAmortRi.AmortizimiShtese))
						clsTrupiAmortRi.AmortizimiShtese = clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej;

					if (llojAmortizimi == "Amortizim linear")
					{
						clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtOld.VleftaGjendje + trupAmortRreshtOld.VleftaPlusMinus;
					}
				}
			}



			return pergjigje;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e vlerave te reja te amortizimit per daljen nga magazina.
		/// </summary>
		/// <param name="clsTrupiAmortRi">(clsAmortizimiTrupi) Trupi i amortizimit te ri qe po llogaritim amortizimin e ri.</param>
		/// <param name="clsTrupiAmortVjeter">(clsAmortizimiTrupi) Trupi i amortizimit paraardhes qe na nevojiten per t'iu referuar vlerave.</param>
		/// <param name="llojDokumentiAktual">(int) Id e llojit te dokumentit qe po kryhet. Nevojitet per te llogaritur +1 ne amortizim nese eshte nevoja.</param>
		/// <param name="seriali">(clsSerialetMagazine) Seriali qe po i llogariten vlerat e amortizimit.</param>
		/// <param name="historikSerialiPrind">(clsHistorikAQTSeriale) Te dhenat e serialit te prindit te serialit per t'iu referuar ne llogaritje.</param>
		/// <param name="karakteristikaStandartit">(clsKarakteristikaStandarti) Karakteristikat e standartit qe po behet amortizimi.</param>
		/// <param name="idStandartAmortizimi">(int) Id e standartit te amortizimit.</param>
		/// <param name="FAB"></param>
		/// <param name="FASS"></param>
		/// <param name="FAFanalitike"></param>
		/// <param name="FAFpermbledhese"></param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese llogaritja kryhet me sukses, ne te kundert False.</returns>
		private static bool llogaritVleraAmortizimiNgaVeprimeMagazineDalje(clsAmortizimiTrupiAbstract clsTrupiAmortRi, clsAmortizimiTrupiAbstract clsTrupiAmortVjeter, int llojDokumentiAktual, clsSerialetMagazine seriali, double sasiaDaljeSerialit, clsHistorikAQTSeriale historikSerialiPrind, clsKarakteristikaStandarti karakteristikaStandartit, int FAB, int FASS, int FAFanalitike, int FAFpermbledhese, enumObjekteAmortizimi objektiKod)
		{
			bool pergjigje = true;

			//Llogarit vlerat e pergjithshme sipas rregullave te pergjithshme
			pergjigje = llogaritVleraAmortizimiAtributeTePergjithshme(clsTrupiAmortRi, clsTrupiAmortVjeter, llojDokumentiAktual, karakteristikaStandartit, 0, false, FAB, FASS, FAFpermbledhese, FAFanalitike, false, objektiKod);
			if (!pergjigje)
				return pergjigje;

			double sasiaProgresive;

			if (historikSerialiPrind != null && historikSerialiPrind.IdHistoriku > 0)
				sasiaProgresive = historikSerialiPrind.SasiaProgresive + sasiaDaljeSerialit;
			else
				sasiaProgresive = 1;

			//Llogarit vlerat qe ndryshojne nga rastet e pergjithshme
			if (objektiKod == enumObjekteAmortizimi.REZERVA)
				clsTrupiAmortRi.VleftaPlusMinus = -clsTrupiAmortRi.VleftaGjendje;
			else clsTrupiAmortRi.VleftaPlusMinus = -seriali.Vlefta;
			//clsTrupiAmortRi.HdAmortizimGjithsej = -seriali.Sasia * (clsTrupiAmortRi.AmortizimiGjithsej + clsTrupiAmortRi.AmortizimiShtese) / sasiaProgresive;
			clsTrupiAmortRi.HdAmortizimGjithsej = clsTrupiAmortRi.VleftaGjendje == 0 ? 0 : (clsTrupiAmortRi.VleftaPlusMinus * (clsTrupiAmortRi.AmortizimiGjithsej + clsTrupiAmortRi.AmortizimiShtese) / clsTrupiAmortRi.VleftaGjendje);
			clsTrupiAmortRi.HdAmortizimVjetor = clsTrupiAmortRi.VleftaGjendje == 0 ? 0 : clsTrupiAmortRi.VleftaPlusMinus * clsTrupiAmortRi.AmortizimiVjetor / clsTrupiAmortRi.VleftaGjendje;

			return pergjigje;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e vlerave te reja te amortizimit per blerjet.
		/// </summary>
		/// <param name="clsTrupiAmortRi">(clsAmortizimiTrupi) Trupi i amortizimit te ri qe po llogaritim amortizimin e ri.</param>
		/// <param name="seriali">(clsSerialetMagazine) Seriali qe po i llogariten vlerat e amortizimit.</param>
		/// <param name="karakteristikaStandartit">(clsKarakteristikaStandarti) Karakteristikat e standartit qe po behet amortizimi.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		private static void llogaritVleraAmortizimiNgaVeprimeMagazineBlerje(clsAmortizimiTrupiAbstract clsTrupiAmortRi, clsSerialetMagazine seriali, clsKarakteristikaStandarti karakteristikaStandartit)
		{
			//Per FAB
			clsTrupiAmortRi.AmortizimiGjithsej = 0;
			clsTrupiAmortRi.AmortizimiVjetor = 0;
			clsTrupiAmortRi.AmortizimiShtese = 0;
			clsTrupiAmortRi.HdAmortizimGjithsej = 0;
			clsTrupiAmortRi.HdAmortizimVjetor = 0;
			clsTrupiAmortRi.DiteAmortizimi = 0;
			clsTrupiAmortRi.VleftaGjendje = 0;
			clsTrupiAmortRi.VleftaPlusMinus = seriali.Vlefta;
			//Gjen normen e artikullit
			clsTrupiAmortRi.NormaAmortizimi = llogaritNormenAmortizimit(clsTrupiAmortRi.IdArtikulli, karakteristikaStandartit.IdStandart, clsTrupiAmortRi.IdNjesiAdministrative, clsTrupiAmortRi.DateAmortizimi, clsTrupiAmortRi.objektiKod, clsTrupiAmortRi.Artikull.KodArtikulli);
		}

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e vlerave te reja te amortizimit nga amortizimi fillestar.
		/// </summary>
		/// <param name="clsTrupiAmortRi">(clsAmortizimiTrupi) Trupi i amortizimit te ri qe po llogaritim amortizimin e ri.</param>
		/// <param name="trupAmortRreshtGride">(clsAmortizimiTrupi) Trupi i amortizimit te vjeter qe duhet te llogaritim amortizimin e ri.</param>
		/// <param name="karakteristikaStandartit">(clsKarakteristikaStandarti) Karakteristikat e standartit qe po behet amortizimi.</param>
		/// <param name="nrSerialeve">(float) Numri serial per sa do te ndahet amortizimet e meparshme.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		private static void llogaritVleraAmortizimiAmortizimiFillestar(clsAmortizimiTrupiAbstract clsTrupiAmortRi, clsAmortizimiTrupiAbstract trupAmortRreshtGride, clsKarakteristikaStandarti karakteristikaStandartit, double nrSerialeve)
		{
			//Per FAF
			if (nrSerialeve > 0)
			{
				clsTrupiAmortRi.AmortizimiGjithsej = trupAmortRreshtGride.AmortizimiGjithsej / nrSerialeve;
				clsTrupiAmortRi.AmortizimiVjetor = trupAmortRreshtGride.AmortizimiVjetor / nrSerialeve;
			}
			else
			{
				clsTrupiAmortRi.AmortizimiGjithsej = 0;
				clsTrupiAmortRi.AmortizimiVjetor = 0;
			}

			clsTrupiAmortRi.AmortizimiShtese = 0;
			clsTrupiAmortRi.HdAmortizimGjithsej = 0;
			clsTrupiAmortRi.HdAmortizimVjetor = 0;
			clsTrupiAmortRi.DiteAmortizimi = 0;
			clsTrupiAmortRi.VleftaPlusMinus = 0;

			//Gjen normen e artikullit
			clsTrupiAmortRi.NormaAmortizimi = llogaritNormenAmortizimit(clsTrupiAmortRi.IdArtikulli, karakteristikaStandartit.IdStandart, clsTrupiAmortRi.IdNjesiAdministrative, clsTrupiAmortRi.DateAmortizimi, clsTrupiAmortRi.objektiKod, clsTrupiAmortRi.Artikull.KodArtikulli);
		}

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e vlerave te reja te amortizimit per blerjet.
		/// </summary>
		/// <param name="clsTrupiAmortRi">(clsAmortizimiTrupi) Trupi i amortizimit te ri qe po llogaritim amortizimin e ri.</param>
		/// <param name="seriali">(clsSerialetMagazine) Seriali qe po i llogariten vlerat e amortizimit.</param>
		/// <param name="idStandarti">(int) Id e llojit te standartit.</param>
		/// <param name="vleraShperndarje">(float) Vlera e shperndarjes se shpenzimeve ne raport me nr e serialeve nga dokumenti i magazines.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		private static void llogaritVleraAmortizimiNgaShperndarjeShpenzime(clsAmortizimiTrupiAbstract clsTrupiAmortRi, DateTime dateDok, int idndermarje, int idStandarti, float vleraShperndarje, int nrrendorditor)
		{
			//Per FASS
			clsTrupiAmortRi.AmortizimiGjithsej = 0;
			clsTrupiAmortRi.AmortizimiVjetor = 0;
			clsTrupiAmortRi.AmortizimiShtese = 0;
			clsTrupiAmortRi.HdAmortizimGjithsej = 0;
			clsTrupiAmortRi.HdAmortizimVjetor = 0;
			clsTrupiAmortRi.DiteAmortizimi = 0;
			//clsTrupiAmortRi.VleftaGjendje = vleftaGjendja;
			clsTrupiAmortRi.VleftaGjendje = clsSerialetMagazine.ktheSerialetMagazineSipasIDSerialDheDates(clsTrupiAmortRi.IdAQTSeriali, idndermarje, dateDok, nrrendorditor);
			clsTrupiAmortRi.VleftaPlusMinus = vleraShperndarje;

			//Gjen normen e artikullit
			clsTrupiAmortRi.NormaAmortizimi = llogaritNormenAmortizimit(clsTrupiAmortRi.IdArtikulli, idStandarti, clsTrupiAmortRi.IdNjesiAdministrative, clsTrupiAmortRi.DateAmortizimi, clsTrupiAmortRi.objektiKod, clsTrupiAmortRi.Artikull.KodArtikulli);
		}

		#endregion

		#region Llogaritjen numerike te vlerave per te gjitha metodat

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e e atributeve per rillogaritjen.
		/// </summary>
		/// <param name="clsTrupiAmortRi">(clsAmortizimiTrupi) Trupi i amortizimit te ri qe po llogaritim amortizimin e ri.</param>
		/// <param name="trupAmortRreshtVjeter">(clsAmortizimiTrupi) Trupi i amortizimit te vjeter qe duhet te llogaritim amortizimin e ri.</param>
		/// <param name="idKonfigAmbjenti">(int) Id e llojit te dokumentit.</param>
		/// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
		/// <param name="idLlojStandarti">(int) Id e llojit te standartit.</param>
		/// <param name="karakteristikaStandarti">(clsKarakteristikaStandarti) Merr karakteristikat e standartit.</param>
		/// <param name="meSerial">True nese llogaritja do te behet per artikujt me serial dhe False nese llogaritja do te behet per artikujt pa serial</param>
		/// <param name="FA"></param>
		/// <param name="FAB"></param>
		/// <param name="FAS"></param>
		/// <param name="FAD"></param>
		/// <param name="FADT"></param>
		/// <param name="FADTK"></param>
		/// <param name="FANS"></param>
		/// <param name="FASS"></param>
		/// <param name="FRApermbledhese"></param>
		/// <param name="FRAanalitike"></param>
		/// <param name="FAFpermbledhese"></param>
		/// <param name="FAFanalitike"></param>
		/// <param name="dbAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese krijimi kryhet me sukses, ne te kundert False.</returns>
		private static bool llogaritAtributetPerRillogaritje(clsAmortizimiTrupiAbstract clsTrupiAmortRi, clsAmortizimiTrupiAbstract trupAmortRreshtVjeter, int idKonfigAmbjenti, int idNdermarrje, int idLlojStandarti, clsKarakteristikaStandarti karakteristikaStandarti, bool meSerial, int FA, int FAB, int FAS, int FAD, int FADT, int FADTK, int FANS, int FASS, int FRApermbledhese, int FRAanalitike, int FAFpermbledhese, int FAFanalitike, string llojAmortizimi, clsDatabazeAsete dbAsete)
		{
			bool pergjigje = true;
			if (idKonfigAmbjenti == FAFanalitike || idKonfigAmbjenti == FAFpermbledhese)
				return pergjigje;
			clsTrupiAmortRi.DateMePareAmortizimi = trupAmortRreshtVjeter.DateAmortizimi;

			if (FANS != idKonfigAmbjenti)
				clsTrupiAmortRi.DateNdryshimStatusMagazine = trupAmortRreshtVjeter.DateNdryshimStatusMagazine;

			clsTrupiAmortRi.NormaAmortizimi = llogaritNormenAmortizimit(clsTrupiAmortRi.IdArtikulli, idLlojStandarti, clsTrupiAmortRi.IdNjesiAdministrative, clsTrupiAmortRi.DateAmortizimi, clsTrupiAmortRi.objektiKod, clsTrupiAmortRi.Artikull.KodArtikulli);

			//Llogarit ditet qe nuk eshte llogaritur amortizimi.
			pergjigje = llogaritDitetAmortizimShtese(clsTrupiAmortRi, trupAmortRreshtVjeter, idKonfigAmbjenti, karakteristikaStandarti, 0, false, FAFanalitike, FAFpermbledhese, dbAsete);

			if (!pergjigje)
				return pergjigje;

			clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtVjeter.VleftaGjendje + trupAmortRreshtVjeter.VleftaPlusMinus;
			if (clsTrupiAmortRi.DateMePareAmortizimi.Year == clsTrupiAmortRi.DateAmortizimi.Year)
				clsTrupiAmortRi.AmortizimiVjetor = trupAmortRreshtVjeter.AmortizimiVjetor + trupAmortRreshtVjeter.HdAmortizimVjetor;
			else
				clsTrupiAmortRi.AmortizimiVjetor = trupAmortRreshtVjeter.AmortizimiGjithsej + trupAmortRreshtVjeter.AmortizimiShtese + trupAmortRreshtVjeter.HdAmortizimGjithsej;

			string llojAmortizimiRi = clsAseteLlojAmortizimi.merrEmeritimiLlojAmortizimiSipasID(clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasID(clsTrupiAmortRi.IdArtikull_LlojAmortizimi, clsTrupiAmortRi.objektiKod));
			string llojAmortizimiVjeter = clsAseteLlojAmortizimi.merrEmeritimiLlojAmortizimiSipasID(clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasID(trupAmortRreshtVjeter.IdArtikull_LlojAmortizimi, trupAmortRreshtVjeter.objektiKod));

			if (llojAmortizimiVjeter == llojAmortizimi && llojAmortizimi == "Amortizim linear")
			{
				clsDatabazeAsete veprimePara = new clsDatabazeAsete();
				if (veprimePara.aKaVeprimeMeLlojiAmortizimiTeNdryshemMePara(trupAmortRreshtVjeter.IdArtikulli, trupAmortRreshtVjeter.DateAmortizimi, clsAseteNormaAmortizimiAbstract.merrIDLlojAmortizimiNormaAmortizimiSipasID(trupAmortRreshtVjeter.IdArtikull_LlojAmortizimi, trupAmortRreshtVjeter.objektiKod)))
				{
					clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtVjeter.VleftaGjendje - trupAmortRreshtVjeter.AmortizimiVjetor + trupAmortRreshtVjeter.VleftaPlusMinus;
				}
			}

			if (llojAmortizimiVjeter == "Amortizim mbi vleren e mbetur" && llojAmortizimi == "Amortizim linear")
			{
				clsTrupiAmortRi.AmortizimiShtese = llogaritAmortizimShteseHeraEPare(clsTrupiAmortRi.VleftaGjendje, clsTrupiAmortRi.AmortizimiVjetor, clsTrupiAmortRi.NormaAmortizimi, (int)clsTrupiAmortRi.DiteAmortizimi, new DateTime(clsTrupiAmortRi.DateAmortizimi.Year, 12, 31).DayOfYear, llojAmortizimi);

			}
			else clsTrupiAmortRi.AmortizimiShtese = llogaritAmortizimShtese(clsTrupiAmortRi.VleftaGjendje, clsTrupiAmortRi.AmortizimiVjetor, clsTrupiAmortRi.NormaAmortizimi, (int)clsTrupiAmortRi.DiteAmortizimi, new DateTime(clsTrupiAmortRi.DateAmortizimi.Year, 12, 31).DayOfYear, llojAmortizimi);

			clsTrupiAmortRi.AmortizimiGjithsej = trupAmortRreshtVjeter.HdAmortizimGjithsej + trupAmortRreshtVjeter.AmortizimiGjithsej + trupAmortRreshtVjeter.AmortizimiShtese;

			if (Math.Abs(clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej) < Math.Abs(clsTrupiAmortRi.AmortizimiShtese))
				clsTrupiAmortRi.AmortizimiShtese = clsTrupiAmortRi.VleftaGjendje - clsTrupiAmortRi.AmortizimiGjithsej;

			if (llojAmortizimi == "Amortizim linear")
			{
				clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtVjeter.VleftaGjendje + trupAmortRreshtVjeter.VleftaPlusMinus;

			}
			if (FA != idKonfigAmbjenti && FANS != idKonfigAmbjenti && FRAanalitike != idKonfigAmbjenti && FRApermbledhese != idKonfigAmbjenti && FAFanalitike != idKonfigAmbjenti && FAFpermbledhese != idKonfigAmbjenti)
			{
				if (FASS != idKonfigAmbjenti)
				{
					if (FADT != idKonfigAmbjenti && FADTK != idKonfigAmbjenti && FAD != idKonfigAmbjenti && FAS != idKonfigAmbjenti)
					{
						clsTrupiAmortRi.HdAmortizimVjetor = -(clsTrupiAmortRi.AmortizimiVjetor + trupAmortRreshtVjeter.HdAmortizimVjetor);
						clsTrupiAmortRi.HdAmortizimGjithsej = -(clsTrupiAmortRi.AmortizimiGjithsej + trupAmortRreshtVjeter.HdAmortizimGjithsej + clsTrupiAmortRi.AmortizimiShtese);

						if (meSerial)
							clsTrupiAmortRi.VleftaPlusMinus = trupAmortRreshtVjeter.VleftaGjendje;
						else
						{
							clsAmortizimiKoka kokaAmortizimi = new clsAmortizimiKoka();
							clsAmortizimiKoka kokaAmortizimiGjeneruese = new clsAmortizimiKoka();
							kokaAmortizimi.ktheAmortizimKokaSipasId(clsTrupiAmortRi.IdAmortizimKoka);
							kokaAmortizimiGjeneruese.ktheAmortizimKokaSipasId(kokaAmortizimi.IdDokGjenerues);

							//Merr serialin nga eshte gjeneruar per te marre sasine totale.
							colSerialetMagazine serialetDalje = new colSerialetMagazine();
							serialetDalje.merrSerialetMagazineSipasIDSeriali(clsTrupiAmortRi.IdPrind, idNdermarrje);
							double sasiDaljeTotale = serialetDalje.FindAll(x => x.IdDok == kokaAmortizimiGjeneruese.IdDokGjenerues).Sum(item => item.Sasia);
							//Merr te dhenat per hyrjet e serialit
							colSerialetMagazine serialetHyrje = new colSerialetMagazine();
							//Merr dokumentin e hyrjes per te gjetur sasiHyrje
							DbRegjistrim.clsKokaMagazina magazineHyrje = new clsKokaMagazina();
							magazineHyrje.mbushKokaMagazinaSipasIDGjenerues(kokaAmortizimiGjeneruese.IdDokGjenerues, 1, kokaAmortizimiGjeneruese.IdKonfigGjenerues);
							serialetHyrje.merrSerialetMagazineSipasIDSeriali(clsTrupiAmortRi.IdAQTSeriali, idNdermarrje);
							double sasiTotaleHyrje = serialetHyrje.FindAll(x => x.IdDok == magazineHyrje.IdKokaMagazina).Sum(item => item.Sasia);

							clsTrupiAmortRi.HdAmortizimVjetor = -trupAmortRreshtVjeter.HdAmortizimVjetor * sasiTotaleHyrje / sasiDaljeTotale;
							clsTrupiAmortRi.HdAmortizimGjithsej = -trupAmortRreshtVjeter.HdAmortizimGjithsej * sasiTotaleHyrje / sasiDaljeTotale;
							clsTrupiAmortRi.VleftaPlusMinus = -trupAmortRreshtVjeter.VleftaPlusMinus * sasiTotaleHyrje / sasiDaljeTotale;

							//Kur dokumenti eshte FAHT ose FAHTK zeron vlerat me poshte per rastet pa serial.
							clsTrupiAmortRi.VleftaGjendje = 0;
							clsTrupiAmortRi.AmortizimiVjetor = 0;
							clsTrupiAmortRi.AmortizimiShtese = 0;
							clsTrupiAmortRi.AmortizimiGjithsej = 0;
						}
					}
					else
					{
						clsTrupiAmortRi.HdAmortizimVjetor = -(clsTrupiAmortRi.AmortizimiVjetor);
						clsTrupiAmortRi.HdAmortizimGjithsej = -(clsTrupiAmortRi.AmortizimiGjithsej + clsTrupiAmortRi.AmortizimiShtese);

						if (meSerial)
							clsTrupiAmortRi.VleftaPlusMinus = -clsTrupiAmortRi.VleftaGjendje;
						else
						{
							clsAmortizimiKoka kokaAmortizimi = new clsAmortizimiKoka();
							kokaAmortizimi.ktheAmortizimKokaSipasId(clsTrupiAmortRi.IdAmortizimKoka);
							colSerialetMagazine serialet = new colSerialetMagazine();
							serialet.merrSerialetMagazineSipasIDSeriali(clsTrupiAmortRi.IdAQTSeriali, idNdermarrje);
							double vleftaDalje;
							if (FAS != idKonfigAmbjenti)
								vleftaDalje = serialet.FindAll(x => x.IdDok == kokaAmortizimi.IdDokGjenerues).Sum(item => item.Vlefta);
							else
								vleftaDalje = serialet.FindAll(x => DbRegjistrim.clsKokaMagazina.merrIdGjenerues(x.IdDok) == kokaAmortizimi.IdDokGjenerues).Sum(item => item.Vlefta);

							if (clsTrupiAmortRi.VleftaGjendje != 0)
							{
								clsTrupiAmortRi.HdAmortizimVjetor = (clsTrupiAmortRi.HdAmortizimVjetor) * vleftaDalje / clsTrupiAmortRi.VleftaGjendje;
								clsTrupiAmortRi.HdAmortizimGjithsej = (clsTrupiAmortRi.HdAmortizimGjithsej) * vleftaDalje / clsTrupiAmortRi.VleftaGjendje;
							}
							else
							{
								clsTrupiAmortRi.HdAmortizimVjetor = 0;
								clsTrupiAmortRi.HdAmortizimGjithsej = 0;
							}
							clsTrupiAmortRi.VleftaPlusMinus = -vleftaDalje;
						}
						clsTrupiAmortRi.IdNjesiAdministrative = trupAmortRreshtVjeter.IdNjesiAdministrative;
					}
				}
			}
			else
			{
				//Per dokumentat FRAanalitike, FRApermbledhese nuk preket vlefta plusminus se vendoset me dore nga perdoruesi
				if (FRAanalitike != idKonfigAmbjenti && FRApermbledhese != idKonfigAmbjenti)
				{
					clsTrupiAmortRi.VleftaPlusMinus = 0;
				}

				if (FANS != idKonfigAmbjenti)
					clsTrupiAmortRi.IdNjesiAdministrative = trupAmortRreshtVjeter.IdNjesiAdministrative;

				clsTrupiAmortRi.HdAmortizimVjetor = 0;
				clsTrupiAmortRi.HdAmortizimGjithsej = 0;
			}

			if (llojAmortizimi == "Amortizim linear")
			{
				clsTrupiAmortRi.VleftaGjendje = trupAmortRreshtVjeter.VleftaGjendje + trupAmortRreshtVjeter.VleftaPlusMinus;
			}

			return pergjigje;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e diteve shtese te amortizimit.
		/// </summary>
		/// <param name="clsTrupiAmortRi">(clsAmortizimiTrupi) Trupi i amortizimit te ri qe po llogaritim amortizimin e ri.</param>
		/// <param name="trupAmortRresht">(clsAmortizimiTrupi) Trupi i amortizimit te vjeter qe duhet te llogaritim amortizimin e ri.</param>
		/// <param name="llojiDokumentitAktual">(int) Id e llojit te dokumentit qe po kryhet. Nevojitet per te llogaritur +1 ne amortizim nese eshte nevoja.</param>
		/// <param name="karakteristikaStandarti">(clsKarakteristikaStandarti) Karakteristikat e standartit qe po behet amortizimi.</param>
		/// <param name="idhistorikufundit">(int) </param>
		/// <param name="ndryshimstatusimagazine">(bool) </param>
		/// <param name="FAB"></param>
		/// <param name="FASS"></param>
		/// <param name="FAFanalitike"></param>
		/// <param name="FAFpermbledhese"></param>
		/// <param name="dbAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese llogaritja kryhet me sukses, ne te kundert False.</returns>
		//private static bool llogaritDitetAmortizimShtese(clsAmortizimiTrupiAbstract clsTrupiAmortRi, clsAmortizimiTrupiAbstract trupAmortRresht, int llojiDokumentitAktual, clsKarakteristikaStandarti karakteristikaStandarti, int idhistorikufundit, bool ndryshimstatusimagazine, int FAB, int FASS, int FAFanalitike, int FAFpermbledhese)
		private static bool llogaritDitetAmortizimShtese(clsAmortizimiTrupiAbstract clsTrupiAmortRi, clsAmortizimiTrupiAbstract trupAmortRresht, int llojiDokumentitAktual, clsKarakteristikaStandarti karakteristikaStandarti, int idhistorikufundit, bool ndryshimstatusimagazine, int FAFanalitike, int FAFpermbledhese, clsDatabazeAsete dbAsete)
		{
			if ((float)(clsTrupiAmortRi.DateAmortizimi - clsTrupiAmortRi.DateMePareAmortizimi).TotalDays < 0)
				clsTrupiAmortRi.DateAmortizimi = clsTrupiAmortRi.DateMePareAmortizimi;

			bool pergjigje = true;
			if (llojiDokumentitAktual == FAFanalitike || llojiDokumentitAktual == FAFpermbledhese)
				return pergjigje;

			clsKarakteristikaStandartiTrupi karakteristikaTrupiStandart = new clsKarakteristikaStandartiTrupi();
			int idStatusMagazine = 0;
			if (ndryshimstatusimagazine)
			{
				clsHistorikStatusMagazine historikifundit = new clsHistorikStatusMagazine();
				historikifundit.merrHistorikMagazinaSipasIdHistoriku(idhistorikufundit);
				idStatusMagazine = historikifundit.IdStatusMagazine;

			}
			else
				idStatusMagazine = clsHistorikStatusMagazine.merrIDStatusMagazinaSipasIdNjesiAdministrativeDateStatusi(trupAmortRresht.IdNjesiAdministrative, clsTrupiAmortRi.DateAmortizimi);

			if (idStatusMagazine == -1)
				throw new MyException($"Magazina me kod {new clsNjesiAdministrative(trupAmortRresht.IdNjesiAdministrative).Kodi} nuk ka status magazine!");

			pergjigje = karakteristikaTrupiStandart.merrKonfigurimStandartiTrupiSipasIDKokaIDStatus(karakteristikaStandarti.IdKarakteristika, idStatusMagazine, dbAsete);

			if (!pergjigje)
				return pergjigje;
			karakteristikaStandarti.ColTrupi.Add(karakteristikaTrupiStandart);

			//Llogariten diten e amortizimit shtese sipas parametrave te marre me siper.
			if (karakteristikaStandarti.ColTrupi[0].LlogaritAmortizim)
			{
				//Kontrollohet nese amortizimi duhet llogaritur pas disa muajsh per standartin.
				if (karakteristikaStandarti.ColTrupi[0].FilloAmortiziminPas > 0)
				{
					//pjestohet me nje mesatare ditesh mujore qe te nxjerri numrin e muajve
					int muajTeKaluar = (int)((clsTrupiAmortRi.DateAmortizimi - clsTrupiAmortRi.DateNdryshimStatusMagazine).TotalDays / 30.4);

					//Nese kane kaluar me shume muaj ne magazine sesa koha qe duhet te prese pa amortizim ath llogariten ditet, ne te kundert nuk llogariten dite amoritizmi.
					if (muajTeKaluar >= karakteristikaStandarti.ColTrupi[0].FilloAmortiziminPas)
						if (clsTrupiAmortRi.DateNdryshimStatusMagazine.AddMonths(karakteristikaStandarti.ColTrupi[0].FilloAmortiziminPas) > clsTrupiAmortRi.DateMePareAmortizimi)
							clsTrupiAmortRi.DiteAmortizimi = (float)(clsTrupiAmortRi.DateAmortizimi - clsTrupiAmortRi.DateNdryshimStatusMagazine.AddMonths(karakteristikaStandarti.ColTrupi[0].FilloAmortiziminPas)).TotalDays;
						else
							clsTrupiAmortRi.DiteAmortizimi = (float)(clsTrupiAmortRi.DateAmortizimi - clsTrupiAmortRi.DateMePareAmortizimi).TotalDays;
					else
					{
						clsTrupiAmortRi.DiteAmortizimi = 0;
						return pergjigje;
					}
				}
				else
				{
					clsTrupiAmortRi.DiteAmortizimi = (float)(clsTrupiAmortRi.DateAmortizimi - clsTrupiAmortRi.DateMePareAmortizimi).TotalDays;
				}

				//Perfshihet dita e pare nese ka vetem nje dokument me pare.
				if (karakteristikaStandarti.PerfshihetDitaPare)
				{
					clsAmortizimiKoka amortizimiVjeter = new clsAmortizimiKoka();
					amortizimiVjeter.ktheAmortizimKokaSipasId(trupAmortRresht.IdAmortizimKoka);
					//Kur dokumenti aktual eshte i ndryshem nga FAB dhe FASS dhe FAF dhe dokumenti i meparshem eshte FAB ose FASS ose FAF duhet llogaritur +1 dite
					/*if ((FASS != llojiDokumentitAktual && FAB != llojiDokumentitAktual
                        && FAFpermbledhese != llojiDokumentitAktual && FAFanalitike != llojiDokumentitAktual) && (FASS == amortizimiVjeter.IdKonfigurimAmbjenti
                        || FAB == amortizimiVjeter.IdKonfigurimAmbjenti || FAFpermbledhese == amortizimiVjeter.IdKonfigurimAmbjenti || FAFanalitike == amortizimiVjeter.IdKonfigurimAmbjenti))*/
					if (clsTrupiAmortRi.AmortizimAkumuluar == 0)
					{
						//U kthye ne baze te llojeve te dokumentave, pasi kishte raste te paparashikuara nga procedura e perdorur.
						//if (clsAmortizimiTrupi.merrNrAmortizimTrupiSipasIdSerialiDateAmortizimi(clsTrupiAmortRi.IdAQTSeriali, clsTrupiAmortRi.DateAmortizimi, idStandartAmortizimi, dbAsete) == 1)
						clsTrupiAmortRi.DiteAmortizimi = clsTrupiAmortRi.DiteAmortizimi + 1;
					}
				}

				return pergjigje;
			}
			clsTrupiAmortRi.DiteAmortizimi = 0;
			return pergjigje;
		}

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e amortizimit shtese te serialit.
		/// </summary>
		/// <param name="vleftaGjendja">(float) Vlefta gjendje e amortizimit te ri.</param>
		/// <param name="amortizimVjetor">(float) Amortizimi vjetor me gjithe amortizimin e ri.</param>
		/// <param name="normeAmort">(float) Norma e amortizimit te artikullit.</param>
		/// <param name="diteAmort">(int) Ditet per te cilat duhet te llogaritet amortizimi.</param>
		/// <param name="diteViti">(int) Ditet e vitit, duke dalluar dhe vitin e brishte.</param>
		/// <param name="idLlojAmortizimi">(int) Id e llojit te amortizimit.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen True nese llogaritja kryhet me sukses, ne te kundert False.</returns>
		private static double llogaritAmortizimShtese(double vleftaGjendja, double amortizimVjetor, double normeAmort, int diteAmort, double diteViti, string llojAmortizimi)
		{
			if (llojAmortizimi == clsAseteLlojAmortizimi.PA_AMORTIZIM)
				return 0;
			if (llojAmortizimi == clsAseteLlojAmortizimi.AMORTIZIM_LINEAR)
				return (vleftaGjendja * (double)(normeAmort / 100) * (double)(diteAmort / diteViti));
			if (llojAmortizimi == clsAseteLlojAmortizimi.AMORTIZIM_MBI_VLERE_TE_SHTUAR)
				return ((vleftaGjendja - amortizimVjetor) * (double)(normeAmort / 100) * (double)(diteAmort / diteViti));
			return 0;
		}
		private static double llogaritAmortizimShteseHeraEPare(double vleftaGjendja, double amortizimVjetor, double normeAmort, int diteAmort, double diteViti, string llojAmortizimi)
		{
			return ((vleftaGjendja - amortizimVjetor) * (double)(normeAmort / 100) * (double)(diteAmort / diteViti));
		}

		/// <summary>
		/// MODULI ASETE:
		/// Llogaritja e normes se amortizimit.
		/// </summary>
		/// <param name="idArtikulli">(int) Id e artikullit qe do i llogaritet norma.</param>
		/// <param name="idStandarti">(int) Id e standartit per te cilin do llogaritet norma.</param>
		/// <param name="idNjesiAdministrative">(int) Id e magazines per te cilin do llogaritet norma.</param>
		/// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
		/// <returns>Kthen normen float nese gjen nje te tille, ne te kundert do te ktheje 0.</returns>
		private static double llogaritNormenAmortizimit(int idArtikulli, int idStandarti, int idNjesiAdministrative, DateTime data, enumObjekteAmortizimi objektkod, string kodartikulli)
		{
			bool pergjigje = true;
			double normaVleresuar = 0;
			//Gjen normen e artikullit
			clsAseteNormaAmortizimiAbstract norma = clsAseteNormaAmortizimiAbstract.krijoInstance(objektkod);
			pergjigje = norma.merrArtikullNormaAmortizimiSipasIDArtikullStandart(idArtikulli, idStandarti, data);
			//Nese ndodh ndonje gabim atehere norma e amortizimit do te kthehet -1
			if (!pergjigje)
				return 0;
			if (norma.IdLidhjeArtikullLlojAmort <= 0 && objektkod == enumObjekteAmortizimi.ASETE)
				throw new Exception("Mungon norma e amortizimit per artikull " + kodartikulli + "!");
			//Merr normen nga kohezgjatja e magazines nese NormeMagazine eshte True; Ne te kundert merret nga artikulli.
			if (norma.NormeMagazine)
			{
				normaVleresuar = clsNjesiAdministrative.ktheKohezgjatjeNjesiAdministrativeSipasiDPaAutorizime(idNjesiAdministrative);
				if (normaVleresuar != 0)
					normaVleresuar = 100 / normaVleresuar;
			}
			else
				normaVleresuar = norma.Norme;

			return normaVleresuar;
		}
		#endregion

		#endregion

		#region Metoda publike abstrakte

		/// <summary>
		/// MODULI ASETE:
		/// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_AMORTIZIMI_TRUPI ne nje list objektesh clsAmortizimiTrupi.
		/// </summary>
		/// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
		/// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
		public abstract bool mbushAmortizimiTrupiList(DataTable dt);

		/// <summary>
		/// MODULI ASETE:
		/// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_AMORTIZIMI_TRUPI ne nje list objektesh clsAmortizimiTrupi per rillogaritjen e artikujve pa serial.
		/// </summary>
		/// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
		/// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
		public abstract bool mbushAmortizimiTrupiListPerRillogaritjeArtikujPaSerial(DataTable dt);

		#endregion
	}
}
