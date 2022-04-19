using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e normave te amortizimit sipas artikujve dhe llojeve te amortizimit.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT
    /// </summary>
    public abstract class clsAseteNormaAmortizimiAbstract
    {
        public virtual enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ABSTRACT;

        #region Atribute

        private int idLidhjeArtikullLlojAmort;
        private int idArtikulli;
        private int idLlojAmortizimi;
        private int idStandartAmortizimi;
        private bool normeMagazine;
        private double norme;
        private string artikulli;
        private string llojAmortizimi;
        private string standartAmortizimi;
        private DateTime dtAktivizimi;
        private DataRow rreshti;
        private String className;
        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike e lidhjes se artikullit me llojin e amortizimit per normat e amortizimit.
        /// </summary>
        public int IdLidhjeArtikullLlojAmort
        {
            get { return idLidhjeArtikullLlojAmort; }
            set { idLidhjeArtikullLlojAmort = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se artikullit per te cilen eshte i vlefshem konfigurimi i normes.
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te amortizimit.
        /// </summary>
        public int IdLlojAmortizimi
        {
            get { return idLlojAmortizimi; }
            set { idLlojAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id e standartit te amortizimit.
        /// </summary>
        public int IdStandartAmortizimi
        {
            get { return idStandartAmortizimi; }
            set { idStandartAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese norma do te merret per magazine apo thjesht per artikull.
        /// </summary>
        public bool NormeMagazine
        {
            get { return normeMagazine; }
            set { normeMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (float) Merr ose jep vlere normes qe do te llogaritet amortizimi per artikull.
        /// </summary>
        public double Norme
        {
            get { return norme; }
            set { norme = value; }
        }

        public DateTime DtAktivizimi
        {
            get
            {
                return dtAktivizimi;
            }

            set
            {
                dtAktivizimi = value;
            }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsAseteNormaAmortizimiAbstract per normat e amortizimit te artikujve.
        /// </summary>
        public clsAseteNormaAmortizimiAbstract()
        {
            Type type = this.GetType().UnderlyingSystemType;
            this.className = type.Name;
        }

        public clsAseteNormaAmortizimiAbstract(int idartikulli, int idllojamortizimi, int idstandarte, bool normeMagazine, double norme, string kodartikulli, string llojamortizimi, string standart, int idndermarje, bool shtim, bool vjenNgaImporti, DateTime dtaktvizimi)
        {
            try
            {
                Type type = this.GetType().UnderlyingSystemType;
                this.className = type.Name;
                this.idArtikulli = idartikulli;
                this.idLlojAmortizimi = idllojamortizimi;
                this.idStandartAmortizimi = idstandarte;
                this.normeMagazine = normeMagazine;
                this.norme = norme;
                this.dtAktivizimi = dtaktvizimi;
                this.artikulli = kodartikulli;
                this.llojAmortizimi = llojamortizimi;
                this.standartAmortizimi = standart;
                clsMesazh mesazh = this.kontrolloNorma(shtim, idndermarje, vjenNgaImporti);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin me parametra te klases clsAseteNormaAmortizimiAbstract per normat e amortizimit te artikujve.
        /// </summary>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <param name="rresht">(Dictionary) Rreshti i cili do te mbushe objektin.</param>
        public clsAseteNormaAmortizimiAbstract(int idndermarje, Dictionary<string, object> rresht, DateTime dtaktivizimi)
        {
            Type type = this.GetType().UnderlyingSystemType;
            this.className = type.Name;

            string standart = rresht["Standart"].ToString();
            idLlojAmortizimi = int.Parse(rresht["IdLlojAmortizimi"].ToString());
            string normemag = rresht["NormeMagazine"].ToString();

            norme = double.Parse(rresht["Norme"].ToString());
            idStandartAmortizimi = DbAsete.clsStandarteAmortizim.merrIDStandartinAmortizimitTeNdermarrjesSipasEmertimit(standart, idndermarje);
            if (normemag == "Artikull")
                normeMagazine = false;
            else normeMagazine = true;
            this.dtAktivizimi = dtaktivizimi;
            if (IdLlojAmortizimi != 1 && !normeMagazine && norme == 0)
                throw new Exception("Zgjidhni normen per standartin " + standart);
        }

        public clsAseteNormaAmortizimiAbstract(DataRow rreshti)
        {
            Type type = this.GetType().UnderlyingSystemType;
            this.className = type.Name;
            //clsAseteNormaAmortizimiAbstract klasa = krijoInstance(rreshti, this.className);
            
            mbushArtikullNormaAmortizimiObjekt(rreshti);

        }

        #endregion

        #region Krijues instance

        public static clsAseteNormaAmortizimiAbstract krijoInstance(enumObjekteAmortizimi objekti)
        {
            if (objekti==enumObjekteAmortizimi.REZERVA)
                return new clsNormaAmortizimiRezerva();
            else
                return new clsAseteNormaAmortizimi();
        }

        public static clsAseteNormaAmortizimiAbstract krijoInstance(int idndermarje, Dictionary<string, object> rresht, DateTime dtaktivizim, enumObjekteAmortizimi objekti)
        {
            if (objekti == enumObjekteAmortizimi.REZERVA)
                return new clsNormaAmortizimiRezerva(idndermarje, rresht, dtaktivizim);
            else
                return new clsAseteNormaAmortizimi(idndermarje, rresht, dtaktivizim);
        }

        public static clsAseteNormaAmortizimiAbstract krijoInstance(DataRow rreshti, enumObjekteAmortizimi objekti)
        {
            if (objekti == enumObjekteAmortizimi.REZERVA)
                return new clsNormaAmortizimiRezerva(rreshti);
            else
                return new clsAseteNormaAmortizimi(rreshti);
        }

        public static clsAseteNormaAmortizimiAbstract krijoInstance(int IdArtikulli, int IdLlojAmortizimi, int IdStandartAmortizimi, bool NormeMagazine, double norme, string kodartikulli, string llojamortizimi, string standart, int indermarje, bool shtim, bool vjenNgaImporti, DateTime dtkativizimi, enumObjekteAmortizimi objekti)
        {
            if (objekti == enumObjekteAmortizimi.REZERVA)
                return new clsNormaAmortizimiRezerva(IdArtikulli, IdLlojAmortizimi, IdStandartAmortizimi, NormeMagazine, norme, kodartikulli, llojamortizimi, standart, indermarje, shtim, true, dtkativizimi);
            else
                return new clsAseteNormaAmortizimi(IdArtikulli, IdLlojAmortizimi, IdStandartAmortizimi, NormeMagazine, norme, kodartikulli, llojamortizimi, standart, indermarje, shtim, true, dtkativizimi);
        }

        #endregion

        #region Metoda Publike

        public clsAseteNormaAmortizimiAbstract krijoPerImport(string kodartikulli, string llojamortizimi, string standart, string normemagazine, double norme, int indermarje, bool shtim, DateTime dtkativizimi)
        {
            IdArtikulli = DbInventari.clsArtikulli.ktheIdArtikulli(kodartikulli, indermarje);
            if (normemagazine == "Artikull")
                NormeMagazine = false;
            else if (normemagazine == "Magazine")
                NormeMagazine = true;
            else throw new MyException("Kjo lloj norme nuk ekziston!");
            IdStandartAmortizimi = clsStandarteAmortizim.merrIDStandartinAmortizimitTeNdermarrjesSipasEmertimit(standart, indermarje);
            IdLlojAmortizimi = clsAseteLlojAmortizimi.ktheIdLlojAmortizimiSipasEmertimit(llojamortizimi);
            return clsAseteNormaAmortizimiAbstract.krijoInstance(IdArtikulli, IdLlojAmortizimi, IdStandartAmortizimi, NormeMagazine, norme, kodartikulli, llojamortizimi, standart, indermarje, shtim, true, dtkativizimi, this.objektiKod);
        }

        private clsMesazh kontrolloNorma(bool shtim, int idndermarje, bool vjenNgaImporti)
        {
            if (artikulli == "")
                return new clsMesazh(false, "Plotesoni artikullin!");
            if (llojAmortizimi == "")
                return new clsMesazh(false, "Plotesoni metoden e amortizimit!");
            if (standartAmortizimi == "")
                return new clsMesazh(false, "Plotesoni standartin e amortizimit!");
            DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(artikulli, idndermarje);
            if (art.IdArtikulli == 0)
                return new clsMesazh(false, String.Format("Artikulli me kod {0} nuk ekziston!", artikulli));
            if (this.objektiKod == enumObjekteAmortizimi.REZERVA && !art.MeRezerveRivleresimi)
                return new clsMesazh(false, String.Format("Artikulli me kod {0} nuk eshte me rezerve rivleresimi!", artikulli));
            if (!clsStandarteAmortizim.kontrolloEkzistonStandartiAmortizimit(standartAmortizimi, idndermarje))
                return new clsMesazh(false, "Standarti nuk ekziston!");
            if (idLlojAmortizimi < 0)
                return new clsMesazh(false, "Metoda e amortizimit nuk ekziston!");
            if (!vjenNgaImporti && shtim && kontrolloArtikullNormaAmortizimit(idArtikulli, idStandartAmortizimi, dtAktivizimi))//ne rast se po behet import i normes, ne rast se ekziston duhet qe norma te modifikohet me vlerat e importuara, perndryshe te shtohet e re            
                return new clsMesazh(false, "Ekziston ky lloj standarti per kete artikull!");
            if (norme < 0 || norme > 100)
                return new clsMesazh(false, "Norma duhet te jete midis 0 dhe 100");

            return new clsMesazh(true, "Kontrollet e pikes u kaluan me sukses");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e normes per artikullin dhe standartin nese norma ne ndermarrjen ne perdorim nuk eshte ruajtur njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            if (!kontrolloArtikullNormaAmortizimit(idArtikulli, idStandartAmortizimi, dtAktivizimi))
            {
                clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
                clsMesazh pergjigja = moduliAsete.ruajArtikulliNormaAmortizimi(out idLidhjeArtikullLlojAmort, idArtikulli, idLlojAmortizimi, idStandartAmortizimi, normeMagazine, norme, dtAktivizimi);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Ekziston nje norme per kete artikull dhe standart!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e normes per artikullin dhe standartin nese norma ne ndermarrjen ne perdorim eshte ruajtur njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko()
        {
            if (kontrolloArtikullNormaAmortizimit(idArtikulli, idStandartAmortizimi, dtAktivizimi))
            {
                clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
                clsMesazh pergjigja = moduliAsete.modifikimiArtikulliNormaAmortizimi(idLidhjeArtikullLlojAmort, idLlojAmortizimi, normeMagazine, norme);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje norme per kete artikull dhe standart!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e normes per artikullin dhe standartin nese norma ne ndermarrjen ne perdorim eshte ruajtur njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            if (kontrolloArtikullNormaAmortizimit(idArtikulli, idStandartAmortizimi, dtAktivizimi))
            {
                clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
                clsMesazh pergjigja = moduliAsete.fshiArtikullNormaAmortizimi(idLidhjeArtikullLlojAmort);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje norme per kete artikull dhe standart!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e normes se grupit te amortizimit sipas kerkimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se normes se artikullit te amortizimit ose False ne te kundert.</returns>
        public bool merrArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            bool pergjigje = mbushArtikullNormaAmortizimiObjekt(moduliAsete.ktheArtikullNormaAmortizimiSipasIDArtikullStandart(idArtikulli, idStandarti, data));
            moduliAsete.Dispose();
            return pergjigje;
        }
        /// <summary>
        /// MODULI ASETE:
        /// Merr nje datatable me normen dhe metoden e amortizimit sipas idArtikulli dhe dates
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>kthen datatable me te dhena.</returns>
        public string merrNormenDheMetodenAmortizimitSipasIdArtikullStandartit(int idArtikulli, int idStandarti, DateTime data)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            return  moduliAsete.merrNormenDheMetodenAmortizimitSipasIdArtikullStandartit(idArtikulli, idStandarti, data);
        }
        /// <summary>
        /// MODULI ASETE:
        /// Merr id e normes se grupit te amortizimit sipas kerkimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id numrit automatik te normes se artikullit te amortizimit nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDArtikullNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data, enumObjekteAmortizimi objektiKod)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(objektiKod);
            int pergjigje = moduliAsete.ktheIDArtikullNormaAmortizimiSipasIDArtikullStandart(idArtikulli, idStandarti, data);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e llojit te amortizimit sipas kerkimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id e llojit te amortizimit per artikullin dhe standartin nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(int idArtikulli, int idStandarti, DateTime data, enumObjekteAmortizimi objektiKod)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(objektiKod);
            int pergjigje = moduliAsete.ktheIDLlojAmortizimiNormaAmortizimiSipasIDArtikullStandart(idArtikulli, idStandarti, data);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e llojit te amortizimit sipas kerkimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikullLlojAmort">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <returns>Kthen id e llojit te amortizimit per artikullin dhe standartin nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDLlojAmortizimiNormaAmortizimiSipasID(int idArtikullLlojAmort, enumObjekteAmortizimi objektiKod)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(objektiKod);
            int pergjigje = moduliAsete.ktheIDLlojAmortizimiNormaAmortizimiSipasID(idArtikullLlojAmort);
            moduliAsete.Dispose();
            return pergjigje;
        }
        /// <summary>
        /// MODULI ASETE:
        /// Merr id e llojit te amortizimit sipas kerkimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idArtikullLlojAmort">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <returns>Kthen id e llojit te amortizimit per artikullin dhe standartin nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDLlojAmortizimiNormaAmortizimiSipasID(int idArtikullLlojAmort, enumObjekteAmortizimi objektiKod, clsDatabazeAseteAbstract moduliAsete)
        {
            return moduliAsete.TransCache.getIdLlojAmortizimiNormaAmortizimiSipasId(idArtikullLlojAmort, moduliAsete);
        }

        /// <summary>
        /// Kontrollon nese norma per grupin dhe standartin e kerkuar ekziston njehere i ruajtur ne bazen e te dhenave.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit qe po i regjistrohet norma.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen True nese ekziston njehere norma per artikullin dhe standartin e kerkuar dhe False ne te kundert.</returns>
        private bool kontrolloArtikullNormaAmortizimit(int idArtikulli, int idStandarti, DateTime data)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            bool pergjigje = moduliAsete.ekzistonArtikullNormaAmortizimit(idArtikulli, idStandarti, data);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAseteNormaAmortizimiAbstract sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.
        /// </summary>
        /// <param name="dbDataRowNormaAmortizimi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal abstract bool mbushArtikullNormaAmortizimiObjekt(DataRow dbDataRowNormaAmortizimi);

        #endregion
    }
}
