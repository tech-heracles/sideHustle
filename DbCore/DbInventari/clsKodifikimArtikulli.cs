using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbKontabiliteti;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  kodifikimet e artikujve
    ///  (Te dhenat  merren nga tabela : T_KODIFIKIMARTIKULLI)
    /// </summary>
    public class clsKodifikimArtikulli
    {
        #region Atributet

        private int idKodifikimi;
        private string kodKodifikimi;
        private string pershkrimKodifikimi;
        private int idPrindi;
        private int nivelKodifikimi;
        private int idPerdoruesi;
        //private int idNderViti;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int llojKodifikimi;
        private string kodPrindi;
        private string kodLloji;
        private int idSkemaKontabel;
        private int idLlogariVlere;
        private int idLlogariShitje;
        private int idLlogariInventari;
        private int idLlogariShpenzimi;
        private int idLlogariNeProces;
        private int idLlogariAmortizimi;
        private int idFormatiSerial;
        private int idLlogariPakesim;
        private bool llojArtikulli;
        private string nrLlogPakesimi;
        private DbAsete.colGrupNormaAmortizimi colNorma;
        private DataRow rreshti;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKodifikimi
        {
            get { return idKodifikimi; }
            set { idKodifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e kodifikimit
        /// </summary>
        public String KodKodifikimi
        {
            get { return kodKodifikimi; }
            set { kodKodifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// lloji i artikullit  false afatshkurter true aqt
        /// </summary>
        public bool LlojArtikulli
        {
            get
            {
                return llojArtikulli;
            }
            set
            {
                llojArtikulli = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne pershkrimin e kodifikimit.
        /// </summary>
        public String PershkrimKodifikimi
        {
            get { return pershkrimKodifikimi; }
            set { pershkrimKodifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit.
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}

        /// <summary>
        /// Kthen/Vendos ID-ne e prindit.
        /// </summary>
        public int IdPrindi
        {
            get { return idPrindi; }
            set { idPrindi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nivelin e kodifikimit.
        /// </summary>
        public int NivelKodifikimi
        {
            get { return nivelKodifikimi; }
            set { nivelKodifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        public int LlojKodifikimi
        {
            get { return llojKodifikimi; }
            set { llojKodifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se skemes kontabel qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdSkemaKontabel
        {
            get { return idSkemaKontabel; }
            set { idSkemaKontabel = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise se vleres kontabel qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdLlogariVlere
        {
            get { return idLlogariVlere; }
            set { idLlogariVlere = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise se shitjes qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdLlogariShitje
        {
            get { return idLlogariShitje; }
            set { idLlogariShitje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise se inventarit qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdLlogariInventari
        {
            get { return idLlogariInventari; }
            set { idLlogariInventari = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise se shpenzimeve qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdLlogariShpenzimi
        {
            get { return idLlogariShpenzimi; }
            set { idLlogariShpenzimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise ne proces qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdLlogariNeProces
        {
            get { return idLlogariNeProces; }
            set { idLlogariNeProces = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise se amortizimit qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdLlogariAmortizimi
        {
            get { return idLlogariAmortizimi; }
            set { idLlogariAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se formatit te serialilt qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdFormatiSerial
        {
            get { return idFormatiSerial; }
            set { idFormatiSerial = value; }
        }
        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise se pakesimit qe do te perdoret per grupin e artikujve afatgjate.
        /// </summary>
        public int IdLlogariPakesimi
        {
            get { return idLlogariPakesim; }
            set { idLlogariPakesim = value; }
        }
        public string NrLlogPakesimi
        {
            get { return nrLlogPakesimi; }
            set { nrLlogPakesimi = value; }
        }


        public DbAsete.colGrupNormaAmortizimi ColNorma
        {
            get
            {
                return colNorma;
            }
            set
            {
                colNorma = value;
            }
        }

        public IDictionary<string, object> HfArkiva { get; set; }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idKodifikimi"> id ritese e kodifikimit</param>
        /// <param name="kodKodifikimi">kodi i kodifikimit</param>
        /// <param name="pershkrimKodifikimi">pershkrimi i kodifikimit</param>
        /// <param name="idPrindi">id e prindit</param>
        /// <param name="nivelKodifikimi"> niveli i kodifikimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti">id ndermarje viti</param>
        /// <param name="idNdermarje">id ndermarje</param>
        public clsKodifikimArtikulli(int idKodifikimi, string kodKodifikimi, String pershkrimKodifikimi, int idPrindi, int nivelKodifikimi,
            int idPerdoruesi, int idNdermarje, int idStatusDok, int llojKodifikimi)
        {
            this.idKodifikimi = idKodifikimi;
            this.kodKodifikimi = kodKodifikimi;
            this.pershkrimKodifikimi = pershkrimKodifikimi;
            this.idPrindi = idPrindi;
            this.nivelKodifikimi = nivelKodifikimi;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            this.llojKodifikimi = llojKodifikimi;
            this.colNorma = new DbAsete.colGrupNormaAmortizimi();
           
        }

        /// <summary>
        /// ASETE:
        /// Konstruktor per krijimin e grupeve per asete te cilat kane dhe parametra shtese nga grupet normale.
        /// </summary>
        /// <param name="idKodifikimi">Id rritese e kodifikimit</param>
        /// <param name="kodKodifikimi">Kodi i grupit</param>
        /// <param name="pershkrimKodifikimi">Pershkrimi i grupit</param>
        /// <param name="idPrindi">Id e prindit te grupit, pra nese grupi aktual eshte nengrup</param>
        /// <param name="nivelKodifikimi">Niveli i kodifikimit</param>
        /// <param name="idPerdoruesi">Id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">Id e ndermarrjes ku eshte grupi</param>
        /// <param name="idStatusDok">Id e gjendjes se grupit, nese eshte i fshire, modifikuar ose i ruajtur </param>
        /// <param name="llojKodifikimi">Lloji i grupit</param>
        /// <param name="idSkemaKontabel">Id e skemes kontabel qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariVlere">Id e llogarise se vleres qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariShitje">Id e llogarise se shitjes qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariInventari">Id e llogarise se inventarit qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariShpenzimi">Id e llogarise se shpenzimeve qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariNeProces">Id e llogarise ne proces qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idLlogariAmortizimi">Id e llogarise se amortizimit qe do te perdoret per grupin e artikujve afatgjate</param>
        /// <param name="idFormatiSerial">Id e formatit te serialit qe do te perdoren per artikujt afatgjate te ketij grupi</param>
        public clsKodifikimArtikulli(int idKodifikimi, string kodKodifikimi, String pershkrimKodifikimi, int idPrindi, int nivelKodifikimi, int idPerdoruesi, int idNdermarje, int idStatusDok, int llojKodifikimi, int idSkemaKontabel, int idLlogariVlere, int idLlogariShitje, int idLlogariInventari, int idLlogariShpenzimi, int idLlogariNeProces, int idLlogariAmortizimi, int idFormatiSerial, bool llojartikulli,int  idllogpakesimi,string nrllogpakesimi)
        {
            this.idKodifikimi = idKodifikimi;
            this.kodKodifikimi = kodKodifikimi;
            this.pershkrimKodifikimi = pershkrimKodifikimi;
            this.idPrindi = idPrindi;
            this.nivelKodifikimi = nivelKodifikimi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            this.llojKodifikimi = llojKodifikimi;
            this.idSkemaKontabel = idSkemaKontabel;
            this.idLlogariVlere = idLlogariVlere;
            this.idLlogariShitje = idLlogariShitje;
            this.idLlogariInventari = idLlogariInventari;
            this.idLlogariShpenzimi = idLlogariShpenzimi;
            this.idLlogariNeProces = idLlogariNeProces;
            this.idLlogariAmortizimi = idLlogariAmortizimi;
            this.idFormatiSerial = idFormatiSerial;
            this.llojArtikulli = llojartikulli;
            this.idLlogariPakesim = idllogpakesimi;
            this.nrLlogPakesimi = nrllogpakesimi;
            this.colNorma = new DbAsete.colGrupNormaAmortizimi();
            //this.hfArkiva = hfArkiva;
        }

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="kodKodifikimi"> kodi i kodifikimit</param>
        /// <param name="pershkrimKodifikimi"> pershkrimi i kodifikimit</param>
        /// <param name="idPrindi">id e prindit</param>
        /// <param name="nivelKodifikimi"> niveli i kodifikimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti">id e ndermarje viti</param>
        /// <param name="idNdermarje"> id e ndermarjes</param>
        public clsKodifikimArtikulli(string kodKodifikimi, String pershkrimKodifikimi, int idPrindi, int nivelKodifikimi, int idPerdoruesi, int idNdermarje, int idstatusdok, int llojKodifikimi)
        {
            this.kodKodifikimi = kodKodifikimi;
            this.pershkrimKodifikimi = pershkrimKodifikimi;
            this.idPrindi = idPrindi;
            this.nivelKodifikimi = nivelKodifikimi;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idstatusdok;
            this.llojKodifikimi = llojKodifikimi;
            this.colNorma = new DbAsete.colGrupNormaAmortizimi();
        }

        public clsKodifikimArtikulli(int id, string kodi, string pershkrimi, int idprindi, string kodprindi, int niveli, int idperdoruesi, int idndermarje, int lloj, string llojkodifikimi, int idSkemaKontabel, int idLlogariVlere, int idLlogariShitje, int idLlogariInventari, int idLlogariShpenzimi, int idLlogariNeProces, int idLlogariAmortizimi, int idFormatiSerial, bool llojartikulli, DbAsete.colGrupNormaAmortizimi colnorma, bool shtim, int idllogpak, string nrllogpakesimi, IDictionary<string, object> hfArkiva)
        {

            this.idStatusDok = 1;
            this.idKodifikimi = id;
            this.kodKodifikimi = kodi;
            this.pershkrimKodifikimi = pershkrimi;
            this.idPrindi = idprindi;
            this.nivelKodifikimi = niveli;
            this.idPerdoruesi = idperdoruesi;
            this.idNdermarje = idndermarje;
            this.llojKodifikimi = lloj;
            this.kodPrindi = kodprindi;
            this.kodLloji = llojkodifikimi;
            this.idSkemaKontabel = idSkemaKontabel;
            this.idLlogariVlere = idLlogariVlere;
            this.idLlogariShitje = idLlogariShitje;
            this.idLlogariInventari = idLlogariInventari;
            this.idLlogariShpenzimi = idLlogariShpenzimi;
            this.idLlogariNeProces = idLlogariNeProces;
            this.idLlogariAmortizimi = idLlogariAmortizimi;
            this.idFormatiSerial = idFormatiSerial;
            this.llojArtikulli = llojartikulli;
            this.idLlogariPakesim = idllogpak;
            this.nrLlogPakesimi = nrllogpakesimi;
            this.HfArkiva = hfArkiva;
            this.colNorma = colnorma;
            clsMesazh mesazh = this.kontrolloKodifikimArtikull(shtim);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idkodifikimi">id e kodifikimit</param>
        public clsKodifikimArtikulli(int idkodifikimi)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            mbushKodifikimArtikulli(dbKodifikimArtikujsh.merrKodifikimArtikulli(idkodifikimi));
            dbKodifikimArtikujsh.Dispose();
        }

        public clsKodifikimArtikulli(int idkodifikimi, clsDatabaseInventari dbKodifikimArtikujsh)
        {
            mbushKodifikimArtikulli(dbKodifikimArtikujsh.TransCache.getKodifikimArtikulli(idkodifikimi, dbKodifikimArtikujsh));
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKodifikimArtikulli()
        {
            this.colNorma = new DbAsete.colGrupNormaAmortizimi();

        }

        public clsKodifikimArtikulli(string kodkodifikimi, int idndermarje, int llojkod, bool llojartikulli)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            mbushKodifikimArtikulli(dbKodifikimArtikujsh.merrKodifikimArtikulliKodLloj(kodkodifikimi, idndermarje, llojkod,llojartikulli));
            dbKodifikimArtikujsh.Dispose();
        }
        public clsKodifikimArtikulli(string kodkodifikimi, int idndermarje, int llojkod)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            mbushKodifikimArtikulli(dbKodifikimArtikujsh.merrKodifikimArtikulliKod(kodkodifikimi, idndermarje, llojkod));
            dbKodifikimArtikujsh.Dispose();
        }

        public clsKodifikimArtikulli(string kodkodifikimi, int idndermarje, int llojkod,bool llojartikulli, clsDatabaseInventari dbKodifikimArtikujsh)
        {
            mbushKodifikimArtikulli(dbKodifikimArtikujsh.merrKodifikimArtikulliKodLloj(kodkodifikimi, idndermarje, llojkod,llojartikulli));
        }

        public clsKodifikimArtikulli(DataRow rreshti)
        {
            
            mbushKodifikimArtikulli(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsKodifikimArtikulli krijoKodifikimPerImport(string kodi, string pershkrimi, string prindi, string lloji, int idndermarje, int idperdoruesi, bool shtim, ResourceManager rm, CultureInfo ci)
        {
            try
            {
                switch (lloji) {
                    case "Grupimi 1":
                        llojKodifikimi = 1;
                        break;
                    case "Grupimi 2":
                        llojKodifikimi = 2;
                        break;
                    case "Grupimi 3":
                        llojKodifikimi = 3;
                        break;
                    default:
                        llojKodifikimi = 0;
                        break; 
                }

                clsKodifikimArtikulli pri = new clsKodifikimArtikulli(prindi, idndermarje, llojKodifikimi,false);
                idPrindi = pri.IdKodifikimi;
                nivelKodifikimi = pri.NivelKodifikimi + 1;
                return new clsKodifikimArtikulli(0, kodi, pershkrimi, idPrindi, prindi, nivelKodifikimi, idperdoruesi, idndermarje, llojKodifikimi, lloji, 0, 0, 0, 0, 0, 0, 0, 0, false, new DbAsete.colGrupNormaAmortizimi(), shtim, 0, "", null);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private clsMesazh kontrolloKodifikimArtikull(bool shtim)
        {
            if (kodKodifikimi == "")
                return new clsMesazh(false, "Plotesoni kodin e grupit!");
            clsMesazh kontrollkodKodifikimi = clsFunksione.kontrolloKaraktereMeMesazh(kodKodifikimi, FusheKontrolli.Kodi, false);
            if (!kontrollkodKodifikimi.Status)
                return kontrollkodKodifikimi;

            if (pershkrimKodifikimi == "")
                return new clsMesazh(false, "Plotesoni pershkrimin e grupit!");
            clsMesazh kontrollpershkrimKodifikimi = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimKodifikimi, FusheKontrolli.Pershkrimi, true);
            if (!kontrollpershkrimKodifikimi.Status)
                return kontrollpershkrimKodifikimi;

            if (kodLloji == "")
                return new clsMesazh(false, "Plotesoni Llojin e grupit!");
            if (llojKodifikimi == 0)
                return new clsMesazh(false, "Ky lloj grupi nuk ekziston!");
            if (shtim && DbCore.DbInventari.clsKodifikimArtikulli.ekzistonSipasKodLloj(kodKodifikimi, idNdermarje, llojKodifikimi))
            {
                return new clsMesazh(false, "Ekziston nje grup me kete kod. Ju lutem shenoni nje kod tjeter!");
            }
            if (!String.IsNullOrEmpty(kodPrindi))
            {
                if (!DbCore.DbInventari.clsKodifikimArtikulli.ekzistonSipasKodLloj(kodPrindi, idNdermarje, llojKodifikimi))
                {
                    return new clsMesazh(false, "Prindi i grupit nuk ekziston!");
                }
            }
            if (nivelKodifikimi < 0)
            {
                return new clsMesazh(false, "Niveli duhet te jete numer pozitiv!");
            }
            DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            if (dbInventari.kaVeprimeKodifikimArtikulliPaStandart(idPrindi))
            {
                return new clsMesazh(false, "Ky grup eshte perdorur ne veprime dhe nuk mund te detajohet. Ju lutem zgjidhni nje grup tjeter per prind");
            }
            dbInventari.Dispose();
            return new clsMesazh(true, "Kontrollet e grupit u kaluan me sukses");
        }

        public clsMesazh transfero(object[] idkodifikimi, List<object> idndermarje, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh();
            bool sukses = true;
            int idndermarrjeOwn = DbCore.DbAdmin.clsNdermarrje.merrIdNdermarrjeOwn();
            for (int i = 0; i < idkodifikimi.Length; i++)
            {
                clsKodifikimArtikulli kod = new clsKodifikimArtikulli(int.Parse(idkodifikimi[i].ToString()));
                foreach (object id in idndermarje)
                {
                    if (Convert.ToInt32(id) == idndermarrjeOwn && (kod.LlojArtikulli || kod.LlojKodifikimi == 3))
                        continue;
                    clsDatabaseInventari db = new clsDatabaseInventari();
                    db.beginTransaksion();
                    mesazh = kontrollotransferim(kod, int.Parse(id.ToString()), db, idperdoruesi);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        sukses = false;
                    }
                    db.commitTransaksion();
                }
            }
            if (sukses) { mesazh.PershkrimMesazhi = "Transferimi mbaroi me sukses!"; mesazh.Status = true; }
            else { mesazh.PershkrimMesazhi = "Disa nga grupet e artikujve nuk u transferuan!"; mesazh.Status = false; }
            return mesazh;
        }

        public clsMesazh kontrollotransferim(clsKodifikimArtikulli kod, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!DbCore.DbInventari.clsKodifikimArtikulli.ekzistonSipasKodLloj(kod.kodKodifikimi, idndermarje, kod.llojKodifikimi, db))
            {
                if (kod.idPrindi > 0)//nqs ka prind kontrollojme prindin 
                {
                    clsKodifikimArtikulli prindi = new clsKodifikimArtikulli(kod.idPrindi, db);
                    mesazh = kontrollotransferim(prindi, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                     kod.idPrindi = prindi.idKodifikimi;
                }
                kod.colNorma = new DbAsete.colGrupNormaAmortizimi();
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;

            }
            else
            {
                clsKodifikimArtikulli kodnderm = new clsKodifikimArtikulli(kod.kodKodifikimi, idndermarje, kod.llojKodifikimi,false, db);     
                kod.idKodifikimi = kodnderm.idKodifikimi;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    if (kod.idPrindi > 0)
                    {
                        clsKodifikimArtikulli prindi = new clsKodifikimArtikulli(kod.idPrindi, db);
                        kontrollotransferim(prindi, idndermarje, db, idperdoruesi);
                        if (!mesazh.Status)
                            return mesazh;
                        kod.idPrindi = prindi.idKodifikimi;
                    }
                    kod.idPerdoruesi = idperdoruesi;
                    kod.idNdermarje = idndermarje;
                    kod.colNorma = new DbAsete.colGrupNormaAmortizimi();
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;


                }

            }
            return mesazh;
        }

        /// <summary>
        /// Ruan objektin kodifikim artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajKodifikimArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(bool vjenNgaImportSQL, string idLlogImp, string emerTabele, string primaryKey, string ndermarrjeKey)
        {
            using (var scope=new MyTransactionScope())
            {
                clsDatabaseInventari data = new clsDatabaseInventari();
                //data.beginTransaksion();
                clsMesazh u_ruajt = ruaj(data);
                if (!u_ruajt.Status)
                    return u_ruajt;
                u_ruajt = DbShare.colArkiva.RuajArkiven(idKodifikimi, 67, idPerdoruesi, idNdermarje, HfArkiva);
                if (!u_ruajt.Status)
                    return u_ruajt;
                if (vjenNgaImportSQL)
                {
                    DbImporte.clsDatabazeImporte dbImport = new DbImporte.clsDatabazeImporte(data );
                    u_ruajt = DbImporte.colImportSQL.updateDokTabeleTemportal(idLlogImp, idNdermarje, 1, emerTabele, primaryKey, ndermarrjeKey, dbImport);
                    if (!u_ruajt.Status)                    
                        return u_ruajt;                    
                }
                scope.Complete();
                //clsMesazh u_ruajt = data.ruajKodifikimArtikulli(this);
                return u_ruajt;
            }
        }

        public clsMesazh ruaj(clsDatabaseInventari data)
        {

            int id;
            clsMesazh u_ruajt = data.ruajKodifikimArtikulli(out id, this.KodKodifikimi, this.PershkrimKodifikimi, this.IdPrindi, this.NivelKodifikimi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.llojKodifikimi, this.idSkemaKontabel, this.idLlogariVlere, this.idLlogariShitje, this.idLlogariInventari, this.idLlogariShpenzimi, this.idLlogariNeProces, this.idLlogariAmortizimi, this.idFormatiSerial, this.llojArtikulli,this.idLlogariPakesim);
            this.idKodifikimi = id;
            if (!u_ruajt.Status)
                return u_ruajt;
            DbAsete.clsDatabazeAsete dbasete = new DbAsete.clsDatabazeAsete(data );
            foreach (DbAsete.clsGrupNormaAmortizimi grup in colNorma)
            {
                int idgrup;
                u_ruajt = dbasete.ruajGrupNormaAmortizimi(out idgrup, this.idKodifikimi, grup.IdLlojAmortizimi, grup.IdStandartAmortizimi, grup.NormeMagazine, grup.Norme, grup.IdStatusDokumenti, grup.IdNdermarrje, grup.IdPerdoruesi, grup.IdKrijuesi);
                if (!u_ruajt.Status)
                    return u_ruajt;
            }
            if (this.nivelKodifikimi == 1 && this.llojArtikulli==true && this.llojKodifikimi==1)
            {
                if (!DbCore.DbAsete.clsStandarteAmortizim.kontrolloEkzistonStandartiAmortizimit("Shqiptar", this.idNdermarje))
                    return u_ruajt;
               
                int idKarakteristika = 0;
                u_ruajt = dbasete.ruajKonfigurimStandartiNdermarjeRe(out idKarakteristika, this.IdKodifikimi, this.idNdermarje, this.idPerdoruesi);

                if (!u_ruajt.Status)
                    return u_ruajt;
            }
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin kodifikim artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoKodifikimArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            using (var scope=new MyTransactionScope())
            {
                clsDatabaseInventari data = new clsDatabaseInventari();
                data.beginTransaksion();
                clsMesazh u_modifikua = modifiko(data);
                if (!u_modifikua.Status)
                {
                    return u_modifikua;
                    //if (!u_modifikua.Status)
                    // data.commitTransaksion();
                    
                }
                scope.Complete();
               // else data.rollbackTransaksion();
                return u_modifikua;
            }
        }

        public clsMesazh modifiko(clsDatabaseInventari data)
        {

            clsMesazh u_modifikua = data.modifikoKodifikimArtikulli(this.IdKodifikimi, this.KodKodifikimi, this.PershkrimKodifikimi, this.IdPrindi, this.NivelKodifikimi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.llojKodifikimi, this.idSkemaKontabel, this.idLlogariVlere, this.idLlogariShitje, this.idLlogariInventari, this.idLlogariShpenzimi, this.idLlogariNeProces, this.idLlogariAmortizimi, this.idFormatiSerial, this.llojArtikulli,this.idLlogariPakesim);
            if (!u_modifikua.Status)
                return u_modifikua;
            DbAsete.clsDatabazeAsete dbasete = new DbAsete.clsDatabazeAsete(data );
            foreach (DbAsete.clsGrupNormaAmortizimi grup in colNorma)
            {
                int idgrup=dbasete.ktheIDGrupNormaAmortizimiSipasIDKonfigStandart(this.IdKodifikimi,grup.IdStandartAmortizimi);
                if (idgrup>0)
                {
                    u_modifikua = dbasete.modifikimiGrupNormaAmortizimi(idgrup, grup.IdLlojAmortizimi, grup.NormeMagazine, grup.Norme, grup.IdPerdoruesi);
                }
                else
                    u_modifikua = dbasete.ruajGrupNormaAmortizimi(out idgrup, this.idKodifikimi, grup.IdLlojAmortizimi, grup.IdStandartAmortizimi, grup.NormeMagazine, grup.Norme, grup.IdStatusDokumenti, grup.IdNdermarrje, grup.IdPerdoruesi, grup.IdKrijuesi);
                if (!u_modifikua.Status)
                    return u_modifikua;
            }



            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin kodifikim artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiKodifikimArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiKodifikimArtikulliStatus(this.IdKodifikimi, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiKodifikimArtikulli(this);
            return u_fshi;
        }

        /// <summary>
        /// Fshin objektin kodifikim artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiKodifikimArtikulliStatus"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(DbCore.DbAsete.clsDatabazeAsete dbAsete)
        {
            clsDatabaseInventari data = new clsDatabaseInventari(dbAsete );
            clsMesazh u_fshi = data.fshiKodifikimArtikulliStatus(this.IdKodifikimi, this.idPerdoruesi);
            return u_fshi;
        }

        /// <summary>
        /// Fshin objektin kodifikim artikulli dhe lidhjen standart status mag ne tabelat perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja e kodifikimit dhe lidhjes qe jane ne transaksion u krye me sukses apo jo</returns>
        public clsMesazh fshiKodifikimArtDheLidhjeStandartStatusMag()
        {
            using (var scope=new MyTransactionScope())
            {
                DbCore.DbAsete.clsDatabazeAsete dbAsete = new DbCore.DbAsete.clsDatabazeAsete();
                //dbAsete.beginTransaksion();
                clsMesazh mesazh = fshiKodifikimArtDheLidhjeStandartStatusMag(dbAsete);
                if (!mesazh.Status)
                {
                    //dbAsete.rollbackTransaksion();
                    return mesazh;
                }
                scope.Complete();
                //dbAsete.commitTransaksion();
                return mesazh;
            }
        }

        /// <summary>
        /// Fshin objektin kodifikim artikulli dhe lidhjen standart status mag ne tabelat perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbAsete.clsDatabazeAsete.fshiKonfigurimStandartiStatusSipasKodifilimArt"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja e kodifikimit dhe lidhjes qe jane ne transaksion u krye me sukses apo jo</returns>
        public clsMesazh fshiKodifikimArtDheLidhjeStandartStatusMag(DbCore.DbAsete.clsDatabazeAsete dbAsete)
        {
            clsMesazh mesazhFshiLidhje = dbAsete.fshiKonfigurimStandartiStatusSipasKodifilimArt(this.IdKodifikimi, this.idPerdoruesi);
            if (!mesazhFshiLidhje.Status)
                return mesazhFshiLidhje;
            clsMesazh fshiKodifikim = this.fshi(dbAsete);
            return fshiKodifikim;
        }

        /// <summary>
        /// Merr objektin kodifikim artikulli nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrKodifikimArtikulli"/> 
        /// </summary>
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.merrKodifikimArtikulliPaKthim(this.IdKodifikimi);
            data.Dispose();
            //data.merrKodifikimArtikulli(this);
        }

        public bool ekziston()
        {
            return ekziston(this.idKodifikimi);
        }

        public static bool ekziston(int idKodifikimi)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool ekziston = db.ekzistonKodifikimArtikulli(idKodifikimi);
            db.Dispose();
            return ekziston;
        }

        public static bool ekzistonSipasKodLloj(string kodkodifikimi, int idndermarje, int llojkod)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool ekziston = ekzistonSipasKodLloj(kodkodifikimi, idndermarje, llojkod, db);
            db.Dispose();
            return ekziston;
        }

        public static bool eshteTransferuarTekBij(string kodkodifikimi, int idndermarje, int llojkod)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool ekziston = db.eshteTransferuarTekBij(kodkodifikimi, idndermarje, llojkod);
            db.Dispose();
            return ekziston;
        }

        public static bool ekzistonSipasKodLloj(string kodkodifikimi, int idndermarje, int llojkod, clsDatabaseInventari db)
        {
            bool ekziston = db.ekzistonKodifikimArtikulliKodLloj(kodkodifikimi, idndermarje, llojkod);
            return ekziston;
        }

        public bool eshtePrind()
        {
            return eshtePrind(this.idKodifikimi);
        }

        public static bool eshtePrind(int idKodifikimi)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool eshtePrind = dbInventari.eshtePrind(idKodifikimi);
            dbInventari.Dispose();
            return eshtePrind;
        }

        public static int ktheIdKodifikimi(string kodi, int idndermarrje, int llojkodifikimi, bool llojartikulli)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            int idKodifikimi = db.ktheIdKodifikimArtikulliKodLloj(kodi, idndermarrje, llojkodifikimi, llojartikulli);
            db.Dispose();
            return idKodifikimi;
        }

        public static string ktheKodKodifikimi(int idKodifikimi)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            string kodKodifikimi = db.ktheKodKodifikimArtikulliSipasId(idKodifikimi);
            db.Dispose();
            return kodKodifikimi;
        }

        public static int ktheIdPrindiFillestar(int idkodifikimi,clsDatabaseInventari dbinv)
        {
            clsKodifikimArtikulli prindi = new clsKodifikimArtikulli(idkodifikimi,dbinv);
            if (prindi.IdPrindi == 0)
                return prindi.IdKodifikimi;
            return ktheIdPrindiFillestar(prindi.IdPrindi,dbinv);
        }

        public static int ktheIdPrindiFillestar(int idkodifikimi)
        {
            using (clsDatabaseInventari dbinv = new clsDatabaseInventari()) {
                return ktheIdPrindiFillestar(idkodifikimi, dbinv);
            }
        }

        public static int MerrIdKodifikimi(string kodKodifikimi, int idNdermarrje)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.MerrIdKodifikimiSipasKodit(kodKodifikimi, idNdermarrje);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kodifikimin e artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowKodifikimArtikulli">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKodifikimArtikulli(DataRow dbDataRowKodifikimArtikulli)
        {
            if (dbDataRowKodifikimArtikulli != null)
            {

                try
                {
                    int.TryParse(dbDataRowKodifikimArtikulli["IDKODIFIKIMI"].ToString(), out idKodifikimi);
                    kodKodifikimi = dbDataRowKodifikimArtikulli["KODKODIFIKIMI"].ToString();
                    pershkrimKodifikimi = dbDataRowKodifikimArtikulli["PERSHKRIMKODIFIKIMI"].ToString();
                    int.TryParse(dbDataRowKodifikimArtikulli["IDPRINDI"].ToString(), out idPrindi);
                    int.TryParse(dbDataRowKodifikimArtikulli["NIVELKODIFIKIMI"].ToString(), out nivelKodifikimi);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowKodifikimArtikulli["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowKodifikimArtikulli["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKodifikimArtikulli["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowKodifikimArtikulli["LLOJKODIFIKIMI"].ToString(), out llojKodifikimi);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDSKEMAKONTABEL"].ToString(), out idSkemaKontabel);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDLLOGARIVLERE"].ToString(), out idLlogariVlere);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDLLOGARISHITJE"].ToString(), out idLlogariShitje);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDLLOGARIINVENTARI"].ToString(), out idLlogariInventari);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDLLOGARISHPENZIMI"].ToString(), out idLlogariShpenzimi);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDLLOGARINEPROCES"].ToString(), out idLlogariNeProces);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDLLOGARIAMORTIZIMI"].ToString(), out idLlogariAmortizimi);
                    int.TryParse(dbDataRowKodifikimArtikulli["IDFORMATISERIALIT"].ToString(), out idFormatiSerial);
                    bool.TryParse(dbDataRowKodifikimArtikulli["LLOJARTIKULLI"].ToString(), out llojArtikulli);
                    if (int.TryParse(dbDataRowKodifikimArtikulli["IDLLOGARIPAKESIM"].ToString(), out idLlogariPakesim))
                    { 
                       clsLlogari llogari=new clsLlogari(idLlogariPakesim);
                       this.nrLlogPakesimi = llogari.NrLlogari;
                    }
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kodeve te artikullit nga db-ja");
                }
            }
            else
                return false;
        }
        internal clsMesazh mbushKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        {
            IdKodifikimi = kodifikimArtikulli.IdKodifikimi;
            KodKodifikimi = kodifikimArtikulli.KodKodifikimi;
            PershkrimKodifikimi = kodifikimArtikulli.PershkrimKodifikimi;
            IdPrindi = kodifikimArtikulli.IdPrindi;
            nivelKodifikimi = kodifikimArtikulli.NivelKodifikimi;
            IdPerdoruesi = kodifikimArtikulli.IdPerdoruesi;
            IdNdermarje = kodifikimArtikulli.IdNdermarje;
            IdStatusDok = kodifikimArtikulli.IdStatusDok;
            dtKrijimi = kodifikimArtikulli.DtKrijimi;
            dtModifikimi = kodifikimArtikulli.DtModifikimi;
            LlojKodifikimi = kodifikimArtikulli.LlojKodifikimi;
            IdSkemaKontabel = kodifikimArtikulli.IdSkemaKontabel;
            IdLlogariVlere = kodifikimArtikulli.IdLlogariVlere;
            IdLlogariShitje = kodifikimArtikulli.IdLlogariShitje;
            IdLlogariInventari = kodifikimArtikulli.IdLlogariInventari;
            IdLlogariShpenzimi = kodifikimArtikulli.IdLlogariShpenzimi;
            IdLlogariNeProces = kodifikimArtikulli.IdLlogariNeProces;
            IdLlogariAmortizimi = kodifikimArtikulli.IdLlogariAmortizimi;
            IdFormatiSerial = kodifikimArtikulli.IdFormatiSerial;
            LlojArtikulli = kodifikimArtikulli.LlojArtikulli;
            IdLlogariPakesimi = kodifikimArtikulli.IdLlogariPakesimi;
            NrLlogPakesimi = kodifikimArtikulli.NrLlogPakesimi;
            return new clsMesazh(true, $"Mbushja e kodifikimit te artikullit me kod {kodifikimArtikulli.KodKodifikimi} u krye me sukses!");
        }
        #endregion
    }
}
