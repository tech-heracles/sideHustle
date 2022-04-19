using DbCore.DbAdmin;
using DbCore.DbAsete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DbCore.DbRegjistrim;
using DbCore.DbArkaBanka;
using DbCore.DbQendraKosto;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne koken e nje flete kontabel
    ///  (Te dhenat  merren nga tabela : T_KOKAFLETEKONTABEL)
    /// </remarks>
    public class clsKokaFleteKontabel
    {
        #region Atributet

        private const int RrumbullakosMe = 13;
        private int _idKokaFleteKontabel;
        private string _nrDokumentiKokaFleteKontabel;
        private DateTime _dateDokumentiKokaFleteKontabel;
        private DateTime _dateRegjistrimiKokaFleteKontabel;
        private string _nrReferenceKokaFleteKontabel;
        private string _pershkrimKokaFleteKontabel;
        private int _idGrupKontabilizimi;
        private int _idSkemaKontabel;
        private int _idNderViti;
        private bool _kontabilizuar;
        private double _vleftaFleteKontabel;
        private int _idPerdoruesi;
        private int _idLlojDok;
        private int _idDokNga;
        private int _idStatusDokumenti;//tregon nese fleta kontabel eshte ruajtur si draft (0) apo jo(1)
        private int _idKonfigAmbjente;
        private int _idKonfigGjenerues;
        private int _idKategoria;
        private int _idNivel;
        private int _idNivelGjenerues;
        private int _idGjenerues;
        private DateTime _dtKrijimi;
        private DateTime _dtModifikimi;
        private colTrupatFletetKontabel _oColTrupi;
        private int _idPeriudha;
        private int _idNdermarje;
        //private string nrGrupKontabilizimi;
        private DbQendraKosto.clsKokaQendraKosto _kokaQendraKosto;
        private DataRow _rreshti;

        #endregion

        #region Properties
        public int IdPeriudha
        {
            get { return _idPeriudha; }
            set
            {
                if (_idPeriudha == value)
                    return;
                _idPeriudha = value;
            }
        }

        public int IdNdermarje
        {
            get { return _idNdermarje; }
            set
            {
                if (_idNdermarje == value)
                    return;
                _idNdermarje = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos Id-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKokaFleteKontabel
        {
            get { return _idKokaFleteKontabel; }
            set { _idKokaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e dokumentit
        /// </summary>
        public string NrDukumentiKokaFleteKontabel
        {
            get { return _nrDokumentiKokaFleteKontabel; }
            set { _nrDokumentiKokaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e dokumentit
        /// </summary>
        public DateTime DateDokumentiKokaFleteKontabel
        {
            get { return _dateDokumentiKokaFleteKontabel; }
            set { _dateDokumentiKokaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e regjistrimit
        /// </summary>
        public DateTime DateRegjistrimiKokaFleteKontabel
        {
            get { return _dateRegjistrimiKokaFleteKontabel; }
            set { _dateRegjistrimiKokaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e references
        /// </summary>
        public string NrReferenceKokaFleteKontabel
        {
            get { return _nrReferenceKokaFleteKontabel; }
            set { _nrReferenceKokaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin
        /// </summary>
        public string PershkrimKokaFleteKontabel
        {
            get { return _pershkrimKokaFleteKontabel; }
            set { _pershkrimKokaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos grupin e kontabilizimit <seealso cref="DbCore.DbKontabiliteti.clsGrupKontabilizimi"/>/>
        /// </summary>
        public int IdGrupKontabilizimi
        {
            get { return _idGrupKontabilizimi; }
            set { _idGrupKontabilizimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne lidhese ndermarrje - vit
        /// </summary>
        public int IdNderViti
        {
            get { return _idNderViti; }
            set { _idNderViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos true nese fleta kontabel eshte kontabilizuar apo jo
        /// </summary>
        public bool Kontabilizuar
        {
            get { return _kontabilizuar; }
            set { _kontabilizuar = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften e fletes kontabel
        /// </summary>
        public double VleftaFleteKontabel
        {
            get { return _vleftaFleteKontabel; }
            set { _vleftaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po kryen veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get { return _idPerdoruesi; }
            set { _idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e dokumentit
        /// </summary>
        public int IdLlojDok
        {
            get { return _idLlojDok; }
            set { _idLlojDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga u gjenerua ne rastet e modifikimit dhe fshirjes
        /// </summary>
        public int IdDokNga
        {
            get { return _idDokNga; }
            set { _idDokNga = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin e dokumenntit - Ruajtur, Draft
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return _idStatusDokumenti; }
            set { _idStatusDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te ambjentit
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return _idKonfigAmbjente; }
            set { _idKonfigAmbjente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te ambjentit nga eshte gjeneruar fleta kontabel
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return _idKonfigGjenerues; }
            set { _idKonfigGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kategorise se dokumentit nga eshte gjeneruar fleta kontabel
        /// </summary>
        public int IdKategoria
        {
            get { return _idKategoria; }
            set { _idKategoria = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit
        /// </summary>
        public int IdNivel
        {
            get { return _idNivel; }
            set { _idNivel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit se dokumentit nga eshte gjeneruar fleta kontabel
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return _idNivelGjenerues; }
            set { _idNivelGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga eshte generuar fleta kontabel nga nje ambjent tjeter
        /// </summary>
        public int IdGjenerues
        {
            get { return _idGjenerues; }
            set { _idGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsTrupiFleteKontabel"/>
        /// </summary>
        public colTrupatFletetKontabel OColTrupi
        {
            get { return _oColTrupi; }
            set { _oColTrupi = value; }
        }


        public DateTime DtKrijimi => _dtKrijimi;

        public DateTime DtModifikimi => _dtModifikimi;

        public DbQendraKosto.clsKokaQendraKosto KokaQendraKosto
        {
            get
            {
                return _kokaQendraKosto;
            }
            set
            {
                _kokaQendraKosto = value;
            }
        }

        #endregion

        #region Kontruktoret

        public clsKokaFleteKontabel(string kodi, int idnderviti, clsDatabaseKontabilitet dbKokaFleteKontabel)
        {
            MbushKokaFleteKontabel(dbKokaFleteKontabel.ktheKokaFleteKontabelSipasKodit(kodi, idnderviti));
        }

        /// <summary>
        /// konstrukor me 1 parameter integer
        /// </summary>
        /// <param name="id">id e kokes se fletes kontabel</param>
        public clsKokaFleteKontabel(int id)
        {
            using (var dbKokaFleteKontabel = new clsDatabaseKontabilitet())
                MbushKokaFleteKontabel(dbKokaFleteKontabel.ktheKokaFleteKontabelSipasID(id));
        }

        public clsKokaFleteKontabel(int id, clsDatabaseKontabilitet dbKokaFleteKontabel)
        {
            MbushKokaFleteKontabel(dbKokaFleteKontabel.ktheKokaFleteKontabelSipasID(id));
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="iddok">id e dokumentit</param>
        /// <param name="idkategori">id e kategorise</param>
        /// <param name="dbKokaFleteKontabel"></param>
        public clsKokaFleteKontabel(int iddok, int idkategori, clsDatabaseKontabilitet dbKokaFleteKontabel)
        {
            MbushKokaFleteKontabel(dbKokaFleteKontabel.ktheKokaFleteKontabelSipasIDGjeneruesAndIdKategoria(iddok, idkategori));
        }

        public clsKokaFleteKontabel(int iddok, int idkategori, DateTime data, clsDatabaseKontabilitet dbKokaFleteKontabel)
        {
            MbushKokaFleteKontabel(dbKokaFleteKontabel.ktheKokaFleteKontabelSipasIDGjeneruesAndIdKategoriaDheData(iddok, idkategori, data));
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="iddok">id e dokumentit</param>
        /// <param name="idkategori">id e llojit</param>
        public clsKokaFleteKontabel(int iddok, int idkategori)
        {
            using (var dbKokaFleteKontabel = new clsDatabaseKontabilitet())
                MbushKokaFleteKontabel(dbKokaFleteKontabel.ktheKokaFleteKontabelSipasIDGjeneruesAndIdKategoria(iddok, idkategori));
        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsKokaFleteKontabel()
        {
            _oColTrupi = new colTrupatFletetKontabel();
            KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
        }

        public clsKokaFleteKontabel(DataRow rreshti)
        {
            MbushKokaFleteKontabel(rreshti);
        }

        #endregion

        #region Metoda Publike

        public static Tuple<clsMesazh, bool, int, string, int, int> RuajFleteKontabel(int idNdermarrje,
            int idNdervit,
            int idPerdorues,
            int statusDokumenti,
            bool kontabilizuar,
            object[] dokumenti,
            bool azhornim,
            DateTime data,
            DateTime dateRegjistrimi,
            string kodKonfig,
            string nrDokumenti,
            string nrReference,
            string pershkrimi,
            int idPeriudha,
            System.Web.UI.Page page,
            IDictionary<string, object> hfNrAutoShitje,
            int idGrupKontabilizimi,
            string shtimModifikim,
            string eshteLidhur,
            int idKokaFleteKontabel,
            int idGjuha)
        {
            var mesazh = new clsMesazh();
            var koka = new clsKokaFleteKontabel();
            clsKokaQendraKosto kokaQendraKosto;
            Tuple<colTrupatFletetKontabel, bool> krijoTrup;
            var shfaqmesazhapolupe = "jo";
            try
            {
                var objektivat = new colObjektivaKosto();
                var vleratobjektiva = new List<double>();
                var vleratobjektivamonbaze = new List<double>();
                var idllogobj = new List<int>();

                krijoTrup = colTrupatFletetKontabel.KrijoTrup(idNdermarrje, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, dokumenti, azhornim, data);

                var konfig = new clsKonfigurimAmbjenti();
                konfig.mbushKonfigAmbjSipasKod(kodKonfig, idNdermarrje);
                if (statusDokumenti != 0)
                    kokaQendraKosto = clsKokaQendraKosto.KrijoQenderRe(konfig, krijoTrup.Item1, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, statusDokumenti, out shfaqmesazhapolupe, idNdermarrje, idGjuha, shtimModifikim, idKokaFleteKontabel, pershkrimi, data, dateRegjistrimi, idNdervit, idPerdorues, nrDokumenti);
                else kokaQendraKosto = null;
                mesazh = koka.KrijoFlete(statusDokumenti, idNdervit, nrDokumenti, nrReference, data, dateRegjistrimi, konfig.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPerdorues, krijoTrup.Item1, kontabilizuar, 5, 5, idPeriudha, idNdermarrje, kokaQendraKosto);
            }
            catch (Exception ex)
            {
                mesazh.Status = false;
                mesazh.PershkrimMesazhi = ex.Message;
                return new Tuple<clsMesazh, bool, int, string, int, int>(mesazh, false, 0, shfaqmesazhapolupe, 0, 0);
            }
            if (!mesazh.Status)
                return new Tuple<clsMesazh, bool, int, string, int, int>(mesazh, false, 0, shfaqmesazhapolupe, 0, 0);

            if (shtimModifikim == "shtim" || shtimModifikim == "klonim")
                mesazh = koka.Ruaj(hfNrAutoShitje);
            else
            {
                koka.IdKokaFleteKontabel = idKokaFleteKontabel;
                var lidhur = koka.EshteILidhur();
                if (lidhur.ToString() != eshteLidhur)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["msgDokumentiEshteILidhur"];
                }
                else
                    mesazh = koka.Modifiko(false, lidhur, hfNrAutoShitje);
            }
            return new Tuple<clsMesazh, bool, int, string, int, int>(mesazh, krijoTrup.Item2, koka.IdKokaFleteKontabel, shfaqmesazhapolupe, koka.IdKonfigAmbjente, (kokaQendraKosto != null) ?kokaQendraKosto.IdKonfigAmbjente: 0); ;
        }

        public static (clsMesazh, bool, int, int, string) RuajFleteKontabel2(int idNdermarrje,
            int idNdervit,
            int idPerdorues,
            int statusDokumenti,
            bool kontabilizuar,
            object[] dokumenti,
            bool azhornim,
            DateTime data,
            DateTime dateRegjistrimi,
            string kodKonfig,
            string nrDokumenti,
            string nrReference,
            string pershkrimi,
            int idPeriudha,
            System.Web.UI.Page page,
            IDictionary<string, object> hfNrAutoShitje,
            int idGrupKontabilizimi,
            string shtimModifikim,
            string eshteLidhur,
            int idKokaFleteKontabel,
            int idGjuha)
        {
            var mesazh = new clsMesazh();
            var koka = new clsKokaFleteKontabel();
            Tuple<colTrupatFletetKontabel, bool> krijoTrup;
            var shfaqmesazhapolupe = "jo";
            try
            {
                var objektivat = new colObjektivaKosto();
                var vleratobjektiva = new List<double>();
                var vleratobjektivamonbaze = new List<double>();
                var idllogobj = new List<int>();

                krijoTrup = colTrupatFletetKontabel.KrijoTrup(idNdermarrje, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, dokumenti, azhornim, data);

                var konfig = new clsKonfigurimAmbjenti();
                konfig.mbushKonfigAmbjSipasKod(kodKonfig, idNdermarrje);

                var kokaQendraKosto = clsKokaQendraKosto.KrijoQenderRe(konfig, krijoTrup.Item1, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, statusDokumenti, out shfaqmesazhapolupe, idNdermarrje, idGjuha, shtimModifikim, idKokaFleteKontabel, pershkrimi, data, dateRegjistrimi, idNdervit, idPerdorues, nrDokumenti);

                mesazh = koka.KrijoFlete(statusDokumenti, idNdervit, nrDokumenti, nrReference, data, dateRegjistrimi, konfig.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPerdorues, krijoTrup.Item1, kontabilizuar, 5, 5, idPeriudha, idNdermarrje, kokaQendraKosto);
            }
            catch (Exception ex)
            {
                mesazh.Status = false;
                mesazh.PershkrimMesazhi = ex.Message;
                return (mesazh, false, 0, 0, shfaqmesazhapolupe);
            }
            if (!mesazh.Status)
                return (mesazh, false, 0, 0, shfaqmesazhapolupe);

            if (shtimModifikim == "shtim" || shtimModifikim == "klonim")
                mesazh = koka.Ruaj(hfNrAutoShitje);
            else
            {
                koka.IdKokaFleteKontabel = idKokaFleteKontabel;
                var lidhur = koka.EshteILidhur();
                if (lidhur.ToString() != eshteLidhur)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["msgDokumentiEshteILidhur"];
                }
                else
                    mesazh = koka.Modifiko(false, lidhur, hfNrAutoShitje);
            }
            return (mesazh, krijoTrup.Item2, koka.IdKokaFleteKontabel, koka.IdKonfigAmbjente, shfaqmesazhapolupe); ;
        }

        public bool MerrFleteSipasIdGjeneruesDheKonfigGjenerues(int iddok, int idkonf, clsDatabaseKontabilitet db) =>
            MbushKokaFleteKontabel(db.ktheKokaFleteKontabelSipasIDGjeneruesAndKonfigGjenerues(iddok, idkonf));

        public clsMesazh KrijoFleteKontPerImportFk(string serverName, int idNdervit, string nrDokumenti, string nrReference, DateTime dtDok, DateTime dtRegj, string kodKonfigAmbient, int idGrupKontabilizimi, string pershkrimi, int idPerdoruesi, colTrupatFletetKontabel colTrupi, bool kontabilizuar, int idLlojDok, int idKategoria, int idPeriudha, int idndermarje, clsKokaQendraKosto kokaqender)
        {
            var idKonfigAmbjente = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(kodKonfigAmbient, idndermarje);

            if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDok, serverName, idndermarje, KategoriDokumenti.FleteKontabel, idKonfigAmbjente))
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);

            var statusdok = (clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "SDI") == "Draft") ? 0 : 1;

            return KrijoFlete(statusdok, idNdervit, nrDokumenti, nrReference, dtDok, dtRegj, idKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPerdoruesi, colTrupi, kontabilizuar, idLlojDok, idKategoria, idPeriudha, idndermarje, _kokaQendraKosto);
        }

        public clsMesazh KrijoFlete(int statusDokumenti, int idNdervit, string nrDokumenti, string nrReference, DateTime dtDok, DateTime dtRegj, int iDKonfigAmbient, int idGrupKontabilizimi, string pershkrimi, int idPerdoruesi, colTrupatFletetKontabel colTrupi, bool kontabilizuar, int idLlojDok, int idKategoria, int idPeriudha, int idndermarje, DbQendraKosto.clsKokaQendraKosto kokaqender) =>
            KrijoFlete(new DbData(), statusDokumenti, idNdervit, nrDokumenti, nrReference, dtDok, dtRegj, iDKonfigAmbient, idGrupKontabilizimi, pershkrimi, idPerdoruesi, colTrupi, kontabilizuar, idLlojDok, idKategoria, idPeriudha, 0, 0, 0, -1, idndermarje, kokaqender);

        /// <summary>
        /// krijon nje flete  te re kontabel sipas te dhenave te plotesuara nga perdoruesi                          
        /// </summary>
        /// <param name="statusDokumenti"></param>
        /// <param name="idNdervit"></param>
        /// <param name="nrDokumenti"></param>
        /// <param name="nrReference"></param>
        /// <param name="dtDok"></param>
        /// <param name="dtRegj"></param>
        /// <param name="iDKonfigAmbient"></param>
        /// <param name="idGrupKontabilizimi"></param>
        /// <param name="pershkrimi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="colTrupi"></param>
        /// <param name="kontabilizuar"></param>
        /// <param name="idLlojDok"></param>
        /// <param name="idKategoria"></param>
        /// <param name="idPeriudha"></param>
        /// <param name="idNivGjen"></param>
        /// <param name="idGjenerues"></param>
        /// <param name="idKonfGjen"></param>
        /// <param name="idDokNga"></param>
        /// <param name="idndermarje"></param>
        /// <param name="kokaqender"></param>
        /// <returns></returns>
        public clsMesazh KrijoFlete(
            DbData dbData,
            int statusDokumenti,
            int idNdervit,
            string nrDokumenti,
            string nrReference,
            DateTime dtDok,
            DateTime dtRegj,
            int iDKonfigAmbient,
            int idGrupKontabilizimi,
            string pershkrimi,
            int idPerdoruesi,
            colTrupatFletetKontabel colTrupi,
            bool kontabilizuar,
            int idLlojDok,
            int idKategoria,
            int idPeriudha,
            int idNivGjen,
            int idGjenerues,
            int idKonfGjen,
            int idDokNga,
            int idndermarje,
            clsKokaQendraKosto kokaqender)
        {
            clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
            _nrDokumentiKokaFleteKontabel = nrDokumenti;
            _nrReferenceKokaFleteKontabel = nrReference;
            _dateDokumentiKokaFleteKontabel = dtDok;
            _dateRegjistrimiKokaFleteKontabel = dtRegj;
            _idDokNga = idDokNga;
            _idLlojDok = idLlojDok;//lloj dokumenti i fletes kontabel per ambjentet e tjera eshte ndryshe
            _idKategoria = idKategoria;//kategoria e fletes kontabel per ambjentet e tjera eshte ndryshe
            _idStatusDokumenti = statusDokumenti;
            _idKonfigAmbjente = iDKonfigAmbient;
            _idKonfigGjenerues = idKonfGjen;
            _idNivelGjenerues = idNivGjen;
            _idGjenerues = idGjenerues;
            var konf = new clsKonfigurimAmbjenti(iDKonfigAmbient, dbShare);
            IdNivel = konf.IdNivel;
            _idGrupKontabilizimi = idGrupKontabilizimi;
            _idNderViti = idNdervit;
            IdNdermarje = idndermarje;
            PershkrimKokaFleteKontabel = pershkrimi;
            IdPerdoruesi = idPerdoruesi;
            OColTrupi = colTrupi;
            _idPeriudha = idPeriudha;
            _kokaQendraKosto = kokaqender;
            var kontMeMinus = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "KLVN", dbShare);
            clsFormatiKonfig formatNrPerKonfig;
            if (idKonfGjen != 0)
            {
                formatNrPerKonfig = new clsFormatiKonfig(idKonfGjen, dbShare);
            }
            else
                formatNrPerKonfig = new clsFormatiKonfig(iDKonfigAmbient, dbShare);

            clsMonedha monedhaNderm = new clsMonedha();
            monedhaNderm.mbushMonedhenENdermarrjes(idndermarje, dbAdmin);
            var formatMonedhe = new clsFormatKonfigTrup();
            formatMonedhe = formatNrPerKonfig.IdFormatKonfig > 0
                ? formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(monedhaNderm.IdMonedha)
                : new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            var mesazhTotali = CheckTotalet(formatMonedhe.ShifraPasPresjesVlefta, kontMeMinus == "Po");
            if (!mesazhTotali.Status)
                return mesazhTotali;
            if (OColTrupi == null || OColTrupi.Count == 0)
                return new clsMesazh(false, MessagesResource.Messages["msgFletaKontabelNukMundTeJeteBosh"]);
            _kontabilizuar = kontabilizuar;
            return new clsMesazh(true, MessagesResource.Messages["msgKokaFletesKontabelUKrijuaMeSukses"]);
        }

        /// <summary>
        /// krijon nje flete  te re kontabel sipas te dhenave te plotesuara nga perdoruesi
        /// </summary>
        /// <param name="statusDokumenti"></param>
        /// <param name="idNdervit"></param>
        /// <param name="nrDokumenti"></param>
        /// <param name="nrReference"></param>
        /// <param name="dtDok"></param>
        /// <param name="dtRegj"></param>
        /// <param name="iDKonfigAmbient"></param>
        /// <param name="idGrupKontabilizimi"></param>
        /// <param name="pershkrimi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="colTrupi"></param>
        /// <param name="kontabilizuar"></param>
        /// <param name="idLlojDok"></param>
        /// <param name="idKategoria"></param>
        /// <param name="idPeriudha"></param>
        /// <param name="idNivGjen"></param>
        /// <param name="idGjenerues"></param>
        /// <param name="idKonfGjen"></param>
        /// <param name="idDokNga"></param>
        /// <param name="idndermarje"></param>
        /// <param name="kokaqender"></param>
        /// <param name="db"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <param name="formatNrPerKonfig"></param>
        /// <param name="idMonedheArkeBanke"></param>
        /// <returns></returns>
        public clsMesazh KrijoFlete(int statusDokumenti, int idNdervit, string nrDokumenti, string nrReference, DateTime dtDok, DateTime dtRegj, int iDKonfigAmbient, int idGrupKontabilizimi, string pershkrimi, int idPerdoruesi, colTrupatFletetKontabel colTrupi, bool kontabilizuar, int idLlojDok, int idKategoria, int idPeriudha, int idNivGjen, int idGjenerues, int idKonfGjen, int idDokNga, int idndermarje, DbQendraKosto.clsKokaQendraKosto kokaqender, clsDatabaseShare db, clsFormatiKonfig formatNrPerKonfig, int idMonedheArkeBanke = 0)
        {
            _nrDokumentiKokaFleteKontabel = nrDokumenti;
            _nrReferenceKokaFleteKontabel = nrReference;
            _dateDokumentiKokaFleteKontabel = dtDok;
            _dateRegjistrimiKokaFleteKontabel = dtRegj;
            _idDokNga = idDokNga;
            _idLlojDok = idLlojDok;//lloj dokumenti i fletes kontabel per ambjentet e tjera eshte ndryshe
            _idKategoria = idKategoria;//kategoria e fletes kontabel per ambjentet e tjera eshte ndryshe
            _idStatusDokumenti = statusDokumenti;
            _idKonfigAmbjente = iDKonfigAmbient;
            _idKonfigGjenerues = idKonfGjen;
            _idNivelGjenerues = idNivGjen;
            _idGjenerues = idGjenerues;
            var konf = new clsKonfigurimAmbjenti(iDKonfigAmbient, db);
            IdNivel = konf.IdNivel;
            _idGrupKontabilizimi = idGrupKontabilizimi;
            _idNderViti = idNdervit;
            IdNdermarje = idndermarje;
            PershkrimKokaFleteKontabel = pershkrimi;
            IdPerdoruesi = idPerdoruesi;
            OColTrupi = colTrupi;
            _idPeriudha = idPeriudha;
            _kokaQendraKosto = kokaqender;
            var kontMeMinus = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "KLVN", db);

            var dbadmin = new clsDatabaseAdmin(db);
            var formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
            {
                var nder = new clsNdermarrje(idndermarje, dbadmin);
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(nder.NdermarrjeMonedha);
            }
            else
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            var mesazhTotali = CheckTotalet(formatMonedhe.ShifraPasPresjesZbritja, kontMeMinus == "Po");
            if (!mesazhTotali.Status)
                return mesazhTotali;
            if (OColTrupi == null || OColTrupi.Count == 0)
                return new clsMesazh(false, MessagesResource.Messages["msgFletaKontabelNukMundTeJeteBosh"]);
            _kontabilizuar = kontabilizuar;
            return new clsMesazh(true, MessagesResource.Messages["msgKokaFletesKontabelUKrijuaMeSukses"]);
        }

        /// <summary>
        /// Kontrollon nqs koka e fletes kontabel eshte e lidhur
        /// </summary>
        /// <returns></returns>
        public bool EshteILidhur()
        {
            using (var dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.eshteDokumentiILidhur(_idKokaFleteKontabel, _idNivel, "T_KOKAFLETEKONTABEL", "IDKOKAFLETEKONTABEL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable MerrIdsDokLidhur()
        {
            using (var dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.MerrDokLidhur(_idKokaFleteKontabel, _idNivel, "T_KOKAFLETEKONTABEL", "IDKOKAFLETEKONTABEL");
        }

        public static decimal GjeneroNrReference(int idNderViti)
        {
            using (var dbKontab = new clsDatabaseKontabilitet())
                return dbKontab.gjeneroNrReference(idNderViti);
        }

        public static decimal GjeneroNrReference(int idNderViti, clsDatabaseKontabilitet dbKontab) => dbKontab.gjeneroNrReference(idNderViti);

        public clsMesazh ruajNgaImporti(int idKokaImport, int idDokumentImportuar, ResourceManager rm, CultureInfo ci)
        {
            var mesazh = new clsMesazh();
            var db = new clsDatabaseKontabilitet();
            try
            {
                db.beginTransaksion();
                mesazh = Ruaj(null, db);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
                var dbImportFKont = new DbImporte.clsDatabazeImporte(db);
                mesazh = DbImporte.clsImportKokaFleteKontabel.modifikoKokaFleteKontabelPasImportitStatike(dbImportFKont, idDokumentImportuar, idKokaImport);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
                db.commitTransaksion();
                return mesazh;
            }
            catch (Exception)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, rm.GetString("msgNdodhiGabimGjateRuajtjesSeFletesKontabel", ci));
            }
        }

        /// <summary>
        /// metoda qe therret klasen clsDatabaseKontabilitet per ruajtjen e nje fletekontabel
        /// </summary>
        /// <param name="hfregjistrime"></param>
        /// <returns></returns>
        public clsMesazh Ruaj(IDictionary<string, object> hfregjistrime)
        {
            IdKokaFleteKontabel = 0;
            var dbshare = new clsDatabaseShare();
            var mesazh = Kontrollo(dbshare);
            if (!mesazh.Status)
                return mesazh;
            var db = new clsDatabaseKontabilitet();
            try
            {
                db.beginTransaksion();
                var uRuajt = Ruaj(hfregjistrime, db);
                if (uRuajt.Status)
                    db.commitTransaksion();
                else
                    db.rollbackTransaksion();
                return uRuajt;
            }
            catch (Exception)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, MessagesResource.Messages["msgNdodhiGabimGjateRuajtjesSeFletesKontabel"]);
            }
        }

        public clsMesazh Ruaj(IDictionary<string, object> hfregjistrime, clsDatabaseKontabilitet dbKont)
        {
            //KEVI duhen bere kontrollet para se te ruhet
            bool kaNdryshimNumri;
            var mesazhKontrolli = KontrolloFK(out kaNdryshimNumri, dbKont, hfregjistrime);
            if (!mesazhKontrolli.Status)
                return mesazhKontrolli;
            var id = 0;
            var uRuajt = RuajFleteKontabel(out id, NrDukumentiKokaFleteKontabel, DateDokumentiKokaFleteKontabel, DateRegjistrimiKokaFleteKontabel, PershkrimKokaFleteKontabel, IdGrupKontabilizimi, IdNderViti, Kontabilizuar, VleftaFleteKontabel, IdPerdoruesi, IdLlojDok, IdDokNga, IdStatusDokumenti, IdKonfigAmbjente, IdKonfigGjenerues, IdKategoria, IdNivel, IdNivelGjenerues, IdGjenerues, OColTrupi, dbKont, _idPeriudha, IdNdermarje, _kokaQendraKosto);
            IdKokaFleteKontabel = id;
            return kaNdryshimNumri ? mesazhKontrolli : uRuajt;
        }

        /// <summary>
        /// Ruan koken dhe trupin e fletes kontabel ne tabelat perkatese ne databaze.
        /// Ruajtja behet nga nje dokument tjeter qe gjate kontabilizimit gjeneron nje flete kontabel.Therret funksionin
        /// <see cref="RuajFleteKontabelNgaDokTjeter"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>        
        public clsMesazh Ruaj(clsDatabaseKontabilitet dbKont)
        {
            var dbshare = new clsDatabaseShare(dbKont);
            var mesazh = Kontrollo(dbshare);
            return !mesazh.Status
                ? mesazh
                : RuajFleteKontabelNgaDokTjeter(IdKokaFleteKontabel, NrDukumentiKokaFleteKontabel, DateDokumentiKokaFleteKontabel, DateRegjistrimiKokaFleteKontabel, PershkrimKokaFleteKontabel, IdGrupKontabilizimi, IdNderViti, Kontabilizuar, VleftaFleteKontabel, IdPerdoruesi, IdLlojDok, IdDokNga, IdStatusDokumenti, IdKonfigAmbjente, IdKonfigGjenerues, IdKategoria, IdNivel, IdNivelGjenerues, IdGjenerues, OColTrupi, _idPeriudha, dbKont, IdNdermarje, _kokaQendraKosto);
        }

        public clsMesazh ModifikoFleteKontabel(bool regjistrim, clsDatabaseKontabilitet dbKont)
        {
            var id = 0;
            var mes = ModifikoFleteKontabel(IdKokaFleteKontabel, NrDukumentiKokaFleteKontabel, DateDokumentiKokaFleteKontabel, DateRegjistrimiKokaFleteKontabel, PershkrimKokaFleteKontabel, IdGrupKontabilizimi, IdNderViti, Kontabilizuar, VleftaFleteKontabel, IdPerdoruesi, IdLlojDok, IdDokNga, IdStatusDokumenti, IdKonfigAmbjente, IdKonfigGjenerues, IdKategoria, IdNivel, IdNivelGjenerues, IdGjenerues, OColTrupi, regjistrim, dbKont, _idPeriudha, IdNdermarje, _kokaQendraKosto, out id);
            IdKokaFleteKontabel = id;
            return mes;
        }

        /// <summary>
        /// transaksioni per te fshire nje flete kontabel
        /// </summary>
        /// <param name="idkokafletekontabel"></param>
        /// <param name="dbKont"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public clsMesazh FshiFleteKontabel(int idkokafletekontabel, clsDatabaseKontabilitet dbKont)
        {
            var trupat = new colTrupatFletetKontabel(idkokafletekontabel, dbKont);
            var mesazh = new clsMesazh(true);
            try
            {
                foreach (var o in trupat)
                {
                    if (mesazh.Status)
                        mesazh = dbKont.fshiTrupiFleteKontabel(o.IdTrupiFleteKontabel);
                    else
                        return mesazh;
                }
                if (!mesazh.Status) return mesazh;
                mesazh = dbKont.fshiKokaFleteKontabel(idkokafletekontabel);
                return mesazh.Status ? new clsMesazh(true, MessagesResource.Messages["msgFshirjaPerfundoiMeSukses"]) : mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh Fshiupd()
        {
            var dbKontabilitet = new clsDatabaseKontabilitet();
            var mesazhi = fshiupd(dbKontabilitet);
            if (!mesazhi.Status)
                dbKontabilitet.rollbackTransaksion();
            else
                dbKontabilitet.commitTransaksion();
            return mesazhi;
        }

        public clsMesazh Fshiupdam(DbData dbData)
        {
            using (var dbKontabilitet = new clsDatabaseKontabilitet(dbData))
                return fshiupd(dbKontabilitet);
        }

        public clsMesazh fshiupd(clsDatabaseKontabilitet dbKontabilitet)
        {
            IdStatusDokumenti = 2;
            var mesazhi = dbKontabilitet.modifikoKokaFleteKontabelStatus(IdKokaFleteKontabel, IdStatusDokumenti);
            if (!mesazhi.Status)
                return mesazhi;
            mesazhi = KaloNeHistorikKokaFleteKontabel(_idKokaFleteKontabel, dbKontabilitet);
            if (!mesazhi.Status)
            {
                return mesazhi;
            }
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbKontabilitet);
            if (KokaQendraKosto.IdKoka <= 0)
            {
                var kokaqendra = new DbQendraKosto.clsKokaQendraKosto();

                kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfig(IdKokaFleteKontabel, IdKonfigAmbjente, dbqendra);
                if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                {
                    KokaQendraKosto = kokaqendra;
                    KokaQendraKosto.ColTrupi.mbushTrupiQendraKosto(kokaqendra.IdKoka, dbqendra);
                }
                else
                    KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
            }
            KokaQendraKosto.IdStatusDok = 2;
            KokaQendraKosto.IdPerdoruesi = _idPerdoruesi;
            mesazhi = KokaQendraKosto.Fshi(_idPerdoruesi, 2, dbqendra);
            if (!mesazhi.Status)
                return mesazhi;
            var stornim = clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "F/MDK") == "Me Stornim";
            if (!Kontabilizuar || !stornim) return mesazhi;
            _oColTrupi = new colTrupatFletetKontabel(_idKokaFleteKontabel, dbKontabilitet);

            mesazhi = StornimFleteKontabel(IdKokaFleteKontabel, NrDukumentiKokaFleteKontabel, DateDokumentiKokaFleteKontabel, DateRegjistrimiKokaFleteKontabel, PershkrimKokaFleteKontabel, IdGrupKontabilizimi, IdNderViti, Kontabilizuar, VleftaFleteKontabel, IdPerdoruesi, IdLlojDok, IdDokNga, IdStatusDokumenti, IdKonfigAmbjente, IdKonfigGjenerues, IdKategoria, IdNivel, IdNivelGjenerues, IdGjenerues, _oColTrupi, IdPeriudha, IdNdermarje, dbKontabilitet);
            return mesazhi;
        }

        /// <summary>
        /// Modifikon grupin e kontabilizimit te nje flete kontabel. Therret funksionin <see cref="clsDatabaseKontabilitet.modifikoKokaFleteKontabelGrupKontabilizimi"/>
        /// </summary>
        public clsMesazh ModifikoFleteKontabelGrupKontabilizimi()
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.modifikoKokaFleteKontabelGrupKontabilizimi(IdKokaFleteKontabel, IdGrupKontabilizimi);
        }

        public static clsMesazh UpdateStatusDheDateDokumentaKontabel(string idZgjedhur, string  dtRegjistrimiKontabiliteti, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            try
            {
                clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
                using (var scope = new MyTransactionScope(data))
                {
                    mesazh = data.UpdateStatusDheDateDokumentaKontabelDB(idZgjedhur, dtRegjistrimiKontabiliteti, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;

                    scope.Complete();
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                new MesazhGabimi($"Ndodhi nje gabim gjate kontabilizimit se dokumentit te buxhetit me id {idZgjedhur}. \n" + err.Message);
            }

            return mesazh;
        }

        public static DataTable ktheGjitheDokumentatFleteKontabelStatusDraft(int idndermarje, string datanga, string dataderi, int idperdorues, string nrdok, string grupkontabilizimi, string llojdok, int idPerdoruesFilter)
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.ktheGjitheDokumentatFleteKontabelStatusDraft(idndermarje, datanga, dataderi, idperdorues, nrdok, llojdok, grupkontabilizimi, idPerdoruesFilter);
        }

        public static clsMesazh KrijoFleteAzhornim(int idndermarje, int idnderviti, DateTime date, int idperd, int idkonf, int idPeriudha, clsDatabaseKontabilitet dbkont)
        {
            var koka = new clsKokaFleteKontabel();
            koka = GjeneroKontabilizimFleteAzhornim(idndermarje, idnderviti, date, idperd, idkonf, idPeriudha, dbkont);
            if (koka._vleftaFleteKontabel == 0)  //nese ska diferenca kursi kthe flete kontabel bosh
                return new clsMesazh(true);
            return koka.Ruaj(null, dbkont);
        }

        public clsMesazh MbyllVitin(int idnderviti, clsViti viti, DateTime data, clsKonfigurimAmbjenti konf, int idperdoruesi, int idllojdok, int idkategoria, int idndermarje, int idkonfvitindermdefault, int idkonfigvitinderkos)
        {
            var periudhat = new colPeriudhaKontabel();
            periudhat.merrSipasViti(viti.IdViti);
            var uruajt = new clsMesazh(true);
            var db = new clsDatabaseKontabilitet();
            db.beginTransaksion();
            var dbadmin = new clsDatabaseAdmin(db);
            try
            {
                var kokaqendra1 = new DbQendraKosto.clsKokaQendraKosto();

                if (db.ekzistonKokaFleteKontabel("0", viti.MbarimiViti, idnderviti, konf.KodKonfigAmbjente))
                {
                    var kf = new clsKokaFleteKontabel();
                    kf.MerrFleteKontabelSipasLlojit(20, idnderviti, db);//20 lloj per mbyllje viti

                    uruajt = kf.fshiupd(db);
                    if (!uruajt.Status)
                    {
                        db.rollbackTransaksion();
                        return uruajt;
                    }
                }
                var dbshare = new clsDatabaseShare(db);
                var kusht = new clsKusht(konf.IdKonfigAmbjente, "ZRQK", dbshare);
                var konfqk = new clsKonfigurimAmbjenti();
                if (kusht.Vlera != 0)
                    konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
                else
                    konfqk.mbushKonfigAmbjSipasKod("RQK", idndermarje, dbshare);

                if (uruajt.Status)
                {
                    DbQendraKosto.colObjektivaKosto objektivat;
                    List<double> vleratobjektiva;
                    List<double> vleratobjektivamonbaze;
                    List<int> idllogobj;
                    var col = Krijotrup(idndermarje, viti.MbarimiViti, viti.IdLlogMbylljeViti, db, dbadmin, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);
                    var shfaqmesazhapolupe = "jo";
                    var kokaqender = new  DbQendraKosto.clsKokaQendraKosto() ;
                    try
                    {
                         kokaqender = DbQendraKosto.clsKokaQendraKosto.KrijoQK(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, viti.MbarimiViti, "0", 0, 1, idndermarje, idnderviti, idperdoruesi, data, "Mbyllje Viti", konf.IdNivel, konf.IdKonfigAmbjente, 0, col, 0, 0, 0, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, db, kokaqendra1.ColTrupi, 0);
                    }
                    catch (Exception err)
                    {
                        db.rollbackTransaksion();
                        NLog.LogManager.GetCurrentClassLogger().Error(err.Message + MessagesResource.Messages["msgMbylljaVititNkUKrye"]);
                        return new MesazhGabimi(err.Message + " " + MessagesResource.Messages["msgMbylljaVititNkUKrye"]);
                    }
                    if (col.Count > 0)
                        {
                            KrijoFlete(new DbData(), 1, idnderviti, "0", "0", viti.MbarimiViti, data, konf.IdKonfigAmbjente, 0, "Mbyllje Viti", idperdoruesi, col, true, idllojdok, idkategoria, periudhat[periudhat.Count - 1].IdPeriudha, 0, -20, 0, -1, idndermarje, kokaqender);
                            uruajt = Ruaj(db);
                        }
                        if (uruajt.Status)
                        {
                            var kodi = int.Parse(viti.KodiViti);
                            kodi++;
                            var vitiRi = new clsViti(0, kodi.ToString(), viti.FillimiViti.AddYears(1), viti.MbarimiViti.AddYears(1), viti.PeriudhaLloji, viti.PeriudhaHapjes, viti.PeriudhaMbylljes, idperdoruesi, viti.IdKonfig, idndermarje, viti.IdStatusDok, new DateTime(), viti.IdLlogMbylljeViti);
                            foreach (var per in periudhat)
                            {
                                per.FillimiPeriudha = per.FillimiPeriudha.AddYears(1);
                                if (per.MbarimiPeriudha.Month == 2 && per.MbarimiPeriudha.Day == 28 && DateTime.IsLeapYear(per.FillimiPeriudha.Year))
                                    per.MbarimiPeriudha = per.MbarimiPeriudha.AddYears(1).AddDays(1);
                                else if (per.MbarimiPeriudha.Month == 2 && per.MbarimiPeriudha.Day == 29 && !DateTime.IsLeapYear(per.FillimiPeriudha.Year))
                                    per.MbarimiPeriudha = per.MbarimiPeriudha.AddDays(-1).AddYears(1);
                                else
                                    per.MbarimiPeriudha = per.MbarimiPeriudha.AddYears(1);
                                per.Ekycur = false;
                                per.PostoAnnualDeclaration = false;
                                per.PostoEpayslip = false;
                                per.PostoMemoBonus = false;
                            }
                            var mes = new clsMesazh();
                            viti.MbyllurMe = data;
                            mes = dbadmin.modifikoVit(viti.IdViti, viti.KodiViti, viti.FillimiViti, viti.MbarimiViti, viti.PeriudhaLloji, viti.PeriudhaHapjes, viti.PeriudhaMbylljes, idperdoruesi, viti.IdKonfig, viti.IdNdermarje, viti.IdStatusDok, viti.MbyllurMe, viti.IdLlogMbylljeViti);
                            if (!dbadmin.ekzistonVit(kodi.ToString(), idndermarje).Status) //nqs ekziston viti mbyll transaksionin prn ruhet viti i ri
                                if (mes.Status)
                                {
                                    db.commitTransaksion();
                                    uruajt.Status = true;
                                    uruajt.PershkrimMesazhi = MessagesResource.Messages["msgVitetVitiUshtrimorUMbyllMeSukses"];
                                    return uruajt;
                                }
                                else
                                {
                                    db.rollbackTransaksion();
                                    uruajt.Status = false;
                                    uruajt.PershkrimMesazhi = mes.PershkrimMesazhi;
                                    return uruajt;
                                }
                            if (mes.Status)
                            {
                                mes = vitiRi.ruaj(periudhat, idkonfvitindermdefault, idkonfigvitinderkos, dbadmin);
                                if (mes.Status)
                                {
                                    var idNV = 0;
                                    var nderviti = new clsNdermarrjeViti(kodi, vitiRi.IdViti, idndermarje, false, vitiRi.FillimiViti, vitiRi.MbarimiViti, idperdoruesi);
                                    mes = dbadmin.ruajNdermarrjeReVit(out idNV, nderviti.IdViti, nderviti.Viti, nderviti.IdNdermarrje, nderviti.NdermarrjeVitiMbyllur, nderviti.NdermarrjeVitiFillim, nderviti.NdermarrjeVitiFund, nderviti.IdPerdoruesi);

                                    if (mes.Status)
                                    {
                                        var klonoTeDrejta = new clsTeDrejtaRoliKoka();
                                        mes = klonoTeDrejta.klonoTeGjitheTeDrejtaPerMbylljeViti(dbadmin, idndermarje, viti.IdViti, vitiRi.IdViti);
                                        if (mes.Status)
                                        {
                                            var idLlojBuxheti = clsLlojBuxheti.mbushIDLlojBuxheti("KategoriShpenzimi", db);
                                            mes = colBuxhetet.krijoBuxhetetPerGjitheKategoriteENder(idLlojBuxheti, idndermarje, idNV, vitiRi.FillimiViti, dbadmin);
                                            if (mes.Status)
                                            {
                                                db.commitTransaksion();
                                                uruajt.Status = true;
                                                uruajt.PershkrimMesazhi = MessagesResource.Messages["msgVitetVitiUshtrimorUMbyllMeSukses"];
                                                return uruajt;
                                            }
                                            db.rollbackTransaksion();
                                            uruajt.Status = false;
                                            uruajt.PershkrimMesazhi = MessagesResource.Messages["msgVituUshtrimorUMbyllMeGabime"];
                                            return uruajt;
                                        }
                                        db.rollbackTransaksion();
                                        uruajt.Status = false;
                                        uruajt.PershkrimMesazhi = MessagesResource.Messages["msgVituUshtrimorUMbyllMeGabime"];
                                        return uruajt;
                                    }
                                    db.rollbackTransaksion();
                                    uruajt.Status = false;
                                    uruajt.PershkrimMesazhi = mes.PershkrimMesazhi;
                                    return uruajt;
                                }
                                db.rollbackTransaksion();
                                uruajt.Status = false;
                                uruajt.PershkrimMesazhi = mes.PershkrimMesazhi;
                                return uruajt;
                            }
                            db.rollbackTransaksion();
                            uruajt.Status = false;
                            uruajt.PershkrimMesazhi = mes.PershkrimMesazhi;
                            return uruajt;
                        }
                
                    db.rollbackTransaksion();
                    return uruajt;
                }
                db.rollbackTransaksion();
                return uruajt;
            }
            catch (Exception)
            {
                db.rollbackTransaksion();
                return uruajt;
            }
        }

        public static int merrNrFunditPerImportFleteKontAlbsig(int idNdermarrje, string dt)
        {
            var data = new clsDatabaseKontabilitet();
            var nrKoka = data.merrNrFunditPerImportFleteKontAlbsig(idNdermarrje, dt);
            var nr = 1;
            if (nrKoka != null)
                nr = int.Parse(nrKoka.Substring(nrKoka.LastIndexOf("_") + 1)) + 1;
            data.Dispose();
            return nr;
        }

        public static void ShtoTeDhenaPerObjektivat(DateTime data, DbQendraKosto.colObjektivaKosto objektivat, List<double> vleratobjektiva, List<double> vleratobjektivamonbaze, List<int> idllogobj, clsLlogari oLlogari, clsTrupiFleteKontabel tLlogKf, DbQendraKosto.clsObjektivaKosto objekt)
        {
            if (objekt.Nga > data || objekt.Deri != new DateTime() && objekt.Deri < data) return;
            var eksitonObjperLlog = false;
            for (var ob = 0; ob < objektivat.Count; ob++)
            {
                if (objektivat[ob].Id != objekt.Id || idllogobj[ob] != oLlogari.IdLlogari) continue;
                eksitonObjperLlog = true;
                vleratobjektiva[ob] += tLlogKf.VleftaDebiTrupiFleteKontabel - tLlogKf.VleftaKrediTrupiFleteKontabel;
                vleratobjektivamonbaze[ob] += tLlogKf.VleftaDebiMonBazeTrupiFleteKontabel - tLlogKf.VleftaKrediMonBazeTrupiFleteKontabel;
            }
            if (eksitonObjperLlog) return;
            objektivat.Add(objekt);
            vleratobjektiva.Add(tLlogKf.VleftaDebiTrupiFleteKontabel - tLlogKf.VleftaKrediTrupiFleteKontabel);
            vleratobjektivamonbaze.Add(tLlogKf.VleftaDebiMonBazeTrupiFleteKontabel - tLlogKf.VleftaKrediMonBazeTrupiFleteKontabel);
            idllogobj.Add(oLlogari.IdLlogari);
        }

        public static clsKokaFleteKontabel GjeneroKontabilizimFleteAzhornim(int idndermarje, int idnderviti, DateTime date, int idperd, int idkonf, int idPeriudha, ResourceManager rm, CultureInfo ci)
        {
            using (var dbkont = new clsDatabaseKontabilitet())
                return GjeneroKontabilizimFleteAzhornim(idndermarje, idnderviti, date, idperd, idkonf, idPeriudha, dbkont);
        }
        public void EkzistonFleteKontabel(string kodi, string nrdokumentikokafletekontabel,
            DateTime datedokumentifletekontabel, int idnderviti)
        {
            using (var dbKont = new clsDatabaseKontabilitet())
                if (dbKont.ekzistonKokaFleteKontabel(nrdokumentikokafletekontabel, datedokumentifletekontabel, idnderviti, kodi))
                    throw new MyException(MessagesResource.Messages["msgEkzistonDokumentiMeKeteNumerDheKeteDate"]);
        }

        #endregion

        #region Gjenerim Kontabilizimi

        internal static clsKokaFleteKontabel GjeneroKontabilizimShitje(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colTrupiShitje coltrupi, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, bool shitje_blerje, int idklientfurnitori, double zbritje, double tvsh, double kursi, int idmonedha, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, bool llogaritfitimhumbjeneto, int idkusht, colAmortizimiKoka colamortizimi, colTrupiMagazina trupimag, int iddokngaqk, clsFormatiKonfig formatNrPerKonfig, clsDatabaseKontabilitet db)
        {
            #region KOKA E FLETES KONTABEL
            var dbshare = new clsDatabaseShare(db);
            var konf = new clsKonfigurimAmbjenti(idKonfgjenerues, dbshare);
            var konfFk = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);
            var kusht = new clsKusht(konfFk.IdKonfigAmbjente, "ZRQK", dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbshare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbshare) == "Po";
            var colTrupFK = new colTrupatFletetKontabel();
            var coltrupiQK = new DbQendraKosto.colTrupiQendraKosto();
            colTrupFK = KrijoTrupFletKontabelShitje(idNder, konfFk.IdKonfigAmbjente, konfFk.IdSkemeKontabel, coltrupi, shitje_blerje, idklientfurnitori, zbritje, tvsh, kursi, idmonedha, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, llogaritfitimhumbjeneto, idkusht, colamortizimi, trupimag, pershkrimi, idPer, db, out coltrupiQK, trupivjeterqendra, iddegeadm, idPer, out shfaqmesazhapolupe, rishpernda, shperndaDifQKPModDok, idStatusDok);
            var kokaqender = new DbQendraKosto.clsKokaQendraKosto();
            var dbqend = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var mesazh = DbQendraKosto.clsKokaQendraKosto.KrijoQKLP(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaqk, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFk.IdNivel, konfFk.IdKonfigAmbjente, 0, coltrupiQK, dbqend, ref kokaqender, shperndaDifQKPModDok);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);

            #endregion

            var kokaFK = new clsKokaFleteKontabel();
            mesazh = kokaFK.KrijoFlete(idStatusDok, idNdVt, nrDk, "1", dtDk, dtRegj, konfFk.IdKonfigAmbjente, 0, pershkrimi, idPer, colTrupFK, true, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;


        }

        private static colTrupatFletetKontabel KrijoTrupFletKontabelShitje(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colTrupiShitje trupishitje, bool shitje_blerje, int idklientfurnitori, double zbritje, double tvsh, double kursi, int idmonedha, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, bool llogaritfitimhumbjeneto, int idkusht, colAmortizimiKoka colamortizimi, colTrupiMagazina trupimag, string pershkrimi, int idPerdorues, clsDatabaseKontabilitet db, out DbQendraKosto.colTrupiQendraKosto trupiQK, DbQendraKosto.colTrupiQendraKosto trupiQKold, int iddegeadm, int idperdoruesi, out string shfaqmesazh, bool rishpernda, bool shperndaDifQKPModDok, int statusdok)
        {
            shfaqmesazh = "jo";
            if (db.TransCache.ColKarakteristikaStandarti.Count == 0)
                db.TransCache.ColKarakteristikaStandarti = new colKarakteristikaStandarti(idNdermarrje, new clsDatabazeAsete(db));
            trupiQK = new DbQendraKosto.colTrupiQendraKosto();
            var dbshare = new clsDatabaseShare(db);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var dbAdmin = new clsDatabaseAdmin(db);
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbasete = new clsDatabazeAsete(db);
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            const bool eshteAzhornim = false;
            double totaliPaTvsh = 0;
            double totaliMeTvsh = 0;

            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = false;
            var monedheNdermarrje = new clsMonedha();
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK", dbshare);
            if (alternativa == "Azhornim")
            {
                azhornim = true;
                monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbAdmin);
            }
            var taksat = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdorues, dbregj);

            var kurseDate = new colKurset(idNdermarrje, data, dbAdmin);
            foreach (var tsh in trupishitje)
            {
                totaliPaTvsh += tsh.VleftaPaTvsh;
                totaliMeTvsh += tsh.VleftaMeTvsh;
            }
            var perqindjeZbritje = totaliMeTvsh == 0 ? 0 : zbritje / totaliMeTvsh;
            var VLZBTVSH = (1 - perqindjeZbritje) * totaliMeTvsh;//vlera totale me zbritje me TVSH = (1-ZbritjeTotale)*Totali vleftes me tvsh
            var VLZBPATVSH = VLZBTVSH - tvsh; rreshtakf = new List<string>(); emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();
            var VLZB = totaliPaTvsh * perqindjeZbritje; // vlera e zbritjes = total vlefte pa tvsh *zbritje totale
            var list = new List<structVlera>();
            var s = new structVlera();
            s.kodi = "VLZBTVSH";
            s.vlera = VLZBTVSH;
            list.Add(s);
            s = new structVlera
            {
                kodi = "VLZBPATVSH",
                vlera = VLZBPATVSH
            };
            list.Add(s);
            s = new structVlera
            {
                kodi = "TVSH",
                vlera = tvsh
            };
            list.Add(s);
            s = new structVlera
            {
                kodi = "VLZB",
                vlera = VLZB
            };
            list.Add(s);
            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var oArtikull = new clsArtikulli();
            var tempCol = new colSkemaKontabelTrupiNew();
            var clsSkemaKontNew = new clsSkemaKontabelNew(idSkemeKontabel);

            var nderm = new clsNdermarrje(idNdermarrje, dbAdmin);
            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel

            foreach (var sk in clsSkemaKontNew.OColSkemakontabelTrupiNew)
            {
                var indexGrupimi = sk.IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                var tempSkemaKontTrupiNew = from l in tempCol
                                            where l.IndeksGrupimi == indexGrupimi
                                            select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Any()) continue;
                //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                var tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                if (tempSkemaKontTrupiNew_2.Count() == 1)
                {
                    tempCol.Add(tempSkemaKontTrupiNew_2.First());
                }
            }
            List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK> objektetQK = new List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK>();
            foreach (clsSkemaKontabelTrupiNew t in tempCol)
            {
                var tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = t;

                var oLlojLlogarish = new clsLlojLlogarish();
                var oNenLlojLlogarie = new clsNenLlojLlogarish();
                IEnumerable<double> vl;

                double v = 0;
                //oLlojLlogarish = transactionCache.GetLlojLlogariFromCache(colLlojLlogCache, int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), db);
                oLlojLlogarish = new clsLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), db);// oLlojLlogarish.merrLlojLlogarieSipasID();
                if (!llogaritfitimhumbjeneto && tempSkemaKontTrupi.KushtiSkemeKontTrupi == idkusht)
                    continue;

                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "LL"://nese ne kete rresht te grides eshte zgjedhur llogari                                                        
                            foreach (var veprimTrupi in trupishitje)
                            {
                                objektivat = new DbQendraKosto.colObjektivaKosto();
                                vleratobjektiva = new List<double>();
                                vleratobjektivamonbaze = new List<double>();
                                idllogobj = new List<int>();
                                var colTrupatPerQK = new colTrupatFletetKontabel();
                                if (veprimTrupi.IdLlojVeprimi != 3) continue;
                                var shenime = (veprimTrupi.Shenime == "") ? pershkrimi : veprimTrupi.Shenime;
                                oLlogari = (clsLlogari)veprimTrupi.Element;

                                double vleftaTrup = 0;
                                if ((tempSkemaKontTrupi.FormulaSkemeKontTrupi == "PAZBRITJE"))
                                    vleftaTrup = veprimTrupi.VleftaPaTvsh * kursi;
                                else
                                    vleftaTrup = (1 - perqindjeZbritje) * veprimTrupi.VleftaPaTvsh * kursi;

                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vleftaTrup, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, shenime, kurseDate, monedheNdermarrje, dbAdmin);

                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra); //todo kevi
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    var ekzistonqk = false;
                                    foreach (var f in colTrupatPerQK)
                                    {
                                        if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekzistonqk = true;
                                    }



                                    if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                }
                                var ekziston = false;
                                foreach (var f in colTrupFK)
                                {
                                    if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                    }
                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                    }
                                    ekziston = true;
                                }
                                if (!ekziston)
                                    colTrupFK.Add(tLlogKF);
                                
                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze }; //objektivat
                                objektetQK.Add(obj);
                                //var mesazh = "jo";
                                //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazh = mesazh;
                            }

                            break;
                        case "ART"://nese ne kete rresht te grides eshte zgjedhur artikull
                            var rreshti = -1;

                            #region kontabilizimi i magazines brenda shitjes per rastin e alphabank

                            if (llogaritfitimhumbjeneto && tempSkemaKontTrupi.KushtiSkemeKontTrupi == idkusht)
                            {
                                foreach (var veprimTrupi in trupimag)
                                {
                                    objektivat = new DbQendraKosto.colObjektivaKosto();
                                    vleratobjektiva = new List<double>();
                                    vleratobjektivamonbaze = new List<double>();
                                    idllogobj = new List<int>();
                                    var colTrupatPerQk = new colTrupatFletetKontabel();
                                    var njesi = new clsNjesiAdministrative(veprimTrupi.IdMag, dbregj); //todo kevi
                                    var idnenllojllogarie = tempSkemaKontTrupi.IdNenLlojLlogarie;
                                    rreshti++;
                                    var ekziston = false;
                                    var vlerakont = veprimTrupi.Vlefta;
                                    if (veprimTrupi.IdLlojVeprimi == 1)//Artikull
                                    {
                                        oArtikull = (clsArtikulli)veprimTrupi.Element;
                                        if (oArtikull.LlojiArt)
                                        {

                                            if (tempSkemaKontTrupi.FormulaSkemeKontTrupi == "art")//rresht flete kontabel vetem per art
                                                continue;

                                            var status = new clsStatusMagazine_Asete(njesi.IdStatusAktualMagazine, dbasete);

                                            if (colamortizimi.Count == 0)//hyrjet nga vkm nuk kane amortizim
                                            {
                                                if (status.Emertimi != "Aktive" && !shitje_blerje)//hyrje vetem aktivet
                                                    idnenllojllogarie = "20";
                                            }
                                            foreach (var kokamortizim in colamortizimi)
                                            {
                                                if (!new clsKarakteristikaStandarti(kokamortizim.IdLlojStandarti, oArtikull.Kodifikimi1Artikulli, 0, false, dbasete).Kontabilizim)
                                                    continue;
                                                if (status.Emertimi != "Aktive" && !shitje_blerje)//hyrje vetem aktivet
                                                    continue;
                                                vlerakont = veprimTrupi.Shenja > 0
                                                    ? kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.VleftaPlusMinus))
                                                    : kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => -item.VleftaPlusMinus);
                                                if (shitje_blerje)//dalje
                                                {
                                                    if (tempSkemaKontTrupi.DebikrediSkemeKontTrupi == 2)//kredi
                                                        if (status.Emertimi != "Aktive")//magazina joaktive preket llogaria aa ne proces
                                                            idnenllojllogarie = "20";
                                                        else//debi
                                                        {
                                                            if (tempSkemaKontTrupi.IdNenLlojLlogarie == "27")//amortizimi
                                                                vlerakont = kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.HdAmortizimGjithsej));
                                                            else
                                                                vlerakont -= kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.HdAmortizimGjithsej));
                                                        }
                                                }
                                                break;//vetem nje standart eshte me kontabilitet
                                            }
                                        }
                                        else if (tempSkemaKontTrupi.FormulaSkemeKontTrupi == "aqt")//rresht flete kontabel vetem per aqt
                                            continue;
                                    }
                                    int idNenLlojLlogari;
                                    if ((oArtikull.Klasa == 5 || oArtikull.Klasa == 6) && tempSkemaKontTrupi.DebikrediSkemeKontTrupi == 1 && veprimTrupi.Shenja == -1) //prodhim merret llogaria e shpenzimit
                                        idNenLlojLlogari = 21;
                                    else
                                        idNenLlojLlogari = int.Parse(idnenllojllogarie);
                                    var nenLlojLlogaria = new clsNenLlojLlogarish(idNenLlojLlogari, db);
                                    oLlogari = oArtikull.merrLlogariArtikulli(nenLlojLlogaria.KodNenLlojLlogarie, db);
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlerakont, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                    var objekt = new DbQendraKosto.clsObjektivaKosto();
                                    if (oArtikull.IdObjektivaKosto != 0 && oArtikull.IdObjektivaKosto != -1)
                                    {
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqendra);
                                        if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    }
                                    else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    if (objekt.Id != 0 && objekt.Id != -1)
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                    if (idNenLlojLlogari == 10 && llogaritfitimhumbjeneto)//llogaria e blerjes
                                    {
                                        var llogarishitje = oArtikull.merrLlogariArtikulli(new clsNenLlojLlogarish(9, db).KodNenLlojLlogarie, db);
                                        var tr = colTrupFK.Find(x => x.IdLlogari == llogarishitje.IdLlogari);
                                        if (tr != null)
                                        {
                                            if (tr.VleftaKrediMonBazeTrupiFleteKontabel >= tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel)
                                            {
                                                tr.VleftaKrediMonBazeTrupiFleteKontabel -= tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                tr.VleftaKrediTrupiFleteKontabel -= tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                continue;

                                            }
                                            tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel -= tr.VleftaKrediMonBazeTrupiFleteKontabel;
                                            tLlogKF.VleftaDebiTrupiFleteKontabel -= tr.VleftaKrediTrupiFleteKontabel;
                                            colTrupFK.Remove(tr);
                                        }

                                    }
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        var ekzistonqk = false;
                                        foreach (var f in colTrupatPerQk)
                                        {
                                            if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                        if (!ekzistonqk)
                                            colTrupatPerQk.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                    }
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekziston = true;
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);

                                    clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQk, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = njesi.IdNjesiAdministrative, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze }; //objektivat
                                    objektetQK.Add(obj);

                                    //var mesazh = "jo";
                                    //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQk, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, njesi.IdNjesiAdministrative, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                    //if (mesazh != "jo")
                                    //    shfaqmesazh = mesazh;
                                }
                            }

                            #endregion

                            else
                                foreach (var veprimTrupi in trupishitje)
                                {
                                    objektivat = new DbQendraKosto.colObjektivaKosto();
                                    vleratobjektiva = new List<double>();
                                    vleratobjektivamonbaze = new List<double>();
                                    idllogobj = new List<int>();
                                    var colTrupatPerQK = new colTrupatFletetKontabel();
                                    if (veprimTrupi.IdLlojVeprimi != 1) continue;
                                    var ekziston = false;
                                    oArtikull = (clsArtikulli)veprimTrupi.Element;
                                    int idNenLlojLlogari;
                                    if (oArtikull.Klasa == 2 || oArtikull.Klasa == 3) //i pastokueshem dhe sherbim merret llogaria e shitjes
                                        idNenLlojLlogari = shitje_blerje ? 9 : 10;
                                    else
                                        idNenLlojLlogari = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                                    if (oArtikull.LlojiArt && !shitje_blerje && nderm.Lloji == 2)
                                        idNenLlojLlogari = 20;//per artikujt afatgjate ne buxhetor tek blerja merret llogari shpenzim ritje amortizimi
                                    double vleftaTrup = 0;
                                    if ((tempSkemaKontTrupi.FormulaSkemeKontTrupi == "PAZBRITJE"))
                                        vleftaTrup = veprimTrupi.VleftaPaTvsh * kursi;
                                    else
                                        vleftaTrup = (1 - perqindjeZbritje) * veprimTrupi.VleftaPaTvsh * kursi;
                                    oLlogari = oArtikull.merrLlogariArtikulli(new clsNenLlojLlogarish(idNenLlojLlogari, db).KodNenLlojLlogarie, db);
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vleftaTrup, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                    var objekt = new DbQendraKosto.clsObjektivaKosto();
                                    if (oArtikull.IdObjektivaKosto != 0 && oArtikull.IdObjektivaKosto != -1)
                                    {
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqendra);
                                        if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    }
                                    else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    if (objekt.Id != 0 && objekt.Id != -1)
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        var ekzistonqk = false;
                                        foreach (var f in colTrupatPerQK)
                                        {
                                            if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                        if (!ekzistonqk)
                                            colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                    }
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekziston = true;
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);

                                    clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = veprimTrupi.IdMagazina, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                    objektetQK.Add(obj);

                                    //var mesazh = "jo";
                                    //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, veprimTrupi.IdMagazina, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                    //if (mesazh != "jo")
                                    //    shfaqmesazh = mesazh;
                                }
                            break;
                    }
                }
                else
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "TKS":
                            oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db);
                            switch (oNenLlojLlogarie.KodNenLlojLlogarie)
                            {
                                case "TKSK":

                                    foreach (var taksa in trupishitje)
                                    {
                                        var colTrupatPerQK = new colTrupatFletetKontabel();
                                        if (taksa.Tvsh == 0) continue;
                                        var oT = taksat.First(x => x.IdTaksa == taksa.Tvsh);
                                        oLlogari = new clsLlogari(oT.LlogariKredi, idNdermarrje, db);
                                        var vlerakont = double.Parse(((1 - perqindjeZbritje) * (taksa.VleftaMeTvsh - taksa.VleftaPaTvsh)).ToString()) * kursi;

                                        tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlerakont, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                        var ekziston = false;

                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                        }
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel == 0 && tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel == 0) continue;
                                        var ekzistonqk = false;
                                        foreach (var f in colTrupatPerQK)
                                        {
                                            if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;

                                        }

                                        if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                        if (!ekziston)
                                            colTrupFK.Add(tLlogKF);

                                        clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                        objektetQK.Add(obj);

                                        //var mesazh = "jo";
                                        //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                        //if (mesazh != "jo")
                                        //    shfaqmesazh = mesazh;
                                    }
                                    break;
                                case "TKSD":
                                    foreach (var taksa in trupishitje)
                                    {
                                        var colTrupatPerQK = new colTrupatFletetKontabel();
                                        if (taksa.Tvsh == 0) continue;
                                        var ekziston = false;
                                        var oT = taksat.First(x => x.IdTaksa == taksa.Tvsh);
                                        oLlogari = new clsLlogari(oT.LlogariDebi, idNdermarrje, db);
                                        tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, double.Parse(((1 - perqindjeZbritje) * (taksa.VleftaMeTvsh - taksa.VleftaPaTvsh)).ToString()) * kursi, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                        }
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel == 0 && tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel == 0) continue;
                                        var ekzistonqk = false;
                                        foreach (var f in colTrupatPerQK)
                                        {
                                            if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                        if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                        if (!ekziston)
                                            colTrupFK.Add(tLlogKF);

                                        clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                        objektetQK.Add(obj);

                                        //var mesazh = "jo";
                                        //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                        //if (mesazh != "jo")
                                        //    shfaqmesazh = mesazh;
                                    }
                                    break;
                            }
                            break;

                        case "FIX":
                            oLlogari = new clsLlogari(tempSkemaKontTrupi.IdLlogarieFikse, idNdermarrje, db);
                            vl = from l in list
                                 where l.kodi == tempSkemaKontTrupi.KodSkemeKontTrupi
                                 select l.vlera;
                            v = vl.First();
                            var colTrupatPerQK1 = new colTrupatFletetKontabel();
                            tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, v * kursi, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                            {
                                var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                            }
                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                            {
                                var ekzistonqk = false;
                                foreach (var f in colTrupatPerQK1)
                                {
                                    if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                    }
                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                    }
                                    ekzistonqk = true;
                                }
                                if (!ekzistonqk)
                                    colTrupatPerQK1.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                colTrupFK.Add(tLlogKF);

                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK1, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                objektetQK.Add(obj);

                                //var mesazh = "jo";
                                //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK1, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazh = mesazh;
                            }
                            break;
                        case "KL":
                            {
                                oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db);
                                oKlient = new clsKlientFurnitor(idklientfurnitori, db);
                                oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, db);
                                if ((oNenLlojLlogarie.KodNenLlojLlogarie == "DKL" || oNenLlojLlogarie.KodNenLlojLlogarie == "DFR") && oLlogari.IdLlogari < 1)
                                    throw new MyException("Nuk keni zgjedhur llogari parapagimi per kete klient!");
                                if (oNenLlojLlogarie.KodNenLlojLlogarie == "ZKL" && oLlogari.IdLlogari < 1)
                                    throw new MyException("Nuk keni zgjedhur llogari zbritje per kete klient!");
                                var colTrupatPerQK = new colTrupatFletetKontabel();
                                //nepermjet idnenllojllogarise gjejme llogarine qe duhet te marrim nga klienti
                                //per percaktimin e vleftes mund te veprojme ne kete menyre. Vlerat qe jane gjetur me siper
                                //i ruajme ne nje array ku cdo element i arrayt perbehet nga dy fusha , nje qe permban kodin e vleftes dhe tjetri
                                //qe permban vleren perkatese
                                //nese llogaria do preket ne debi apo ne kredi e marrim nga rreshti perkates qe kemi gjetur
                                //pasi mbarojme pune me kete indeksgrupimi duhet te evitojme gjithe rreshtat e tjere qe ndodhen 
                                //ne kete collection qe kane te njejtin indeksgrupimi
                                vl = from l in list
                                     where l.kodi == tempSkemaKontTrupi.KodSkemeKontTrupi
                                     select l.vlera;
                                v = vl.First();
                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, v * kursi, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);

                                var objektkl = new DbQendraKosto.clsObjektivaKosto();
                                if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto, dbqendra);
                                    if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                            objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                if (objektkl.Id != 0 && objektkl.Id != -1)
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                if (oNenLlojLlogarie.KodNenLlojLlogarie != "ZKL"
                                    || (oNenLlojLlogarie.KodNenLlojLlogarie == "ZKL" && (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)))
                                {
                                    var ekzistonqk = false;
                                    foreach (var f in colTrupatPerQK)
                                    {
                                        if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekzistonqk = true;
                                    }

                                    if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));

                                    colTrupFK.Add(tLlogKF);

                                    clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                    objektetQK.Add(obj);

                                    //var mesazh = "jo";
                                    //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                    //if (mesazh != "jo")
                                    //    shfaqmesazh = mesazh;

                                    if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                    {
                                        trupiPerGjendjeKF.Add(tLlogKF.Clone());
                                        rreshtakf.Add(oKlient.LlojiKF ? "KL" : "FR");
                                        emrakf.Add(oKlient.IdKlientFurnitor);
                                    }
                                }
                            }
                            break;
                        case "FR":  //llogari klient , eshte ne db rreshti tek T_LLOJLLOGARISH me id 1
                                    //duhet gjetur rreshti qe permbush kushtin e klientit
                                    //nga ky objekt merret idnenllojllogaria perkatese
                                    //oNenLlojLlogarie = transactionCache.GetNenLlojLlogarishFromCache(colNenLlojLlogCache, int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db);
                            oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db);//oNenLlojLlogarie.merrNenLlojLlogarieSipasID();
                                                                                                                            //oKlient = transactionCache.GetKfFromCache(colKfCache, idklientfurnitori, db);
                            oKlient = new clsKlientFurnitor(idklientfurnitori, db);
                            oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, db);
                            if ((oNenLlojLlogarie.KodNenLlojLlogarie == "DKL" || oNenLlojLlogarie.KodNenLlojLlogarie == "DFR") && oLlogari.IdLlogari < 1)
                                throw new MyException("Nuk keni zgjedhur llogari parapagimi per kete furnitor!");
                            if ((oNenLlojLlogarie.KodNenLlojLlogarie == "ZKL") && oLlogari.IdLlogari < 1)
                                throw new MyException("Nuk keni zgjedhur llogari zbritje per kete klient!");
                            var colTrupatPerQK2 = new colTrupatFletetKontabel();
                            //nepermjet idnenllojllogarise gjejme llogarine qe duhet te marrim nga klienti
                            //per percaktimin e vleftes mund te veprojme ne kete menyre. Vlerat qe jane gjetur me siper
                            //i ruajme ne nje array ku cdo element i arrayt perbehet nga dy fusha , nje qe permban kodin e vleftes dhe tjetri
                            //qe permban vleren perkatese
                            //nese llogaria do preket ne debi apo ne kredi e marrim nga rreshti perkates qe kemi gjetur
                            //pasi mbarojme pune me kete indeksgrupimi duhet te evitojme gjithe rreshtat e tjere qe ndodhen 
                            //ne kete collection qe kane te njejtin indeksgrupimi
                            vl = from l in list
                                 where l.kodi == tempSkemaKontTrupi.KodSkemeKontTrupi
                                 select l.vlera;
                            v = vl.First<double>();
                            tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, v * kursi, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                            var objektfr = new DbQendraKosto.clsObjektivaKosto();
                            if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                            {
                                objektfr = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto, dbqendra);
                                if (!(objektfr.Nga <= data && (objektfr.Deri == new DateTime() || objektfr.Deri >= data)))
                                    if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        objektfr = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                            }
                            else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                objektfr = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                            if (objektfr.Id != 0 && objektfr.Id != -1)
                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektfr);
                            if (oNenLlojLlogarie.KodNenLlojLlogarie != "ZKL" || (oNenLlojLlogarie.KodNenLlojLlogarie == "ZKL" && (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)))
                            {
                                var ekzistonqk = false;
                                foreach (var f in colTrupatPerQK2)
                                {
                                    if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                    }
                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                    }
                                    ekzistonqk = true;
                                }
                                if (!ekzistonqk) colTrupatPerQK2.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                colTrupFK.Add(tLlogKF);

                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK2, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                objektetQK.Add(obj);

                                //var mesazh = "jo";
                                //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK2, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazh = mesazh;

                                if (oNenLlojLlogarie.KodNenLlojLlogarie == "KFR")
                                {
                                    trupiPerGjendjeKF.Add(tLlogKF.Clone());
                                    rreshtakf.Add(oKlient.LlojiKF ? "KL" : "FR");
                                    emrakf.Add(oKlient.IdKlientFurnitor);
                                }
                            }
                            break;
                        case "":
                            break;
                    }
                }
            }
            var mesazh = "jo";
            trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.KrijoTrupQK(dbqendra, objektetQK, trupiQKold, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
            if (mesazh != "jo")
                shfaqmesazh = mesazh;

            return colTrupFK;
        }

        /// <summary>
        ///gjeneron kontabilizimin e nje dokumenti magazine.
        ///gjenerohet koka e fletes kontabel, trupi, dhe llogarite e kontabilitetit sipas skemes se kontabilitetit me te cilen eshte ruajtur dokumenti
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="vlera"> vlera totale e dokumentit</param>
        /// <param name="ocolTrupiMagazina">kolektion i trupit te magazines</param>
        /// <returns>kthen nje obj clsKokaFleteKontabel me kontabilizimin e dokumentit te magazines</returns>
        internal static clsKokaFleteKontabel GjeneroKontabilizimMagazine(int idMagKoka, int idNiv, int idKonf, DateTime dtDk, string nrDk, double vlera, int idNder, int idNdVt, int idPer, DateTime dtRegj, colTrupiMagazina ocolTrupiMagazina, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idllogari, int idnjesivartese, bool mekonfirmim, colShperndarjeShpenzimeLlogarite colLlogarite, double shumallog, int idkategoria, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, int iddokngaQK, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, colTrupiMagazina trupiTransferim, colAmortizimiKoka colAmortizimi, bool gjithmone, clsDatabaseKontabilitet db, int idMagazina)
        {
            #region KOKA E FLETES KONTABEL

            var kokaFk = new clsKokaFleteKontabel();
            var dbshare = new clsDatabaseShare(db);
            var konf = new clsKonfigurimAmbjenti(idKonf, dbshare);

            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            shfaqmesazhapolupe = "";
            if (konf.IdSkemeKontabel == 0)
                return kokaFk;
            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);

            var colTrupFK = new colTrupatFletetKontabel();
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonf, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbshare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbshare) == "Po";
            var coltrupiQK = new DbQendraKosto.colTrupiQendraKosto();
            colTrupFK = KrijoTrupFletKontabelMagazine(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, ocolTrupiMagazina, vlera, idllogari, idnjesivartese, mekonfirmim, colLlogarite, shumallog, dtDk, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, trupiTransferim, colAmortizimi, gjithmone, pershkrimi, db, out coltrupiQK, trupivjeterqendra, iddegeadm, idPer, out shfaqmesazhapolupe, rishpernda, shperndaDifQKPModDok, idMagazina, idStatusDok);
            var kokaqender = new DbQendraKosto.clsKokaQendraKosto();
            var dbqend = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var mesazh = DbQendraKosto.clsKokaQendraKosto.KrijoQKLP(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaQK, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, coltrupiQK, dbqend, ref kokaqender, shperndaDifQKPModDok);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);

            #endregion

            if (colTrupFK.Count == 0)//rasti per aqt ne magazina joaktive
                return kokaFk;
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, idKonf);
            mesazh = kokaFk.KrijoFlete(idStatusDok, idNdVt, nrDk, "1", dtDk, dtRegj, konfFK.IdKonfigAmbjente, 0, pershkrimi, idPer, colTrupFK, true, idLlojDok, idkategoria, idPeriudha, idNiv, idMagKoka, idKonf, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFk;
        }

        private static colTrupatFletetKontabel KrijoTrupFletKontabelMagazine(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, DbCore.DbRegjistrim.colTrupiMagazina trupiMag, double vlera, int idllogari, int idnjesivartese, bool mekonfirmim, DbRegjistrim.colShperndarjeShpenzimeLlogarite colLlog, double shumallog, DateTime data, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, DbCore.DbRegjistrim.colTrupiMagazina trupiTransferim, colAmortizimiKoka colamortizimi, bool gjithmone, string pershkrimi, clsDatabaseKontabilitet db, out DbQendraKosto.colTrupiQendraKosto trupiQK, DbQendraKosto.colTrupiQendraKosto trupiQKold, int iddegeadm, int idperdoruesi, out string shfaqmesazh, bool rishpernda, bool shperndaDifQKPModDok, int idMagKoka, int statusdok)
        {
            shfaqmesazh = "jo";
            if (db.TransCache.ColKarakteristikaStandarti.Count == 0)
                db.TransCache.ColKarakteristikaStandarti = new colKarakteristikaStandarti(idNdermarrje, new clsDatabazeAsete(db));
            objektivat = new colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            bool eshteAzhornim = false;
            trupiQK = new colTrupiQendraKosto();
            clsDatabaseShare dbshare = new clsDatabaseShare(db);
            clsDatabaseInventari dbinv = new clsDatabaseInventari(db);
            clsDatabaseQendraKosto dbqendra = new clsDatabaseQendraKosto(db);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(db);
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim(db);
            clsDatabazeAsete dbasete = new clsDatabazeAsete(db);
            colTrupatFletetKontabel colTrupFK = new colTrupatFletetKontabel();
            bool azhornim = false;
            clsMonedha monedheNdermarrje = new clsMonedha();
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK", dbshare);
            if (alternativa == "Azhornim")
            {
                azhornim = true;
                monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbAdmin);
            }
            colKurset kurseDate = new colKurset(idNdermarrje, data, dbAdmin);

            clsSkemaKontabelTrupiNew tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            //clsKlientFurnitor oKlient = new clsKlientFurnitor();
            clsLlogari oLlogari = new clsLlogari();
            //DbInventari.clsArtikulli oArtikull = new DbInventari.clsArtikulli();
            //collection temporar
            colSkemaKontabelTrupiNew tempCol = new colSkemaKontabelTrupiNew();
            int indexGrupimi = 0;

            clsSkemaKontabelNew clsSkemaKontNew = clsSkemaKontabelNew.getSkemaKontabelNew(idSkemeKontabel, db);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }
                }
            }
            List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK> objektetQK = new List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK>();
            for (var i = 0; i < tempCol.Count; i++)
            {
                if (tempCol[i].FormulaSkemeKontTrupi == "MeKonfirmim" && !mekonfirmim)
                    continue;
                clsTrupiFleteKontabel tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = tempCol[i];

                //clsLlojLlogarish oLlojLlogarish = transactionCache.GetLlojLlogariFromCache(colLlojLlogCache, int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), db);
                clsNenLlojLlogarish oNenLlojLlogarie = new clsNenLlojLlogarish();
                clsLlojLlogarish oLlojLlogarish = new clsLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), db);
                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "ART"://nese ne kete rresht te grides eshte zgjedhur artikull
                            int rreshti = -1;
                            bool zbritamortizimshtesengallogaria = false;
                            foreach (clsTrupiMagazina veprimTrupi in trupiMag)
                            {
                                var skemaClone = tempSkemaKontTrupi.Clone();

                                objektivat = new DbQendraKosto.colObjektivaKosto();
                                vleratobjektiva = new List<double>();
                                vleratobjektivamonbaze = new List<double>();
                                idllogobj = new List<int>();
                                string idnenllojllogarie = skemaClone.IdNenLlojLlogarie;
                                rreshti++;
                                if (veprimTrupi.IdLlojVeprimi != 1)//Artikull
                                    continue;

                                colTrupatFletetKontabel colTrupatPerQK = new colTrupatFletetKontabel();
                                double vlerakont = veprimTrupi.Vlefta;
                                bool ekziston = false;
                                DbInventari.clsArtikulli oArtikull = (DbInventari.clsArtikulli)veprimTrupi.Element;
                                //rresht flete kontabel vetem per art
                                if (oArtikull.LlojiArt && skemaClone.FormulaSkemeKontTrupi == "art")
                                    continue;
                                //rresht flete kontabel vetem per aqt
                                if (oArtikull.LlojiArt && skemaClone.FormulaSkemeKontTrupi == "Rezerve" && !oArtikull.MeRezerveRivleresimi)
                                    continue;
                                if (!oArtikull.LlojiArt && (skemaClone.FormulaSkemeKontTrupi == "aqt" || (skemaClone.FormulaSkemeKontTrupi == "Rezerve" && !oArtikull.MeRezerveRivleresimi)))
                                    continue;

                                DbRegjistrim.clsNjesiAdministrative njesi = new DbRegjistrim.clsNjesiAdministrative(veprimTrupi.IdMag, dbregj);

                                vlerakont = ktheVlerePerKontabilizim(veprimTrupi, oArtikull, njesi, colamortizimi, dbasete, skemaClone, ref idnenllojllogarie, gjithmone, trupiTransferim, rreshti, dbregj, ref zbritamortizimshtesengallogaria);

                                //jane rreshta te amortizimit qe duhen ruajtur vetem kur eshte kushti per kontabilitet gjithmone
                                if (!gjithmone && skemaClone.FormulaSkemeKontTrupi == "gjithmone")
                                    continue;
                                if (skemaClone.DebikrediSkemeKontTrupi == 0)
                                    continue;

                                int idNenLlojLlogari;
                                if ((oArtikull.Klasa == 5 || oArtikull.Klasa == 6) && skemaClone.DebikrediSkemeKontTrupi == 1 && veprimTrupi.Shenja == -1) //prodhim merret llogaria e shpenzimit
                                    idNenLlojLlogari = 21;
                                else
                                    idNenLlojLlogari = int.Parse(idnenllojllogarie);

                                oLlogari = oArtikull.merrLlogariArtikulli(new clsNenLlojLlogarish(idNenLlojLlogari, db).KodNenLlojLlogarie, db);

                                tLlogKF = new clsTrupiFleteKontabel(skemaClone.DebikrediSkemeKontTrupi, vlerakont, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                DbQendraKosto.clsObjektivaKosto objekt = new DbQendraKosto.clsObjektivaKosto();
                                if (oArtikull.IdObjektivaKosto > 0)
                                {
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqendra);
                                    if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                            objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                }
                                if (objekt.Id != 0 && objekt.Id != -1)
                                {
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    bool ekzistonqk = false;
                                    foreach (clsTrupiFleteKontabel f in colTrupatPerQK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                        if (idllogari != 0 && zbritamortizimshtesengallogaria && f.IdLlogari == idllogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel -= tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel -= tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel -= tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel -= tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                        }
                                    }
                                    if (!ekzistonqk)
                                        colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                }

                                foreach (clsTrupiFleteKontabel f in colTrupFK)
                                {
                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekziston = true;
                                    }
                                    if (idllogari != 0 && zbritamortizimshtesengallogaria && f.IdLlogari == idllogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel -= tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel -= tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel -= tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel -= tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }

                                        for(int j = objektetQK.Count - 1; j>=0; j-- )
                                        {
                                            var objektQKekzistues = objektetQK[j].TrupatFK.Find(x => x.IdLlogari == idllogari);
                                            if (objektQKekzistues == null)
                                                continue;
                                            if (f.VleftaDebiMonBazeTrupiFleteKontabel - f.VleftaKrediMonBazeTrupiFleteKontabel == 0)
                                            {
                                                objektetQK[j].TrupatFK.Remove(objektQKekzistues);
                                                if(objektetQK[j].TrupatFK.Count == 0)
                                                    objektetQK.RemoveAt(j);
                                                continue;
                                            }

                                            objektQKekzistues.VleftaDebiMonBazeTrupiFleteKontabel = f.VleftaDebiMonBazeTrupiFleteKontabel;
                                            objektQKekzistues.VleftaDebiTrupiFleteKontabel = f.VleftaDebiTrupiFleteKontabel;
                                            objektQKekzistues.VleftaKrediMonBazeTrupiFleteKontabel = f.VleftaKrediMonBazeTrupiFleteKontabel;
                                            objektQKekzistues.VleftaKrediTrupiFleteKontabel = f.VleftaKrediTrupiFleteKontabel;
                                        }
                                    }
                                }
                                if (!ekziston)
                                    colTrupFK.Add(tLlogKF);

                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = njesi.IdNjesiAdministrative, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze }; 
                                objektetQK.Add(obj);

                                //string mesazh = "jo";
                                //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, njesi.IdNjesiAdministrative, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazh = mesazh;
                            }
                            break;
                        case "NV":
                            #region NjesiVartese
                            DbInventari.clsNjesiVartese njesivartese = new DbInventari.clsNjesiVartese(idnjesivartese, dbinv);
                            oLlogari = new clsLlogari(njesivartese.IdLlogari, db);
                            bool ekzistonPerNV = false;
                            int rreshtiNV = -1;
                            bool zbritamortizimshtesengallogariaNV = false;
                            foreach (DbCore.DbRegjistrim.clsTrupiMagazina veprimTrupi in trupiMag)
                            {
                                var skemaClone = tempSkemaKontTrupi.Clone();
                                string idnenllojllogarie = skemaClone.IdNenLlojLlogarie;
                                rreshtiNV++;
                                if (veprimTrupi.IdLlojVeprimi != 1)//Artikull
                                    continue;
                                double vlerakont = veprimTrupi.Vlefta;

                                DbInventari.clsArtikulli oArtikull = (DbInventari.clsArtikulli)veprimTrupi.Element;
                                DbRegjistrim.clsNjesiAdministrative njesi = new DbRegjistrim.clsNjesiAdministrative(veprimTrupi.IdMag, dbregj);
                                vlerakont = ktheVlerePerKontabilizim(veprimTrupi, oArtikull, njesi, colamortizimi, dbasete, skemaClone, ref idnenllojllogarie, gjithmone, trupiTransferim, rreshtiNV, dbregj, ref zbritamortizimshtesengallogariaNV);

                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlerakont, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    DbQendraKosto.clsObjektivaKosto objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                colTrupatFletetKontabel colTrupatPerQKNV = new colTrupatFletetKontabel();
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    bool ekzistonqk = false;
                                    foreach (clsTrupiFleteKontabel f in colTrupatPerQKNV)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                    }
                                    if (!ekzistonqk)
                                        colTrupatPerQKNV.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                    foreach (clsTrupiFleteKontabel f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonPerNV = true;
                                        }
                                    }
                                    if (!ekzistonPerNV)
                                        colTrupFK.Add(tLlogKF);

                                    clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQKNV, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze }; 
                                    objektetQK.Add(obj);

                                    //string mesazh = "jo";
                                    //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQKNV, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                    //if (mesazh != "jo")
                                    //    shfaqmesazh = mesazh;
                                }
                            }
                            #endregion
                            break;
                    }
                }
                else
                {
                    #region Vlera Totale
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "FIX":
                            oLlogari = new clsLlogari(tempSkemaKontTrupi.IdLlogarieFikse, idNdermarrje, db);
                            tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlera, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                            {
                                DbQendraKosto.clsObjektivaKosto objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                            }
                            colTrupatFletetKontabel colTrupatPerQK1 = new colTrupatFletetKontabel();
                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                            {
                                colTrupatPerQK1.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                colTrupFK.Add(tLlogKF);
                            }

                            clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK1, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                            objektetQK.Add(obj);

                            //string mesazh1 = "jo";
                            //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK1, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh1, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                            //if (mesazh1 != "jo")
                            //    shfaqmesazh = mesazh1;

                            break;
                        case "LL":
                            #region Llogari
                            if (colLlog != null)
                                foreach (DbRegjistrim.clsShperndarjeShpenzimeLlogarite llog in colLlog)
                                {
                                    objektivat = new DbQendraKosto.colObjektivaKosto();
                                    vleratobjektiva = new List<double>();
                                    vleratobjektivamonbaze = new List<double>();
                                    idllogobj = new List<int>();
                                    colTrupatFletetKontabel colTrupatPerQK2 = new colTrupatFletetKontabel();
                                    oLlogari = new clsLlogari(llog.IdLlogari, db);
                                    //oLlogari = transactionCache.getLlogariFromCache(colLlogCache, llog.IdLlogari, db);
                                    //oLlogari = new clsLlogari(llog.IdLlogari, db);
                                    //llogaria preket sipas peshes qe ze vlera e vendosur ne gride me shumen e grides * shumen e faturave te dates se kaluar si paramete
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, (tempSkemaKontTrupi.KushtiSkemeKontTrupi == 1 ? (llog.Vlefta / shumallog) * vlera : -(llog.Vlefta / shumallog)) * vlera, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                    //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, (tempSkemaKontTrupi.KushtiSkemeKontTrupi == 1 ? (llog.Vlefta / shumallog) * vlera : -(llog.Vlefta / shumallog)) * vlera, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                    if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    {
                                        DbQendraKosto.clsObjektivaKosto objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                    }
                                    bool ekziston = false;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        bool ekzistonqk = false;
                                        foreach (clsTrupiFleteKontabel f in colTrupatPerQK2)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekzistonqk = true;
                                            }
                                        }



                                        if (!ekzistonqk) colTrupatPerQK2.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));

                                        foreach (clsTrupiFleteKontabel f in colTrupFK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekziston = true;
                                            }
                                        }
                                        if (!ekziston)
                                            colTrupFK.Add(tLlogKF);

                                        clsKokaQendraKosto.TrupaFkPerShperndarjeQK objekti = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK2, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = idMagKoka, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                        objektetQK.Add(objekti);

                                        //string mesazh = "jo";
                                        ////TODO PATI: ALPHAWEB-11629 - Te vendoset id e magazines nqs te gjithe rreshtat e trupit kane te njejten magazine
                                        //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK2, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, idMagKoka, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                        //if (mesazh != "jo")
                                        //    shfaqmesazh = mesazh;
                                    }
                                }
                            if (idllogari != 0)
                            {
                                oLlogari = new clsLlogari(idllogari, db);
                                //oLlogari = transactionCache.getLlogariFromCache(colLlogCache, idllogari, db);
                                //oLlogari = new clsLlogari(idllogari, db);
                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlera, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlera, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    DbQendraKosto.clsObjektivaKosto objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                colTrupatFletetKontabel colTrupatPerQK3 = new colTrupatFletetKontabel();
                                bool ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    bool ekzistonqk = false;
                                    foreach (clsTrupiFleteKontabel f in colTrupatPerQK3)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                    }



                                    if (!ekzistonqk) colTrupatPerQK3.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                    foreach (clsTrupiFleteKontabel f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);

                                    clsKokaQendraKosto.TrupaFkPerShperndarjeQK objekti = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK3, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = idMagKoka, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                    objektetQK.Add(objekti);

                                    //string mesazh = "jo";
                                    //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK3, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, idMagKoka, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                    //if (mesazh != "jo")
                                    //    shfaqmesazh = mesazh;
                                }
                            }
                            #endregion
                            break;
                        case "NV":
                            #region NjesiVartese
                            DbInventari.clsNjesiVartese njesivartese = new DbInventari.clsNjesiVartese(idnjesivartese, dbinv);
                            oLlogari = new clsLlogari(njesivartese.IdLlogari, db);
                            bool eksist = false;
                            tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlera, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                            {
                                DbQendraKosto.clsObjektivaKosto objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                            }
                            colTrupatFletetKontabel colTrupatPerQK = new colTrupatFletetKontabel();
                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                            {
                                bool ekzistonqk = false;
                                foreach (clsTrupiFleteKontabel f in colTrupatPerQK)
                                {
                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekzistonqk = true;
                                    }
                                }
                                if (!ekzistonqk)
                                    colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                foreach (clsTrupiFleteKontabel f in colTrupFK)
                                {
                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        eksist = true;
                                    }
                                }
                                if (!eksist)
                                    colTrupFK.Add(tLlogKF);

                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK objekti = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                objektetQK.Add(objekti);

                                //string mesazh = "jo";
                                //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazh = mesazh;
                            }
                            #endregion
                            break;
                    }
                    #endregion
                }
            }

            string mesazh = "jo";
            trupiQK.AddRange(clsKokaQendraKosto.KrijoTrupQK(dbqendra, objektetQK, trupiQKold, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
            if (mesazh != "jo")
                shfaqmesazh = mesazh;

            return colTrupFK;
        }


        public static double ktheVlerePerKontabilizim(DbCore.DbRegjistrim.clsTrupiMagazina veprimTrupi, DbInventari.clsArtikulli oArtikull, DbRegjistrim.clsNjesiAdministrative njesi, colAmortizimiKoka colamortizimi, DbAsete.clsDatabazeAsete dbasete, clsSkemaKontabelTrupiNew skemaClone,
            ref string idnenllojllogarie, bool gjithmone, DbCore.DbRegjistrim.colTrupiMagazina trupiTransferim, int rreshti, DbRegjistrim.clsDatabaseRegjistrim dbregj, ref bool zbritamortizimshtesengallogaria)
        {
            double vlerakont = veprimTrupi.Vlefta;
            #region AQT
            if (oArtikull.LlojiArt)
            {
                DbAsete.clsStatusMagazine_Asete status = new DbAsete.clsStatusMagazine_Asete(njesi.IdStatusAktualMagazine, dbasete);

                if (colamortizimi.Count == 0)//hyrjet nga vkm nuk kane amortizim
                {
                    if (status.Emertimi != "Aktive" && veprimTrupi.Sasia * veprimTrupi.Shenja >= 0)//hyrje vetem aktivet
                        idnenllojllogarie = "20";
                }
                foreach (clsAmortizimiKoka kokamortizim in colamortizimi)
                {
                    if (!new clsKarakteristikaStandarti(kokamortizim.IdLlojStandarti, oArtikull.Kodifikimi1Artikulli, 0, false, dbasete).Kontabilizim)
                        continue;

                    if (skemaClone.FormulaSkemeKontTrupi != "gjithmone" && skemaClone.FormulaSkemeKontTrupi != "Rezerve")
                    {
                        DbAsete.clsStatusMagazine_Asete statustransf = new DbAsete.clsStatusMagazine_Asete();
                        if (trupiTransferim.Count > rreshti)//transferimet
                        {
                            DbRegjistrim.clsNjesiAdministrative njesitransf = new DbRegjistrim.clsNjesiAdministrative(trupiTransferim[rreshti].IdMag, dbregj);

                            statustransf = new clsStatusMagazine_Asete(njesitransf.IdStatusAktualMagazine, dbasete);
                            if (status.Emertimi != "Aktive" && statustransf.Emertimi == "Aktive") //jo aktiv ne aktiv
                            {
                                if (skemaClone.IdNenLlojLlogarie == "11")//llogaria e inventarit
                                    skemaClone.DebikrediSkemeKontTrupi = 1;
                                else skemaClone.DebikrediSkemeKontTrupi = 2;
                            }
                            else if (status.Emertimi == "Aktive" && statustransf.Emertimi != "Aktive")//aktiv ne joaktiv
                            {
                                if (skemaClone.IdNenLlojLlogarie == "11")//llogaria e inventarit
                                    skemaClone.DebikrediSkemeKontTrupi = 2;
                                else skemaClone.DebikrediSkemeKontTrupi = 1;
                            }
                            else
                            {
                                if (!gjithmone)
                                    continue;
                                if (status.Emertimi != "Aktive" && statustransf.Emertimi != "Aktive")
                                {
                                    if (skemaClone.IdNenLlojLlogarie == "11")//llogaria e inventarit shkon si llogari aa ne proces
                                    {
                                        idnenllojllogarie = "20";
                                        skemaClone.DebikrediSkemeKontTrupi = 1;
                                    }
                                    else
                                    {
                                        skemaClone.DebikrediSkemeKontTrupi = 2;
                                    }
                                }
                                else if (status.Emertimi == "Aktive" && statustransf.Emertimi == "Aktive")
                                {
                                    if (skemaClone.IdNenLlojLlogarie == "20")//llogaria e inventarit shkon si llogari aa ne proces
                                    {
                                        idnenllojllogarie = "11";
                                        skemaClone.DebikrediSkemeKontTrupi = 2;
                                    }
                                    else
                                    {
                                        skemaClone.DebikrediSkemeKontTrupi = 1;
                                    }
                                }
                            }
                        }
                        if (status.Emertimi != "Aktive" && statustransf.Emertimi == null && veprimTrupi.Sasia * veprimTrupi.Shenja > 0)//hyrje vetem aktivet
                            continue;
                        if (veprimTrupi.Shenja > 0)
                            vlerakont = kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.VleftaPlusMinus));
                        else
                            vlerakont = kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => -item.VleftaPlusMinus);
                        if (veprimTrupi.Sasia * veprimTrupi.Shenja < 0 && statustransf.Emertimi == null)//dalje
                        {
                            if (skemaClone.DebikrediSkemeKontTrupi == 2)//kredi
                            {
                                if (status.Emertimi != "Aktive")//magazina joaktive preket llogaria aa ne proces
                                    idnenllojllogarie = "20";
                                if (skemaClone.FormulaSkemeKontTrupi == "MeKonfirmim" && skemaClone.IdNenLlojLlogarie == "25")
                                    vlerakont -= kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.HdAmortizimGjithsej));
                            }
                            else//debi
                            {
                                if (skemaClone.FormulaSkemeKontTrupi == "MeKonfirmim" && skemaClone.IdNenLlojLlogarie != "20")
                                    continue;
                                if (skemaClone.IdNenLlojLlogarie == "27")//amortizimi
                                    vlerakont = kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.HdAmortizimGjithsej));
                                else vlerakont -= kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.HdAmortizimGjithsej));

                                zbritamortizimshtesengallogaria = true;
                            }
                        }
                    }
                    else if (skemaClone.FormulaSkemeKontTrupi == "Rezerve" && oArtikull.MeRezerveRivleresimi)
                    {
                        vlerakont = kokamortizim.ColTrupiRezerva.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.VleftaPlusMinus - item.HdAmortizimGjithsej));
                    }
                    else
                    {
                        if (gjithmone && skemaClone.PershkrimSkemeKontTrupi == "HDAmortGjith")
                            vlerakont = kokamortizim.ColTrupi.FindAll(x => x.IdArtikulli == oArtikull.IdArtikulli && x.IdNjesiAdministrative == veprimTrupi.IdMag && x.NrRendor == rreshti).Sum(item => Math.Abs(item.HdAmortizimGjithsej));
                        else
                        {
                            break;

                        }
                    }
                    break;//vetem nje standart eshte me kontabilitet

                }
            }
            #endregion

            return vlerakont;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimNdryshimCmimSasi(int idMagKoka, int idNiv, int idKonf, DateTime dtDk, string nrDk, double vlera, int idNder, int idNdVt, int idPer, DateTime dtRegj, colTrupiNdryshimCmimSasi ocolTrupiMagazina, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idllogari, int idkategoria, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, int iddokngaQK, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, clsDatabaseKontabilitet db, ResourceManager rm, CultureInfo ci)
        {
            #region KOKA E FLETES KONTABEL
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var kontabilizuar = true;
            var idGrupKontabilizimi = 0;
            var dbshare = new clsDatabaseShare(db);
            var konf = new clsKonfigurimAmbjenti(idKonf, dbshare);

            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);

            var colTrupFK = new colTrupatFletetKontabel();
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonf, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbshare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbshare) == "Po";
            var coltrupiQK = new DbQendraKosto.colTrupiQendraKosto();
            colTrupFK = krijoTrupFletKontabelNdryshimCmimSasi(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, ocolTrupiMagazina, vlera, idllogari, dtDk, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, pershkrimi, db, out coltrupiQK, trupivjeterqendra, iddegeadm, idPer, out shfaqmesazhapolupe, rishpernda, shperndaDifQKPModDok, idStatusDok);
            var kokaqender = new DbQendraKosto.clsKokaQendraKosto();
            var dbqend = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var mesazh = DbQendraKosto.clsKokaQendraKosto.KrijoQKLP(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaQK, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, coltrupiQK, dbqend, ref kokaqender, shperndaDifQKPModDok);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            //DbQendraKosto.clsKokaQendraKosto kokaqender = DbQendraKosto.clsKokaQendraKosto.krijoQKShitje(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaQK, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, dbshare, trupivjeterqendra, 0);
            #endregion

            var kokaFK = new clsKokaFleteKontabel();
            if (colTrupFK.Count == 0)//rasti per aqt ne magazina joaktive
                return kokaFK;
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, idKonf);
            mesazh = kokaFK.KrijoFlete(idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNiv, idMagKoka, idKonf, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelNdryshimCmimSasi(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colTrupiNdryshimCmimSasi trupiMag, double vlera, int idllogari, DateTime data, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, string pershkrimi, clsDatabaseKontabilitet db, out DbQendraKosto.colTrupiQendraKosto trupiQK, DbQendraKosto.colTrupiQendraKosto trupiQKold, int iddegeadm, int idperdoruesi, out string shfaqmesazh, bool rishpernda, bool shperndaDifQKPModDok, int statusdok)
        {
            shfaqmesazh = "jo";
            trupiQK = new DbQendraKosto.colTrupiQendraKosto();
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var eshteAzhornim = false;
            var dbshare = new clsDatabaseShare(db);
            var dbinv = new clsDatabaseInventari(db);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var dbAdmin = new clsDatabaseAdmin(db);
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbasete = new clsDatabazeAsete(db);
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = false;
            var monedheNdermarrje = new clsMonedha();
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK");
            if (alternativa == "Azhornim")
            {
                azhornim = true;
                monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbAdmin);
            }
            var kurseDate = new colKurset(idNdermarrje, data, dbAdmin);

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            //clsKlientFurnitor oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            //DbInventari.clsArtikulli oArtikull = new DbInventari.clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;

            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count() == 1)
                        tempCol.Add(tempSkemaKontTrupiNew_2.First());
                }
            }
            List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK> objektetQK = new List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK>();
            for (var i = 0; i < tempCol.Count; i++)
            {

                var tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = tempCol[i];

                var oLlojLlogarish = new clsLlojLlogarish();
                var oNenLlojLlogarie = new clsNenLlojLlogarish();

                oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                oLlojLlogarish = new clsLlojLlogarish(oLlojLlogarish.IdLlojLlogarie, db);

                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "ART"://nese ne kete rresht te grides eshte zgjedhur artikull
                            var rreshti = -1;
                            foreach (var veprimTrupi in trupiMag)
                            {
                                objektivat = new DbQendraKosto.colObjektivaKosto();
                                vleratobjektiva = new List<double>();
                                vleratobjektivamonbaze = new List<double>();
                                idllogobj = new List<int>();
                                var colTrupatPerQK = new colTrupatFletetKontabel();
                                var idnenllojllogarie = tempSkemaKontTrupi.IdNenLlojLlogarie;
                                rreshti++;

                                var vlerakont = veprimTrupi.VleftaRe - veprimTrupi.VleftaGjendje;
                                var ekziston = false;
                                var oArtikull = new clsArtikulli();
                                oArtikull.mbushArtikull(veprimTrupi.IdArtikulli, dbinv);
                                if (tempSkemaKontTrupi.DebikrediSkemeKontTrupi == 0)
                                    continue;
                                int idNenLlojLlogari;
                                idNenLlojLlogari = int.Parse(idnenllojllogarie);
                                var nenLlojLlogaria = new clsNenLlojLlogarish(idNenLlojLlogari, db);
                                oLlogari = oArtikull.merrLlogariArtikulli(nenLlojLlogaria.KodNenLlojLlogarie, db);

                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlerakont, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                var objekt = new DbQendraKosto.clsObjektivaKosto();
                                if (oArtikull.IdObjektivaKosto != 0 && oArtikull.IdObjektivaKosto != -1)
                                {
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqendra);
                                    if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);

                                        }
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);

                                }
                                if (objekt.Id != 0 && objekt.Id != -1)
                                {
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    var ekzistonqk = false;
                                    foreach (var f in colTrupatPerQK)
                                    {
                                        if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekzistonqk = true;
                                    }



                                    if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                }
                                foreach (var f in colTrupFK)
                                {
                                    if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                    }
                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                    }
                                    ekziston = true;
                                }
                                if (!ekziston)
                                    colTrupFK.Add(tLlogKF);

                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = veprimTrupi.IdMag, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                objektetQK.Add(obj);

                                //var mesazh = "jo";
                                //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, veprimTrupi.IdMag, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazh = mesazh;
                            }
                            break;
                    }
                }
                else
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "LL":
                            if (idllogari != 0)
                            {
                                var colTrupatPerQK = new colTrupatFletetKontabel();
                                oLlogari = new clsLlogari(idllogari, db);
                                //oLlogari = transactionCache.getLlogariFromCache(myColLlog,idllogari, db );
                                //oLlogari = new clsLlogari(idllogari, db);
                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlera, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlera, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                var ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    var ekzistonqk = false;
                                    foreach (var f in colTrupatPerQK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                    }

                                    if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);
                                    
                                    clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                    objektetQK.Add(obj);

                                    //var mesazh = "jo";
                                    //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                    //if (mesazh != "jo")
                                    //    shfaqmesazh = mesazh;
                                }
                            }
                            break;
                        case "":
                            break;
                    }
                }
            }

            string mesazh = "jo";
            trupiQK.AddRange(clsKokaQendraKosto.KrijoTrupQK(dbqendra, objektetQK, trupiQKold, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
            if (mesazh != "jo")
                shfaqmesazh = mesazh;

            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimNdryshimStatusiMagazine(int idMag, int idNiv, int idKonf, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, int iddokngaQK, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, colAQTSeriale colseriale, int idstatusmagpara, int idstatusmagaktuale, clsDatabaseKontabilitet db, ResourceManager rm, CultureInfo ci)
        {
            #region KOKA E FLETES KONTABEL
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var kontabilizuar = true;
            var idGrupKontabilizimi = 0;
            var dbshare = new clsDatabaseShare(db);

            var konfFK = new clsKonfigurimAmbjenti();
            konfFK.mbushKonfigAmbjSipasKod("FKNS", idNder, dbshare);
            var colTrupFK = new colTrupatFletetKontabel();
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonf, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbshare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbshare) == "Po";
            var coltrupiQK = new DbQendraKosto.colTrupiQendraKosto();
            colTrupFK = krijoTrupFletKontabelNdryshimStatusiMagazine(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, dtDk, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, colseriale, idstatusmagpara, idstatusmagaktuale, pershkrimi, db, out coltrupiQK, trupivjeterqendra, iddegeadm, idPer, out shfaqmesazhapolupe, rishpernda, shperndaDifQKPModDok, idMag, idStatusDok);
            var kokaqender = new DbQendraKosto.clsKokaQendraKosto();
            var dbqend = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var mesazh = DbQendraKosto.clsKokaQendraKosto.KrijoQKLP(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaQK, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, coltrupiQK, dbqend, ref kokaqender, shperndaDifQKPModDok);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            //DbQendraKosto.clsKokaQendraKosto kokaqender = DbQendraKosto.clsKokaQendraKosto.krijoQKShitje(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaQK, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, dbshare, trupivjeterqendra, 0);
            #endregion

            var kokaFK = new clsKokaFleteKontabel();
            if (colTrupFK.Count == 0)//rasti per aqt ne magazina joaktive
                return kokaFK;
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, idKonf);
            mesazh = kokaFK.KrijoFlete(idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNiv, idMag, idKonf, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelNdryshimStatusiMagazine(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, DateTime data, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, colAQTSeriale colseriale, int idstatusmagpara, int idstatusmagaktuale, string pershkrimi, clsDatabaseKontabilitet db, out DbQendraKosto.colTrupiQendraKosto trupiQK, DbQendraKosto.colTrupiQendraKosto trupiQKold, int iddegeadm, int idperdoruesi, out string shfaqmesazh, bool rishpernda, bool shperndaDifQKPModDok, int idmag, int statusdok)
        {
            shfaqmesazh = "jo";
            trupiQK = new DbQendraKosto.colTrupiQendraKosto();
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var eshteAzhornim = false;
            var dbshare = new clsDatabaseShare(db);
            var dbinv = new clsDatabaseInventari(db);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var dbAdmin = new clsDatabaseAdmin(db);
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbasete = new clsDatabazeAsete(db);
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = false;
            var monedheNdermarrje = new clsMonedha();
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK");
            if (alternativa == "Azhornim")
            {
                azhornim = true;
                monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbAdmin);
            }

            var kurseDate = new colKurset(idNdermarrje, data, dbAdmin);

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            //clsKlientFurnitor oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            //DbInventari.clsArtikulli oArtikull = new DbInventari.clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;

            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }
                }
            }
            List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK> objektetQK = new List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK>();
            for (var i = 0; i < tempCol.Count; i++)
            {
                var tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = tempCol[i];

                var oLlojLlogarish = new clsLlojLlogarish();
                var oNenLlojLlogarie = new clsNenLlojLlogarish();

                oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                oLlojLlogarish = new clsLlojLlogarish(oLlojLlogarish.IdLlojLlogarie, db);

                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    if (tempSkemaKontTrupi.FormulaSkemeKontTrupi != "gjithmone")
                    {
                        switch (oLlojLlogarish.KodLlojLlogarie)
                        {
                            case "ART"://nese ne kete rresht te grides eshte zgjedhur artikull
                                var status = new clsStatusMagazine_Asete(idstatusmagpara, dbasete);
                                var statustransf = new clsStatusMagazine_Asete(idstatusmagaktuale, dbasete);

                                if (status.Emertimi != "Aktive" && statustransf.Emertimi == "Aktive") //jo aktiv ne aktiv
                                {
                                    if (tempSkemaKontTrupi.IdNenLlojLlogarie == "11")//llogaria e inventarit
                                        tempSkemaKontTrupi.DebikrediSkemeKontTrupi = 1;
                                    else tempSkemaKontTrupi.DebikrediSkemeKontTrupi = 2;
                                }
                                else if (status.Emertimi == "Aktive" && statustransf.Emertimi != "Aktive")//aktiv ne joaktiv
                                {
                                    if (tempSkemaKontTrupi.IdNenLlojLlogarie == "11")//llogaria e inventarit
                                        tempSkemaKontTrupi.DebikrediSkemeKontTrupi = 2;
                                    else tempSkemaKontTrupi.DebikrediSkemeKontTrupi = 1;
                                }
                                else continue;

                                foreach (var serial in colseriale)
                                {
                                    var colTrupatPerQK = new colTrupatFletetKontabel();
                                    double vlerakont = 0;
                                    var ekziston = false;
                                    var oArtikull = new clsArtikulli();
                                    oArtikull.mbushArtikull(serial.IdAQTArt, dbinv);

                                    var cmimi = clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, idNdermarrje, data, 0, dbasete);
                                    if (oArtikull.MeSerial)
                                        vlerakont = cmimi;
                                    else
                                    {
                                        var historik = new clsHistorikAQTSeriale();
                                        historik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idNdermarrje, dbasete);
                                        vlerakont = historik.VleftaProgresive;
                                    }
                                    if (tempSkemaKontTrupi.DebikrediSkemeKontTrupi == 0)
                                        continue;

                                    oLlogari = oArtikull.merrLlogariArtikulli(new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db).KodNenLlojLlogarie, db);

                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlerakont, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                    //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, vlerakont, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                    var objekt = new DbQendraKosto.clsObjektivaKosto();
                                    if (oArtikull.IdObjektivaKosto != 0 && oArtikull.IdObjektivaKosto != -1)
                                    {
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqendra);
                                        if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    }
                                    else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    if (objekt.Id != 0 && objekt.Id != -1)
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        var ekzistonqk = false;
                                        foreach (var f in colTrupatPerQK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekzistonqk = true;
                                            }
                                        }

                                        if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekziston = true;
                                            }
                                        }
                                        if (!ekziston)
                                            colTrupFK.Add(tLlogKF);

                                        clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = idmag, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                        objektetQK.Add(obj);

                                        //var mesazh = "jo";
                                        //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, idmag, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                        //if (mesazh != "jo")
                                        //    shfaqmesazh = mesazh;
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            string mesazh = "jo";
            trupiQK.AddRange(clsKokaQendraKosto.KrijoTrupQK(dbqendra, objektetQK, trupiQKold, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
            if (mesazh != "jo")
                shfaqmesazh = mesazh;

            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimBanka(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colVeprimBankaTrupi coltrupi, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, double totali, double kursi, int idmonedha, out List<(int,double)> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, double komisionBanke, int idbanka, string kredite, clsDatabaseArkaBanka db, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra)
        {
            #region KOKA E FLETES KONTABEL
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var dbshare = new clsDatabaseShare(db);
            //dbshare.vendosManager(db );
            var konf = new clsKonfigurimAmbjenti(idKonfgjenerues, dbshare);

            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);

            var colTrupFK = new colTrupatFletetKontabel();
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else

                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            colTrupFK = krijoTrupFletKontabelBanka(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, coltrupi, totali, kursi, idmonedha, out emrakf, out rreshtakf, out trupiPerGjendjeKF, komisionBanke, idbanka, kredite, dtDk, db, out objektivat, pershkrimi, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);

            var kokaqender = clsKokaQendraKosto.KrijoQK(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, dbshare, trupivjeterqendra, 0);
            #endregion
            var kokaFK = new clsKokaFleteKontabel();
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, idKonfgjenerues);
            var mesazh = kokaFK.KrijoFlete(idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelBanka(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colVeprimBankaTrupi trupi, double totali, double kursi, int idmonedha, out List<(int,double)> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, double komisionBanke, int idbanka, string kredite, DateTime data, clsDatabaseArkaBanka db, out colObjektivaKosto objektivat, string pershkrimi, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            objektivat = new colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var dbshare = new clsDatabaseShare(db);
            var dbqendra = new clsDatabaseQendraKosto(db);
            //dbshare.vendosManager(db );
            var dbkont = new clsDatabaseKontabilitet(db);
            var dbAdmin = new clsDatabaseAdmin(db);
            var eshteAzhornim = false;
            double VF = 0;//vlera e furnitorit
            double VKO = 0;
            if (komisionBanke != 0)
                VKO = komisionBanke;//vlera e komisionit
            else
                VKO = 0;
            double VS = 0; // vlera e skontos
            double VC = 0;//Vlera e krediteve te perfituara
            double VLD = 0; //Vlera me te cilen eshte prekur llogaria debi tek trupi i bankes
            double VLK = 0; //Vlera me te cilen eshte prekur llogaria kredi tek trupi i bankes
            double VP = 0; //Vlera e paarketueshme
            var VV = totali;//vlera e bankes
            var KV = kursi;//KV= Kursi i bankes

            foreach (var veprimTrupi in trupi)
            {
                if (veprimTrupi.Lloji == "Klient" || veprimTrupi.Lloji == "Furnitor")
                {
                    VF = veprimTrupi.VleraPaguarMonedhaBaze;
                    VS = veprimTrupi.Zbritja;
                    VC = veprimTrupi.Kreditet;
                    VP = veprimTrupi.VleraPaArketueshme;
                }
                else if (veprimTrupi.Lloji == "Llogari" || veprimTrupi.Lloji == "Punonjes")
                {
                    if (veprimTrupi.DebiKredi == "Debi")
                        VLD += veprimTrupi.VleraPaguarMonedhaBaze;
                    else if (veprimTrupi.DebiKredi == "Kredi")
                        VLK += veprimTrupi.VleraPaguarMonedhaBaze;
                }
            }
            //shton te gjitha vlerat primare ne nje list 
            var list = new List<structVlera>();
            var s = new structVlera();
            s.kodi = "VKO";
            s.vlera = Math.Round(VKO * KV, RrumbullakosMe);
            list.Add(s);
            s = new structVlera();
            s.kodi = "VV";
            s.vlera = Math.Round(VV * KV, RrumbullakosMe);//vlera e bankes ne monedhe baze
            list.Add(s);
            s = new structVlera();
            s.kodi = "VKD";
            s.vlera = Math.Round(VC * KV, RrumbullakosMe);
            list.Add(s);
            s = new structVlera();
            s.kodi = "VZB";
            s.vlera = Math.Round(VS * KV, RrumbullakosMe);
            list.Add(s);
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = false;
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK");

            if (alternativa == "Azhornim")
                azhornim = true;

            var monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbAdmin);
            var kurseDate = new colKurset(idNdermarrje, data, dbAdmin);
            rreshtakf = new List<string>();
            emrakf = new List<(int, double)>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var debiKredi = 0;

            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;

            var clsSkemaKontNew = new clsSkemaKontabelNew(idSkemeKontabel, dbkont);


            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            for (var i = 0; i < tempCol.Count; i++)
            {
                List<double> kurseKf = new List<double>();
                var tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = tempCol[i];

                var oLlojLlogarish = new clsLlojLlogarish();
                var oNenLlojLlogarie = new clsNenLlojLlogarish();
                IEnumerable<double> vl;

                double v = 0;
                oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                oLlojLlogarish = new clsLlojLlogarish(oLlojLlogarish.IdLlojLlogarie, dbkont);


                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "LL"://nese ne kete rresht te grides eshte zgjedhur llogari ose punonjes
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = new clsNenLlojLlogarish(oNenLlojLlogarie.IdNenLlojLlogarie, dbkont);
                            foreach (var veprimTrupi in trupi)
                            {
                                if (veprimTrupi.Lloji == "Llogari" || veprimTrupi.Lloji == "Punonjes") //Llogari ose llogaria e punonjesit
                                {
                                    oLlogari = veprimTrupi.Lloji == "Llogari" ? new clsLlogari(veprimTrupi.IdSubjekti, dbkont) : new clsLlogari(new DbListPagesat.clsPunonjes(veprimTrupi.IdSubjekti, new DbListPagesat.clsDatabazeListPagesa(dbkont)).IdLlogari, dbkont);
                                   
                                    debiKredi = clsSkemaKontabelTrupiNew.ktheDebiKredi(tempSkemaKontTrupi, veprimTrupi.DebiKredi);
                                    
                                    var ekziston = false;
                                    var pershkrimitrupi = (veprimTrupi.PershkrimiTrupi == "") ? pershkrimi : veprimTrupi.PershkrimiTrupi;
                                    tLlogKF = new clsTrupiFleteKontabel(debiKredi, veprimTrupi.VleraPaguarMonedhaBaze, oLlogari, azhornim, (monedheNdermarrje.IdMonedha == idmonedha || veprimTrupi.MeKursFature) ? veprimTrupi.KMK : kursi, idmonedha, eshteAzhornim, pershkrimitrupi, kurseDate, monedheNdermarrje, dbAdmin, (monedheNdermarrje.IdMonedha == idmonedha || veprimTrupi.MeKursFature) ? false : true);
                                    if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    {
                                        var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                    }
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari && f.Kursi == tLlogKF.Kursi)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekziston = true;
                                            }
                                        }
                                        if (!ekziston) colTrupFK.Add(tLlogKF);
                                    }
                                }
                            }
                            break;
                        case "KL":
                            kurseKf = new List<double>();
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = new clsNenLlojLlogarish(oNenLlojLlogarie.IdNenLlojLlogarie, dbkont);
                            foreach (var veprimTrupi in trupi)
                            {
                                if (veprimTrupi.Lloji == "Klient" || veprimTrupi.Lloji == "Furnitor")
                                {
                                    oKlient = new clsKlientFurnitor(veprimTrupi.IdSubjekti, dbkont);
                                    oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, dbkont);
                                    debiKredi = clsSkemaKontabelTrupiNew.ktheDebiKredi(tempSkemaKontTrupi, veprimTrupi.DebiKredi);
                                   
                                    var pershkrimitrupi = (veprimTrupi.PershkrimiTrupi == "") ? pershkrimi : veprimTrupi.PershkrimiTrupi;
                                    //ndryshuar vlera me te cilen preket furnitori ku shte i lidhur me nje kusht pagese. Ishte Convert.ToDecimal(veprimTrupi.VleraPaguarMonedhaBaze)
                                    tLlogKF = new clsTrupiFleteKontabel(debiKredi, veprimTrupi.VleraPaguarMonedhaBaze + veprimTrupi.Zbritja, oLlogari, azhornim, (monedheNdermarrje.IdMonedha == idmonedha || veprimTrupi.MeKursFature) ? veprimTrupi.KMK : kursi, idmonedha, eshteAzhornim, pershkrimitrupi, kurseDate, monedheNdermarrje, dbAdmin, (monedheNdermarrje.IdMonedha == idmonedha || veprimTrupi.MeKursFature) ? false : true);

                                    var objektkl = new DbQendraKosto.clsObjektivaKosto();
                                    if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                    {
                                        objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto, dbqendra);
                                        if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                            {
                                                objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                            }
                                    }
                                    else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    {
                                        objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    }
                                    if (objektkl.Id != 0 && objektkl.Id != -1)
                                    {
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                    }
                                    var ekziston = false;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        var eksistonkf = false;
                                        if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                        {
                                            for (var k = 0; k < emrakf.Count; k++)
                                            {
                                                if (emrakf[k].Item1 ==oKlient.IdKlientFurnitor && emrakf[k].Item2 == tLlogKF.Kursi)//oKlient.IdKlientFurnitor == Convert.ToInt32(emrakf[k]) && kurs == kurseKf[k])
                                                {
                                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        trupiPerGjendjeKF[k].VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                        trupiPerGjendjeKF[k].VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                    }
                                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        trupiPerGjendjeKF[k].VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                        trupiPerGjendjeKF[k].VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                    }
                                                    eksistonkf = true;
                                                    break;
                                                }
                                            }
                                            if (!eksistonkf)
                                            {
                                                var t = new clsTrupiFleteKontabel();
                                                t.VleftaDebiMonBazeTrupiFleteKontabel = tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                t.VleftaDebiTrupiFleteKontabel = tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                t.VleftaKrediMonBazeTrupiFleteKontabel = tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                t.VleftaKrediTrupiFleteKontabel = tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                t.IdMonedha = tLlogKF.IdMonedha;
                                                t.Kursi = tLlogKF.Kursi;

                                                trupiPerGjendjeKF.Add(t);
                                                if (oKlient.LlojiKF == true)
                                                    rreshtakf.Add("KL");
                                                else
                                                    rreshtakf.Add("FR");
                                                emrakf.Add((oKlient.IdKlientFurnitor, tLlogKF.Kursi));
                                            }
                                        }
                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari && tLlogKF.Kursi == f.Kursi)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekziston = true;
                                            }
                                        }
                                        if (!ekziston)
                                            colTrupFK.Add(tLlogKF);
                                    }
                                }
                            }
                            break;
                    }
                }
                else if (tempSkemaKontTrupi.KodSkemeKontTrupi != "VDK")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "BN":
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = new clsNenLlojLlogarish(oNenLlojLlogarie.IdNenLlojLlogarie, dbkont);

                            var oBanka = new clsBanka();
                            oBanka.mbushBankeID(idbanka, db);
                            oLlogari = oBanka.merrLlogariBanke(oNenLlojLlogarie.KodNenLlojLlogarie, dbkont);

                            vl = from l in list
                                 where l.kodi == tempSkemaKontTrupi.KodSkemeKontTrupi
                                 select l.vlera;
                            v = vl.First<double>();
                           
                            tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, v, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                            {
                                var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                            }
                            var ekziston = false;
                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                            {
                                foreach (var f in colTrupFK)
                                {
                                    if (f.IdLlogari == tLlogKF.IdLlogari && f.Kursi == tLlogKF.Kursi)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekziston = true;
                                    }
                                }
                                if (!ekziston) colTrupFK.Add(tLlogKF);
                            }
                            break;
                        case "LL":
                            if (kredite == "")
                                break;
                            oLlogari = new clsLlogari(kredite, idNdermarrje, dbkont);
                            //oLlogari = transactionCache.getLlogariFromCache(kredite, idNdermarrje, dbkont);
                            //oLlogari = new clsLlogari(kredite, idNdermarrje, dbkont);

                            vl = from l in list
                                 where l.kodi == tempSkemaKontTrupi.KodSkemeKontTrupi
                                 select l.vlera;
                            v = vl.First<double>();
                            tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, v, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                            {
                                var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                            }
                            ekziston = false;
                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                            {
                                foreach (var f in colTrupFK)
                                {
                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekziston = true;
                                    }
                                }
                                if (!ekziston) colTrupFK.Add(tLlogKF);
                            }
                            break;
                        case "KL":  //llogari klient , eshte ne db rreshti tek T_LLOJLLOGARISH me id 1
                            //duhet gjetur rreshti qe permbush kushtin e klientit
                            //nga ky objekt merret idnenllojllogaria perkatese

                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = new clsNenLlojLlogarish(oNenLlojLlogarie.IdNenLlojLlogarie, dbkont);
                            foreach (var veprimTrupi in trupi)
                            {
                                if (veprimTrupi.Lloji == "Klient" || veprimTrupi.Lloji == "Furnitor")
                                {
                                    oKlient = new clsKlientFurnitor(veprimTrupi.IdSubjekti, dbkont);
                                    oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, dbkont);
                                    debiKredi = clsSkemaKontabelTrupiNew.ktheDebiKredi(tempSkemaKontTrupi, veprimTrupi.DebiKredi);

                                    //ndryshuar vlera me te cilen preket furnitori ku shte i lidhur me nje kusht pagese. Ishte Convert.ToDecimal(veprimTrupi.VleraPaguarMonedhaBaze)
                                    var pershkrimitrupi = (veprimTrupi.PershkrimiTrupi == "") ? pershkrimi : veprimTrupi.PershkrimiTrupi;
                                    tLlogKF = new clsTrupiFleteKontabel(debiKredi, veprimTrupi.Zbritja, oLlogari, azhornim, veprimTrupi.MeKursFature ? veprimTrupi.KMK : kursi, idmonedha, eshteAzhornim, pershkrimitrupi, kurseDate, monedheNdermarrje, dbAdmin);

                                    var objektkl = new clsObjektivaKosto();
                                    if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                    {
                                        objektkl = new clsObjektivaKosto(oKlient.IdObjektivaKosto, dbqendra);
                                        if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                            {
                                                objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                            }
                                    }
                                    else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    {
                                        objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);

                                    }
                                    if (objektkl.Id != 0 && objektkl.Id != -1)
                                    {
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                    }
                                    ekziston = false;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari && f.Kursi == tLlogKF.Kursi)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekziston = true;
                                            }
                                        }
                                        var eksistontrupiperkf = false;
                                        if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                        {
                                            foreach (var ff in trupiPerGjendjeKF)
                                            {
                                                if (ff.IdLlogari == tLlogKF.IdLlogari && ff.Kursi == tLlogKF.Kursi)
                                                {
                                                    eksistontrupiperkf = true;
                                                }
                                            }
                                            if (!eksistontrupiperkf)
                                            {
                                                trupiPerGjendjeKF.Add(tLlogKF);

                                                if (oKlient.LlojiKF == true)
                                                    rreshtakf.Add("KL");
                                                else
                                                    rreshtakf.Add("FR");
                                                emrakf.Add((oKlient.IdKlientFurnitor, tLlogKF.Kursi));
                                            }
                                        }
                                        if (!ekziston)
                                            colTrupFK.Add(tLlogKF);
                                    }
                                }
                            }
                            break;
                        case "":
                            break;
                    }
                }
            }
            //per te grupuar vlerat e llogarive
            foreach (var f in colTrupFK)
            {
                if (f.VleftaDebiMonBazeTrupiFleteKontabel != 0 && f.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                {
                    var diferenca = f.VleftaDebiMonBazeTrupiFleteKontabel - f.VleftaKrediMonBazeTrupiFleteKontabel;
                    if (diferenca > 0)
                    {
                        f.VleftaDebiMonBazeTrupiFleteKontabel = diferenca;
                        f.VleftaDebiTrupiFleteKontabel = f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel;
                        f.VleftaKrediTrupiFleteKontabel = 0;
                        f.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                    }
                    else if (diferenca < 0)
                    {
                        f.VleftaKrediMonBazeTrupiFleteKontabel = Math.Abs(diferenca);
                        f.VleftaKrediTrupiFleteKontabel = Math.Abs(f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel);
                        f.VleftaDebiTrupiFleteKontabel = 0;
                        f.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    }
                }
            }
            return colTrupFK;
        }

      
        internal static clsKokaFleteKontabel gjeneroKontabilizimVDK(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colVeprimBankaTrupi coltrupi, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, double kursi, int idmonedha, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, int idklientfurnitor, colDokumentLidhesTrupi coltrupidokumenti, int idllojdokbanka, clsDatabaseArkaBanka db, clsKokaShitje kokashitje, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra)
        {
            #region KOKA E FLETES KONTABEL

            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var dbshare = new clsDatabaseShare(db);
            //dbshare.vendosManager(db );
            var konf = new clsKonfigurimAmbjenti(idKonfgjenerues, dbshare);

            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            var colTrupFK = new colTrupatFletetKontabel();
            colTrupFK = krijoTrupFletKontabelVDK(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, dtDk, coltrupi, kursi, idmonedha, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, idklientfurnitor, coltrupidokumenti, idllojdokbanka, db, kokashitje, pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);

            var kokaqender = clsKokaQendraKosto.KrijoQK(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, db, trupivjeterqendra, 0);
            if (colTrupFK.Count == 0)
                return new clsKokaFleteKontabel();
           
            #endregion

            var kokaFK = new clsKokaFleteKontabel();
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, idKonfgjenerues);
            var mesazh = kokaFK.KrijoFlete(idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelVDK(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, DateTime datDok, colVeprimBankaTrupi trupi, double kursi, int idmonedha, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, int idklientfurnitor, colDokumentLidhesTrupi trupiDokumenti, int idllojdok, clsDatabaseArkaBanka db, clsKokaShitje kokashitje, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var dbshare = new clsDatabaseShare(db);
            //dbshare.vendosManager(db );
            var dbkont = new clsDatabaseKontabilitet(db);
            var dbAdmin = new clsDatabaseAdmin(db);
            var dbregj = new clsDatabaseRegjistrim(db);
            //dbregj.vendosManager(db );
            var eshteAzhornim = true;
            var kv = kursi;
            double kk = 0;//KK = Kursi i fatures
            double kmk = 0; // KMK = Kursi i monedhes se fatures ne daten e dokumentit te bankes
            double vv = 0;//VV= Vlera e arketuar (e rreshtit ne gride)

            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim"; 
            var kusht = new clsKusht(idKonfigAmbjente, "LLFK", dbshare);
            var monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbAdmin);

            var kurseDate = new colKurset(idNdermarrje, data, dbAdmin); rreshtakf = new List<string>(); emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var monklient = 0; var dkmon = 0;

            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            var debiKredi = 0;
            var clsSkemaKontNew = new clsSkemaKontabelNew(idSkemeKontabel, dbkont);


            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            var m = 0;
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            foreach (var veprimTrupi in trupi)
            {
                for (var i = 0; i < tempCol.Count; i++)
                {

                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];

                    var oLlojLlogarish = new clsLlojLlogarish();
                    var oNenLlojLlogarie = new clsNenLlojLlogarish();

                    oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                    oLlojLlogarish = new clsLlojLlogarish(oLlojLlogarish.IdLlojLlogarie, dbkont);

                    if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VDK")
                    {
                        double VDK = 0; var idmonedhafat = 0; var idkf = 0; var dK = 0;
                        double kursifat = 0; DateTime datafat;

                        switch (oLlojLlogarish.KodLlojLlogarie)
                        {
                            case "ML":  //llogari monedhe
                                oNenLlojLlogarie = new clsNenLlojLlogarish();
                                oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                                oNenLlojLlogarie = new clsNenLlojLlogarish(oNenLlojLlogarie.IdNenLlojLlogarie, dbkont);


                                if (veprimTrupi.Lloji == "Klient" || veprimTrupi.Lloji == "Furnitor")
                                    if (veprimTrupi.IdSubjekti == idklientfurnitor)
                                    {
                                        VDK = 0; idmonedhafat = 0; idkf = 0;
                                        kursifat = 0;

                                        if (veprimTrupi.IdFatura != 0)
                                        {
                                            //DbCore.DbRegjistrim.clsNivelRegjistrimi niveli = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                                            //niveli.mbushNivelRegjistrimiSipasID(veprimTrupi.IdNivel, dbregj);
                                            var idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(veprimTrupi.IdNivel, dbregj);
                                            if (idKategoria != 20)
                                            {
                                                var fatura = new clsKokaShitje(veprimTrupi.IdFatura, dbregj);//rast kur gjenerohet nga fatura sepse selekti jep lock //new DbCore.DbRegjistrim.clsKokaShitje();
                                                //fatura.mbushKokaShitjeSipasID(veprimTrupi.IdFatura,dbregj);

                                                idkf = fatura.IdKlientFurnitor;
                                                idmonedhafat = fatura.IdMonedha;
                                                kursifat = fatura.Kursi;
                                                datafat = fatura.DtDok;
                                            }
                                            else
                                            {
                                                var fatura = new colVeprimeKFTrupi();
                                                fatura.MbushVeprimeKfTrupi(veprimTrupi.IdFatura, dbregj);
                                                idkf = fatura[0].IdKF;
                                                idmonedhafat = fatura[0].IdMonedha;
                                                kursifat = fatura[0].Kursi;
                                                datafat = fatura[0].Data;
                                            }

                                            var azh = new clsAzhornimKFKoka(veprimTrupi.IdSubjekti, datDok, dbregj);
                                            if (azh.DateDok >= datafat)
                                                kk = azh.Kursi;
                                            else
                                                kk = kursifat;
                                            kmk = veprimTrupi.KMK;
                                            vv = veprimTrupi.VleraPaguar;
                                            VDK = LlogaritDiferenceKursi(kv, kk, kmk, vv);


                                            var klienti = new clsKlientFurnitor(idkf, dbkont);
                                            //DbCore.DbAdmin.clsNdermarrje nd = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje, dbAdmin);

                                            oLlogari = new clsLlogari();
                                            //DbCore.DbAdmin.clsMonedha monKF = new DbCore.DbAdmin.clsMonedha();
                                            //monKF.IdMonedha = nd.NdermarrjeMonedha;
                                            //monKF = new DbAdmin.clsMonedha(nd.NdermarrjeMonedha, dbAdmin);

                                            oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, idmonedhafat, klienti.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie, dbkont);

                                            dK = 0;

                                            if (idllojdok == 3)
                                            {
                                                if (VDK > 0)
                                                    dK = 2;
                                                else if (VDK < 0)
                                                    dK = 1;
                                            }
                                            if (idllojdok == 4)
                                            {
                                                if (VDK > 0)
                                                    dK = 1;
                                                else if (VDK < 0)
                                                    dK = 2;
                                            }
                                            if (dK == 1) dkmon = 2;
                                            else dkmon = 1;
                                            debiKredi = dkmon;//tempSkemaKontTrupi.DebikrediSkemeKontTrupi;

                                            var krijoFK = (oNenLlojLlogarie.KodNenLlojLlogarie == "LLMF" && dkmon == 2) || (oNenLlojLlogarie.KodNenLlojLlogarie == "LLMH" && dkmon == 1);


                                            if (krijoFK == true)
                                            {
                                                tLlogKF = new clsTrupiFleteKontabel(debiKredi, VDK, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                                //tLlogKF = new clsTrupiFleteKontabel(debiKredi, VDK, oLlogari, azhornim, idNdermarrje, kursi, idmonedha, data, eshteAzhornim, pershkrimi, dbAdmin);
                                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                {
                                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                                }
                                                var ekziston = false;
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    foreach (var f in colTrupFK)
                                                    {
                                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                                        {
                                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                            {
                                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                            }
                                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                            {
                                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                            }
                                                            ekziston = true;
                                                        }
                                                    }
                                                    if (!ekziston) colTrupFK.Add(tLlogKF);
                                                }
                                            }
                                        }
                                    }

                                break;
                            case "KL":
                                oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbkont);
                                VDK = 0;
                                dK = 0;
                                idmonedhafat = 0; idkf = 0;
                                kursifat = 0;

                                if ((veprimTrupi.Lloji == "Klient" || veprimTrupi.Lloji == "Furnitor") && veprimTrupi.IdFatura != 0)
                                {
                                    if (veprimTrupi.IdSubjekti == idklientfurnitor)
                                    {
                                        //DbCore.DbRegjistrim.clsNivelRegjistrimi niveli = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                                        //niveli.mbushNivelRegjistrimiSipasID(veprimTrupi.IdNivel, dbregj);
                                        var idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(veprimTrupi.IdNivel, dbregj);
                                        if (idKategoria != 20)
                                        {
                                            var fatura = new clsKokaShitje(veprimTrupi.IdFatura, dbregj);
                                            idkf = fatura.IdKlientFurnitor;
                                            idmonedhafat = fatura.IdMonedha;
                                            kursifat = fatura.Kursi;
                                            datafat = fatura.DtDok;
                                        }
                                        else
                                        {
                                            var fatura = new colVeprimeKFTrupi();
                                            fatura.MbushVeprimeKfTrupi(veprimTrupi.IdFatura, dbregj);
                                            idkf = fatura[0].IdKF; //VER BRAKE POINT KETU PER BUG: 6630
                                            idmonedhafat = fatura[0].IdMonedha;
                                            kursifat = fatura[0].Kursi;
                                            datafat = fatura[0].Data;
                                        }

                                        var azh = new clsAzhornimKFKoka(veprimTrupi.IdSubjekti, datDok, dbregj);
                                        if (azh.DateDok >= datafat)
                                            kk = azh.Kursi;
                                        else
                                            kk = kursifat;
                                        kmk = veprimTrupi.KMK;
                                        vv = veprimTrupi.VleraPaguar;
                                        VDK = LlogaritDiferenceKursi(kv, kk, kmk, vv);
                                        //oKlient = transactionCache.GetKfFromCache(colKfCache, idkf, dbkont);
                                        oKlient = new clsKlientFurnitor(idkf, dbkont);
                                        oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, dbkont);
                                        //oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, dbkont); 
                                        monklient = oLlogari.IdMonedha;//todo Nestila - monklient nuk perdoret mos duhet hequr
                                        if (idllojdok == 3)
                                        {
                                            if (VDK > 0)
                                                dK = 2;
                                            else if (VDK < 0)
                                                dK = 1;
                                        }
                                        if (idllojdok == 4)
                                        {
                                            if (VDK > 0)
                                                dK = 1;
                                            else if (VDK < 0)
                                                dK = 2;
                                        }
                                        if (dK == 1) dkmon = 2;
                                        else dkmon = 1;
                                        tLlogKF = new clsTrupiFleteKontabel(dK, VDK, oLlogari, azhornim, kursi, idmonedha, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                        //tLlogKF = new clsTrupiFleteKontabel(dK, VDK, oLlogari, azhornim, idNdermarrje, kursi, idmonedha, data, eshteAzhornim, pershkrimi, dbAdmin);
                                        var objektkl = new DbQendraKosto.clsObjektivaKosto();
                                        if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto, dbqendra);
                                            if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                {
                                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);

                                                }
                                        }
                                        else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);

                                        }
                                        if (objektkl.Id != 0 && objektkl.Id != -1)
                                        {
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                        }
                                        var ekziston = false;
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            foreach (var f in colTrupFK)
                                            {
                                                if (f.IdLlogari == tLlogKF.IdLlogari)
                                                {
                                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                    }
                                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                    }
                                                    ekziston = true;
                                                }
                                            }
                                            var eksistontrupiperkf = false;
                                            if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                            {
                                                foreach (var f in trupiPerGjendjeKF)
                                                {
                                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                                    {

                                                        eksistontrupiperkf = true;
                                                    }
                                                }
                                                if (!eksistontrupiperkf)
                                                {
                                                    trupiPerGjendjeKF.Add(tLlogKF);
                                                    if (veprimTrupi.Lloji == "Klient")
                                                        rreshtakf.Add("KL");
                                                    else rreshtakf.Add("FR");
                                                    emrakf.Add(oKlient.IdKlientFurnitor);

                                                }
                                            }
                                            if (!ekziston)
                                                colTrupFK.Add(tLlogKF);
                                        }

                                    }
                                }

                                break;
                        }
                    }
                }
                if (veprimTrupi.IdFatura != 0 && (veprimTrupi.Lloji == "Klient" || veprimTrupi.Lloji == "Furnitor") && veprimTrupi.IdSubjekti == idklientfurnitor) m += 1;
            }
            //per te grupuar vlerat e llogarive
            foreach (var f in colTrupFK)
            {
                if (f.VleftaDebiMonBazeTrupiFleteKontabel != 0 && f.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                {
                    var diferenca = f.VleftaDebiMonBazeTrupiFleteKontabel - f.VleftaKrediMonBazeTrupiFleteKontabel;
                    if (diferenca > 0)
                    {
                        f.VleftaDebiMonBazeTrupiFleteKontabel = diferenca;
                        f.VleftaDebiTrupiFleteKontabel = f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel;
                        f.VleftaKrediTrupiFleteKontabel = 0;
                        f.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                    }
                    else if (diferenca < 0)
                    {
                        f.VleftaKrediMonBazeTrupiFleteKontabel = Math.Abs(diferenca);
                        f.VleftaKrediTrupiFleteKontabel = Math.Abs(f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel);
                        f.VleftaDebiTrupiFleteKontabel = 0;
                        f.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    }
                }
            }
            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimLidhjeDok(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colDokumentat doklidhes, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, int idklientfurnitor, colDokumentat dokKryesor, double totali, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha)
        {
            #region KOKA E FLETES KONTABEL
            var dbshare = new clsDatabaseShare(new clsDatabaseKontabilitet());
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(idKonfgjenerues);
            var konfFK = new clsKonfigurimAmbjenti();
            konfFK.mbushKonfiguriminMeID(konf.IdSkemeKontabel);
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK");
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD") == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD") == "Po";
            var colTrupFK = new colTrupatFletetKontabel(); rreshtakf = new List<string>(); emrakf = new List<int>();
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();
            if (dokKryesor.Count == 1 && doklidhes.Count >= 1)

                colTrupFK = krijoTrupFletKontabelLidhjeDok1meshume(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, doklidhes, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, idklientfurnitor, dokKryesor[0], pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);
            else if (dokKryesor.Count >= 1 && doklidhes.Count == 1)
                colTrupFK = krijoTrupFletKontabelLidhjeDokshumeme1(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, dokKryesor, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, idklientfurnitor, doklidhes[0], totali, out objektivat, pershkrimi, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);
            var kokaqender = clsKokaQendraKosto.KrijoQK(new DbData(), konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, trupivjeterqendra, 0, rishpernda, shperndaDifQKPModDok);
            #endregion
            var kokaFK = new clsKokaFleteKontabel();
            if (colTrupFK.Count == 0)
                return new clsKokaFleteKontabel();
            var mesazh = kokaFK.KrijoFlete(new DbData(), idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimLidhjeDok(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colDokumentat doklidhes, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, int idklientfurnitor, colDokumentat dokKryesor, double totali, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, clsDatabaseKontabilitet db, int idkonfigdoklidhes)
        {
            #region KOKA E FLETES KONTABEL


            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var shfaqmesazhapolupe = "Jo";
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbshare = new clsDatabaseShare(db);
            var dbadmn = new clsDatabaseAdmin(db);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var konf = new clsKonfigurimAmbjenti(idkonfigdoklidhes, dbshare);


            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);


            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk.mbushKonfigAmbjSipasId(kusht.Vlera, dbshare, idGjuha);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            var colTrupFK = new colTrupatFletetKontabel(); rreshtakf = new List<string>(); emrakf = new List<int>();
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();
            var kokaFK = new clsKokaFleteKontabel();
            if (dokKryesor.Count == 1 && doklidhes.Count >= 1)

                colTrupFK = krijoTrupFletKontabelLidhjeDok1meshume(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, doklidhes, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, idklientfurnitor, dokKryesor[0], pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, db);
            if (colTrupFK.Count == 0)
                return kokaFK;
            var kokaqender = DbQendraKosto.clsKokaQendraKosto.KrijoQK(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, db, trupivjeterqendra, 0);
            #endregion
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, idKonfgjenerues);
            var mesazh = kokaFK.KrijoFlete(idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelLidhjeDok1meshume(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colDokumentat doklidhes, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, int idklientfurnitor, clsDokumenti dokKryesor, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var eshteAzhornim = true;
            var kahuDokKryesor = KtheKahunDokumentitSipasIdDok(dokKryesor.IdNiveli, dokKryesor.IdDokumenti);

            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim";
            rreshtakf = new List<string>(); emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var monklient = 0; var dkmon = 0;

            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            var debiKredi = 0;
            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            var nd = new clsNdermarrje(idNdermarrje);
            var kurs = new clsKurset(dokKryesor.IdMonedha, data);
            foreach (var veprimTrupi in doklidhes)
            {
                for (var i = 0; i < tempCol.Count; i++)
                {

                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];

                    var oLlojLlogarish = new clsLlojLlogarish();
                    var oNenLlojLlogarie = new clsNenLlojLlogarish();

                    oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                    oLlojLlogarish = oLlojLlogarish.merrLlojLlogarieSipasID();
                    double kmk = 0;
                    double VDK = 0;
                    var dK = 0;
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "ML":  //llogari monedhe
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();
                            kmk = 0;
                            VDK = 0;
                            if (kurs != null)
                            {
                                if (veprimTrupi.IdMonedha == dokKryesor.IdMonedha)
                                    kmk = veprimTrupi.Kursi;
                                else kmk = kurs.VleraKursi;
                                VDK = LlogaritDiferenceKursi(veprimTrupi.Kursi, dokKryesor.Kursi, kmk, veprimTrupi.Vlefta);//llogaritDiferenceKursi(KV,KK,KMK,VV); 
                            }
                            var klienti = new clsKlientFurnitor(idklientfurnitor);
                            oLlogari = new clsLlogari();
                            oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, nd.NdermarrjeMonedha, klienti.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie);
                            debiKredi = dkmon;// tempSkemaKontTrupi.DebikrediSkemeKontTrupi;

                            var krijoFK = false;
                            if ((oNenLlojLlogarie.KodNenLlojLlogarie == "LLMF" && dkmon == 2) || (oNenLlojLlogarie.KodNenLlojLlogarie == "LLMH" && dkmon == 1))
                                krijoFK = true;

                            if (krijoFK == true)
                            {
                                tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), debiKredi, VDK, oLlogari, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                var ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston) colTrupFK.Add(tLlogKF);
                                }
                            }
                            break;
                        case "KL":
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();

                            kmk = 0;
                            VDK = 0;
                            dK = 0;

                            if (kurs != null)
                            {
                                if (dokKryesor.IdMonedha == veprimTrupi.IdMonedha)
                                    kmk = veprimTrupi.Kursi;
                                else
                                    kmk = kurs.VleraKursi;
                                VDK = LlogaritDiferenceKursi(veprimTrupi.Kursi, dokKryesor.Kursi, kmk, veprimTrupi.Vlefta);//llogaritDiferenceKursi(KV,KK,KMK,VV); 
                                oKlient = new clsKlientFurnitor(idklientfurnitor);
                                var oLlogariKF = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie);
                                monklient = oLlogariKF.IdMonedha;
                                if (kahuDokKryesor == "Debi")
                                {
                                    if (VDK > 0) //klienti preket ne debi (gjithmone ne kahun e dokumentit kryesor)
                                        dK = 1;
                                    else if (VDK < 0)
                                        dK = 2;
                                }
                                else if (kahuDokKryesor == "Kredi")
                                {
                                    if (VDK > 0) //klienti preket ne kredi (gjithmone ne kahun e dokumentit kryesor)
                                        dK = 2;
                                    else if (VDK < 0)
                                        dK = 1;
                                }
                                if (dK == 1) dkmon = 2;
                                else dkmon = 1;

                                tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), dK, VDK, oLlogariKF, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);
                                var objektkl = new DbQendraKosto.clsObjektivaKosto();
                                if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto);
                                    if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);

                                        }
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);

                                }
                                if (objektkl.Id != 0 && objektkl.Id != -1)
                                {
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                }
                                var ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    var eksistontrupiperkf = false;
                                    if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                    {
                                        foreach (var f in trupiPerGjendjeKF)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {

                                                eksistontrupiperkf = true;
                                            }
                                        }
                                        if (!eksistontrupiperkf)
                                        {
                                            trupiPerGjendjeKF.Add(tLlogKF);
                                            if (oKlient.LlojiKF == true)
                                                rreshtakf.Add("KL");
                                            else rreshtakf.Add("FR");
                                            emrakf.Add(oKlient.IdKlientFurnitor);

                                        }
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);
                                }

                            }
                            break;
                    }

                }
            }

            return colTrupFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelLidhjeDok1meshume(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colDokumentat doklidhes, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, int idklientfurnitor, clsDokumenti dokKryesor, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, clsDatabaseKontabilitet db)
        {
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var eshteAzhornim = true;
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbshare = new clsDatabaseShare(db);
            var dbadmn = new clsDatabaseAdmin(db);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var kahuDokKryesor = KtheKahunDokumentitSipasIdDok(dokKryesor.IdNiveli, dokKryesor.IdDokumenti, dbregj);

            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim";
            var monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbadmn);

            var kurseDate = new colKurset(idNdermarrje, data, dbadmn); rreshtakf = new List<string>(); emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var monklient = 0; var dkmon = 0;

            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            var debiKredi = 0;
            var clsSkemaKontNew = new clsSkemaKontabelNew(idSkemeKontabel, db);


            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            foreach (var veprimTrupi in doklidhes)
            {
                for (var i = 0; i < tempCol.Count; i++)
                {

                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];

                    var oLlojLlogarish = new clsLlojLlogarish();
                    var oNenLlojLlogarie = new clsNenLlojLlogarish();

                    oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                    oLlojLlogarish = new clsLlojLlogarish(oLlojLlogarish.IdLlojLlogarie, db);

                    var kurs = new clsKurset(dokKryesor.IdMonedha, data, dbadmn);
                    double kmk = 0;
                    double VDK = 0;
                    var dK = 0;

                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "ML":  //llogari monedhe
                            oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db);

                            kmk = 0;
                            VDK = 0;
                            if (kurs != null)
                            {
                                if (veprimTrupi.IdMonedha == dokKryesor.IdMonedha)
                                    kmk = veprimTrupi.Kursi;
                                else kmk = kurs.VleraKursi;
                                VDK = LlogaritDiferenceKursi(veprimTrupi.Kursi, dokKryesor.Kursi, kmk, veprimTrupi.Vlefta);//llogaritDiferenceKursi(KV,KK,KMK,VV); 
                            }
                            var klienti = new clsKlientFurnitor(idklientfurnitor, db);
                            oLlogari = new clsLlogari();
                            //DbCore.DbAdmin.clsMonedha monKF = new DbCore.DbAdmin.clsMonedha(nd.NdermarrjeMonedha, dbadmn);

                            oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, monedheNdermarrje.IdMonedha, klienti.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie, db);
                            debiKredi = dkmon;// tempSkemaKontTrupi.DebikrediSkemeKontTrupi;

                            var krijoFK = false;
                            if ((oNenLlojLlogarie.KodNenLlojLlogarie == "LLMF" && dkmon == 2) || (oNenLlojLlogarie.KodNenLlojLlogarie == "LLMH" && dkmon == 1))
                                krijoFK = true;

                            if (krijoFK == true)
                            {
                                tLlogKF = new clsTrupiFleteKontabel(debiKredi, VDK, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbadmn);
                                //tLlogKF = new clsTrupiFleteKontabel(debiKredi, VDK, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbadmn);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                var ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston) colTrupFK.Add(tLlogKF);
                                }
                            }
                            break;
                        case "KL":
                            oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db);

                            kmk = 0;
                            VDK = 0;
                            dK = 0;

                            if (kurs != null)
                            {
                                if (dokKryesor.IdMonedha == veprimTrupi.IdMonedha)
                                    kmk = veprimTrupi.Kursi;
                                else
                                    kmk = kurs.VleraKursi;
                                VDK = LlogaritDiferenceKursi(veprimTrupi.Kursi, dokKryesor.Kursi, kmk, veprimTrupi.Vlefta);//llogaritDiferenceKursi(KV,KK,KMK,VV); 
                                oKlient = new clsKlientFurnitor(idklientfurnitor, db);
                                var oLlogariKF = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, db);
                                //monklient = oLlogariKF.IdMonedha;
                                if (kahuDokKryesor == "Debi")
                                {
                                    if (VDK > 0) //klienti preket ne debi (gjithmone ne kahun e dokumentit kryesor)
                                        dK = 1;
                                    else if (VDK < 0)
                                        dK = 2;
                                }
                                else if (kahuDokKryesor == "Kredi")
                                {
                                    if (VDK > 0) //klienti preket ne kredi (gjithmone ne kahun e dokumentit kryesor)
                                        dK = 2;
                                    else if (VDK < 0)
                                        dK = 1;
                                }
                                if (dK == 1) dkmon = 2;
                                else dkmon = 1;
                                //DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha(nd.NdermarrjeMonedha, dbadmn);
                                tLlogKF = new clsTrupiFleteKontabel(dK, VDK, oLlogariKF, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbadmn); //todo Nestila ketu ishte oLlogari po vura oLlogariKF se me duket gabim sic ishte
                                //tLlogKF = new clsTrupiFleteKontabel(dK, VDK, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbadmn);
                                var objektkl = new DbQendraKosto.clsObjektivaKosto();
                                if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto, dbqendra);
                                    if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                        if (oLlogariKF.IdObjektivaKosto != 0 && oLlogariKF.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogariKF.IdObjektivaKosto, dbqendra);

                                        }
                                }
                                else if (oLlogariKF.IdObjektivaKosto != 0 && oLlogariKF.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogariKF.IdObjektivaKosto, dbqendra);

                                }
                                if (objektkl.Id != 0 && objektkl.Id != -1)
                                {
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogariKF, tLlogKF, objektkl);
                                }
                                var ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    var eksistontrupiperkf = false;
                                    if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                    {
                                        foreach (var f in trupiPerGjendjeKF)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {

                                                eksistontrupiperkf = true;
                                            }
                                        }
                                        if (!eksistontrupiperkf)
                                        {
                                            trupiPerGjendjeKF.Add(tLlogKF);
                                            if (oKlient.LlojiKF == true)
                                                rreshtakf.Add("KL");
                                            else rreshtakf.Add("FR");
                                            emrakf.Add(oKlient.IdKlientFurnitor);

                                        }
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);
                                }

                            }
                            break;
                    }

                }
            }

            return colTrupFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelLidhjeDokshumeme1(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colDokumentat dokkryesor, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, int idklientfurnitor, clsDokumenti doklidhes, double totali, out DbQendraKosto.colObjektivaKosto objektivat, string pershkrimi, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var eshteAzhornim = true;

            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim";
            rreshtakf = new List<string>(); emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var monklient = 0; var dkmon = 0;

            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            var debiKredi = 0;
            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            foreach (var dok in dokkryesor)
            {
                var kahuDokKryesor = KtheKahunDokumentitSipasNrDok(dok.IdNiveli, dok.NrDokumenti, dok.DtDokumenti, dok.IdKlientFurnitori, idNdermarrje);
                var kurs = new clsKurset(dok.IdMonedha, doklidhes.DtDokumenti);
                double kmk = 1;
                double VDK = 0;
                if (kurs != null)
                {
                    if (doklidhes.IdMonedha == dok.IdMonedha)
                        kmk = doklidhes.Kursi;
                    else kmk = kurs.VleraKursi;
                }
                VDK = LlogaritDiferenceKursi(doklidhes.Kursi, dok.Kursi, kmk, (doklidhes.Vlefta * dok.Vlefta / totali));//llogaritDiferenceKursi(kurs i dok lidhes,kurs i dok kryesor,KMK,vlere e dok lidhes);                        



                for (var i = 0; i < tempCol.Count; i++)
                {

                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];

                    var oLlojLlogarish = new clsLlojLlogarish();
                    var oNenLlojLlogarie = new clsNenLlojLlogarish();
                    debiKredi = 0;
                    oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                    oLlojLlogarish = oLlojLlogarish.merrLlojLlogarieSipasID();
                    var nd = new clsNdermarrje(idNdermarrje);

                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "ML":  //llogari monedhe
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();

                            var klienti = new clsKlientFurnitor(idklientfurnitor);
                            oLlogari = new clsLlogari();
                            oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, nd.NdermarrjeMonedha, klienti.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie);
                            debiKredi = dkmon;// tempSkemaKontTrupi.DebikrediSkemeKontTrupi;

                            var krijoFK = false;
                            if ((oNenLlojLlogarie.KodNenLlojLlogarie == "LLMF" && dkmon == 2) || (oNenLlojLlogarie.KodNenLlojLlogarie == "LLMH" && dkmon == 1))
                                krijoFK = true;

                            if (krijoFK == true)
                            {
                                tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), debiKredi, VDK, oLlogari, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                var ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston) colTrupFK.Add(tLlogKF);
                                }
                            }
                            break;
                        case "KL":
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();

                            var dK = 0;


                            oKlient = new clsKlientFurnitor(idklientfurnitor);
                            var oLlogariKF = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie);
                            monklient = oLlogariKF.IdMonedha;
                            if (kahuDokKryesor == "Debi")
                            {
                                if (VDK > 0) //klienti preket ne debi (gjithmone ne kahun e dokumentit kryesor)
                                    dK = 1;
                                else if (VDK < 0)
                                    dK = 2;
                            }
                            else if (kahuDokKryesor == "Kredi")
                            {
                                if (VDK > 0) //klienti preket ne kredi (gjithmone ne kahun e dokumentit kryesor)
                                    dK = 2;
                                else if (VDK < 0)
                                    dK = 1;
                            }
                            if (dK == 1) dkmon = 2;
                            else dkmon = 1;
                            tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), dK, VDK, oLlogariKF, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);
                            var objektkl = new DbQendraKosto.clsObjektivaKosto();
                            if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                            {
                                objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto);
                                if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                    if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    {
                                        objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);

                                    }
                            }
                            else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                            {
                                objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);

                            }
                            if (objektkl.Id != 0 && objektkl.Id != -1)
                            {
                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                            }
                            var ekzistonkl = false;
                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                            {
                                foreach (var f in colTrupFK)
                                {
                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekzistonkl = true;
                                    }
                                }
                                var eksistontrupiperkf = false;
                                if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                {
                                    foreach (var f in trupiPerGjendjeKF)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {

                                            eksistontrupiperkf = true;
                                        }
                                    }
                                    if (!eksistontrupiperkf)
                                    {
                                        trupiPerGjendjeKF.Add(tLlogKF);
                                        if (oKlient.LlojiKF == true)
                                            rreshtakf.Add("KL");
                                        else rreshtakf.Add("FR");
                                        emrakf.Add(oKlient.IdKlientFurnitor);

                                    }
                                }
                                if (!ekzistonkl)
                                    colTrupFK.Add(tLlogKF);
                            }


                            break;
                    }

                }

            }
            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimFleteDoganore(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colFleteDoganoreTaksa coltaksa, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, colFleteDoganoreTVSH coltvsh, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            #region KOKA E FLETES KONTABEL
            var dbshare = new clsDatabaseShare(new clsDatabaseKontabilitet());
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(idKonfgjenerues);
            int idKonfigFK = clsKusht.ktheVlereKushti(idKonfgjenerues, "ZFK", dbshare);
            var konfFK = new clsKonfigurimAmbjenti(idKonfigFK);
            int idSkemeKontabel = clsKusht.ktheVlereKushti(konfFK.IdKonfigAmbjente, "SK", dbshare);

            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK");
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);

            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD") == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD") == "Po";
            var colTrupFK = new colTrupatFletetKontabel();
            colTrupFK = krijoTrupFletKontabelFleteDoganore(idNder, konfFK.IdKonfigAmbjente, idSkemeKontabel, coltaksa, dtDk, coltvsh, pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);
            var kokaqender = DbQendraKosto.clsKokaQendraKosto.KrijoQK(new DbData(), konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, trupivjeterqendra, 0, rishpernda, shperndaDifQKPModDok);
            if (colTrupFK.Count == 0)
                return new clsKokaFleteKontabel();
            #endregion
            var kokaFK = new clsKokaFleteKontabel();
            var mesazh = kokaFK.KrijoFlete(new DbData(), idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelFleteDoganore(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colFleteDoganoreTaksa coltaksa, DateTime data, colFleteDoganoreTVSH coltvsh, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var eshteAzhornim = false;

            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim";

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;

            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            for (var i = 0; i < tempCol.Count; i++)
            {

                var tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = tempCol[i];

                var oLlojLlogarish = new clsLlojLlogarish();
                var oNenLlojLlogarie = new clsNenLlojLlogarish();

                oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                oLlojLlogarish = oLlojLlogarish.merrLlojLlogarieSipasID();

                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "TD")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "TKS":
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();
                            switch (oNenLlojLlogarie.KodNenLlojLlogarie)
                            {
                                case "TKSK":
                                    foreach (var taksa in coltaksa)
                                    {
                                        var ekziston = false;
                                        var oT = new clsTaksa(taksa.KodTaksa, idNdermarrje);
                                        oLlogari = new clsLlogari(oT.LlogariKredi, idNdermarrje);
                                        tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), tempSkemaKontTrupi.DebikrediSkemeKontTrupi, double.Parse(taksa.Vlefta.ToString()), oLlogari, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                        }
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            foreach (var f in colTrupFK)
                                            {
                                                if (f.IdLlogari == tLlogKF.IdLlogari)
                                                {
                                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                    }
                                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                    }
                                                    ekziston = true;
                                                }
                                            }
                                        else ekziston = true;
                                        if (!ekziston) colTrupFK.Add(tLlogKF);

                                    }
                                    break;
                                case "TKSD":
                                    foreach (var taksa in coltaksa)
                                    {
                                        var ekziston = false;
                                        var oT = new clsTaksa(taksa.KodTaksa, idNdermarrje);
                                        oLlogari = new clsLlogari(oT.LlogariDebi, idNdermarrje);
                                        tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), tempSkemaKontTrupi.DebikrediSkemeKontTrupi, double.Parse(taksa.Vlefta.ToString()), oLlogari, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                        }
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            foreach (var f in colTrupFK)
                                            {
                                                if (f.IdLlogari == tLlogKF.IdLlogari)
                                                {
                                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                    }
                                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                    }
                                                    ekziston = true;
                                                }
                                            }
                                        else ekziston = true;
                                        if (!ekziston) colTrupFK.Add(tLlogKF);

                                    }
                                    break;
                            }
                            break;


                    }
                }
                else
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "TKS":
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();
                            switch (oNenLlojLlogarie.KodNenLlojLlogarie)
                            {
                                case "TKSDOG":
                                    foreach (var tvsh in coltvsh)
                                    {

                                        var oT = new clsTaksa(tvsh.KodTVSH, idNdermarrje);
                                        oLlogari = new clsLlogari(oT.LlogariDogane, idNdermarrje);
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                        }
                                        var ekziston = false;
                                        tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), tempSkemaKontTrupi.DebikrediSkemeKontTrupi, double.Parse(tvsh.VleftaTvsh.ToString()), oLlogari, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);

                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            foreach (var f in colTrupFK)
                                            {
                                                if (f.IdLlogari == tLlogKF.IdLlogari)
                                                {
                                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                    }
                                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                    }
                                                    ekziston = true;
                                                }
                                            }
                                            if (!ekziston) colTrupFK.Add(tLlogKF);
                                        }

                                    }
                                    break;
                                case "TKSD":
                                    foreach (var tvsh in coltvsh)
                                    {
                                        var ekziston = false;
                                        var oT = new clsTaksa(tvsh.KodTVSH, idNdermarrje);
                                        oLlogari = new clsLlogari(oT.LlogariDebi, idNdermarrje);
                                        tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), tempSkemaKontTrupi.DebikrediSkemeKontTrupi, double.Parse(tvsh.VleftaTvsh.ToString()), oLlogari, azhornim, idNdermarrje, null, null, data, pershkrimi, eshteAzhornim);
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                        }
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            foreach (var f in colTrupFK)
                                            {
                                                if (f.IdLlogari == tLlogKF.IdLlogari)
                                                {
                                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                    }
                                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                    }
                                                    ekziston = true;
                                                }
                                            }
                                            if (!ekziston) colTrupFK.Add(tLlogKF);
                                        }

                                    }
                                    break;
                            }
                            break;


                        case "":
                            break;
                    }

                }
            }
            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimVeprimKF(DbData dbData, int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colVeprimeKFTrupi coltrupi, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci, int idDegeAdministrative)
        {
            #region KOKA E FLETES KONTABEL
            var dbShare = new clsDatabaseShare(dbData);
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var konf = new clsKonfigurimAmbjenti(idKonfgjenerues, dbShare);
            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbShare);
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbShare);
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbShare);

            clsKonfigurimAmbjenti konfqk;
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbShare);
            else
                konfqk = new clsKonfigurimAmbjenti("RQK", idNder, dbShare);

            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbShare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbShare) == "Po";
            var colTrupFK = new colTrupatFletetKontabel();
            colTrupFK = krijoTrupFletKontabelVeprimKF(dbData, idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, coltrupi, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);

            var kokaqender = clsKokaQendraKosto.KrijoQK(dbData, konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, trupivjeterqendra, 0, rishpernda, shperndaDifQKPModDok);
            
            #endregion
            var kokaFK = new clsKokaFleteKontabel();
            var mesazh = kokaFK.KrijoFlete(dbData, idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelVeprimKF(DbData dbData, int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colVeprimeKFTrupi trupi, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
            clsDatabaseQendraKosto dbQendraKosto = new clsDatabaseQendraKosto(dbData);
            clsDatabaseKontabilitet dbKontabilitet = new clsDatabaseKontabilitet(dbData);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);

            clsMonedha monedhaNderm = new clsMonedha();
            monedhaNderm.mbushMonedhenENdermarrjes(idNdermarrje, dbAdmin);

            var eshteAzhornim = false;
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK", dbShare) == "Azhornim";
            rreshtakf = new List<string>(); emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var debiKredi = 0;

            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            
            clsSkemaKontabelNew clsSkemaKontNew = clsSkemaKontabelNew.getSkemaKontabelNew(idSkemeKontabel, dbKontabilitet);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            for (var i = 0; i < tempCol.Count; i++)
            {//ketu jane rreshtat qe gjenerohen te skema

                var tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = tempCol[i];

                clsLlojLlogarish oLlojLlogarish = new clsLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), dbKontabilitet);
                clsNenLlojLlogarish oNenLlojLlogarie = new clsNenLlojLlogarish();

                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "LL"://nese ne kete rresht te grides eshte zgjedhur llogari
                            oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbKontabilitet);
                            foreach (var veprimTrupi in trupi)
                            {
                                oLlogari = new clsLlogari(veprimTrupi.IdLlogKunderParti, dbKontabilitet);
                                debiKredi = clsSkemaKontabelTrupiNew.ktheDebiKrediKunderParti(tempSkemaKontTrupi, veprimTrupi.DebiKredi);                                

                                var ekziston = false;
                                tLlogKF = new clsTrupiFleteKontabel(dbAdmin, debiKredi, veprimTrupi.VleftaMonBaze, oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, veprimTrupi.IdMonedha, data, pershkrimi, eshteAzhornim);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbQendraKosto);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                
                                //per krijimin e trupit te fletes kontabel
                                //kontrollojme per llogarite a ekzistojne apo jo tek lista colTrupFK. 
                                //Nqs ekziston i shtojme vlerat e reja per colTrupFK per llogarine perkatese, perndryshe i shtojme te lista.
                                foreach (var f in colTrupFK)
                                {
                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekziston = true;
                                    }
                                }
                                if (!ekziston)
                                    colTrupFK.Add(tLlogKF);
                            }

                            break;
                        case "KL":
                            oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbKontabilitet); 
                            //merret nenllogaria dhe shohim se cfare llogarie do marrim (psh te klienti: kryesore, dytesore, etj)
                            foreach (var veprimTrupi in trupi)
                            {
                                var kursi = veprimTrupi.Kursi;
                                if (tempSkemaKontTrupi.FormulaSkemeKontTrupi == "kunderparti")
                                {
                                    oKlient = new clsKlientFurnitor(veprimTrupi.IdKfKunderParti, dbKontabilitet);//marrim klientin e trupit te dokumentit
                                    oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, dbKontabilitet); //marrim llogarine qe do preket sipas skemes
                                                                                                                                        //do shtohet kushti, ne qofte se trupi.formula eshte kunderparti, atehere do merret e kundertae debikredi, perndryshe lere keshtu sic eshte.
                                    debiKredi = clsSkemaKontabelTrupiNew.ktheDebiKrediKunderParti(tempSkemaKontTrupi, veprimTrupi.DebiKredi);
                                    
                                    if (oLlogari.IdMonedha == monedhaNderm.IdMonedha) //kunderpartia ka te njejten monedhe me ndermarrjen
                                        kursi = 1;
                                    else if (oLlogari.IdMonedha != veprimTrupi.IdMonedha) //kunderpartia nuk ka monedhe te njejte me ndermarrjen dhe ka monedhe te ndryshme nga klienti i zgjedhur ne trup
                                    {
                                        var kursikunder = new clsKurset(oLlogari.IdMonedha, data);
                                        kursi = kursikunder.VleraKursi;
                                    }
                                }
                                else
                                {
                                    oKlient = new clsKlientFurnitor(veprimTrupi.IdKF, dbKontabilitet);//marrim klientin e trupit te dokumentit
                                    oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, dbKontabilitet); //marrim llogarine qe do preket sipas skemes
                                                                                                                                        //do shtohet kushti, ne qofte se trupi.formula eshte kunderparti, atehere do merret e kundertae debikredi, perndryshe lere keshtu sic eshte.
                                    debiKredi = clsSkemaKontabelTrupiNew.ktheDebiKredi(tempSkemaKontTrupi, veprimTrupi.DebiKredi);
                                }
                                //ndryshuar vlera me te cilen preket furnitori ku shte i lidhur me nje kusht pagese. Ishte Convert.ToDecimal(veprimTrupi.VleraPaguarMonedhaBaze)
                                tLlogKF = new clsTrupiFleteKontabel(dbAdmin, debiKredi, veprimTrupi.VleftaMonBaze, oLlogari, azhornim, idNdermarrje, kursi, oLlogari.IdMonedha, data, pershkrimi, eshteAzhornim);
                                var objektkl = new clsObjektivaKosto();
                                if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                {
                                    objektkl = new clsObjektivaKosto(oKlient.IdObjektivaKosto, dbQendraKosto);
                                    if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbQendraKosto);

                                        }
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbQendraKosto);

                                }
                                if (objektkl.Id != 0 && objektkl.Id != -1)
                                {
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                }
                                var ekziston = false;

                               
                                //gjejme se cilet kliente preken te ky dokument, dhe se cfare vlere preket per secilin prej tyre.
                                var eksistonkf = false;
                                if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                {
                                    //per krijimin e gjendjek
                                    //kontrollojme per klientet e dokumentit a ekziston apo jo tek lista emrakf. 
                                    //Nqs ekziston i shtojme vlerat e reja per trupigjendjekf, perndryshe i shtojme te lista.
                                    for (var k = 0; k < emrakf.Count; k++)
                                    {
                                        if (oKlient.IdKlientFurnitor == Convert.ToInt32(emrakf[k]))
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                trupiPerGjendjeKF[k].VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                trupiPerGjendjeKF[k].VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                trupiPerGjendjeKF[k].VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                trupiPerGjendjeKF[k].VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            eksistonkf = true;
                                            break;
                                        }
                                    }
                                    if (!eksistonkf)
                                    {
                                        var t = new clsTrupiFleteKontabel();
                                        t.VleftaDebiMonBazeTrupiFleteKontabel = tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                        t.VleftaDebiTrupiFleteKontabel = tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        t.VleftaKrediMonBazeTrupiFleteKontabel = tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                        t.VleftaKrediTrupiFleteKontabel = tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        t.IdMonedha = tLlogKF.IdMonedha;
                                        t.Kursi = tLlogKF.Kursi;

                                        trupiPerGjendjeKF.Add(t);
                                        if (oKlient.LlojiKF == true)
                                            rreshtakf.Add("KL");
                                        else rreshtakf.Add("FR");
                                        emrakf.Add(oKlient.IdKlientFurnitor);
                                    }
                                }
                                foreach (var f in colTrupFK)
                                {
                                    //e njejta gje behet dhe per llogarine si me lart. 
                                    //kjo eshte per krijimin e fletes kontabel.
                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                    {
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        }
                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        }
                                        ekziston = true;
                                    }
                                }
                                if (!ekziston)
                                    colTrupFK.Add(tLlogKF);
                                
                            }
                            break;
                    }
                }
            }
            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimVDKVKF(DbData dbData, int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colVeprimeKFTrupi coltrupi, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, int idklientfurnitor, colDokumentLidhesTrupi coltrupidokumenti, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci, int idDegeAdm)
        {
            #region KOKA E FLETES KONTABEL
            var dbshare = new clsDatabaseShare(dbData);
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(idKonfgjenerues);
            var konfFK = new clsKonfigurimAmbjenti();
            konfFK.mbushKonfiguriminMeID(konf.IdSkemeKontabel);
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);

            clsKonfigurimAmbjenti konfqk;
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else
                konfqk = new clsKonfigurimAmbjenti("RQK", idNder, dbshare);

            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbshare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbshare) == "Po";
            var colTrupFK = new colTrupatFletetKontabel();
            colTrupFK = krijoTrupFletKontabelVDKVKF(dbData, idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, coltrupi, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, idklientfurnitor, coltrupidokumenti, pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);
            var kokaqender = clsKokaQendraKosto.KrijoQK(dbData, konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, trupivjeterqendra, 0, rishpernda, shperndaDifQKPModDok);
            if (colTrupFK.Count == 0)
                return new clsKokaFleteKontabel();
            #endregion
            var kokaFK = new clsKokaFleteKontabel();
            var mesazh = kokaFK.KrijoFlete(dbData, idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelVDKVKF(DbData dbData, int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colVeprimeKFTrupi trupi, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, int idklientfurnitor, colDokumentLidhesTrupi trupiDokumenti, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
            clsDatabaseQendraKosto dbQendraKosto = new clsDatabaseQendraKosto(dbData);
            clsDatabaseKontabilitet dbKontabilitet = new clsDatabaseKontabilitet(dbData);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);

            clsMonedha monedha = new clsMonedha();
            monedha.mbushMonedhenENdermarrjes(idNdermarrje, dbAdmin);

            objektivat = new colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var eshteAzhornim = true;
            double kv = 0;
            double kk = 0;//KK = Kursi i fatures
            double kmk = 0; // KMK = Kursi i monedhes se fatures ne daten e dokumentit te bankes
            double vv = 0;//VV= Vlera e arketuar (e rreshtit ne gride)

            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK", dbShare) == "Azhornim";
            rreshtakf = new List<string>(); emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var monklient = 0; var dkmon = 0;

            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            var debiKredi = 0;
            var clsSkemaKontNew = clsSkemaKontabelNew.getSkemaKontabelNew(idSkemeKontabel, dbKontabilitet);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            var m = 0;
            foreach (var veprimTrupi in trupi)
            {
                var fatura = new clsKokaShitje();
                var kursimon = new clsKurset();
                for (var i = 0; i < tempCol.Count; i++)
                {

                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];

                    clsLlojLlogarish oLlojLlogarish = new clsLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), dbKontabilitet);
                    clsNenLlojLlogarish oNenLlojLlogarie = new clsNenLlojLlogarish();

                    if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VDK")
                    {
                        double VDK = 0; var idmonedhafat = 0; var idkf = 0; var dK = 0;
                        double kursifat = 0; var datafat = new DateTime();
                        switch (oLlojLlogarish.KodLlojLlogarie)
                        {
                            case "ML":  //llogari monedhe
                                oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbKontabilitet);
                                if (veprimTrupi.IdKF == idklientfurnitor)
                                {
                                    VDK = 0; idmonedhafat = 0; idkf = 0;
                                    kursifat = 0;

                                    if (veprimTrupi.IdFatura != 0)
                                    {
                                        //DbCore.DbRegjistrim.clsKokaShitje fatura = new DbCore.DbRegjistrim.clsKokaShitje();
                                        if (fatura.IdShitjeKoka == 0)
                                        {
                                            fatura.mbushKokaShitjeSipasIDPaTrup(veprimTrupi.IdFatura);
                                            kursimon = clsKurset.getKursSipasIdMonedhaFromCache(fatura.IdMonedha, data, dbAdmin);
                                        }

                                        idkf = fatura.IdKlientFurnitor;
                                        idmonedhafat = fatura.IdMonedha;
                                        kursifat = fatura.Kursi;
                                        datafat = fatura.DtDok;
                                        //DbCore.DbAdmin.clsKurset kursimon = new DbCore.DbAdmin.clsKurset(fatura.IdMonedha, data);
                                        if (fatura.IdMonedha == veprimTrupi.IdMonedha)        //nqs monedha eshte e njejte merret kursi i bankes prn kursi ne daten e bankes per kete monedhe
                                            kmk = veprimTrupi.Kursi;
                                        else if (kursimon != null)
                                        {
                                            kmk = kursimon.VleraKursi;
                                        }
                                        else kmk = 0;



                                        var azh = new clsAzhornimKFKoka();
                                        azh.MerrAzhornimSipasKlientitMeIFundit(veprimTrupi.IdKF);
                                        if (azh.DateDok >= datafat)
                                            kk = azh.Kursi;
                                        else
                                            kk = kursifat;

                                        vv = veprimTrupi.Vlefta;
                                        kv = veprimTrupi.Kursi;
                                        VDK = LlogaritDiferenceKursi(kv, kk, kmk, vv);


                                        var klienti = new clsKlientFurnitor(idkf, dbKontabilitet);
                                        oLlogari = new clsLlogari();
                                        oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, monedha.IdMonedha, klienti.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie);


                                        debiKredi = dkmon;//tempSkemaKontTrupi.DebikrediSkemeKontTrupi;

                                        var krijoFK = false;
                                        if ((oNenLlojLlogarie.KodNenLlojLlogarie == "LLMF" && dkmon == 2) || (oNenLlojLlogarie.KodNenLlojLlogarie == "LLMH" && dkmon == 1))
                                            krijoFK = true;


                                        if (krijoFK == true)
                                        {

                                            tLlogKF = new clsTrupiFleteKontabel(dbAdmin, debiKredi, VDK, oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, veprimTrupi.IdMonedha, data, pershkrimi, eshteAzhornim);
                                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                            {
                                                var objekt = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbQendraKosto);
                                                ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                            }
                                            var ekziston = false;
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                foreach (var f in colTrupFK)
                                                {
                                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                                    {
                                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                        {
                                                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                        }
                                                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                        {
                                                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                        }
                                                        ekziston = true;
                                                    }
                                                }
                                                if (!ekziston) colTrupFK.Add(tLlogKF);
                                            }
                                        }
                                    }
                                }

                                break;
                            case "KL":
                                oNenLlojLlogarie = new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbKontabilitet);
                                VDK = 0;
                                dK = 0;
                                idmonedhafat = 0; idkf = 0;
                                kursifat = 0;
                                {
                                    if (veprimTrupi.IdKF == idklientfurnitor && veprimTrupi.IdFatura != 0)
                                    {


                                        //DbCore.DbRegjistrim.clsKokaShitje fatura = new DbCore.DbRegjistrim.clsKokaShitje();
                                        if (fatura.IdShitjeKoka == 0)
                                        {
                                            fatura.mbushKokaShitjeSipasIDPaTrup(veprimTrupi.IdFatura);
                                            kursimon = clsKurset.getKursSipasIdMonedhaFromCache(fatura.IdMonedha, data, dbAdmin);
                                        }

                                        idkf = fatura.IdKlientFurnitor;
                                        idmonedhafat = fatura.IdMonedha;
                                        kursifat = fatura.Kursi;
                                        datafat = fatura.DtDok;
                                        //DbCore.DbAdmin.clsKurset kursimon = new DbCore.DbAdmin.clsKurset(fatura.IdMonedha, data);
                                        if (fatura.IdMonedha == veprimTrupi.IdMonedha)        //nqs monedha eshte e njejte merret kursi i bankes prn kursi ne daten e bankes per kete monedhe
                                            kmk = veprimTrupi.Kursi;
                                        else if (kursimon != null)
                                        {
                                            kmk = kursimon.VleraKursi;
                                        }
                                        else kmk = 0;



                                        var azh = new clsAzhornimKFKoka();
                                        azh.MerrAzhornimSipasKlientitMeIFundit(veprimTrupi.IdKF);
                                        if (azh.DateDok >= datafat)
                                            kk = azh.Kursi;
                                        else
                                            kk = kursifat;
                                        vv = veprimTrupi.Vlefta;
                                        kv = veprimTrupi.Kursi;
                                        VDK = LlogaritDiferenceKursi(kv, kk, kmk, vv);

                                        oKlient = new clsKlientFurnitor(idkf, dbKontabilitet);
                                        oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie, dbKontabilitet);
                                        monklient = oLlogari.IdMonedha;
                                        if (oKlient.LlojiKF == false)
                                        {
                                            if (VDK > 0)
                                                dK = 2;
                                            else if (VDK < 0)
                                                dK = 1;
                                        }
                                        if (oKlient.LlojiKF == true)
                                        {
                                            if (VDK > 0)
                                                dK = 1;
                                            else if (VDK < 0)
                                                dK = 2;
                                        }
                                        if (dK == 1) dkmon = 2;
                                        else dkmon = 1;
                                        tLlogKF = new clsTrupiFleteKontabel(dbAdmin, dK, VDK, oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, veprimTrupi.IdMonedha, data, pershkrimi, eshteAzhornim);
                                        var objektkl = new clsObjektivaKosto();
                                        if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new clsObjektivaKosto(oKlient.IdObjektivaKosto, dbQendraKosto);
                                            if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                {
                                                    objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbQendraKosto);

                                                }
                                        }
                                        else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbQendraKosto);

                                        }
                                        if (objektkl.Id != 0 && objektkl.Id != -1)
                                        {
                                            ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                        }
                                        var ekziston = false;
                                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                        {
                                            foreach (var f in colTrupFK)
                                            {
                                                if (f.IdLlogari == tLlogKF.IdLlogari)
                                                {
                                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                        f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                    }
                                                    else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                    {
                                                        f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                        f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                    }
                                                    ekziston = true;
                                                }
                                            }
                                            var eksistontrupiperkf = false;
                                            if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                            {
                                                foreach (var f in trupiPerGjendjeKF)
                                                {
                                                    if (f.IdLlogari == tLlogKF.IdLlogari)
                                                    {

                                                        eksistontrupiperkf = true;
                                                    }
                                                }
                                                if (!eksistontrupiperkf)
                                                {
                                                    trupiPerGjendjeKF.Add(tLlogKF);
                                                    if (oKlient.LlojiKF)
                                                        rreshtakf.Add("KL");
                                                    else rreshtakf.Add("FR");
                                                    emrakf.Add(oKlient.IdKlientFurnitor);

                                                }
                                            }
                                            if (!ekziston)
                                                colTrupFK.Add(tLlogKF);
                                        }

                                    }
                                }

                                break;
                        }
                    }
                }
                if (veprimTrupi.IdFatura != 0 && veprimTrupi.IdKF == idklientfurnitor) m += 1;
            }
            //per te grupuar vlerat e llogarive
            foreach (var f in colTrupFK)
            {
                if (f.VleftaDebiMonBazeTrupiFleteKontabel != 0 && f.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                {
                    var diferenca = f.VleftaDebiMonBazeTrupiFleteKontabel - f.VleftaKrediMonBazeTrupiFleteKontabel;
                    if (diferenca > 0)
                    {
                        f.VleftaDebiMonBazeTrupiFleteKontabel = diferenca;
                        f.VleftaDebiTrupiFleteKontabel = f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel;
                        f.VleftaKrediTrupiFleteKontabel = 0;
                        f.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                    }
                    else if (diferenca < 0)
                    {
                        f.VleftaKrediMonBazeTrupiFleteKontabel = Math.Abs(diferenca);
                        f.VleftaKrediTrupiFleteKontabel = Math.Abs(f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel);
                        f.VleftaDebiTrupiFleteKontabel = 0;
                        f.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    }
                }
            }

            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimAzhornimKF(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colAzhornimKFTrupi coltrupi, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            #region KOKA E FLETES KONTABEL
            var dbshare = new clsDatabaseShare(new clsDatabaseKontabilitet());
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(idKonfgjenerues);
            var konfFK = new clsKonfigurimAmbjenti();
            konfFK.mbushKonfiguriminMeID(konf.IdSkemeKontabel);
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK");
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD") == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD") == "Po";
            var colTrupFK = new colTrupatFletetKontabel();
            colTrupFK = krijoTrupFletKontabelAzhornimKF(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, coltrupi, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);
            var kokaqender = DbQendraKosto.clsKokaQendraKosto.KrijoQK(new DbData(), konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, trupivjeterqendra, 0, rishpernda, shperndaDifQKPModDok);
            #endregion
            var kokaFK = new clsKokaFleteKontabel();
            var mesazh = kokaFK.KrijoFlete(new DbData(), idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelAzhornimKF(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colAzhornimKFTrupi trupi, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            var eshteAzhornim = true;
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim";
            rreshtakf = new List<string>();
            emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            var debiKredi = 0;
            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            var dkmon = 0;
            foreach (var veprimTrupi in trupi)
            {
                for (var i = 0; i < tempCol.Count; i++)
                {

                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];

                    var oLlojLlogarish = new clsLlojLlogarish();
                    var oNenLlojLlogarie = new clsNenLlojLlogarish();

                    oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                    oLlojLlogarish = oLlojLlogarish.merrLlojLlogarieSipasID();

                    if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                    {
                        oLlogari = new clsLlogari();
                        var llogkf = new clsLlogari();
                        var ekziston = false;
                        switch (oLlojLlogarish.KodLlojLlogarie)
                        {
                            case "ML"://nese ne kete rresht te grides eshte zgjedhur llogari
                                oNenLlojLlogarie = new clsNenLlojLlogarish();
                                oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                                oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();
                                var klienti = new clsKlientFurnitor(veprimTrupi.IdKlientFurnitor);
                                llogkf = new clsLlogari(klienti.IdLlogari);
                                if (veprimTrupi.IdLlogariKp == 0)

                                    oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje), klienti.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie);
                                else
                                {
                                    oLlogari.IdLlogari = veprimTrupi.IdLlogariKp;
                                    oLlogari = oLlogari.merrLlogariSipasId();
                                }
                                debiKredi = dkmon;//tempSkemaKontTrupi.DebikrediSkemeKontTrupi;

                                var krijoFK = false;
                                if ((oNenLlojLlogarie.KodNenLlojLlogarie == "LLMF" && dkmon == 2) || (oNenLlojLlogarie.KodNenLlojLlogarie == "LLMH" && dkmon == 1))
                                    krijoFK = true;
                                if (krijoFK == true)
                                {

                                    tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), debiKredi, veprimTrupi.Vlefta, oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, llogkf.IdMonedha, data, pershkrimi, eshteAzhornim);

                                    if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    {
                                        var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                    }
                                    ekziston = false;
                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekziston = true;
                                            }
                                        }
                                        if (!ekziston) colTrupFK.Add(tLlogKF);
                                    }
                                }


                                break;
                            case "KL":
                                oNenLlojLlogarie = new clsNenLlojLlogarish();
                                oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                                oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();
                                var dK = 0;
                                oKlient = new clsKlientFurnitor(veprimTrupi.IdKlientFurnitor);
                                oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie);
                                llogkf = new clsLlogari(oKlient.IdLlogari);
                                if (veprimTrupi.DebiKredi == false)
                                {
                                    if (veprimTrupi.Vlefta > 0)
                                        dK = 2;
                                    else if (veprimTrupi.Vlefta < 0)
                                        dK = 1;
                                }
                                else
                                {
                                    if (veprimTrupi.Vlefta > 0)
                                        dK = 1;
                                    else if (veprimTrupi.Vlefta < 0)
                                        dK = 2;
                                }
                                if (dK == 1) dkmon = 2;
                                else dkmon = 1;
                                tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), dK, veprimTrupi.Vlefta, oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, llogkf.IdMonedha, data, pershkrimi, eshteAzhornim);
                                var objektkl = new DbQendraKosto.clsObjektivaKosto();
                                if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto);
                                    if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);

                                        }
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);

                                }
                                if (objektkl.Id != 0 && objektkl.Id != -1)
                                {
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                }
                                ekziston = false; var eksistonkf = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                    {
                                        for (var k = 0; k < emrakf.Count; k++)
                                        {
                                            if (oKlient.IdKlientFurnitor == Convert.ToInt32(emrakf[k]))
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    trupiPerGjendjeKF[k].VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    trupiPerGjendjeKF[k].VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    trupiPerGjendjeKF[k].VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    trupiPerGjendjeKF[k].VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                eksistonkf = true;
                                                break;
                                            }


                                        }
                                        if (!eksistonkf)
                                        {
                                            var t = new clsTrupiFleteKontabel();
                                            t.VleftaDebiMonBazeTrupiFleteKontabel = tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            t.VleftaDebiTrupiFleteKontabel = tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            t.VleftaKrediMonBazeTrupiFleteKontabel = tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            t.VleftaKrediTrupiFleteKontabel = tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            t.IdMonedha = tLlogKF.IdMonedha;
                                            t.Kursi = tLlogKF.Kursi;

                                            trupiPerGjendjeKF.Add(t);
                                            if (oKlient.LlojiKF == true)
                                                rreshtakf.Add("KL");
                                            else rreshtakf.Add("FR");
                                            emrakf.Add(oKlient.IdKlientFurnitor);
                                        }
                                    }
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }

                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);
                                }

                                break;

                        }
                    }

                }
            } //per te grupuar vlerat e llogarive
            foreach (var f in colTrupFK)
            {
                if (f.VleftaDebiMonBazeTrupiFleteKontabel != 0 && f.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                {
                    var diferenca = f.VleftaDebiMonBazeTrupiFleteKontabel - f.VleftaKrediMonBazeTrupiFleteKontabel;
                    if (diferenca > 0)
                    {
                        f.VleftaDebiMonBazeTrupiFleteKontabel = diferenca;
                        f.VleftaDebiTrupiFleteKontabel = f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel;
                        f.VleftaKrediTrupiFleteKontabel = 0;
                        f.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                    }
                    else if (diferenca < 0)
                    {
                        f.VleftaKrediMonBazeTrupiFleteKontabel = Math.Abs(diferenca);
                        f.VleftaKrediTrupiFleteKontabel = Math.Abs(f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel);
                        f.VleftaDebiTrupiFleteKontabel = 0;
                        f.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    }
                }
            }
            return colTrupFK;
        }

        internal static clsKokaFleteKontabel gjeneroKontabilizimMbylljeKF(int idgjenerues, int idNivgjenerues, int idKonfgjenerues, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, colTrupiMbylljeKF coltrupi, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            #region KOKA E FLETES KONTABEL
            var dbshare = new clsDatabaseShare(new clsDatabaseKontabilitet());
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var idGrupKontabilizimi = 0;
            var kontabilizuar = true;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(idKonfgjenerues);
            var konfFK = new clsKonfigurimAmbjenti();
            konfFK.mbushKonfiguriminMeID(konf.IdSkemeKontabel);
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK");
            int idStatusDok = MerrStatusFleteKontabel(idKonfgjenerues, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD") == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD") == "Po";
            var colTrupFK = new colTrupatFletetKontabel();
            colTrupFK = krijoTrupFletKontabelMbylljeKF(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, coltrupi, out emrakf, out rreshtakf, out trupiPerGjendjeKF, dtDk, pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj);
            var kokaqender = DbQendraKosto.clsKokaQendraKosto.KrijoQK(new DbData(), konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, trupivjeterqendra, 0, rishpernda, shperndaDifQKPModDok);
            #endregion
            var kokaFK = new clsKokaFleteKontabel();
            var mesazh = kokaFK.KrijoFlete(new DbData(), idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNivgjenerues, idgjenerues, idKonfgjenerues, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelMbylljeKF(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colTrupiMbylljeKF trupi, out List<int> emrakf, out List<string> rreshtakf, out colTrupatFletetKontabel trupiPerGjendjeKF, DateTime data, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            var eshteAzhornim = false;
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim";
            rreshtakf = new List<string>();
            emrakf = new List<int>();
            trupiPerGjendjeKF = new colTrupatFletetKontabel();
            //DbCore.DbAdmin.clsNdermarrje nd = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            var idMon = clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje);
            //DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha(clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje));
            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            var oArtikull = new clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;
            var debiKredi = 0;
            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;

            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }
                }
            }
            var monklient = 0; var dkmon = 0; var dkllog = 0;
            foreach (var veprimTrupi in trupi)
            {
                for (var i = 0; i < tempCol.Count; i++)
                {
                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];

                    var oLlojLlogarish = new clsLlojLlogarish();
                    var oNenLlojLlogarie = new clsNenLlojLlogarish();

                    oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                    oLlojLlogarish = oLlojLlogarish.merrLlojLlogarieSipasID();

                    if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                    {
                        oLlogari = new clsLlogari();
                        var llogkf = new clsLlogari();
                        var ekziston = false;
                        switch (oLlojLlogarish.KodLlojLlogarie)
                        {
                            case "LL"://nese ne kete rresht te grides eshte zgjedhur llogari
                                oNenLlojLlogarie = new clsNenLlojLlogarish();
                                oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                                oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();
                                var klienti = new clsKlientFurnitor(veprimTrupi.IdKf);
                                llogkf = new clsLlogari(klienti.IdLlogari);
                                if (veprimTrupi.IdLlogariKp == 0)

                                    oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, idMon, klienti.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie);
                                else
                                {
                                    oLlogari.IdLlogari = veprimTrupi.IdLlogariKp;
                                    oLlogari = oLlogari.merrLlogariSipasId();
                                }
                                debiKredi = dkmon;//tempSkemaKontTrupi.DebikrediSkemeKontTrupi;

                                tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), debiKredi, Math.Abs(veprimTrupi.GjendjaMonBaze), oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, llogkf.IdMonedha, data, pershkrimi, eshteAzhornim);

                                if (dkllog != dkmon)
                                {

                                    var debi = tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                    var kredi = tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                    tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel = debi;
                                    tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel = kredi;
                                    if (oLlogari.IdMonedha == idMon)
                                    {
                                        var debil = tLlogKF.VleftaDebiTrupiFleteKontabel;
                                        var kredil = tLlogKF.VleftaKrediTrupiFleteKontabel;
                                        tLlogKF.VleftaKrediTrupiFleteKontabel = debil;
                                        tLlogKF.VleftaDebiTrupiFleteKontabel = kredil;
                                    }
                                }
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston) colTrupFK.Add(tLlogKF);
                                }
                                break;
                            case "KL":
                                oNenLlojLlogarie = new clsNenLlojLlogarish();
                                oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                                oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();

                                var dK = 0;

                                oKlient = new clsKlientFurnitor(veprimTrupi.IdKf);
                                oLlogari = oKlient.merrLlogariKlientFurnitori(oNenLlojLlogarie.KodNenLlojLlogarie);
                                llogkf = new clsLlogari(oKlient.IdLlogari);
                                monklient = oLlogari.IdMonedha;

                                if (veprimTrupi.DebiKredi == 1)
                                {
                                    if (veprimTrupi.GjendjaLlog > 0)
                                        dK = 2;
                                    else if (veprimTrupi.GjendjaLlog < 0)
                                        dK = 1;
                                }
                                else
                                {
                                    if (veprimTrupi.GjendjaLlog > 0)
                                        dK = 1;
                                    else if (veprimTrupi.GjendjaLlog < 0)
                                        dK = 2;
                                }
                                if (dK == 1)
                                    dkmon = 2;
                                else dkmon = 1;
                                //if (veprimTrupi.GjendjaMonBaze == 0 && veprimTrupi.GjendjaLlog != 0)
                                //    tLlogKF = new clsTrupiFleteKontabel(dK, Math.Abs(veprimTrupi.GjendjaLlog), oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, llogkf.IdMonedha, data, eshteAzhornim);
                                //else
                                tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), dK, Math.Abs(veprimTrupi.GjendjaMonBaze), oLlogari, azhornim, idNdermarrje, veprimTrupi.Kursi, llogkf.IdMonedha, data, pershkrimi, eshteAzhornim);
                                var objektkl = new DbQendraKosto.clsObjektivaKosto();
                                if (oKlient.IdObjektivaKosto != 0 && oKlient.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oKlient.IdObjektivaKosto);
                                    if (!(objektkl.Nga <= data && (objektkl.Deri == new DateTime() || objektkl.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        {
                                            objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                        }
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    objektkl = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                }
                                if (objektkl.Id != 0 && objektkl.Id != -1)
                                {
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objektkl);
                                }
                                if ((veprimTrupi.GjendjaLlog > 0 && veprimTrupi.GjendjaMonBaze < 0) || (veprimTrupi.GjendjaLlog < 0 && veprimTrupi.GjendjaMonBaze > 0))
                                {
                                    dkllog = dK;
                                    var debi = tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                    var kredi = tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                    tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel = debi;
                                    tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel = kredi;
                                }
                                else dkllog = dkmon;
                                if (monklient != idMon)
                                {
                                    if (veprimTrupi.GjendjaLlog != 0 && veprimTrupi.GjendjaMonBaze == 0)
                                    {
                                        tLlogKF.Kursi = 1;
                                        if (dK == 1)
                                        {
                                            tLlogKF.VleftaKrediTrupiFleteKontabel = veprimTrupi.GjendjaLlog;
                                            tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaDebiTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                                        }
                                        else if (dK == 2)
                                        {
                                            tLlogKF.VleftaDebiTrupiFleteKontabel = veprimTrupi.GjendjaLlog;
                                            tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaKrediTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                                        }
                                    }
                                    else if (veprimTrupi.GjendjaLlog == 0 && veprimTrupi.GjendjaMonBaze != 0)
                                    {
                                        tLlogKF.Kursi = 1;
                                        if (dK == 1)
                                        {
                                            tLlogKF.VleftaKrediTrupiFleteKontabel = veprimTrupi.GjendjaMonBaze;
                                            tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaDebiTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                                        }
                                        else if (dkllog == 2)
                                        {
                                            tLlogKF.VleftaDebiTrupiFleteKontabel = veprimTrupi.GjendjaMonBaze;
                                            tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaKrediTrupiFleteKontabel = 0;
                                            tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                                        }
                                    }
                                }
                                ekziston = false;
                                var eksistonkf = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    if (oNenLlojLlogarie.KodNenLlojLlogarie == "KKL")
                                    {
                                        for (var k = 0; k < emrakf.Count; k++)
                                        {
                                            if (oKlient.IdKlientFurnitor == Convert.ToInt32(emrakf[k]))
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    trupiPerGjendjeKF[k].VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    trupiPerGjendjeKF[k].VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    trupiPerGjendjeKF[k].VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    trupiPerGjendjeKF[k].VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                eksistonkf = true;
                                                break;
                                            }
                                        }
                                        if (!eksistonkf)
                                        {
                                            var t = new clsTrupiFleteKontabel();
                                            t.VleftaDebiMonBazeTrupiFleteKontabel = tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                            t.VleftaDebiTrupiFleteKontabel = tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            t.VleftaKrediMonBazeTrupiFleteKontabel = tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                            t.VleftaKrediTrupiFleteKontabel = tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            t.IdMonedha = tLlogKF.IdMonedha;
                                            t.Kursi = tLlogKF.Kursi;

                                            trupiPerGjendjeKF.Add(t);
                                            if (oKlient.LlojiKF == true)
                                                rreshtakf.Add("KL");
                                            else rreshtakf.Add("FR");
                                            emrakf.Add(oKlient.IdKlientFurnitor);
                                        }
                                    }
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);
                                }
                                break;
                        }
                    }
                }
            } //per te grupuar vlerat e llogarive
            foreach (var f in colTrupFK)
            {
                if (f.VleftaDebiMonBazeTrupiFleteKontabel != 0 && f.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                {
                    var diferenca = f.VleftaDebiMonBazeTrupiFleteKontabel - f.VleftaKrediMonBazeTrupiFleteKontabel;
                    if (diferenca > 0)
                    {
                        f.VleftaDebiMonBazeTrupiFleteKontabel = diferenca;
                        f.VleftaDebiTrupiFleteKontabel = f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel;
                        f.VleftaKrediTrupiFleteKontabel = 0;
                        f.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                    }
                    else if (diferenca < 0)
                    {
                        f.VleftaKrediMonBazeTrupiFleteKontabel = Math.Abs(diferenca);
                        f.VleftaKrediTrupiFleteKontabel = Math.Abs(f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel);
                        f.VleftaDebiTrupiFleteKontabel = 0;
                        f.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    }
                }
            }
            return colTrupFK;
        }

        /// <summary>
        ///gjeneron kontabilizimin e nje dokumenti proces prodhimi.
        ///gjenerohet koka e fletes kontabel, trupi, dhe llogarite e kontabilitetit sipas skemes se kontabilitetit me te cilen eshte ruajtur dokumenti
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te prodhimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te prodhimit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idkoka">id ritese e kokes se prodhimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="vlera"> vlera totale e dokumentit</param>
        /// <param name="colProdukt">kolektion i trupit te prodhimit</param>
        /// <returns>kthen nje obj clsKokaFleteKontabel me kontabilizimin e dokumentit te prodhimit</returns>
        internal static clsKokaFleteKontabel gjeneroKontabilizimProdhim(int idkoka, int idNiv, int idKonf, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, DbProdhimi.colProduktProdhimi colProdukt, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, int iddegeadm, int iddep, int idnendep, out string shfaqmesazhapolupe, int iddokngaqendra, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci, clsDatabaseKontabilitet dbkont)
        {
            #region KOKA E FLETES KONTABEL
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var kontabilizuar = true;
            var idGrupKontabilizimi = 0;
            var dbshare = new clsDatabaseShare(dbkont);
            var konf = new clsKonfigurimAmbjenti(idKonf, dbshare);

            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);

            var colTrupFK = new colTrupatFletetKontabel();
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonf, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk.mbushKonfigAmbjSipasId(kusht.Vlera, dbshare, idGjuha);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbshare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbshare) == "Po";
            var coltrupiQK = new DbQendraKosto.colTrupiQendraKosto();
            colTrupFK = krijoTrupFletKontabelProdhim(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, colProdukt, dtDk, pershkrimi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, dbkont, out coltrupiQK, trupivjeterqendra, iddegeadm, idPer, out shfaqmesazhapolupe, rishpernda, shperndaDifQKPModDok, idStatusDok);

            var kokaqender = new DbQendraKosto.clsKokaQendraKosto();
            var dbqend = new DbQendraKosto.clsDatabaseQendraKosto(dbkont);
            var mesazh = DbQendraKosto.clsKokaQendraKosto.KrijoQKLP(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaqendra, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, coltrupiQK, dbqend, ref kokaqender, shperndaDifQKPModDok);
            if (kokaqender.ColTrupi.Count == 0)
                shfaqmesazhapolupe = "Jo";
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);


            //DbQendraKosto.clsKokaQendraKosto kokaqender = DbQendraKosto.clsKokaQendraKosto.krijoQKShitje(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaqendra, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, dbkont, trupivjeterqendra, 0);
            #endregion

            var kokaFK = new clsKokaFleteKontabel();

            mesazh = kokaFK.KrijoFlete(new DbData(), idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNiv, idkoka, idKonf, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelProdhim(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, DbProdhimi.colProduktProdhimi colprod, DateTime data, string pershkrimi, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj, clsDatabaseKontabilitet dbkont, out DbQendraKosto.colTrupiQendraKosto trupiQK, DbQendraKosto.colTrupiQendraKosto trupiQKold, int iddegeadm, int idperdoruesi, out string shfaqmesazh, bool rishpernda, bool shperndaDifQKPModDok, int statusdok)
        {
            shfaqmesazh = "jo";
            trupiQK = new DbQendraKosto.colTrupiQendraKosto();
            var eshteAzhornim = false;
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = false;
            var dbshare = new clsDatabaseShare(dbkont);
            var dbadm = new clsDatabaseAdmin(dbkont);
            var monedheNdermarrje = new clsMonedha();
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK");
            if (alternativa == "Azhornim")
            {
                azhornim = true;
                monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbadm);
            }

            var kurseDate = new colKurset(idNdermarrje, data, dbadm);

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oLlogari = new clsLlogari();
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;

            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;


            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }

                }
            }
            var dbinv = new clsDatabaseInventari(dbkont);
            var dbqk = new DbQendraKosto.clsDatabaseQendraKosto(dbkont);
            List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK> objektetQK = new List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK>();
            foreach (var veprimTrupi in colprod)
            {
                var oArtikull = new clsArtikulli();
                oArtikull.mbushArtikull(veprimTrupi.IdArtikulli, dbinv);
                for (var i = 0; i < tempCol.Count; i++)
                {
                    objektivat = new DbQendraKosto.colObjektivaKosto();
                    vleratobjektiva = new List<double>();
                    vleratobjektivamonbaze = new List<double>();
                    idllogobj = new List<int>();
                    var tLlogKF = new clsTrupiFleteKontabel();
                    tempSkemaKontTrupi = tempCol[i];
                    //clsLlojLlogarish oLlojLlogarish = transactionCache.GetLlojLlogariFromCache(colLlojLlogCache, int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), dbkont);
                    var oLlojLlogarish = new clsLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdLlojLlogarise), dbkont);

                    if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                    {
                        switch (oLlojLlogarish.KodLlojLlogarie)
                        {
                            case "ART"://nese ne kete rresht te grides eshte zgjedhur artikull
                                var ekziston = false;
                                var colTrupatPerQK = new colTrupatFletetKontabel();
                                oLlogari = oArtikull.merrLlogariArtikulli(new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbkont).KodNenLlojLlogarie, dbkont);
                                //oLlogari = oArtikull.merrLlogariArtikulli(colLlogCache,transactionCache.GetNenLlojLlogarishFromCache(colNenLlojLlogCache, int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbkont).KodNenLlojLlogarie, dbkont);
                                //oLlogari = oArtikull.merrLlogariArtikulli(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), dbkont);
                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.KostoTotale, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbadm);
                                //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.KostoTotale, oLlogari, azhornim, idNdermarrje, null, null, data,  eshteAzhornim,pershkrimi,dbadm);
                                var objekt = new DbQendraKosto.clsObjektivaKosto();
                                if (oArtikull.IdObjektivaKosto != 0 && oArtikull.IdObjektivaKosto != -1)
                                {
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqk);
                                    if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                            objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqk);
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqk);
                                if (objekt.Id != 0 && objekt.Id != -1)
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    var ekzistonqk = false;
                                    foreach (var f in colTrupatPerQK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                    }
                                    
                                    if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston)
                                        colTrupFK.Add(tLlogKF);

                                    clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = veprimTrupi.IdMag, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                    objektetQK.Add(obj);

                                    //var mesazh = "jo";
                                    //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, veprimTrupi.IdMag, dbqk, rishpernda, shperndaDifQKPModDok, statusdok));
                                    //if (mesazh != "jo")
                                    //    shfaqmesazh = mesazh;
                                }
                                break;
                        }
                    }
                    else if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VRP")
                    {
                        foreach (var rec in veprimTrupi.ColReceptura)
                        {
                            objektivat = new DbQendraKosto.colObjektivaKosto();
                            vleratobjektiva = new List<double>();
                            vleratobjektivamonbaze = new List<double>();
                            idllogobj = new List<int>();
                            switch (oLlojLlogarish.KodLlojLlogarie)
                            {
                                case "ART"://nese ne kete rresht te grides eshte zgjedhur artikull
                                    var ekziston = false;
                                    if (rec.IdArtikulli == 0)
                                        continue;
                                    var oArtikullrec = new clsArtikulli();
                                    oArtikullrec.mbushArtikull(rec.IdArtikulli, dbinv);
                                    var colTrupatPerQK = new colTrupatFletetKontabel();

                                    var idNenLlojLlogari = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                                    if ((oArtikullrec.Klasa == 5 || oArtikullrec.Klasa == 6) && idNenLlojLlogari == 10)//idNenLlojLlogari 10 eshte kodi LLBL
                                        idNenLlojLlogari = 21;
                                    oLlogari = oArtikullrec.merrLlogariArtikulli(new clsNenLlojLlogarish(idNenLlojLlogari, dbkont).KodNenLlojLlogarie, dbkont);
                                    //oLlogari = oArtikullrec.merrLlogariArtikulli(colLlogCache, transactionCache.GetNenLlojLlogarishFromCache(colNenLlojLlogCache, idNenLlojLlogari, dbkont).KodNenLlojLlogarie, dbkont);
                                    //oLlogari = oArtikullrec.merrLlogariArtikulli(idNenLlojLlogari,dbkont);
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, rec.KostoTotale, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbadm);
                                    //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, rec.KostoTotale, oLlogari, azhornim, idNdermarrje, null, null, data,  eshteAzhornim,pershkrimi,dbadm);
                                    var objekt = new DbQendraKosto.clsObjektivaKosto();
                                    if (oArtikull.IdObjektivaKosto != 0 && oArtikull.IdObjektivaKosto != -1)
                                    {
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqk);
                                        if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                            if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqk);
                                    }
                                    else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                        objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqk);
                                    if (objekt.Id != 0 && objekt.Id != -1)
                                        ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);

                                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                    {
                                        var ekzistonqk = false;
                                        foreach (var f in colTrupatPerQK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekzistonqk = true;
                                            }
                                        }
                                        
                                        if (!ekzistonqk) colTrupatPerQK.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));

                                        foreach (var f in colTrupFK)
                                        {
                                            if (f.IdLlogari == tLlogKF.IdLlogari)
                                            {
                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                    f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                }
                                                else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                    f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                }
                                                ekziston = true;
                                            }
                                        }
                                        if (!ekziston)
                                            colTrupFK.Add(tLlogKF);

                                        clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = colTrupatPerQK, IdDegeAdministrative = iddegeadm, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = rec.IdMag, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                        objektetQK.Add(obj);

                                        //var mesazh = "jo";
                                        //trupiQK.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(colTrupatPerQK, iddegeadm, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, trupiQKold, idperdoruesi, rec.IdMag, dbqk, rishpernda, shperndaDifQKPModDok, statusdok));
                                        //if (mesazh != "jo")
                                        //    shfaqmesazh = mesazh;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            string mesazh = "jo";
            trupiQK.AddRange(clsKokaQendraKosto.KrijoTrupQK(dbqk, objektetQK, trupiQKold, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
            if (mesazh != "jo")
                shfaqmesazh = mesazh;

            return colTrupFK;
        }

        /// <summary>
        ///gjeneron kontabilizimin e nje dokumenti amortizim.
        ///gjenerohet koka e fletes kontabel, trupi, dhe llogarite e kontabilitetit sipas skemes se kontabilitetit me te cilen eshte ruajtur dokumenti
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idkokaamortizimi">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="vlera"> vlera totale e dokumentit</param>
        /// <param name="ocolTrupiMagazina">kolektion i trupit te magazines</param>
        /// <returns>kthen nje obj clsKokaFleteKontabel me kontabilizimin e dokumentit te magazines</returns>
        internal static clsMesazh gjeneroKontabilizimAmortizim(int idkokaamortizimi, int idNiv, int idKonf, DateTime dtDk, string nrDk, double vlera, int idNder, int idNdVt, int idPer, DateTime dtRegj, colAmortizimiTrupiAbstract ocolTrupi, colAmortizimiTrupiAbstract ocolTrupiRezerva, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, int idkategoria, out string shfaqmesazhapolupe, int iddokngaQK, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idllojstandarti, int idllogari, clsDatabaseKontabilitet db, out clsKokaFleteKontabel kokaFK)
        {
            #region KOKA E FLETES KONTABEL
            kokaFK = new clsKokaFleteKontabel();
            var nrRef = "1"; //se gjenerohet nga trigger ne db
            var kontabilizuar = true;
            var idGrupKontabilizimi = 0;
            shfaqmesazhapolupe = "";
            var dbshare = new clsDatabaseShare(db);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var konf = new clsKonfigurimAmbjenti(idKonf, dbshare);
            if (konf.IdSkemeKontabel == 0)
                return new clsMesazh(true);
            var konfFK = new clsKonfigurimAmbjenti(konf.IdSkemeKontabel, dbshare);


            var colTrupFK = new colTrupatFletetKontabel();
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK", dbshare);
            int idStatusDok = MerrStatusFleteKontabel(idKonf, dbshare);
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk = new clsKonfigurimAmbjenti(kusht.Vlera, dbshare);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder, dbshare);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD", dbshare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD", dbshare) == "Po";
            var trupiQendra = new DbQendraKosto.colTrupiQendraKosto();
            if (ocolTrupi.Count > 0)
                colTrupFK = krijoTrupFletKontabelAmortizimi(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, ocolTrupi, vlera, dtDk, idllojstandarti, idllogari, idkategoria, pershkrimi, out shfaqmesazhapolupe, out trupiQendra, trupivjeterqendra, idPer, db, rishpernda, shperndaDifQKPModDok, idStatusDok);
            if (ocolTrupiRezerva.Count > 0)
                colTrupFK.AddRange(krijoTrupFletKontabelAmortizimi(idNder, konfFK.IdKonfigAmbjente, konfFK.IdSkemeKontabel, ocolTrupiRezerva, vlera, dtDk, idllojstandarti, idllogari, idkategoria, pershkrimi, out shfaqmesazhapolupe, out trupiQendra, trupivjeterqendra, idPer, db, rishpernda, shperndaDifQKPModDok, idStatusDok));
            var kokaqender = new DbQendraKosto.clsKokaQendraKosto();
            clsMesazh mesazh;
            mesazh = DbQendraKosto.clsKokaQendraKosto.KrijoQKLP(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaQK, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, trupiQendra, dbqendra, ref kokaqender, shperndaDifQKPModDok);
            if (!mesazh.Status)
                return new clsMesazh(false, mesazh.PershkrimMesazhi);
            //DbQendraKosto.clsKokaQendraKosto kokaqender = DbQendraKosto.clsKokaQendraKosto.krijoQKShitje(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, iddokngaQK, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, colTrupFK, iddegeadm, iddep, idnendep, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out  shfaqmesazhapolupe, dbshare , trupivjeterqendra);
            #endregion
            if (colTrupFK.Count == 0)//rasti kur vlerat jane zero
                return new clsMesazh(true);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, idKonf);
            mesazh = kokaFK.KrijoFlete(idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, idNiv, idkokaamortizimi, idKonf, idDokNga, idNder, kokaqender, dbshare, formatNrPerKonfig);

            if (!mesazh.Status)
                return new clsMesazh(false, mesazh.PershkrimMesazhi);  //hedhim exception sepse nuk u krijua fleta
            return new clsMesazh(true);
        }

        private static colTrupatFletetKontabel krijoTrupFletKontabelAmortizimi(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, colAmortizimiTrupiAbstract trupi, double vlera, DateTime data, int idllojstandarti, int idllogari, int idkategoria, string pershkrimi, out string shfaqmesazhapolupe, out DbQendraKosto.colTrupiQendraKosto trupiQendra, DbQendraKosto.colTrupiQendraKosto qendravjetertrupi, int idperdoruesi, clsDatabaseKontabilitet db, bool rishpernda, bool shperndaDifQKPModDok, int statusdok)
        {
            if (db.TransCache.ColKarakteristikaStandarti.Count == 0)
                db.TransCache.ColKarakteristikaStandarti = new colKarakteristikaStandarti(idNdermarrje, new clsDatabazeAsete(db));
            trupiQendra = new DbQendraKosto.colTrupiQendraKosto();
            shfaqmesazhapolupe = "jo";
            //objektivat = new DbQendraKosto.colObjektivaKosto();
            //vleratobjektiva = new List<double>();
            //vleratobjektivamonbaze = new List<double>();
            //idllogobj = new List<int>();
            var eshteAzhornim = false;
            var dbshare = new clsDatabaseShare(db);
            var dbinv = new clsDatabaseInventari(db);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var dbAdmin = new clsDatabaseAdmin(db);
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbasete = new clsDatabazeAsete(db);
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = false;
            var kusht = new clsKusht(idKonfigAmbjente, "LLFK", dbshare);
            var monedheNdermarrje = new clsMonedha();
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK");
            if (alternativa == "Azhornim")
            {
                azhornim = true;
                monedheNdermarrje.merrMonedheNdermarjeNgaCacheja(idNdermarrje, dbAdmin);
            }

            var kurseDate = new colKurset(idNdermarrje, data, dbAdmin);

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            //clsKlientFurnitor oKlient = new clsKlientFurnitor();
            var oLlogari = new clsLlogari();
            //DbInventari.clsArtikulli oArtikull = new DbInventari.clsArtikulli();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;

            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;

            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                    {
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                    }
                }
            }

            List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK> objektetQK = new List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK>();
            for (var i = 0; i < tempCol.Count; i++)
            {

                var tLlogKF = new clsTrupiFleteKontabel();
                tempSkemaKontTrupi = tempCol[i];
                if (trupi.objektiKod == enumObjekteAmortizimi.ASETE && tempSkemaKontTrupi.FormulaSkemeKontTrupi == "Rezerve")
                    continue;
                if (trupi.objektiKod == enumObjekteAmortizimi.REZERVA && tempSkemaKontTrupi.FormulaSkemeKontTrupi != "Rezerve")
                    continue;
                var oLlojLlogarish = new clsLlojLlogarish();

                oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                oLlojLlogarish = new clsLlojLlogarish(oLlojLlogarish.IdLlojLlogarie, db);
                
                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "ART"://nese ne kete rresht te grides eshte zgjedhur artikull
                            var rreshti = -1;
                            foreach (var veprimTrupi in trupi)
                            {
                                rreshti++;
                                var ekziston = false;
                                var oArtikull = veprimTrupi.Artikull ?? new clsArtikulli(veprimTrupi.IdArtikulli, dbinv);
                                var coltrupatePunonjesit = new colTrupatFletetKontabel();
                                var objektivat = new DbQendraKosto.colObjektivaKosto();
                                var vleratobjektiva = new List<double>();
                                var vleratobjektivamonbaze = new List<double>();
                                var idllogobj = new List<int>();
                                if (!new clsKarakteristikaStandarti(idllojstandarti, oArtikull.Kodifikimi1Artikulli, 0, false, dbasete).Kontabilizim)
                                    continue;

                                oLlogari = oArtikull.merrLlogariArtikulli(new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db).KodNenLlojLlogarie, db);
                                if (tempSkemaKontTrupi.PershkrimSkemeKontTrupi == "AmShtese")
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.AmortizimiShtese, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.AmortizimiShtese, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                else
                                if (tempSkemaKontTrupi.PershkrimSkemeKontTrupi == "AmGjithsej")
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.AmortizimiGjithsej, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                else
                                if (tempSkemaKontTrupi.PershkrimSkemeKontTrupi == "GjendjeMinusAmGjithsej")
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.VleftaGjendje - veprimTrupi.AmortizimiGjithsej, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.AmortizimiGjithsej, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                else
                                if (tempSkemaKontTrupi.PershkrimSkemeKontTrupi == "VleftaPlusMinus")
                                {
                                    var njesi = new clsNjesiAdministrative(veprimTrupi.IdNjesiAdministrative, dbregj);
                                    var status = new clsStatusMagazine_Asete(njesi.IdStatusAktualMagazine, dbasete);


                                    if (status.Emertimi != "Aktive")///per jo aktivet merret llogaria aa ne proces
                                    {
                                        tempSkemaKontTrupi.IdNenLlojLlogarie = "20";
                                        oLlogari = oArtikull.merrLlogariArtikulli(new clsNenLlojLlogarish(int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie), db).KodNenLlojLlogarie, db);
                                    }
                                    tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.VleftaPlusMinus, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                    //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, veprimTrupi.VleftaPlusMinus, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                }
                                var objekt = new DbQendraKosto.clsObjektivaKosto();
                                if (oArtikull.IdObjektivaKosto != 0 && oArtikull.IdObjektivaKosto != -1)
                                {
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oArtikull.IdObjektivaKosto, dbqendra);
                                    if (!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data)))
                                        if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                            objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                }
                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                    objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                if (objekt.Id != 0 && objekt.Id != -1)
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    var ekzistonqk = false;
                                    foreach (var f in coltrupatePunonjesit)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                    }

                                    if (!ekzistonqk) coltrupatePunonjesit.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                    
                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston) colTrupFK.Add(tLlogKF);
                                }

                                var mag = new clsNjesiAdministrative(veprimTrupi.IdNjesiAdministrative, dbregj);

                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = coltrupatePunonjesit, IdDegeAdministrative = mag.IdDegeAdministrative, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = mag.IdNjesiAdministrative, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                objektetQK.Add(obj);

                                //var mesazh = "jo";
                                //trupiQendra.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(coltrupatePunonjesit, mag.IdDegeAdministrative, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, qendravjetertrupi, idperdoruesi, mag.IdNjesiAdministrative, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazhapolupe = mesazh;
                            }
                            break;
                    }
                }
                else
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "LL":
                            if (idllogari != 0)
                            {
                                oLlogari = new clsLlogari(idllogari, db);
                                //oLlogari = transactionCache.getLlogariFromCache(myColLlog, idllogari, db);
                                //oLlogari = new clsLlogari(idllogari, db);
                                double totali = 0;
                                var coltrupatePunonjesit = new colTrupatFletetKontabel();
                                var objektivat = new DbQendraKosto.colObjektivaKosto();
                                var vleratobjektiva = new List<double>();
                                var vleratobjektivamonbaze = new List<double>();
                                var idllogobj = new List<int>();
                                foreach (var veprimTrupi in trupi)
                                {
                                    var oArtikull = veprimTrupi.Artikull ?? new clsArtikulli(veprimTrupi.IdArtikulli, dbinv);

                                    if (!new clsKarakteristikaStandarti(idllojstandarti, oArtikull.Kodifikimi1Artikulli, 0, false, dbasete).Kontabilizim)
                                        continue;
                                    // karakteristika.merrKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(oArtikull.Kodifikimi1Artikulli, dbinv), idllojstandarti, idNdermarrje, dbasete);
                                    if (tempSkemaKontTrupi.PershkrimSkemeKontTrupi == "AmGjithsej") totali += veprimTrupi.AmortizimiGjithsej;
                                    if (tempSkemaKontTrupi.PershkrimSkemeKontTrupi == "GjendjeMinusAmGjithsej")
                                        totali += (veprimTrupi.VleftaGjendje - veprimTrupi.AmortizimiGjithsej);
                                    else if (tempSkemaKontTrupi.PershkrimSkemeKontTrupi == "VleftaPlusMinus")
                                        totali += veprimTrupi.VleftaPlusMinus;
                                }
                                tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, totali, oLlogari, azhornim, null, null, eshteAzhornim, pershkrimi, kurseDate, monedheNdermarrje, dbAdmin);
                                //tLlogKF = new clsTrupiFleteKontabel(tempSkemaKontTrupi.DebikrediSkemeKontTrupi, totali, oLlogari, azhornim, idNdermarrje, null, null, data, eshteAzhornim, pershkrimi, dbAdmin);
                                if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                {
                                    var objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto, dbqendra);
                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);
                                }
                                var ekziston = false;
                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                {
                                    var ekzistonqk = false;
                                    foreach (var f in coltrupatePunonjesit)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekzistonqk = true;
                                        }
                                    }
                                    
                                    if (!ekzistonqk) coltrupatePunonjesit.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));

                                    foreach (var f in colTrupFK)
                                    {
                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                        {
                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                            }
                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                            {
                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                            }
                                            ekziston = true;
                                        }
                                    }
                                    if (!ekziston) colTrupFK.Add(tLlogKF);
                                }

                                clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = coltrupatePunonjesit, IdDegeAdministrative = 0, IdDepartamenti = 0, IdNendepartamenti = 0, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze };
                                objektetQK.Add(obj);

                                //var mesazh = "jo";
                                //trupiQendra.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(coltrupatePunonjesit, 0, 0, 0, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, qendravjetertrupi, idperdoruesi, 0, dbqendra, rishpernda, shperndaDifQKPModDok, statusdok));
                                //if (mesazh != "jo")
                                //    shfaqmesazhapolupe = mesazh;
                            }
                            break;
                        case "":
                            break;
                    }
                }
            }

            string mesazh = "jo";
            trupiQendra.AddRange(clsKokaQendraKosto.KrijoTrupQK(dbqendra ,objektetQK, qendravjetertrupi, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
            if (mesazh != "jo")
                shfaqmesazhapolupe = mesazh;

            return colTrupFK;
        }

        /// <summary>
        ///gjeneron kontabilizimin e nje dokumenti listpagese.
        ///gjenerohet koka e fletes kontabel, trupi, dhe llogarite e kontabilitetit sipas skemes se kontabilitetit me te cilen eshte ruajtur dokumenti
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te listpageses</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te listpageses</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idkoka">id ritese e kokes se listpageses</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="vlera"> vlera totale e dokumentit</param>
        /// <param name="ocoltrupat">kolektion i trupit </param>
        /// <returns>kthen nje obj clsKokaFleteKontabel me kontabilizimin e dokumentit te listpageses</returns>
        internal static clsKokaFleteKontabel gjeneroKontabilizimListPagese(int idkoka, int idNiv, int idKonf, DateTime dtDk, string nrDk, int idNder, int idNdVt, int idPer, DateTime dtRegj, DbListPagesat.colTrupiListPagese ocoltrupat, string pershkrimi, int idDokNga, int idLlojDok, int idPeriudha, decimal kursi, int monedha, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {

            #region KOKA E FLETES KONTABEL
            var dbshare = new clsDatabaseShare(new clsDatabaseKontabilitet());
            const string nrRef = "1"; //se gjenerohet nga trigger ne db
            const bool kontabilizuar = true;
            const int idGrupKontabilizimi = 0;
            const int idKategoria = 38;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(idKonf);
            int idKonfigFK = clsKusht.ktheVlereKushti(idKonf, "ZFK", dbshare);
            var konfFK = new clsKonfigurimAmbjenti(idKonfigFK);
            int idSkemeKontabel = clsKusht.ktheVlereKushti(konfFK.IdKonfigAmbjente, "SK", dbshare);
            var kusht = new clsKusht(konfFK.IdKonfigAmbjente, "ZRQK");
            var konfqk = new clsKonfigurimAmbjenti();
            if (kusht.Vlera != 0)
                konfqk.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);
            else
                konfqk.mbushKonfigAmbjSipasKod("RQK", idNder);
            int idStatusDok = MerrStatusFleteKontabel(idKonf, dbshare);
            var colTrupFK = new colTrupatFletetKontabel();

            bool rishpernda = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "RSKDMD") == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfqk.IdKonfigAmbjente, "SHDQKPMD") == "Po";
            var trupiQendra = new DbQendraKosto.colTrupiQendraKosto();
            colTrupFK = krijoTrupFletKontabelListPagesa(idNder, konfFK.IdKonfigAmbjente, idSkemeKontabel, ocoltrupat, kursi, monedha, dtDk, out shfaqmesazhapolupe, pershkrimi, out trupiQendra, trupivjeterqendra, idKonf, idPer, rishpernda, shperndaDifQKPModDok, idStatusDok);
            var kokaqender = DbQendraKosto.clsKokaQendraKosto.KrijoQKLP(konfqk.IdNivel, konfqk.IdKonfigAmbjente, 1, dtDk, nrDk, 0, idStatusDok, idNder, idNdVt, idPer, dtRegj, pershkrimi, konfFK.IdNivel, konfFK.IdKonfigAmbjente, 0, trupiQendra, shperndaDifQKPModDok);//, ocoltrupat.Count

            #endregion

            var kokaFK = new clsKokaFleteKontabel();

            var mesazh = kokaFK.KrijoFlete(new DbData(), idStatusDok, idNdVt, nrDk, nrRef, dtDk, dtRegj, konfFK.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idPer, colTrupFK, kontabilizuar, idLlojDok, idKategoria, idPeriudha, idNiv, idkoka, idKonf, idDokNga, idNder, kokaqender);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        /// <summary>
        /// krijon trupin e fletes kontabel sipas skemes
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="idKonfigAmbjente">id kunfigurimi</param>
        /// <param name="idSkemeKontabel">id skemakontabel</param>
        /// <param name="trupi">trupi</param>
        /// <param name="kursi"> kursi </param>
        /// <param name="idmonedha">monedha</param>
        /// <returns></returns>
        /// <param name="pershkrimi"></param>
        private static colTrupatFletetKontabel krijoTrupFletKontabelListPagesa(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, DbListPagesat.colTrupiListPagese trupi, decimal kursi, int idmonedha, DateTime data, out string shfaqmesazhapolupe, string pershkrimi, out DbQendraKosto.colTrupiQendraKosto trupiQendra, DbQendraKosto.colTrupiQendraKosto qendravjetertrupi, int idkonfiggjenerues, int idperdoruesi, bool rishpernda, bool shperndaDifQKPModDok, int statusdok)
        {
            var eshteAzhornim = false;

            shfaqmesazhapolupe = "jo";
            trupiQendra = new DbQendraKosto.colTrupiQendraKosto();
            var colTrupFK = new colTrupatFletetKontabel();
            var azhornim = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK") == "Azhornim";

            var llogdep = "Asnjera";
            llogdep = clsAlternativaKushti.getAlternativa(idkonfiggjenerues, "LLOGD");

            var tempSkemaKontTrupi = new clsSkemaKontabelTrupiNew();
            var oLlogari = new clsLlogari();
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            var indexGrupimi = 0;

            var clsSkemaKontNew = new clsSkemaKontabelNew();
            clsSkemaKontNew = clsSkemaKontNew.mbushSkemeKontabelNewSipasID(idSkemeKontabel);

            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew;
            IEnumerable<clsSkemaKontabelTrupiNew> tempSkemaKontTrupiNew_2;

            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel
            for (var i = 0; i < clsSkemaKontNew.OColSkemakontabelTrupiNew.Count; i++)
            {
                indexGrupimi = clsSkemaKontNew.OColSkemakontabelTrupiNew[i].IndeksGrupimi;
                //kontrollohet nese tek collectioni temporar ekziston nje element me te njejtin index grupim                
                tempSkemaKontTrupiNew = from l in tempCol
                                        where l.IndeksGrupimi == indexGrupimi
                                        select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Count<clsSkemaKontabelTrupiNew>() == 0)
                {
                    //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                    tempSkemaKontTrupiNew_2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                              where l.IndeksGrupimi == indexGrupimi
                                              select l;
                    //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                    if (tempSkemaKontTrupiNew_2.Count<clsSkemaKontabelTrupiNew>() == 1)
                        tempCol.Add(tempSkemaKontTrupiNew_2.First<clsSkemaKontabelTrupiNew>());
                }
            }

            List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK> objektetQK = new List<clsKokaQendraKosto.TrupaFkPerShperndarjeQK>();
            for (var i = 0; i < tempCol.Count; i++)
            {
                tempSkemaKontTrupi = tempCol[i];

                var tLlogKF = new clsTrupiFleteKontabel();
                var oNenLlojLlogarie = new clsNenLlojLlogarish();

                var oLlojLlogarish = new clsLlojLlogarish();
                oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                oLlojLlogarish = oLlojLlogarish.merrLlojLlogarieSipasID();

                if (tempSkemaKontTrupi.KodSkemeKontTrupi == "VR")
                {
                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "LP"://nese ne kete rresht te grides eshte zgjedhur punonjes
                            oNenLlojLlogarie = new clsNenLlojLlogarish();
                            oNenLlojLlogarie.IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie);
                            oNenLlojLlogarie = oNenLlojLlogarie.merrNenLlojLlogarieSipasID();

                            switch (oNenLlojLlogarie.KodNenLlojLlogarie)
                            {
                                case "LPD":
                                case "LPK":

                                    foreach (var veprimTrupi in trupi)
                                    {
                                        var punonjes = new DbListPagesat.clsPunonjes(veprimTrupi.IdPunonjes);
                                        var objektivat = new DbQendraKosto.colObjektivaKosto();
                                        var vleratobjektiva = new List<double>();
                                        var vleratobjektivamonbaze = new List<double>();
                                        var idllogobj = new List<int>();
                                        var coltrupatePunonjesit = new colTrupatFletetKontabel();

                                        foreach (var komp in veprimTrupi.OColKomp)
                                        {
                                            var ekziston = false;
                                            var komppage = new DbListPagesat.clsKomponentePage(komp.IdKomponentePage);

                                            if ((komppage.IdLlogKredi != 0 && oNenLlojLlogarie.KodNenLlojLlogarie == "LPK") || (komppage.IdLlogDebi != 0 && oNenLlojLlogarie.KodNenLlojLlogarie == "LPD"))
                                            {
                                                oLlogari = new clsLlogari(oNenLlojLlogarie.KodNenLlojLlogarie == "LPK" ? komppage.IdLlogKredi : komppage.IdLlogDebi);
                                                if (llogdep == "Gjitha" || (llogdep == "Llogari page" && (oLlogari.NrLlogari.StartsWith("641") || oLlogari.NrLlogari.StartsWith("421"))))
                                                {
                                                    var dep = new DbListPagesat.clsStrukturaAdministrative(punonjes.IdDepartament);
                                                    if (!clsLlogari.ekzistonLlogari(oLlogari.NrLlogari + dep.Kodi, idNdermarrje))
                                                    {
                                                        oLlogari.NrLlogari = oLlogari.NrLlogari + dep.Kodi;
                                                        oLlogari.ruaj(false, "", "", "", "");
                                                    }
                                                    oLlogari = new clsLlogari(oLlogari.NrLlogari + dep.Kodi, idNdermarrje);
                                                }
                                                tLlogKF = new clsTrupiFleteKontabel(new clsDatabaseAdmin(), tempSkemaKontTrupi.DebikrediSkemeKontTrupi, Convert.ToDouble(komp.Vlera), oLlogari, azhornim, idNdermarrje, Convert.ToDouble(kursi), idmonedha, data, pershkrimi, eshteAzhornim);
                                                var objekt = new DbQendraKosto.clsObjektivaKosto();
                                                if (punonjes.IdObjektivaKosto != 0 && punonjes.IdObjektivaKosto != -1)
                                                {
                                                    objekt = new DbQendraKosto.clsObjektivaKosto(punonjes.IdObjektivaKosto);
                                                    if ((!(objekt.Nga <= data && (objekt.Deri == new DateTime() || objekt.Deri >= data))) && (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1))
                                                        objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);
                                                }
                                                else if (oLlogari.IdObjektivaKosto != 0 && oLlogari.IdObjektivaKosto != -1)
                                                    objekt = new DbQendraKosto.clsObjektivaKosto(oLlogari.IdObjektivaKosto);

                                                if (objekt.Id != 0 && objekt.Id != -1)
                                                    ShtoTeDhenaPerObjektivat(data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, oLlogari, tLlogKF, objekt);

                                                if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0 || tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                {
                                                    var ekzistonqk = false;
                                                    foreach (var f in coltrupatePunonjesit)
                                                    {
                                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                                        {
                                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                            {
                                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                            }
                                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                            {
                                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                            }
                                                            ekzistonqk = true;
                                                        }
                                                    }

                                                    if (!ekzistonqk)
                                                        coltrupatePunonjesit.Add(new clsTrupiFleteKontabel(0, 0, tLlogKF.IdLlogari, tLlogKF.PershkrimTrupiFleteKontabel, tLlogKF.IdMonedha, tLlogKF.Kursi, tLlogKF.VleftaDebiTrupiFleteKontabel, tLlogKF.VleftaKrediTrupiFleteKontabel, tLlogKF.KodMonedha, tLlogKF.KodiSkemaKontabel, tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel, tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel));
                                                    foreach (var f in colTrupFK)
                                                    {
                                                        if (f.IdLlogari == tLlogKF.IdLlogari)
                                                        {
                                                            if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                                                            {
                                                                f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                                                                f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                                                            }
                                                            else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                                                            {
                                                                f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                                                                f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                                                            }
                                                            ekziston = true;
                                                        }
                                                    }
                                                    if (!ekziston)
                                                        colTrupFK.Add(tLlogKF);
                                                }
                                            }
                                        }
                                        clsKokaQendraKosto.TrupaFkPerShperndarjeQK obj = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = coltrupatePunonjesit, IdDegeAdministrative = 0, IdDepartamenti = punonjes.IdDepartament, IdNendepartamenti = punonjes.IdNenDepartament, IdMagazina = 0, Objektivat = objektivat, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze }; //objektivat
                                        objektetQK.Add(obj);
                                    }
                                    //var mesazh = "jo";
                                    //trupiQendra.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQK(objektetQK, qendravjetertrupi, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
                                    ////trupiQendra.AddRange(DbQendraKosto.clsKokaQendraKosto.krijoTrupiQKShitje(coltrupatePunonjesit, 0, punonjes.IdDepartament, punonjes.IdNenDepartament, idNdermarrje, data, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out mesazh, qendravjetertrupi, idperdoruesi, 0, rishpernda, shperndaDifQKPModDok, statusdok, trupi.Count));
                                    //if (mesazh != "jo")
                                    //        shfaqmesazhapolupe = mesazh;
                                    break;
                                default:
                                    break;
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            var mesazh = "jo";
            trupiQendra.AddRange(clsKokaQendraKosto.KrijoTrupQK(new clsDatabaseQendraKosto(), objektetQK, qendravjetertrupi, idperdoruesi, idNdermarrje, data, rishpernda, shperndaDifQKPModDok, statusdok, out mesazh));
            if (mesazh != "jo")
                shfaqmesazhapolupe = mesazh;

            return colTrupFK;
        }

        /// <summary>
        /// per kete ambjent nuk ka nevoje per qendra kosto sepse fshihet
        /// </summary>
        /// <param name="idndermarje"></param>
        /// <param name="idnderviti"></param>
        /// <param name="date"></param>
        /// <param name="idperd"></param>
        /// <param name="idkonf"></param>
        /// <param name="idPeriudha"></param>
        /// <returns></returns>
        internal static clsKokaFleteKontabel GjeneroKontabilizimFleteAzhornim(int idndermarje, int idnderviti, DateTime date, int idperd, int idkonf, int idPeriudha, clsDatabaseKontabilitet dbkont)
        {
            #region KOKA E FLETES KONTABEL

            const string nrRef = "1"; //se gjenerohet nga trigger ne db
            const int idGrupKontabilizimi = 0;
            const bool kontabilizuar = true;
            var dbshare = new clsDatabaseShare(dbkont);
            var konf = new clsKonfigurimAmbjenti(idkonf, dbshare);
            const string pershkrimi = "";
            const int idLlojDok = 22;
            const int idkategoria = 5;

            int idStatusDok = MerrStatusFleteKontabel(idkonf, dbshare);

            var colTrupFK = new colTrupatFletetKontabel();
            colTrupFK = krijoTrupFleteKontabelFleteAzhornim(idndermarje, konf.IdKonfigAmbjente, konf.IdSkemeKontabel, date, pershkrimi, idperd, dbkont);
            if (colTrupFK.Count == 0)
                return new clsKokaFleteKontabel();

            #endregion

            var kokaFK = new clsKokaFleteKontabel();
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, 0);
            var mesazh = kokaFK.KrijoFlete(idStatusDok, idnderviti, "NKM", nrRef, date, date, konf.IdKonfigAmbjente, idGrupKontabilizimi, pershkrimi, idperd, colTrupFK, kontabilizuar, idLlojDok, idkategoria, idPeriudha, 0, 0, 0, 0, idndermarje, new DbQendraKosto.clsKokaQendraKosto(), dbshare, formatNrPerKonfig);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta

            kokaFK.Kontabilizuar = clsAlternativaKushti.getAlternativa(idkonf, "GJK") == "Direkt";
            return kokaFK;
        }

        private static colTrupatFletetKontabel krijoTrupFleteKontabelFleteAzhornim(int idNdermarrje, int idKonfigAmbjente, int idSkemeKontabel, DateTime data, string pershkrimi, int idperd, clsDatabaseKontabilitet dbkont)
        {
            var colllog = new colLlogarite();
            colllog.mbushLLogariteNdermarrjesAndAutorizimePerAzhornim(idNdermarrje, idperd, dbkont);
            var dbshare = new clsDatabaseShare(dbkont);
            var dbadmin = new clsDatabaseAdmin(dbkont);
            var colTrupFk = new colTrupatFletetKontabel();


            #region FLETE KONTABEL TRUPI PER VDK

            var azhornim = false;
            var clsSkemaKontNew = new clsSkemaKontabelNew(idSkemeKontabel, dbkont);
            var alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LLFK");
            var monedheNdermarrje = new clsMonedha();
            if (alternativa == "Azhornim")
            {
                azhornim = true;
                monedheNdermarrje = clsNdermarrje.ktheMonedheNdermSipasID(idNdermarrje, dbadmin);
            }
            var kurseDate = new colKurset(idNdermarrje, data, dbadmin);
            //collection temporar
            var tempCol = new colSkemaKontabelTrupiNew();
            //merren nje nga nje gjithe elementetet qe i perkasin kesaj skeme kontabel            
            foreach (var sk in clsSkemaKontNew.OColSkemakontabelTrupiNew)
            {
                var indexGrupimi = sk.IndeksGrupimi;
                var tempSkemaKontTrupiNew = from l in tempCol
                                            where l.IndeksGrupimi == indexGrupimi
                                            select l;
                //rasti kur nuk gjendet
                if (tempSkemaKontTrupiNew.Any()) continue;
                //merren nga trupi i skemes ekzistuese gjithe elementet me te njejtin index grupimi
                var tempSkemaKontTrupiNew2 = from l in clsSkemaKontNew.OColSkemakontabelTrupiNew
                                             where l.IndeksGrupimi == indexGrupimi
                                             select l;
                //nese gjendet vetem 1 athere shtohet tek collectioni temporar
                if (tempSkemaKontTrupiNew2.Count() == 1)
                {
                    tempCol.Add(tempSkemaKontTrupiNew2.First());
                }
            }
            var dkmon = 0;
            foreach (var llog in colllog)
            {
                var kurs = new clsKurset(llog.IdMonedha, data, dbadmin);
                var GJMB = clsLlogari.merrGjendjeLlogari(llog.IdLlogari, data, dbkont);
                var GJML = clsLlogari.merrGjendjeLlogariMonHuaj(llog.IdLlogari, data, dbkont);
                var gjendja = GJML * kurs.VleraKursi - GJMB;
                foreach (var tempSkemaKontTrupi in tempCol)
                {
                    var tLlogKF = new clsTrupiFleteKontabel();
                    var oLlojLlogarish = new clsLlojLlogarish();
                    var oNenLlojLlogarie = new clsNenLlojLlogarish();

                    oLlojLlogarish.IdLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdLlojLlogarise);
                    oLlojLlogarish = new clsLlojLlogarish(oLlojLlogarish.IdLlojLlogarie, dbkont);

                    if (tempSkemaKontTrupi.KodSkemeKontTrupi != "VR") continue;
                    oNenLlojLlogarie = new clsNenLlojLlogarish
                    {
                        IdNenLlojLlogarie = int.Parse(tempSkemaKontTrupi.IdNenLlojLlogarie)
                    };
                    oNenLlojLlogarie = new clsNenLlojLlogarish(oNenLlojLlogarie.IdNenLlojLlogarie, dbkont);

                    switch (oLlojLlogarish.KodLlojLlogarie)
                    {
                        case "ML":  //llogari monedhe
                            var oLlogari = new clsLlogari();
                            oLlogari.merrLlogFitimHumbjeKlientit(idNdermarrje, llog.IdMonedha, llog.IdLlogari, oNenLlojLlogarie.KodNenLlojLlogarie, dbkont);
                            var debiKredi = dkmon;
                            var krijoFk = oNenLlojLlogarie.KodNenLlojLlogarie == "LLMF" && dkmon == 2 || oNenLlojLlogarie.KodNenLlojLlogarie == "LLMH" && dkmon == 1;
                            if (krijoFk)
                                tLlogKF = new clsTrupiFleteKontabel(debiKredi, gjendja, oLlogari, azhornim, kurs.VleraKursi, llog.IdMonedha, true, pershkrimi, kurseDate, monedheNdermarrje, dbadmin);
                            break;

                        case "KL":
                            var dK = 0;
                            if (GJML > 0)
                            {
                                if (GJML * kurs.VleraKursi - GJMB > 0)
                                    dK = 1;
                                else if (GJML * kurs.VleraKursi - GJMB < 0)
                                    dK = 2;
                            }
                            else
                            {
                                if (GJML * kurs.VleraKursi - GJMB > 0)
                                    dK = 1;
                                else if (GJML * kurs.VleraKursi - GJMB < 0)
                                    dK = 2;
                            }
                            dkmon = dK == 1 ? 2 : 1;
                            tLlogKF = new clsTrupiFleteKontabel(dK, gjendja, llog, azhornim, kurs.VleraKursi, llog.IdMonedha, true, pershkrimi, kurseDate, monedheNdermarrje, dbadmin);
                            break;
                    }

                    var ekziston = false;
                    if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel == 0 && tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel == 0) continue;
                    foreach (var f in colTrupFk)
                    {
                        if (f.IdLlogari != tLlogKF.IdLlogari) continue;
                        if (tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                        {
                            f.VleftaDebiMonBazeTrupiFleteKontabel += tLlogKF.VleftaDebiMonBazeTrupiFleteKontabel;
                            f.VleftaDebiTrupiFleteKontabel += tLlogKF.VleftaDebiTrupiFleteKontabel;
                        }
                        else if (tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                        {
                            f.VleftaKrediMonBazeTrupiFleteKontabel += tLlogKF.VleftaKrediMonBazeTrupiFleteKontabel;
                            f.VleftaKrediTrupiFleteKontabel += tLlogKF.VleftaKrediTrupiFleteKontabel;
                        }
                        ekziston = true;
                    }
                    if (!ekziston)
                        colTrupFk.Add(tLlogKF);
                }
            }

            //per te grupuar vlerat e llogarive
            foreach (var f in colTrupFk)
            {
                if (f.VleftaDebiMonBazeTrupiFleteKontabel == 0 || f.VleftaKrediMonBazeTrupiFleteKontabel == 0) continue;
                var diferenca = f.VleftaDebiMonBazeTrupiFleteKontabel - f.VleftaKrediMonBazeTrupiFleteKontabel;
                if (diferenca > 0)
                {
                    f.VleftaDebiMonBazeTrupiFleteKontabel = diferenca;
                    f.VleftaDebiTrupiFleteKontabel = f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel;
                    f.VleftaKrediTrupiFleteKontabel = 0;
                    f.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                }
                else if (diferenca < 0)
                {
                    f.VleftaKrediMonBazeTrupiFleteKontabel = Math.Abs(diferenca);
                    f.VleftaKrediTrupiFleteKontabel = Math.Abs(f.VleftaDebiTrupiFleteKontabel - f.VleftaKrediTrupiFleteKontabel);
                    f.VleftaDebiTrupiFleteKontabel = 0;
                    f.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                }
            }

            #endregion FLETE KONTABEL TRUPI

            return colTrupFk;
        }

        /// <summary>
        /// Llogarit diferencen nga kursi sipas formules se paracaktuar
        /// </summary>
        /// <param name="KV">Kursi i bankes</param>
        /// <param name="KK">Kursi i fatures</param>
        /// <param name="KMK">Kursi i monedhes se fatures ne daten e dokumentit te bankes</param>
        /// <param name="VV">Vlera e dokumentit te bankes</param>
        /// <returns>Kthen vleren e diferences nga kursi</returns>
        private static double LlogaritDiferenceKursi(double KV, double KK, double KMK, double VV)
        {
            if (KMK != 0)
                return VV * KV - VV * KV * KK / KMK;
            return 0;
        }

        /// <summary>
        /// Kthen kahun per dokumentat e shitjes dhe blerjes (jo per dok e bankes)
        /// </summary>
        /// <param name="idNiveli">Niveli i dokumentit</param>
        /// <param name="nrDok">Numri i dokumentit</param>
        /// <returns>Kthen nje string "Debi" ose "Kredi" qe tregon kahun e dokumentit</returns>
        private static string KtheKahunDokumentitSipasNrDok(int idNiveli, string nrDok, DateTime dtdok, int idklientfurnitori, int idndermarje)
        {
            var kahu = "";
            var idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli);

            switch (idKategori)
            {
                case 1:
                    {
                        var dokumenti = new clsKokaShitje();
                        dokumenti.mbushKokaShitjeSipasIdNivelNrDokDtDok(idNiveli, nrDok, dtdok);
                        if (dokumenti.Totali < 0)
                            kahu = "Kredi";//"-Debi";
                        else if (dokumenti.Totali > 0)
                            kahu = "Debi";
                        break;
                    }
                case 2:
                    {
                        var dokumenti = new clsKokaShitje();
                        dokumenti.mbushKokaShitjeSipasIdNivelNrDokDtDok(idNiveli, nrDok, dtdok);
                        if (dokumenti.Totali < 0)
                            kahu = "Debi";// "-Kredi";
                        else if (dokumenti.Totali > 0)
                            kahu = "Kredi";
                        break;
                    }
                case 20:
                    {
                        var koka = new colVeprimeKFKoka();
                        koka.merrVeprimeKFKokaSipasNrDokDtDokAndKF(nrDok, dtdok, idndermarje, idklientfurnitori);//idklientfurni
                        var dokumenti = new colVeprimeKFTrupi();
                        dokumenti.MbushVeprimeKfTrupi(koka[0].IdVeprimeKFKoka);
                        switch (dokumenti[0].DebiKredi)
                        {
                            case 1:
                                kahu = "Debi";
                                break;
                            case 2:
                                kahu = "Kredi";
                                break;
                        }
                        break;
                    }
            }
            return kahu;
        }

        /// <summary>
        /// Kthen kahun per dokumentat
        /// </summary>
        /// <param name="idNiveli">Niveli i dokumentit</param>
        /// <param name="idDok">ID e dokumentit</param>
        /// <returns>Kthen nje string "Debi" ose "Kredi" qe tregon kahun e dokumentit</returns>
        private static string KtheKahunDokumentitSipasIdDok(int idNiveli, int idDok)
        {
            var kahu = "";
            var idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli);
            var dokumenti = new clsKokaShitje();

            switch (idKategori)
            {
                case 1: //Shitje   
                    dokumenti.mbushKokaShitjeSipasIDPaTrup(idDok); //todo senada
                    if (dokumenti.Totali < 0)
                        kahu = "Kredi";//"-Debi";
                    else
                        if (dokumenti.Totali > 0)
                        kahu = "Debi";
                    break;
                case 2: //Blerje
                    dokumenti.mbushKokaShitjeSipasIDPaTrup(idDok);
                    if (dokumenti.Totali < 0)
                        kahu = "Debi";// "-Kredi";
                    else
                        if (dokumenti.Totali > 0)
                        kahu = "Kredi";
                    break;
                case 3:
                case 4://Arka ose Banka
                    var dokumentBanke = new clsVeprimBankaKoka(idDok);
                    switch (dokumentBanke.LlojiVeprimit)
                    {
                        case "Derdhje":
                            kahu = "Kredi";
                            break;
                        case "Terheqje":
                            kahu = "Debi";
                            break;
                    }
                    break;
                case 20://veprimekf
                    var dokumentKf = new colVeprimeKFTrupi();
                    dokumentKf.MbushVeprimeKfTrupi(idDok);
                    switch (dokumentKf[0].DebiKredi)
                    {
                        case 1:
                            kahu = "Debi";
                            break;
                        case 2:
                            kahu = "Kredi";
                            break;
                    }
                    break;
            }
            return kahu;
        }

        private static string KtheKahunDokumentitSipasIdDok(int idNiveli, int idDok, clsDatabaseRegjistrim dbregj)
        {
            var kahu = "";

            var idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli, dbregj);
            var dokumenti = new clsKokaShitje();

            switch (idKategori)
            {
                case 1: //Shitje   
                    dokumenti.mbushKokaShitjeSipasIDPaTrup(idDok, dbregj);
                    if (dokumenti.Totali < 0)
                        kahu = "Kredi";//"-Debi";
                    else
                        if (dokumenti.Totali > 0)
                        kahu = "Debi";
                    break;
                case 2: //Blerje
                    dokumenti.mbushKokaShitjeSipasIDPaTrup(idDok, dbregj);
                    if (dokumenti.Totali < 0)
                        kahu = "Debi";// "-Kredi";
                    else
                        if (dokumenti.Totali > 0)
                        kahu = "Kredi";
                    break;
                case 3:
                case 4://Arka ose Banka
                    var dbarka = new clsDatabaseArkaBanka(dbregj);
                    var dokumentBanke = new clsVeprimBankaKoka(idDok, dbarka);
                    switch (dokumentBanke.LlojiVeprimit)
                    {
                        case "Derdhje":
                            kahu = "Kredi";
                            break;
                        case "Terheqje":
                            kahu = "Debi";
                            break;
                    }
                    break;
                case 20://veprimekf
                    var dokumentKf = new colVeprimeKFTrupi();
                    dokumentKf.MbushVeprimeKfTrupi(idDok, dbregj);
                    switch (dokumentKf[0].DebiKredi)
                    {
                        case 1:
                            kahu = "Debi";
                            break;
                        case 2:
                            kahu = "Kredi";
                            break;
                    }
                    break;
            }
            return kahu;
        }

        private static int MerrStatusFleteKontabel(int idKonfigurimi, clsDatabaseShare dbShare)
        {
            int idStatusDok = 1;
            String vleraKushtit = clsAlternativaKushti.getAlternativa(idKonfigurimi, "GJK", dbShare);
            if (vleraKushtit == "Direkt")
                idStatusDok = 1;
            else
                if (vleraKushtit == "Indirekt")
                idStatusDok = 0;
            return idStatusDok;

        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Kontrollon totalet te nese jane te kuadruar dhe update vleftenKontabel te kokes
        /// </summary>
        /// <returns>clsMesazh</returns>
        private clsMesazh CheckTotalet(int shifraPasPresjes, bool kontMeMinus)
        {
            double shumadebi = 0;
            double shumakredi = 0;
            foreach (var rreshTrupi in _oColTrupi)
            {
                if (!kontMeMinus)
                {
                    if (rreshTrupi.VleftaDebiMonBazeTrupiFleteKontabel < 0)
                    {
                        rreshTrupi.VleftaKrediMonBazeTrupiFleteKontabel += Math.Abs(rreshTrupi.VleftaDebiMonBazeTrupiFleteKontabel);
                        rreshTrupi.VleftaKrediTrupiFleteKontabel += Math.Abs(rreshTrupi.VleftaDebiTrupiFleteKontabel);
                        rreshTrupi.VleftaDebiTrupiFleteKontabel = 0;
                        rreshTrupi.VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    }
                    if (rreshTrupi.VleftaKrediMonBazeTrupiFleteKontabel < 0)
                    {
                        rreshTrupi.VleftaDebiMonBazeTrupiFleteKontabel += Math.Abs(rreshTrupi.VleftaKrediMonBazeTrupiFleteKontabel);
                        rreshTrupi.VleftaDebiTrupiFleteKontabel += Math.Abs(rreshTrupi.VleftaKrediTrupiFleteKontabel);
                        rreshTrupi.VleftaKrediMonBazeTrupiFleteKontabel = 0;
                        rreshTrupi.VleftaKrediTrupiFleteKontabel = 0;
                    }
                }

                shumadebi += rreshTrupi.VleftaDebiMonBazeTrupiFleteKontabel;
                shumakredi += rreshTrupi.VleftaKrediMonBazeTrupiFleteKontabel;
            }
            var maxSingleErr = double.Parse(clsFunksione.krijoNumer(shifraPasPresjes, "0") + "5", CultureInfo.InvariantCulture);
            if (Math.Abs(Math.Round((shumadebi - shumakredi), shifraPasPresjes + 1)) > maxSingleErr)
                return new clsMesazh(false, MessagesResource.Messages["msgVeprimiNukEshteIKuadruar"]);
            _vleftaFleteKontabel = shumadebi;
            return new clsMesazh(true, MessagesResource.Messages["msgVeprimiEshteIKuadruar"]);
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsKokaFleteKontabel, trupin e tij dhe llogarite e kontabilitetit ne DB.
        /// </summary>
        /// <param name="idkokafletekontabel"></param>
        /// <param name="nrdokumentikokafletekontabel"></param>
        /// <param name="datedokumentifletekontabel"></param>
        /// <param name="dateregjistrimifletekontabel"></param>
        /// <param name="pershkrimfletekontabel"></param>
        /// <param name="idgrupkontabilizimi"></param>
        /// <param name="idnderviti"></param>
        /// <param name="kont"></param>
        /// <param name="vleftafletekontabel"></param>
        /// <param name="idperdoruesi"></param>
        /// <param name="idllojdok"></param>
        /// <param name="iddoknga"></param>
        /// <param name="idstatusdok"></param>
        /// <param name="idkonfigambjente"></param>
        /// <param name="idkonfiggjenerues"></param>
        /// <param name="idkategoria"></param>
        /// <param name="idnivel"></param>
        /// <param name="idnivelgjenerues"></param>
        /// <param name="idgjenerues"></param>
        /// <param name="oColTrupi"></param>
        /// <param name="dbKont"></param>
        /// <param name="idPeriudha"></param>
        /// <param name="idndermarje"></param>
        /// <param name="kokaqender"></param>
        /// <returns></returns>
        private static clsMesazh RuajFleteKontabel(out int idkokafletekontabel, string nrdokumentikokafletekontabel, DateTime datedokumentifletekontabel, DateTime dateregjistrimifletekontabel, string pershkrimfletekontabel, int idgrupkontabilizimi, int idnderviti, bool kont, double vleftafletekontabel, int idperdoruesi, int idllojdok, int iddoknga, int idstatusdok, int idkonfigambjente, int idkonfiggjenerues, int idkategoria, int idnivel, int idnivelgjenerues, int idgjenerues, colTrupatFletetKontabel oColTrupi, clsDatabaseKontabilitet dbKont, int idPeriudha, int idndermarje, DbQendraKosto.clsKokaQendraKosto kokaqender)
        {
            idkokafletekontabel = 0;
            var mesazh = new clsMesazh();
            var kodi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(idkonfigambjente);
            if (dbKont.ekzistonKokaFleteKontabel(nrdokumentikokafletekontabel, datedokumentifletekontabel, idnderviti, kodi))
                return new clsMesazh(false, MessagesResource.Messages["msgEkzistonDokumentiMeKeteNumerDheKeteDate"]);
            idkokafletekontabel = dbKont.ruajKokaFleteKontabel(idkokafletekontabel, nrdokumentikokafletekontabel, datedokumentifletekontabel, dateregjistrimifletekontabel, pershkrimfletekontabel, idgrupkontabilizimi, idnderviti, kont, vleftafletekontabel, idperdoruesi, idllojdok, iddoknga, idstatusdok, idkonfigambjente, idkonfiggjenerues, idkategoria, idnivel, idnivelgjenerues, idgjenerues, idPeriudha, idndermarje);
            if (idkokafletekontabel == 0)
                return new clsMesazh(false, MessagesResource.Messages["msgRuajtjaEKokesSeFletesKontabelNukUKrye"]);
            foreach (var o in oColTrupi)
            {//behet ruajtja e trupit te fleteve kontabel 
                o.IdKokaFleteKontabel = idkokafletekontabel;
                int idT;
                mesazh = dbKont.ruajTrupiFleteKontabel(out idT, o.IdKokaFleteKontabel, o.IdLlogari, o.PershkrimTrupiFleteKontabel, o.IdMonedha,
                    o.Kursi, o.VleftaDebiTrupiFleteKontabel, o.VleftaKrediTrupiFleteKontabel, o.VleftaDebiMonBazeTrupiFleteKontabel, o.VleftaKrediMonBazeTrupiFleteKontabel);
                if (!mesazh.Status)
                    return mesazh;
            }

            if (kokaqender != null && !string.IsNullOrEmpty(kokaqender.NrDok))
            {
                var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbKont);
                kokaqender.NrDok = nrdokumentikokafletekontabel;
                mesazh = kokaqender.Ruaj(dbqendra, idkokafletekontabel);
                if (!mesazh.Status)
                    return mesazh;
            }
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgAdministrimiRuajtjaPerfundoiSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Kontrollon periudhen kontabel
        /// </summary>
        /// <param name="idndermarje"></param>
        /// <param name="dbadmin"></param>
        /// <returns></returns>
        private clsMesazh CheckPeriudheKontabel(int idndermarje, clsDatabaseAdmin dbadmin)
        {
            var periudha = new clsPeriudhaKontabel(_idPeriudha, idndermarje, dbadmin);
            if (periudha.IdPeriudha == 0) //kjo ne rastin kur pas nje modifikimi te vitet, persh behet kycja(nga nje perdorues tjeter) e nje muaji, e cila shoqerohet me fshirjen me delete ne db, pra ndryshojne id e periudhave, edhe pse pas kycjes ruhet ne session periudha me id-te e reja nga perdoruesi tjeter, per perdoruesin qe kryen veprimin ne sesionin e tij ka ende vleren e vjeter prnd duhet mar dhe njehere nga db
                periudha = new clsPeriudhaKontabel(_dateDokumentiKokaFleteKontabel, idndermarje, dbadmin);
            if (periudha.Ekycur)
                return new clsMesazh(false, MessagesResource.Messages["msgNukKryeniVeprimeSePeriudhaEshteEKycur"]);
            if (periudha.FillimiPeriudha > _dateDokumentiKokaFleteKontabel || periudha.MbarimiPeriudha.AddDays(1) < _dateDokumentiKokaFleteKontabel.AddMilliseconds(1))
                return new clsMesazh(false, MessagesResource.Messages["msgDataEDokDuhetTePerfshihetNePeriudhenEZgjedhur"]);
            return new clsMesazh(true, MessagesResource.Messages["msgKontrolliPeriudhesKontabelUKaluaMeSukses"]);
        }

        private clsMesazh Kontrollo(clsDatabaseShare db)
        {
            var formatNrPerKonfig = new clsFormatiKonfig();
            if (_idKonfigGjenerues != 0)
                formatNrPerKonfig = new clsFormatiKonfig(_idKonfigGjenerues, db);
            else
                formatNrPerKonfig = new clsFormatiKonfig(_idKonfigAmbjente, db);

            var kontMeMinus = clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "KLVN", db);
            var dbadmin = new clsDatabaseAdmin(db);
            var nder = new clsNdermarrje(_idNdermarje, dbadmin);
            var formatMonedhe = new clsFormatKonfigTrup();
            formatMonedhe = formatNrPerKonfig.IdFormatKonfig > 0
                ? formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(nder.NdermarrjeMonedha)
                : new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            var mesazhTotali = CheckTotalet(formatMonedhe.ShifraPasPresjesZbritja, kontMeMinus == "Po");
            if (!mesazhTotali.Status)
                return mesazhTotali;
            if (_dateDokumentiKokaFleteKontabel.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, MessagesResource.Messages["msgZgjidhniNjeDateDokumenti"]);
            if (DateRegjistrimiKokaFleteKontabel.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            mesazhTotali = CheckPeriudheKontabel(_idNdermarje, dbadmin);
            if (!mesazhTotali.Status)
                return mesazhTotali;
            if (OColTrupi == null || OColTrupi.Count == 0)
                return new clsMesazh(false, MessagesResource.Messages["msgFletaKontabelNukMundTeJeteBosh"]);
            return new clsMesazh(true, MessagesResource.Messages["msgKontrolletEFletesKontabelUKaluanMeSukses"]);
        }

        private clsMesazh KontrolloFK(out bool kaNdryshimNrAuto, clsDatabaseKontabilitet db, IDictionary<string, object> hfregjistrime) =>
            KontrolloFK(out kaNdryshimNrAuto, db, hfregjistrime, false);

        private clsMesazh KontrolloFK(out bool kaNdryshimNrAuto, clsDatabaseKontabilitet db, IDictionary<string, object> hfregjistrime, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            var kodi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(_idKonfigAmbjente);
            if (_nrDokumentiKokaFleteKontabel == "")
                return new clsMesazh(false, MessagesResource.Messages["msgNumriIDokNukMundTeJeteBosh"]);

            if (modifikim) return new clsMesazh(true, MessagesResource.Messages["msgKontrolletUKaluanMeSukses"]);
            var mes = new clsMesazh();
            if (hfregjistrime != null)
            {
                mes = KontrolloNrAuto(out kaNdryshimNrAuto, db, hfregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (db.ekzistonKokaFleteKontabel(_nrDokumentiKokaFleteKontabel, _dateDokumentiKokaFleteKontabel, _idNdermarje, kodi))
                return new clsMesazh(false, MessagesResource.Messages["msgEkziston1RegjistrimMeTeNjejtinNrDokumenti"]);
            return kaNdryshimNrAuto ? mes : new clsMesazh(true, MessagesResource.Messages["msgKontrolletUKaluanMeSukses"]);
        }

        private clsMesazh KontrolloNrAuto(out bool kaNdryshimNumri, clsDatabaseKontabilitet db, IDictionary<string, object> hfregjistrime)
        {
            var dbadm = new clsDatabaseAdmin(db);
            var list = clsNrAutom.kontrollogjithenumrat(dbadm, hfregjistrime, _dateDokumentiKokaFleteKontabel);
            if (NrAuto.ktheVlerenEre(list, "NrDokumentiKokaFleteKontabel") != "")
                _nrDokumentiKokaFleteKontabel = NrAuto.ktheVlerenEre(list, "NrDokumentiKokaFleteKontabel");
            var mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, _dateDokumentiKokaFleteKontabel, _idPerdoruesi, _idNdermarje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Ruan nje objekt clsKokaFleteKontabel, trupin e tij dhe llogarite e kontabilitetit duke u nisur nga nje dokument tjeter i cili gjeneron kontabilitet.
        /// <param name="idkokafletekontabel">Id qe gjenerohet automatikisht</param>
        /// <param name="nrdokumentikokafletekontabel">Numri i dokumentit</param>
        /// <param name="datedokumentifletekontabel">Data e dokumentit</param>
        /// <param name="dateregjistrimifletekontabel">Data e regjistrimit</param>
        /// <param name="pershkrimfletekontabel">Pershkrimi</param>
        /// <param name="idgrupkontabilizimi">Grupi i kontabilizimit <seealso cref="DbCore.DbKontabiliteti.clsGrupKontabilizimi"/>"/></param>
        /// <param name="idskemakontabel">Skema kontabel <seealso cref="DbCore.DbKontabiliteti.clsSkemaKontabelKoka"/></param>
        /// <param name="idnderviti">Id lidhese ndermarrje-vit</param>
        /// <param name="kont">Kontabilizuar apo jo</param>
        /// <param name="vleftafletekontabel">Vlefta e fletes kontabel</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="idllojdok">Lloji i dokumentit</param>
        /// <param name="iddoknga">Id e dokumentit nga eshte gjeneruar fleta kontabel ne raste modifikimi dhe fshirje</param>
        /// <param name="idstatusdok">Satusi i dokumentit - Ruajtur, Draft</param>
        /// <param name="idkonfigambjente">id e konfigurimit te ambjentit</param>
        /// <param name="idkategoria">kategoria e dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <param name="idkonfiggjenerues"> id e konfigurimit te dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <param name="idgjenerues">id e dokumentit nga eshte gjeneruar fleta kontabel nga nje ambjent tjeter</param>
        /// <param name="idnivel"> id e nivelit </param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te funksioneve(nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        private static clsMesazh RuajFleteKontabelNgaDokTjeter(int idkokafletekontabel, string nrdokumentikokafletekontabel, DateTime datedokumentifletekontabel, DateTime dateregjistrimifletekontabel, string pershkrimfletekontabel, int idgrupkontabilizimi, /*int idskemakontabel, */int idnderviti, bool kont, double vleftafletekontabel, int idperdoruesi, int idllojdok, int iddoknga, int idstatusdok, int idkonfigambjente, int idkonfiggjenerues, int idkategoria, int idnivel, int idnivelgjenerues, int idgjenerues, colTrupatFletetKontabel oColTrupi, int idPeriudha, clsDatabaseKontabilitet dbKont, int idndermarje, DbQendraKosto.clsKokaQendraKosto kokaqender)
        {
            idkokafletekontabel = dbKont.ruajKokaFleteKontabel(idkokafletekontabel, nrdokumentikokafletekontabel, datedokumentifletekontabel, dateregjistrimifletekontabel, pershkrimfletekontabel, idgrupkontabilizimi, /*idskemakontabel,*/idnderviti, kont, vleftafletekontabel, idperdoruesi, idllojdok, iddoknga, idstatusdok, idkonfigambjente, idkonfiggjenerues, idkategoria, idnivel, idnivelgjenerues, idgjenerues, idPeriudha, idndermarje);
            if (idkokafletekontabel == 0)
                return new clsMesazh(false, MessagesResource.Messages["msgGabimGjateRuatjesSeKokesSeFletesKontabel"]);
            oColTrupi.VendosIdKokeNeTrup(idkokafletekontabel);
            var dtTrupi = oColTrupi.ToDataTable("IdTrupiFleteKontabel", "IdKokaFleteKontabel", "IdLlogari", "PershkrimTrupiFleteKontabel", "IdMonedha", "Kursi", "VleftaDebiTrupiFleteKontabel", "VleftaKrediTrupiFleteKontabel", "VleftaDebiMonBazeTrupiFleteKontabel", "VleftaKrediMonBazeTrupiFleteKontabel");
            var mesazh = dbKont.RuajTrupFleteKontabel(dtTrupi);
            if (string.IsNullOrEmpty(kokaqender.NrDok))
                return new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbKont);
            kokaqender.NrDok = nrdokumentikokafletekontabel;
            mesazh = kokaqender.Ruaj(dbqendra, idkokafletekontabel);
            return !mesazh.Status ? mesazh : new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
        }

        /// <summary>
        /// Ekzekuton prc_T_KOKAFLETEKONTABEL_upd per te modifikuar nje objekt clsKokaFleteKontabel ne DB.
        /// <param name="idkokafletekontabel">Id qe gjenerohet automatikisht</param>
        /// <param name="nrdokumentikokafletekontabel">Numri i dokumentit</param>
        /// <param name="datedokumentifletekontabel">Data e dokumentit</param>
        /// <param name="dateregjistrimifletekontabel">Data e regjistrimit</param>
        /// <param name="nrreferencefletekontabel">Numri i references</param>
        /// <param name="pershkrimfletekontabel">Pershkrimi</param>
        /// <param name="idgrupkontabilizimi">Grupi i kontabilizimit <seealso cref="DbCore.DbKontabiliteti.clsGrupKontabilizimi"/>"/></param>
        /// <param name="idskemakontabel">Skema kontabel <seealso cref="DbCore.DbKontabiliteti.clsSkemaKontabelKoka"/></param>
        /// <param name="idnderviti">Id lidhese ndermarrje-vit</param>
        /// <param name="kont">Kontabilizuar apo jo</param>
        /// <param name="vleftafletekontabel">Vlefta e fletes kontabel</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="idllojdok">Lloji i dokumentit</param>
        /// <param name="iddoknga">Id e dokumentit nga eshte gjeneruar fleta kontabel ne raste modifikimi dhe fshirje</param>
        /// <param name="idstatusdok">Satusi i dokumentit - Ruajtur, Draft</param>
        /// <param name="idkonfigambjente">id e konfigurimit te ambjentit</param>
        /// <param name="idkategoria">kategoria e dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <param name="idkonfiggjenerues"> id e konfigurimit te dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <param name="idgjenerues">id e dokumentit nga eshte gjeneruar fleta kontabel nga nje ambjent tjeter</param>
        /// <param name="idnivel"> id e nivelit </param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit nga eshte gjeneruar fleta kontabel</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (true nese veprimi eshte kryer me sukses)</returns>
        /// </summary>
        private static clsMesazh ModifikoFleteKontabel(int idkokafletekontabel, string nrdokumentikokafletekontabel, DateTime datedokumentifletekontabel, DateTime dateregjistrimifletekontabel, string pershkrimfletekontabel, int idgrupkontabilizimi, /*int idskemakontabel,*/int idnderviti, bool kont, double vleftafletekontabel, int idperdoruesi, int idllojdok, int iddoknga, int idstatusdok, int idkonfigambjente, int idkonfiggjenerues, int idkategoria, int idnivel, int idnivelgjenerues, int idgjenerues, colTrupatFletetKontabel oColTrupi, bool regjistrim, clsDatabaseKontabilitet dbKont, int idPeriudha, int idndermarje, DbQendraKosto.clsKokaQendraKosto kokaqendra, out int idre)
        {
            idre = 0;
            var fletaeksituese = new clsKokaFleteKontabel(idkokafletekontabel, dbKont);
            if (fletaeksituese._idKokaFleteKontabel == 0)
                return new clsMesazh(false, MessagesResource.Messages["msgFletaKontabelNukEkziston"]);
            
            if (string.IsNullOrEmpty(fletaeksituese.NrDukumentiKokaFleteKontabel) || fletaeksituese.IdStatusDokumenti == 2)
                return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarHapeniPerseri"]);
            fletaeksituese.IdStatusDokumenti = 2;
            var mesazh = dbKont.modifikoKokaFleteKontabelStatus(fletaeksituese.IdKokaFleteKontabel, fletaeksituese.IdStatusDokumenti);
            if (!mesazh.Status) return mesazh;
            mesazh = KaloNeHistorikKokaFleteKontabel(fletaeksituese._idKokaFleteKontabel, dbKont);
            if (!mesazh.Status) return mesazh;
            kokaqendra.IdStatusDok = 2;
            kokaqendra.IdPerdoruesi = idperdoruesi;
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbKont);
            mesazh = kokaqendra.Fshi(idperdoruesi, 2, dbqendra);
            if (!mesazh.Status) return mesazh;
            var stornim = clsAlternativaKushti.getAlternativa(idkonfigambjente, "F/MDK") == "Me Stornim";
            if (fletaeksituese.Kontabilizuar && stornim)
            {
                fletaeksituese.IdDokNga = fletaeksituese.IdKokaFleteKontabel;
                fletaeksituese.IdStatusDokumenti = 3;
                fletaeksituese.IdKokaFleteKontabel = dbKont.ruajKokaFleteKontabel(fletaeksituese.IdKokaFleteKontabel, fletaeksituese.NrDukumentiKokaFleteKontabel, fletaeksituese.DateDokumentiKokaFleteKontabel, fletaeksituese.DateRegjistrimiKokaFleteKontabel, fletaeksituese.PershkrimKokaFleteKontabel, fletaeksituese.IdGrupKontabilizimi, fletaeksituese.IdNderViti, fletaeksituese.Kontabilizuar, fletaeksituese.VleftaFleteKontabel, fletaeksituese.IdPerdoruesi, fletaeksituese.IdLlojDok, fletaeksituese.IdDokNga, fletaeksituese.IdStatusDokumenti, fletaeksituese.IdKonfigAmbjente, fletaeksituese.IdKonfigGjenerues, fletaeksituese.IdKategoria, fletaeksituese.IdNivel, fletaeksituese.IdNivelGjenerues, fletaeksituese.IdGjenerues, fletaeksituese._idPeriudha, fletaeksituese.IdNdermarje);

                if (fletaeksituese.IdKokaFleteKontabel == 0)
                    return new clsMesazh(false, MessagesResource.Messages["msgGabimGjateRuatjesSeKokesSeFletesKontabel"]);
                if (oColTrupi != null)
                {
                    foreach (var o in oColTrupi)
                    {
                        var debimon = o.VleftaDebiMonBazeTrupiFleteKontabel;
                        var debi = o.VleftaDebiTrupiFleteKontabel;
                        o.VleftaDebiMonBazeTrupiFleteKontabel = o.VleftaKrediMonBazeTrupiFleteKontabel;
                        o.VleftaDebiTrupiFleteKontabel = o.VleftaKrediTrupiFleteKontabel;
                        o.VleftaKrediMonBazeTrupiFleteKontabel = debimon;
                        o.VleftaKrediTrupiFleteKontabel = debi;
                        o.IdKokaFleteKontabel = fletaeksituese.IdKokaFleteKontabel;
                        int idT;
                        mesazh = dbKont.ruajTrupiFleteKontabel(out idT, o.IdKokaFleteKontabel, o.IdLlogari, o.PershkrimTrupiFleteKontabel, o.IdMonedha,
                            o.Kursi, o.VleftaDebiTrupiFleteKontabel, o.VleftaKrediTrupiFleteKontabel, o.VleftaDebiMonBazeTrupiFleteKontabel, o.VleftaKrediMonBazeTrupiFleteKontabel);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
            }
            if (regjistrim)
                return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            {
                iddoknga = idkokafletekontabel;
                idkokafletekontabel = dbKont.ruajKokaFleteKontabel(idkokafletekontabel, nrdokumentikokafletekontabel, datedokumentifletekontabel, dateregjistrimifletekontabel, pershkrimfletekontabel, idgrupkontabilizimi, /*idskemakontabel, */idnderviti, kont, vleftafletekontabel, idperdoruesi, idllojdok, iddoknga, idstatusdok, idkonfigambjente, idkonfiggjenerues, idkategoria, idnivel, idnivelgjenerues, idgjenerues, idPeriudha, idndermarje);
                idre = idkokafletekontabel;
                if (idkokafletekontabel == 0)
                    return new clsMesazh(false, MessagesResource.Messages["msgGabimGjateRuatjesSeKokesSeFletesKontabel"]);
                foreach (var o in oColTrupi)
                {   //behet ruajtja e trupit te fleteve kontabel 
                    o.IdKokaFleteKontabel = idkokafletekontabel;
                    int idF;
                    mesazh = dbKont.ruajTrupiFleteKontabel(out idF, o.IdKokaFleteKontabel, o.IdLlogari, o.PershkrimTrupiFleteKontabel, o.IdMonedha,
                        o.Kursi, o.VleftaDebiTrupiFleteKontabel, o.VleftaKrediTrupiFleteKontabel, o.VleftaDebiMonBazeTrupiFleteKontabel, o.VleftaKrediMonBazeTrupiFleteKontabel);
                    if (!mesazh.Status)
                        return mesazh;
                }
                if (string.IsNullOrEmpty(kokaqendra.NrDok))
                    return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                kokaqendra.IdDokNga = kokaqendra.IdKoka;
                kokaqendra.IdStatusDok = 1;
                mesazh = kokaqendra.Ruaj(dbqendra, idkokafletekontabel);
                if (!mesazh.Status)
                    return mesazh;
            }
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }


        /// <summary>
        /// Modifikon koken dhe trupin e fletes kontabel ne tabelat perkatese ne databaze.
        /// </summary>
        /// <param name="regjistrim"></param>
        /// <param name="lidhur"></param>
        /// <param name="hfregjistrime"></param>
        /// <returns></returns>
        private clsMesazh Modifiko(bool regjistrim, bool lidhur, IDictionary<string, object> hfregjistrime)
        {
            clsMesazh uModifikua;
            var data = new clsDatabaseKontabilitet();
            if (lidhur == false)
            {
                data.beginTransaksion();
                var kokaFleKont = new clsKokaFleteKontabel(IdKokaFleteKontabel);

                uModifikua = KontrolloFk(data, hfregjistrime, kokaFleKont);
                if (!uModifikua.Status)
                    return uModifikua;
                uModifikua = ModifikoFleteKontabel(regjistrim, data);
                if (uModifikua.Status)
                    data.commitTransaksion();
                else
                    data.rollbackTransaksion();
                return uModifikua;
            }
            uModifikua = kontrolloFk(data, hfregjistrime);
            if (!uModifikua.Status)
                return uModifikua;
            uModifikua = data.modifikoKokaFleteKontabelLidh(IdKokaFleteKontabel, NrDukumentiKokaFleteKontabel, DateDokumentiKokaFleteKontabel, DateRegjistrimiKokaFleteKontabel,
                NrReferenceKokaFleteKontabel, PershkrimKokaFleteKontabel, IdGrupKontabilizimi,
                IdNderViti, Kontabilizuar, VleftaFleteKontabel,
                IdPerdoruesi, IdLlojDok, IdStatusDokumenti, IdNdermarje);
            data.Dispose();
            return uModifikua;
        }

        private clsMesazh KontrolloFk(clsDatabaseKontabilitet db, IDictionary<string, object> hfregjistrime, clsKokaFleteKontabel kokaEkzistuese)
        {
            bool kaNdryshimNrAuto;
            return KontrolloFk(out kaNdryshimNrAuto, db, hfregjistrime, true, kokaEkzistuese);
        }

        private clsMesazh kontrolloFk(clsDatabaseKontabilitet db, IDictionary<string, object> hfregjistrime)
        {
            bool kaNdryshimNrAuto;
            return KontrolloFK(out kaNdryshimNrAuto, db, hfregjistrime, true);
        }

        private clsMesazh KontrolloFk(out bool kaNdryshimNrAuto, clsDatabaseKontabilitet db, IDictionary<string, object> hfregjistrime, bool modifikim, clsKokaFleteKontabel kokaEkzistuese)
        {
            kaNdryshimNrAuto = false;
            if (kokaEkzistuese == null) return KontrolloFK(out kaNdryshimNrAuto, db, hfregjistrime, modifikim);
            if (!string.IsNullOrEmpty(kokaEkzistuese.NrDukumentiKokaFleteKontabel) && kokaEkzistuese.IdStatusDokumenti != 2)
                return KontrolloFK(out kaNdryshimNrAuto, db, hfregjistrime, modifikim);
            db.rollbackTransaksion();
            return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarJulutemRihapeni"]);
        }

        private static colTrupatFletetKontabel Krijotrup(int idndermarje, DateTime datefundviti, int idllogmbyllje, clsDatabaseKontabilitet db, clsDatabaseAdmin dbadmin, out DbQendraKosto.colObjektivaKosto objektivat, out List<double> vleratobjektiva, out List<double> vleratobjektivamonbaze, out List<int> idllogobj)
        {
            objektivat = new DbQendraKosto.colObjektivaKosto();
            vleratobjektiva = new List<double>();
            vleratobjektivamonbaze = new List<double>();
            idllogobj = new List<int>();
            var colTrupat = new colTrupatFletetKontabel();
            var colLlog = new colLlogarite(idndermarje, db);
            double shumadebi = 0;
            double shumakredi = 0;
            double shumadebimon = 0;
            double shumakredimon = 0;
            double kursi;
            var dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db);
            var kurset = new colKurset();
            foreach (var llog in colLlog)
            {
                kurset.mbushKursetFunditMonedhesSipasLlojit(llog.IdMonedha, 1, dbadmin);
                kursi = kurset.Count > 0 ? kurset[0].VleraKursi : 1;
                var gjendjamonbaze = clsLlogari.merrGjendjeLlogari(llog.IdLlogari, datefundviti, db);
                var gjendja = clsLlogari.merrGjendjeLlogariMonHuaj(llog.IdLlogari, datefundviti, db);
                double gjdb = 0;
                double gjkr = 0;
                double gjdbmb = 0;
                double gjkrmb = 0;
                if (gjendjamonbaze > 0)
                {
                    gjdbmb = 0;
                    gjdb = 0;
                    gjkrmb = gjendjamonbaze;
                    gjkr = gjendja;
                }
                else if (gjendjamonbaze < 0)
                {
                    gjdbmb = Math.Abs(gjendjamonbaze);
                    gjdb = Math.Abs(gjendja);
                    gjkrmb = 0;
                    gjkr = 0;
                }

                var trupi = new clsTrupiFleteKontabel(0, 0, llog.IdLlogari, "Mbyllje Viti", llog.IdMonedha, kursi, gjdb, gjkr, llog.KodiMonedha, "", gjdbmb, gjkrmb);
                if (llog.IdObjektivaKosto != 0 && llog.IdObjektivaKosto != -1)
                {
                    var objekt = new DbQendraKosto.clsObjektivaKosto(llog.IdObjektivaKosto, dbqendra);
                    ShtoTeDhenaPerObjektivat(datefundviti, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, llog, trupi, objekt);
                }
                if (trupi.VleftaDebiMonBazeTrupiFleteKontabel == 0 && trupi.VleftaKrediMonBazeTrupiFleteKontabel == 0) continue;
                colTrupat.Add(trupi);
                shumakredi += trupi.VleftaDebiTrupiFleteKontabel;
                shumakredimon += trupi.VleftaDebiMonBazeTrupiFleteKontabel;
                shumadebi += trupi.VleftaKrediTrupiFleteKontabel;
                shumadebimon += trupi.VleftaKrediMonBazeTrupiFleteKontabel;
            }
            var llogmbyllje = new clsLlogari(idllogmbyllje);
            kurset.mbushKursetFunditMonedhesSipasLlojit(llogmbyllje.IdMonedha, 1, dbadmin);
            kursi = kurset.Count > 0 ? kurset[0].VleraKursi : 1;
            if (shumadebimon == 0 && shumakredimon == 0) return colTrupat;
            {
                double debiviti = 0;
                double krediviti = 0;
                double debivitimon;
                double kredivitimon;

                if (shumadebimon - shumakredimon > 0)
                {
                    debivitimon = shumadebimon - shumakredimon;
                    kredivitimon = 0;
                }
                else
                {
                    debivitimon = 0;
                    kredivitimon = Math.Abs(shumadebimon - shumakredimon);
                }
                if (shumadebimon - shumakredimon == 0) return colTrupat;
                var trupimbyll = new clsTrupiFleteKontabel(0, 0, llogmbyllje.IdLlogari, "Mbyllje Viti", llogmbyllje.IdMonedha, kursi, debivitimon, kredivitimon, llogmbyllje.KodiMonedha, "", debivitimon, kredivitimon);
                if (llogmbyllje.IdObjektivaKosto != 0 && llogmbyllje.IdObjektivaKosto != -1)
                {
                    var objekt = new DbQendraKosto.clsObjektivaKosto(llogmbyllje.IdObjektivaKosto, dbqendra);
                    ShtoTeDhenaPerObjektivat(datefundviti, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, llogmbyllje, trupimbyll, objekt);
                }
                colTrupat.Add(trupimbyll);
            }
            return colTrupat;
        }

        private clsMesazh StornimFleteKontabel(int idkokafletekontabel, string nrdokumentikokafletekontabel, DateTime datedokumentifletekontabel, DateTime dateregjistrimifletekontabel, string pershkrimfletekontabel, int idgrupkontabilizimi, int idnderviti, bool kont, double vleftafletekontabel, int idperdoruesi, int idllojdok, int iddoknga, int idstatusdok, int idkonfigambjente, int idkonfiggjenerues, int idkategoria, int idnivel, int idnivelgjenerues, int idgjenerues, colTrupatFletetKontabel oColTrup, int idPeriudha, int idndermarje, clsDatabaseKontabilitet dbKont)
        {
            var mesazh = new clsMesazh(true);
            iddoknga = idkokafletekontabel;
            idstatusdok = 3;
            idkokafletekontabel = dbKont.ruajKokaFleteKontabel(idkokafletekontabel, nrdokumentikokafletekontabel, datedokumentifletekontabel, dateregjistrimifletekontabel, pershkrimfletekontabel, idgrupkontabilizimi, idnderviti, kont, vleftafletekontabel, idperdoruesi, idllojdok, iddoknga, idstatusdok, idkonfigambjente, idkonfiggjenerues, idkategoria, idnivel, idnivelgjenerues, idgjenerues, idPeriudha, idndermarje);
            var statusVeprimi = idkokafletekontabel != 0;
            if (!statusVeprimi) return mesazh;
            foreach (var o in oColTrup)
            {
                if (mesazh.Status)
                {
                    var debimon = o.VleftaDebiMonBazeTrupiFleteKontabel;
                    var debi = o.VleftaDebiTrupiFleteKontabel;
                    o.VleftaDebiMonBazeTrupiFleteKontabel = o.VleftaKrediMonBazeTrupiFleteKontabel;
                    o.VleftaDebiTrupiFleteKontabel = o.VleftaKrediTrupiFleteKontabel;
                    o.VleftaKrediMonBazeTrupiFleteKontabel = debimon;
                    o.VleftaKrediTrupiFleteKontabel = debi;
                    o.IdKokaFleteKontabel = idkokafletekontabel;
                    int idF;
                    mesazh = dbKont.ruajTrupiFleteKontabel(out idF, o.IdKokaFleteKontabel, o.IdLlogari, o.PershkrimTrupiFleteKontabel, o.IdMonedha, o.Kursi, o.VleftaDebiTrupiFleteKontabel, o.VleftaKrediTrupiFleteKontabel, o.VleftaDebiMonBazeTrupiFleteKontabel, o.VleftaKrediMonBazeTrupiFleteKontabel);
                }
                else
                    return mesazh;
            }
            return mesazh.Status ? new clsMesazh(true, "Stornimi perfundoi me sukses!") : mesazh;
        }

        private void MerrFleteKontabelSipasLlojit(int idllojdok, int idnderviti, clsDatabaseKontabilitet db) =>
            MbushKokaFleteKontabel(db.ktheKokaFleteKontabelSipasLlojDok(idllojdok, idnderviti));

        private static clsMesazh KaloNeHistorikKokaFleteKontabel(int idkoka, clsDatabaseKontabilitet db) => db.kaloNeHistorikKokaFleteKontabel(idkoka);

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koken e fleteve kontabel nga databaza
        /// </summary>
        /// <param name="dbDataRowKokaFleteKont">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool MbushKokaFleteKontabel(DataRow dbDataRowKokaFleteKont)
        {
            if (dbDataRowKokaFleteKont == null) return false;
            try
            {
                int.TryParse(dbDataRowKokaFleteKont["IDKOKAFLETEKONTABEL"].ToString(), out _idKokaFleteKontabel);
                _nrDokumentiKokaFleteKontabel = dbDataRowKokaFleteKont["NRKOKAFLETEKONTABEL"].ToString();
                DateTime.TryParse(dbDataRowKokaFleteKont["DATEDOKUMENTIKOKAFLETEKONTABEL"].ToString(), out _dateDokumentiKokaFleteKontabel);
                DateTime.TryParse(dbDataRowKokaFleteKont["DATEREGJISTRIMIKOKAFLETEKONTABEL"].ToString(), out _dateRegjistrimiKokaFleteKontabel);
                _nrReferenceKokaFleteKontabel = dbDataRowKokaFleteKont["NRREFERENCEKOKAFLETEKONTABEL"].ToString();
                _pershkrimKokaFleteKontabel = dbDataRowKokaFleteKont["PERSHKRIMKOKAFLETEKONTABEL"].ToString();
                if (dbDataRowKokaFleteKont["IDGRUPKONTABILIZIMI"].ToString() != "0")
                    int.TryParse(dbDataRowKokaFleteKont["IDGRUPKONTABILIZIMI"].ToString(), out _idGrupKontabilizimi);
                if (dbDataRowKokaFleteKont["IDSKEMAKONTABEL"].ToString() != "0")
                    int.TryParse(dbDataRowKokaFleteKont["IDSKEMAKONTABEL"].ToString(), out _idSkemaKontabel);
                int.TryParse(dbDataRowKokaFleteKont["IDNDERVITI"].ToString(), out _idNderViti);
                bool.TryParse(dbDataRowKokaFleteKont["KONTABILIZUAR"].ToString(), out _kontabilizuar);
                double.TryParse(dbDataRowKokaFleteKont["VLEFTAFLETEKONTABEL"].ToString(), out _vleftaFleteKontabel);
                int.TryParse(dbDataRowKokaFleteKont["IDPERDORUESI"].ToString(), out _idPerdoruesi);
                int.TryParse(dbDataRowKokaFleteKont["IDLLOJDOK"].ToString(), out _idLlojDok);
                int.TryParse(dbDataRowKokaFleteKont["IDDOKNGA"].ToString(), out _idDokNga);
                int.TryParse(dbDataRowKokaFleteKont["IDSTATUSDOK"].ToString(), out _idStatusDokumenti);
                int.TryParse(dbDataRowKokaFleteKont["IDKONFIGAMBJENTI"].ToString(), out _idKonfigAmbjente);
                int.TryParse(dbDataRowKokaFleteKont["IDKONFIGGJENERUES"].ToString(), out _idKonfigGjenerues);
                int.TryParse(dbDataRowKokaFleteKont["IDKATEGORIA"].ToString(), out _idKategoria);
                int.TryParse(dbDataRowKokaFleteKont["IDNIVEL"].ToString(), out _idNivel);
                int.TryParse(dbDataRowKokaFleteKont["IDNIVELGJENERUES"].ToString(), out _idNivelGjenerues);
                int.TryParse(dbDataRowKokaFleteKont["IDGJENERUES"].ToString(), out _idGjenerues);
                DateTime.TryParse(dbDataRowKokaFleteKont["DTKRIJIMI"].ToString(), out _dtKrijimi);
                DateTime.TryParse(dbDataRowKokaFleteKont["DTMODIFIKIMI"].ToString(), out _dtModifikimi);
                int.TryParse(dbDataRowKokaFleteKont["IDPERIUDHKONTABEL"].ToString(), out _idPeriudha);
                int.TryParse(dbDataRowKokaFleteKont["IDNDERMARJE"].ToString(), out _idNdermarje);
                _kokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se kokes flete kontabel nga db-ja");
            }
        }

        #endregion
    }
}