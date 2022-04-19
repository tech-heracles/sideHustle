using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System;
using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{

    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje recete optike
    ///  (Te dhenat  merren nga tabela : t_kokarecetaOptike)
    /// </summary>
    public class clsKokaRecetaOptike : IDataBase
    {

        #region Atribute
        /// <summary>
        /// id e kokes se dokumentit te recetave
        /// </summary>
        private int idKoka;
        /// <summary>
        /// numri i dokumentit
        /// </summary>
        private string nrDok;
        /// <summary>
        /// data e dokumentit
        /// </summary>
        private DateTime dtDok;
        /// <summary>
        /// id e garancise
        /// </summary>
        private DateTime dtRegj;
        /// <summary>
        /// shenime
        /// </summary>
        private string shenime;
        /// <summary>
        /// id e klientit
        /// </summary>
        private int idKlient;
        /// <summary>
        /// nr serial
        /// </summary>
        private string nrSerial;
        /// <summary>
        /// adresa
        /// </summary>
        private string adresa;
        /// <summary>
        ///referimi
        /// </summary>
        private string referimi;
        /// <summary>
        /// distanca afer
        /// </summary>
        private string diAfer;
        /// <summary>
        /// distanca larg
        /// </summary>
        private string diLarg;
        /// <summary>
        ///lartesia
        /// </summary>
        private string lartesia;
        /// <summary>
        /// id e dokumentit nga ka ardhur ne rast modifikimi
        /// </summary>
        private int idDokNga;
        /// <summary>
        /// id e statusit te dokumentit
        /// </summary>
        private int idStatusDok;
        /// <summary>
        /// id e nderamrjes
        /// </summary>
        private int idNdermarrje;
        /// <summary>
        /// id nderamrje vit
        /// </summary>
        private int idNderViti;
        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdoruesi;
        /// <summary>
        ///id e Krijuesit
        /// </summary>
        private int idKrijuesi;
        /// <summary>
        /// id e konfigurimit
        /// </summary>
        private int idKonfigAmbjente;
        /// <summary>
        /// id e nivelet te dokumentit qe e ka gjeneruar
        /// </summary>
        private int idNivelGjenerues;
        /// <summary>
        /// id e konfigurimit te dokumentit qe e ka gjeneruar
        /// </summary>
        private int idKonfigGjenerues;
        /// <summary>
        /// id e dokumentit qe e ka gjeneruar
        /// </summary>
        private int idGjenerues;
        /// <summary>
        /// data e krijimit
        /// </summary>
        private DateTime dtKrijimi;
        /// <summary>
        /// data e modifikimit te fundit
        /// </summary>
        private DateTime dtModifikimi;
        /// <summary>
        /// id e nivelit
        /// </summary>
        private int idNivel;
        /// <summary>
        /// id e design-it te raportit
        /// </summary>
        private int idRaportDesign;
        /// <summary>
        /// koleksioni me trupin e recetave
        /// </summary>
        private colTrupiRecetaOptikeSyri colTrupiSyri;
        private colTrupiRecetaOptikePunime colTrupiPunime;
        private readonly IDictionary<string, object> _hfNrAuto;
        private clsPeriudhaKontabel periudhaKontabel;
        private System.Resources.ResourceManager rm;
        private System.Globalization.CultureInfo ci;

        #endregion
        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e nivelit te regjistrimit.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te ambjentit
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        /// <summary>
        /// id e krijuesit
        /// </summary>
        public int IdKrijuesi
        {
            get
            {
                return idKrijuesi;
            }
            set
            {
                idKrijuesi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nr i dokumentit.
        /// </summary>
        public String NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr seriali.
        /// </summary>
        public String NrSerial
        {
            get { return nrSerial; }
            set { nrSerial = value; }
        }
        /// <summary>
        /// Kthen/Vendos adresen
        /// </summary>
        public String Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }
        /// <summary>
        /// Kthen/Vendos referimin.
        /// </summary>
        public String Referimi
        {
            get { return referimi; }
            set { referimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos distancen afer.
        /// </summary>
        public String DiAfer
        {
            get { return diAfer; }
            set { diAfer = value; }
        }
        /// <summary>
        /// Kthen/Vendos distancen larg.
        /// </summary>
        public String DiLarg
        {
            get { return diLarg; }
            set { diLarg = value; }
        }
        /// <summary>
        /// Kthen/Vendos lartesine
        /// </summary>
        public String Lartesia
        {
            get { return lartesia; }
            set { lartesia = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenimet.
        /// </summary>
        public String Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e dokumentit nga i cili gjenerohet ne rastet e modifikimit
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit
        /// <example> ruajtur, draft etj.</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e Klientit
        /// </summary>
        public int IdKlient
        {
            get { return idKlient; }
            set { idKlient = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e design-it te raporteve
        /// </summary>
        public int IdRaportDesign
        {
            get { return idRaportDesign; }
            set { idRaportDesign = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit.
        /// </summary>
        public int IdNderVit
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e perdoruesit qe e ka ruajtur.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e regjistrimit te dokumentit.
        /// </summary>
        public DateTime DtRegj
        {
            get { return dtRegj; }
            set { dtRegj = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit nga eshte gjeneruar 
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit nga eshte gjeneruar 
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga eshte gjeneruar  nga nje ambjent tjeter
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit 
        /// </summary>
        public colTrupiRecetaOptikeSyri ColTrupiSyri
        {
            get { return colTrupiSyri; }
            set { colTrupiSyri = value; }
        }
        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit 
        /// </summary>
        public colTrupiRecetaOptikePunime ColTrupiPunime
        {
            get { return colTrupiPunime; }
            set { colTrupiPunime = value; }
        }
        /// <summary>
        /// data e krijimit te dokumentit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            
        }
        /// <summary>
        /// data e modifikimit te fundit te dokumentit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        #endregion
        #region Konstruktoret
        public clsKokaRecetaOptike(int idKoka)
        {
            using (var db = new clsDatabaseRegjistrim())
            {
                db.ktheKokaReceteSipasID(idKoka, this);
            }

            ColTrupiPunime = new colTrupiRecetaOptikePunime(idKoka);
            ColTrupiSyri = new colTrupiRecetaOptikeSyri(idKoka);

        }
        public clsKokaRecetaOptike(IDataRecord rec)
        {
            Mbush(rec);
        }

        public clsKokaRecetaOptike()
        {
        }
        public clsKokaRecetaOptike( IDictionary<string, object> hfNrAuto, clsPeriudhaKontabel periudhaKontabel, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            _hfNrAuto = hfNrAuto;
            this.periudhaKontabel = periudhaKontabel;
            this.rm = rm;
            this.ci = ci;
        }


        #endregion


        #region Metoda Publike

        public clsMesazh Valido()
        {
            if (nrDok == string.Empty)
                return new MesazhGabimi( "Numri i dokumentit nuk mund të jetë bosh");
            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
                return new MesazhGabimi ("Zgjidhni datën e dokumentit!");
            if (dtRegj == null || dtRegj.ToShortDateString() == "01/01/0100")
                return new MesazhGabimi("Zgjidhni datën e regjistrimit!");
            
            if (periudhaKontabel != null)
            {
                var mesazh = clsFunksione.checkPeriudheKontabel(DtDok, periudhaKontabel, IdStatusDok);
                if (!mesazh) return mesazh;
            }
            if (_hfNrAuto != null)
            {
                bool kaNdryshimNrAuto;
                var mes = kontrolloNrAutoReceta(out kaNdryshimNrAuto);
                if (!mes)
                    return mes;
            }



            if (IdDokNga == 0 && EkzistonReceteMeKeteNrDok())//nese eshte modifikim nuk kemi pse kontrollojme ekzistencen
                return new MesazhGabimi("Ekziston nje regjistrim me kete numer dokumenti!");



            if (IdKlient == 0)
                return new MesazhSuksesi("Kontrollet u kaluan me sukses!");
            //Kontrolli per klientin qe te ekzistoje, te jete i llojit klient dhe te jete aktiv, dhe autorizimi
            var kf = new clsKlientFurnitor(IdKlient);
            if (kf.IdKlientFurnitor < 1)
                return  new MesazhGabimi("Klienti nuk ekziston ose ju nuk keni autorizim për të!");
            if (kf.IdKlientFurnitor > 1 && !kf.AktivKF)
                return new MesazhGabimi("Klienti nuk është aktiv!");
            if (!kf.LlojiKF)
                return new MesazhGabimi("Subjekti i zgjedhur nuk është klient!");
            
            //todo kontrollo kushte specifike
            return new MesazhSuksesi("Kontrollet u kaluan me sukses!");
        }

        

        public clsMesazh Ruaj()
        {
            try
            {
                using (var myScope = new MyTransactionScope())
                {
                    var mesazh = RuajDokumentNeDb();

                    if (mesazh) myScope.Complete();

                    return mesazh;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi($"Dokumenti nuk u ruajt!");
            }
        }

        public clsMesazh Modifiko()
        {
            try
            {
                using (var myScope = new MyTransactionScope())
                {
                    //merr koken e recetave, dhe trupat per  ate koke
                    var kokaEkzistuese = new clsKokaRecetaOptike(IdKoka);

                    if (kokaEkzistuese.IdKoka == 0 || kokaEkzistuese.IdStatusDok == 2)
                        return new MesazhGabimi("Dokumenti ka ndryshuar, ju lutem rihapeni perseri!");
                    

                    //Bejme update idoknga te kokes se re me ate ekzistuese
                    //Dokumentit ekzistues i vendosim status = fshire (2) dhe percaktojme idPerdoruesin
                    IdDokNga = kokaEkzistuese.IdKoka;
                    kokaEkzistuese.IdStatusDok = 2;
                    kokaEkzistuese.IdPerdoruesi = IdPerdoruesi;

                    //Modifikojme koken 
                    var mesazh = kokaEkzistuese.ModifikoKoken();
                    if (!mesazh) return mesazh;

                    //Dokumentin ekzistues bashke me turpat e kalojme ne historik
                    mesazh = kokaEkzistuese.KaloNeHistorik();

                    //Ruajme dokumentin e modifikuar
                    mesazh = RuajDokumentNeDb();

                    if (mesazh) myScope.Complete();
                    return mesazh;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi($"Dokumenti me id {IdKoka} nuk u ruajt!");
            }
        }


        private clsMesazh KaloNeHistorik()
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.hidhNeHistorikRecetaOptike(IdKoka);

        }

        public clsMesazh Fshi()
        {
            try
            {
                using (var myScope = new MyTransactionScope())
                {
                    var kokaEkzistuese = new clsKokaRecetaOptike(IdKoka);
           
                    kokaEkzistuese.IdStatusDok = 2;
                    kokaEkzistuese.IdPerdoruesi = IdPerdoruesi;

                    var mesazh = kokaEkzistuese.ModifikoKoken();
                    if (!mesazh) return mesazh; 

                    mesazh = kokaEkzistuese.KaloNeHistorik();
                    if (mesazh) myScope.Complete();
                    return mesazh;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi($"Dokumenti me id {IdKoka} nuk u fshi!");
            }
        }

        public void Mbush(IDataRecord record)
        {
            int.TryParse(record["IDKOKA"].ToString(), out idKoka);
            nrDok = record["NRDOK"].ToString();
            DateTime.TryParse(record["DTDOK"].ToString(), out dtDok);
            DateTime.TryParse(record["DTREGJ"].ToString(), out dtRegj);
            shenime = record["SHENIME"].ToString();
            int.TryParse(record["IDKLIENTI"].ToString(), out idKlient);
            nrSerial = record["NRSERIAL"].ToString();
            adresa = record["ADRESA"].ToString();
            referimi = record["REFERIMI"].ToString();
            diAfer = record["DIAFER"].ToString();
            diLarg = record["DILARG"].ToString();
            lartesia = record["LARTESIA"].ToString();
            int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
            int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarrje);
            int.TryParse(record["IDNDERVITI"].ToString(), out idNderViti);
            int.TryParse(record["IDKRIJUESI"].ToString(), out idKrijuesi);
            int.TryParse(record["IDPERDORUESI"].ToString(), out idPerdoruesi);
            int.TryParse(record["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
            int.TryParse(record["IDNIVEL"].ToString(), out idNivel);
            int.TryParse(record["IDDOKNGA"].ToString(), out idDokNga);
            int.TryParse(record["IDGJENERUES"].ToString(), out idGjenerues);
            int.TryParse(record["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
            int.TryParse(record["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
            int.TryParse(record["IDRAPORTDESIGN"].ToString(), out idRaportDesign);
        }


        public  static int ktheIdRaportDesign(int idKoka)
        {
            using (var db = new clsDatabaseRegjistrim())
            {
                return db.ktheIdRaportDesignSipasIdKoka(idKoka);
            }
        }

        #endregion

        #region Metoda Internal




        #endregion


        #region METODA PRIVATE
        private void VendosIdKokaTeTrupat()
        {
            ColTrupiPunime.ForEach(x => x.IdKoka = IdKoka);
            ColTrupiSyri.ForEach(x => x.IdKoka = IdKoka);
        }

        private clsMesazh RuajDokumentNeDb()
        {
            var mesazh = Valido();
            if (!mesazh) return mesazh;

            mesazh = RuajKoken();
            if (!mesazh) return mesazh;

            VendosIdKokaTeTrupat();

            mesazh = ColTrupiSyri.Ruaj();
            if (!mesazh) return mesazh;

            mesazh = ColTrupiPunime.Ruaj();
            return mesazh;
        }

        private clsMesazh RuajKoken()
        {
            using (var dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ruajKokaRecetaOptike(out idKoka, NrDok, DtDok, DtRegj, Shenime, IdKlient, NrSerial, Adresa, Referimi, DiAfer, DiLarg, Lartesia, IdStatusDok, IdNdermarrje, IdNderVit, IdKrijuesi, IdPerdoruesi, idKonfigAmbjente, IdNivel, IdDokNga, IdGjenerues, IdKonfigGjenerues, IdNivelGjenerues, IdRaportDesign);
            }
        }
        public clsMesazh ModifikoKoken()
        {
            using (var dbRegj = new clsDatabaseRegjistrim())
            {
                var mesazh = dbRegj.modifikoKokaRecetaOptike(IdKoka, NrDok, DtDok, DtRegj, Shenime, IdKlient, NrSerial, Adresa, Referimi, DiAfer, DiLarg, Lartesia, IdStatusDok, IdNdermarrje, IdNderVit, IdPerdoruesi, IdKonfigAmbjente, IdNivel, IdDokNga, IdGjenerues, IdKonfigGjenerues, IdNivelGjenerues, IdRaportDesign);
                return mesazh;
            }
        }

        private bool EkzistonReceteMeKeteNrDok()
        {
            using(var db = new clsDatabaseRegjistrim())
                return db.ekzistonDokumentRecetaOptike(NrDok, IdNdermarrje, IdNderVit);

        }


        private clsMesazh kontrolloNrAutoReceta(out bool kaNdryshimNumri)
        {
            using (clsDatabaseAdmin dbadm = new clsDatabaseAdmin())
            {
                List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(dbadm, _hfNrAuto, DateTime.Today);
                if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                    this.NrDok = NrAuto.ktheVlerenEre(list, "NrDok");
                return NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.IdPerdoruesi, this.idNdermarrje, dbadm);
            }
        }
        #endregion
    }
}
