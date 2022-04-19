using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Globalization;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbListPagesat
{
    //TODO GETSON gjej nje menyre se si te marresh te dhenat per griden
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje punesim
    ///  (Te dhenat  merren nga tabela : T_PUNESIM)
    /// </summary>
    public class clsPunesim
    {
        private static NLog.Logger logu = NLog.LogManager.GetCurrentClassLogger();
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se punesimit nga db-ja";
        #region Atribute

        private int idPunesim;
        private int idPunonjes;
        private int idDepartament;
        private int idNenDepartament;
        private string detyra;
        private string nrKontrate;
        private int idTipKontrate;
        private DateTime dtFillimi;
        private DateTime dtPerfundimi;
        private bool larguar;
        private DateTime? dtLargimi;
        private string arsyeja;
        private string periudhaNjoftimi;
        private bool neProve;
        private string periudhaProve;
        private string departament;
        private string nenDepartament;
        private string tipKontrate;
        private decimal pagaShtesa;
        private int idgrupPunonjesish;
        private int idProfesioni;
        private int idTitullPune;
        private bool punonjesTurne;
        private bool punonjesGatishmeri;
        private int statusi;
        private int ndryshimPozicioni;
        private DateTime dtNenshkrimi;
        private string shenime;
        private DateTime dtAktivizimi;
        private int idPerdoruesi;
        private bool meKomisione;
        private int idKodeProfesione;
        private string kodeProfesione;
        private DataRow rreshti;
        private string profesioni;
        private string grupPunonjes;
        private string titullPune;
        #endregion

        #region Konstruktor

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idPunesim">id e punesimit</param>
        /// <param name="idPunonjes">id e punonjesit</param>
        /// <param name="idDepartament"> id e departamentit</param>
        /// <param name="idNenDepartament">id e nendepartamentit</param>
        /// <param name="detyra">detyra</param>
        /// <param name="nrKontrate">nrkontrate</param>
        /// <param name="idTipKontrate">tipkontrate</param>
        /// <param name="dtFillimi">data e fillimit</param>
        /// <param name="dtPerfundimi">data e perfundimit</param>
        /// <param name="llogBankare">llogaria bankare</param>
        /// <param name="idBanka">banka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtLargimi">data e largimit</param>
        /// <param name="arsyeja"> arsyeja e largimit</param>
        /// <param name="periudhaNjoftimi"> periudha e njoftimit</param>
        /// <param name="neProve">ne prove</param>
        /// <param name="periudhaProve">periudha ne prove</param>
        public clsPunesim(string nrpersonal, string departament, string nendepartament, string detyra, string nrKontrate, string tipkontrate, DateTime dtFillimi, DateTime dtPerfundimi, bool larguar, DateTime dtLargimi, string arsyeja, string periudhaNjoftimi, bool neProve, string periudhaProve, string grup, string profesioni, string titullpune, bool punonjesturne, bool punonjesgatishmeri, string statusi, string ndryshimpozicioni, DateTime dtnenshkrimi, string shenime, DateTime dtaktivizimi, int idperdoruesi, bool meKomisione, string kodeprofesione, int idndermarje, int idgjuha, int idProfesion = 0, int idTitullPune = 0, int idDepartament = 0, int idNenDepartament = 0)
        {
            #region mbushja e id
            int idpunonjes = 0;
            if (nrpersonal != "")
            {
                clsPunonjes pun = new clsPunonjes(nrpersonal, idndermarje);
                idpunonjes = pun.IdPunonjes;
            }
            int iddep = 0;
            if (departament != "")
            {
                clsStrukturaAdministrative dep = (idDepartament != 0) ? new clsStrukturaAdministrative(idDepartament) : new clsStrukturaAdministrative(idndermarje, departament);
                iddep = dep.IdStrukturaAdm;
                Departament = departament;

            }
            int idnendep = 0;
            if (nendepartament != "")
            {

                clsStrukturaAdministrative nendep = (idNenDepartament != 0) ? new clsStrukturaAdministrative(idNenDepartament) : new clsStrukturaAdministrative(idndermarje, nendepartament);
                idnendep = nendep.IdStrukturaAdm;
                this.NenDepartament = nendepartament;
            }
            int idtip = 0;
            if (tipkontrate != "")
            {
                clsTipeKontrate tip = new clsTipeKontrate(tipkontrate, idndermarje, idgjuha);
                idtip = tip.IdTipKontrate;
                TipKontrate = tipkontrate;
            }
            int idgruppunonjesish = 0;
            if (grup != "")
            {
                clsGrupPunonjesish grupi = new clsGrupPunonjesish(grup, idndermarje);
                idgruppunonjesish = grupi.IdGrupPunonjesish;
                GrupPunonjesish = grup;
            }

            int idprof = 0;
            
            if (profesioni != "")
            {
                clsProfesioneTitujPune prof = idProfesion != 0 ? new clsProfesioneTitujPune(idProfesion) : new clsProfesioneTitujPune(profesioni, idndermarje, 1, idgjuha);
                Profesioni = profesioni;
                idprof = prof.Id;
            }
            int idtitull = 0;
            if (titullpune != "")
            {
                clsProfesioneTitujPune prof = idTitullPune != 0 ? new clsProfesioneTitujPune(idTitullPune) : new clsProfesioneTitujPune(titullpune, idndermarje, 2, idgjuha);
                TitullPune = titullpune;
                idtitull = prof.Id;
            }
            int idkodeprofesione = 0;
            if (kodeprofesione != "")
            {
                clsKodeProfesione prof = new clsKodeProfesione(kodeprofesione, idndermarje);
                KodeProfesione = kodeprofesione;
                idkodeprofesione = prof.Id;
            }
            int roli = 1;
            switch (statusi)
            {
                case "Shef departamenti":
                case "Head of department":
                    roli = 1;
                    break;
                case "Punonjes":
                case "Employee":
                case "Employé":
                    roli = 2;
                    break;
                default:
                    throw new MyException("Ky rol nuk ekziston!");
            }
            int ndryshim = merrIdNdryshimPozicioni(ndryshimpozicioni);
            if(ndryshim==0)
                throw new MyException("Ky ndryshim pozicioni nuk ekziston!");
            
            #endregion
            #region mbushja e objektit
            this.idPunonjes = idpunonjes;
            this.idDepartament = iddep;
            this.idNenDepartament = idnendep;
            this.detyra = detyra;
            this.nrKontrate = nrKontrate;
            this.idTipKontrate = idtip;
            this.dtFillimi = dtFillimi;
            this.dtPerfundimi = dtPerfundimi;
            this.larguar = larguar;
            if (DateTimeUtil.EshteNullOrDefault(dtLargimi))
                this.dtLargimi = null;
            else this.dtLargimi = dtLargimi;
            this.arsyeja = arsyeja;
            this.periudhaNjoftimi = periudhaNjoftimi;
            this.neProve = neProve;
            this.periudhaProve = periudhaProve;
            this.idgrupPunonjesish = idgruppunonjesish;
            this.idProfesioni = idprof;
            this.idTitullPune = idtitull;
            this.punonjesTurne = punonjesturne;
            this.punonjesGatishmeri = punonjesgatishmeri;
            this.statusi = roli;
            this.ndryshimPozicioni = ndryshim;

            this.dtNenshkrimi = dtnenshkrimi;
            this.shenime = shenime;
            this.dtAktivizimi = dtaktivizimi;
            this.idPerdoruesi = idperdoruesi;
            this.meKomisione = meKomisione;
            this.idKodeProfesione = idkodeprofesione;
            this.kodeProfesione = kodeprofesione;
            #endregion


            clsMesazh mesazh = this.kontrollo(nrpersonal, departament, nendepartament, tipkontrate, grup, profesioni, titullpune, kodeprofesione, idndermarje, idgjuha);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }
        private clsMesazh kontrollo(string nrpersonal, string departament, string nendepartament, string tipkontrate, string grupi, string profesioni, string titullpune, string kodeprofesione, int idndermarje, int idgjuha)
        {
            
            if (dtFillimi.ToShortDateString() == "01/01/0001")
                return new clsMesazh(false, "Ju lutem vendosni nje date fillimi!");
            if (nrpersonal != "")
            {
                if (!clsPunonjes.ekzistonPunonjes(nrpersonal, idndermarje))
                    return new clsMesazh(false, "Punonjesi me Nr Personal " + nrpersonal + " nuk ekziston!");
            }
            if (departament != "")
            {
                if (!clsStrukturaAdministrative.ekzistonEmri(departament, idndermarje))
                    return new clsMesazh(false, $"Departamenti {departament} nuk ekziston!");
            }
            if (nendepartament != "")
            {
                if (!clsStrukturaAdministrative.ekzistonEmri(nendepartament, idndermarje))
                    return new clsMesazh(false, $"Nendepartamenti {nendepartament} nuk ekziston!");
            }
            if (tipkontrate != "")
            {
                if (idgjuha == 0)
                {
                    if (!clsTipeKontrate.ekziston(tipkontrate, idndermarje))
                        return new clsMesazh(false, $"Tipi i kontrates {tipkontrate} nuk ekziston!");
                }
                else if (!clsTipeKontrate.ekzistonAng(tipkontrate, idndermarje))
                    return new clsMesazh(false, $"Tipi i kontrates {tipkontrate} nuk ekziston!");
            }
            if (profesioni != "" && IdProfesioni == -1) // nese idprofesioni nuk eshte -1 atehere eshte gjetur nje profesion qe ne konstruktor, nuk ka pse behet kontrolli serish
            {
                if (!clsProfesioneTitujPune.ekzistonProfesionTitujPunePershkrimi(profesioni, idndermarje, 1))
                    return new clsMesazh(false, $"Profesioni {profesioni} nuk ekziston!");
            }
            if (titullpune != "" && IdTitullPune == -1)
            {
                if (idgjuha == 0)
                {
                    if (!clsProfesioneTitujPune.ekzistonProfesionTitujPunePershkrimi(titullpune, idndermarje, 2))
                        return new clsMesazh(false, $"Pozicioni {titullpune} nuk ekziston!");
                }
                else if (!clsProfesioneTitujPune.ekzistonProfesionTitujPunePershkrimiAng(titullpune, idndermarje, 2,0))
                    return new clsMesazh(false, $"Pozicioni {titullpune} nuk ekziston!");
            }
            if (kodeprofesione != "")
            {
                if (!clsKodeProfesione.ekzistonKodeProfesione(kodeprofesione, idndermarje))
                    return new clsMesazh(false, $"Kod profesioni {kodeprofesione} nuk ekziston!");
            }
            if (grupi != "")
            {
                if (!clsGrupPunonjesish.ekziston(grupi, idndermarje))
                    return new clsMesazh(false, "Grupi nuk ekziston!");
            }
            if (Larguar && (DtLargimi == null || DtLargimi < DtFillimi))
            {
                return new MesazhGabimi(MessagesResource.Messages["msgVendosDateLargimiMeTeMadheSeDtFillimi"]);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsPunesim()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="idpunesim">id punesim</param>
        public clsPunesim(int idpunesim)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                db.mbushPunesim(idpunesim, this);
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// data e ndryshimit te fundit
        /// </summary>
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
        /// <summary>
        /// data e nenshkrimit te kontrates
        /// </summary>
        public DateTime DtNenshkrimi
        {
            get
            {
                return dtNenshkrimi;
            }
            set
            {
                dtNenshkrimi = value;
            }
        }
        public int IdKodeProfesione
        {
            get
            {
                return idKodeProfesione;
            }
            set
            {
                idKodeProfesione = value;
            }
        }
        /// <summary>
        /// id e perdoruesit qe beri ndryshimin
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }
        /// <summary>
        /// id e profesionit
        /// </summary>
        public int IdProfesioni
        {
            get
            {
                return idProfesioni;
            }
            set
            {
                idProfesioni = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdPunesim
        {
            get { return idPunesim; }
            set { idPunesim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e punonjesit
        /// </summary>
        public int IdPunonjes
        {
            get { return idPunonjes; }
            set { idPunonjes = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e departamentit
        /// </summary>
        public int IdDepartament
        {
            get
            {
                return idDepartament;
            }
            set
            {
                idDepartament = value;
            }
        }

        /// <summary>
        /// kthen/vendos id e nendepartamentit
        /// </summary>
        public int IdNenDepartament
        {
            get
            {
                return idNenDepartament;
            }
            set
            {
                idNenDepartament = value;
            }
        }

        /// <summary>
        /// kthen/vendos detyren
        /// </summary>
        public string Detyra
        {
            get
            {
                return detyra;
            }
            set
            {
                detyra = value;
            }
        }
        /// <summary>
        /// id e titullit te punes
        /// </summary>
        public int IdTitullPune
        {
            get
            {
                return idTitullPune;
            }
            set
            {
                idTitullPune = value;
            }
        }
        public string KodeProfesione
        {
            get
            {
                return kodeProfesione;
            }
            set
            {
                kodeProfesione = value;
            }
        }
        public bool MeKomisione
        {
            get
            {
                return meKomisione;
            }
            set
            {
                meKomisione = value;
            }
        }
        /// <summary>
        /// ndryshimi i pozicionit psh promovim, tranferim ect
        /// </summary>
        public int NdryshimPozicioni
        {
            get
            {
                return ndryshimPozicioni;
            }
            set
            {
                ndryshimPozicioni = value;
            }
        }
        /// <summary>
        /// kthen/vendos nr e kontrates
        /// </summary>
        public string NrKontrate
        {
            get
            {
                return nrKontrate;
            }
            set
            {
                nrKontrate = value;
            }
        }

        /// <summary>
        /// kthen/vendos tipin e kontrates
        /// </summary>
        public int IdTipKontrate
        {
            get
            {
                return idTipKontrate;
            }
            set
            {
                idTipKontrate = value;
            }
        }

        /// <summary>
        /// kthen vendos daten e fillimit
        /// </summary>
        public DateTime DtFillimi
        {
            get
            {
                return dtFillimi;
            }
            set
            {
                dtFillimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos daten e perfundimit.
        /// </summary>
        public DateTime DtPerfundimi
        {
            get { return dtPerfundimi; }
            set { dtPerfundimi = value; }
        }


        /// <summary>
        /// kthen/vendos larguar
        /// </summary>
        public bool Larguar
        {
            get
            {
                return larguar;
            }
            set
            {
                larguar = value;
            }
        }

        /// <summary>
        /// kthen vendos daten e largimit
        /// </summary>
        public DateTime? DtLargimi
        {
            get
            {
                return larguar ? dtLargimi : null;
            }
            set
            {
                dtLargimi = value;
            }
        }

        /// <summary>
        /// kthen vendos arsyen
        /// </summary>
        public string Arsyeja
        {
            get
            {
                return arsyeja;
            }
            set
            {
                arsyeja = value;
            }
        }

        /// <summary>
        /// kthen vendos periudhen e njoftimit
        /// </summary>
        public string PeriudhaNjoftimi
        {
            get
            {
                return periudhaNjoftimi;
            }
            set
            {
                periudhaNjoftimi = value;
            }
        }

        /// <summary>
        /// kthen vendos ne prove
        /// </summary>
        public bool NeProve
        {
            get
            {
                return neProve;
            }
            set
            {
                neProve = value;
            }
        }

        /// <summary>
        /// kthen vendos periudhen e proves
        /// </summary>
        public string PeriudhaProve
        {
            get
            {
                return periudhaProve;
            }
            set
            {
                periudhaProve = value;
            }
        }

        /// <summary>
        /// kthen kodin e deparamentit
        /// </summary>
        public string Departament
        {
            get
            {
                return departament;
            }
            set
            {
                departament = value;
            }
        }

        /// <summary>
        /// kthen kodin e nendepartamentit
        /// </summary>
        public string NenDepartament
        {
            get
            {
                return nenDepartament;
            }
            set
            {
                nenDepartament = value;
            }
        }
        /// <summary>
        /// punonjes ne gatishmeri
        /// </summary>
        public bool PunonjesGatishmeri
        {
            get
            {
                return punonjesGatishmeri;
            }
            set
            {
                punonjesGatishmeri = value;
            }
        }
        /// <summary>
        /// punonjes qe punon me turne
        /// </summary>
        public bool PunonjesTurne
        {
            get
            {
                return punonjesTurne;
            }
            set
            {
                punonjesTurne = value;
            }
        }
        /// <summary>
        /// shenime
        /// </summary>
        public string Shenime
        {
            get
            {
                return shenime;
            }
            set
            {
                shenime = value;
            }
        }
        /// <summary>
        /// statusi drejtues apo punonjes
        /// </summary>
        public int Statusi
        {
            get
            {
                return statusi;
            }
            set
            {
                statusi = value;
            }
        }
        /// <summary>
        /// kthen kodin e tipit te kontrates
        /// </summary>
        public string TipKontrate
        {
            get
            {
                return tipKontrate;
            }
            set
            {
                tipKontrate = value;
            }
        }

        /// <summary>
        /// kthen pagen me shtesen
        /// </summary>
        public decimal PagaShtesa
        {
            get
            {
                return pagaShtesa;
            }
            set
            {
                pagaShtesa = value;
            }
        }
        /// <summary>
        /// kthen pagen me shtesen
        /// </summary>
        public int IdGrupPunonjesish
        {
            get
            {
                return idgrupPunonjesish;
            }
            set
            {
                idgrupPunonjesish = value;
            }
        }

        public string Profesioni
        {
            get { return profesioni; }
            set { profesioni = value; }
        }

        public string GrupPunonjesish
        {
            get { return grupPunonjes; }
            set { grupPunonjes = value; }
        }

        public string TitullPune
        {
            get { return titullPune; }
            set { titullPune = value; }
        }

        /*
        IdPunesim;IdDepartament;IdNenDepartament;Detyra;NrKontrate;IdTipKontrate;DtFillimi;DtPerfundimi;Larguar;DtLargimi;Arsyeja;PeriudhaNjoftimi;NeProve;PeriudhaProve;Departament;NenDepartament;TipKontrate;IdProfesioni;IdTitullPune;PunonjesTurne;PunonjesGatishmeri;Statusi;NdryshimPozicioni;DtNenshkrimi;Shenime;DtAktivizimi;Profesioni;TitullPune;;;;IdKodeProfesione;MeKomisione
            
            */
        #endregion


        #region Metoda Publike

        public clsMesazh ruaj()
        {
            using (var data = new clsDatabazeListPagesa())
            {
                data.beginTransaksion();

                var mesazh = ruaj(data);
                if (mesazh.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
                return mesazh;
            }
        }

        /// <summary>
        /// Ruan objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajPunesim"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(clsDatabazeListPagesa data)
        {

            int id;
            clsMesazh u_ruajt = data.ruajPunesim(out id, idPunonjes, idDepartament, idNenDepartament, detyra, nrKontrate, idTipKontrate, dtFillimi, dtPerfundimi, larguar, dtLargimi, arsyeja, periudhaNjoftimi, neProve, periudhaProve, idgrupPunonjesish, idProfesioni, idTitullPune, punonjesTurne, punonjesGatishmeri, statusi, ndryshimPozicioni, dtNenshkrimi, shenime, dtAktivizimi, idPerdoruesi, meKomisione, idKodeProfesione);
            idPunesim = id;
            int idarsye = 0;
            u_ruajt = data.ruajArsyeLargimi(out idarsye, this.Arsyeja);
            if (!u_ruajt.Status)
                return new clsMesazh(false, "Gabim gjate ruatjes se arsyes se largimit!");
            return u_ruajt;
        }

        public clsMesazh modifiko(int idpunesimi)
        {
            this.IdPunesim = idpunesimi;
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            data.beginTransaksion();
            clsMesazh mesazh = modifiko(data);
            if (mesazh.Status)
                data.commitTransaksion();
            else data.rollbackTransaksion();
            return mesazh;
        }
        /// <summary>
        /// Modifikon objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoPunesim"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko(clsDatabazeListPagesa data)
        {

            clsMesazh u_modifikua = data.modifikoPunesim(idPunesim, idPunonjes, idDepartament, idNenDepartament, detyra, nrKontrate, idTipKontrate, dtFillimi, dtPerfundimi, larguar, dtLargimi, arsyeja, periudhaNjoftimi, neProve, periudhaProve, idgrupPunonjesish, idProfesioni, idTitullPune, punonjesTurne, punonjesGatishmeri, statusi, ndryshimPozicioni, dtNenshkrimi, shenime, dtAktivizimi, idPerdoruesi, meKomisione, idKodeProfesione);
            int idarsye = 0;
            u_modifikua = data.ruajArsyeLargimi(out idarsye, this.Arsyeja);
            if (!u_modifikua.Status)
                return new clsMesazh(false, "Gabim gjate ruatjes se arsyes se largimit!");
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e punesim ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.fshiPunesim"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi(clsDatabazeListPagesa data)
        {

            clsMesazh u_fshi = data.fshiPunesim(idPunesim);
            return u_fshi;

        }
        internal static string kontrollopunesim(clsPunesim punesim, clsPunesim punesimfundit, out string fushatmod, clsDatabazeListPagesa db)
        {
            string mesazh = "U modifikuan fushat per punesimin:";
            fushatmod = "U modifikuan fushat per punesimin:";
            if (punesim.Arsyeja != punesimfundit.Arsyeja && (punesim.Arsyeja != "" || punesimfundit.Arsyeja != null))
            {
                mesazh += " Arsyeja,";
                fushatmod += String.Format(" Arsyeja nga {0} ne {1},", punesimfundit.Arsyeja, punesim.Arsyeja);
            }
            if (punesim.Detyra != punesimfundit.Detyra && (punesim.Detyra != "" || punesimfundit.Detyra != null))
            {
                mesazh += " Detyra,";
                fushatmod += String.Format(" Detyra nga {0} ne {1},", punesimfundit.Detyra, punesim.Detyra);
            }
            if (punesim.DtAktivizimi.ToShortDateString() != punesimfundit.DtAktivizimi.ToShortDateString())
            {
                mesazh += " Data e aktivizimit,";
                fushatmod += String.Format(" Data e aktivizimit nga {0} ne {1},", punesimfundit.DtAktivizimi.ToShortDateString(), punesim.DtAktivizimi.ToShortDateString());
            }
            if (punesim.DtFillimi.ToShortDateString() != punesimfundit.DtFillimi.ToShortDateString())
            {
                mesazh += " Date fillimi,";
                fushatmod += String.Format(" Date fillimi nga {0} ne {1},", punesimfundit.DtFillimi.ToShortDateString(), punesim.DtFillimi.ToShortDateString());
            }
            if (punesim.DtLargimi?.ToShortDateString()!= punesimfundit.DtLargimi?.ToShortDateString())
            {
                mesazh += " Data Largimi,";
                fushatmod += String.Format(" Data Largimi nga {0} ne {1},", punesimfundit.DtLargimi.GetValueOrDefault().ToShortDateString(), punesim.DtLargimi.GetValueOrDefault().ToShortDateString());
            }
            if (punesim.DtNenshkrimi.ToShortDateString() != punesimfundit.DtNenshkrimi.ToShortDateString())
            {
                mesazh += " Data Nenshkrimi,";
                fushatmod += String.Format(" Data Nenshkrimi nga {0} ne {1},", punesimfundit.DtNenshkrimi.ToShortDateString(), punesim.DtNenshkrimi.ToShortDateString());
            }
            if (punesim.DtPerfundimi.ToShortDateString() != punesimfundit.DtPerfundimi.ToShortDateString())
            {
                mesazh += " Data Perfundimi,";
                fushatmod += String.Format(" Data Perfundimi nga {0} ne {1},", punesimfundit.DtPerfundimi.ToShortDateString(), punesim.DtPerfundimi.ToShortDateString());
            }
            if (punesim.IdDepartament != punesimfundit.IdDepartament)
            {
                mesazh += " Departamenti,";
                clsStrukturaAdministrative stvj = new clsStrukturaAdministrative(punesimfundit.IdDepartament, db);
                clsStrukturaAdministrative stri = new clsStrukturaAdministrative(punesim.IdDepartament, db);
                fushatmod += String.Format(" Departamenti nga {0} ne {1},", stvj.Emri, stri.Emri);
            }
            if (punesim.IdGrupPunonjesish != punesimfundit.IdGrupPunonjesish)
            {
                mesazh += " Grupi,";
                clsGrupPunonjesish gruvj = new clsGrupPunonjesish(punesimfundit.IdGrupPunonjesish, db);
                clsGrupPunonjesish gruri = new clsGrupPunonjesish(punesim.IdGrupPunonjesish, db);
                fushatmod += String.Format(" Grupi nga {0} ne {1},", gruvj.Nr, gruri.Nr);
            }
            if (punesim.IdNenDepartament != punesimfundit.IdNenDepartament)
            {
                mesazh += " Nendepartamenti,";
                clsStrukturaAdministrative stvj = new clsStrukturaAdministrative(punesimfundit.IdNenDepartament, db);
                clsStrukturaAdministrative stri = new clsStrukturaAdministrative(punesim.IdNenDepartament, db);
                fushatmod += String.Format(" Nendepartamenti nga {0} ne {1},", stvj.Emri, stri.Emri);
            }
            if (punesim.IdProfesioni != punesimfundit.IdProfesioni)
            {
                mesazh += " Profesioni,";
                clsProfesioneTitujPune prvj = new clsProfesioneTitujPune(punesimfundit.IdProfesioni, db);
                clsProfesioneTitujPune prri = new clsProfesioneTitujPune(punesim.IdProfesioni, db);

                fushatmod += String.Format(" Profesioni nga {0} ne {1},", prvj.Pershkrimi, prri.Pershkrimi);
            }
            if (punesim.IdTipKontrate != punesimfundit.IdTipKontrate)
            {
                mesazh += " Tip Kontrate,";
                clsTipeKontrate kovj = new clsTipeKontrate(punesimfundit.IdTipKontrate, db);
                clsTipeKontrate kori = new clsTipeKontrate(punesim.IdTipKontrate, db);
                fushatmod += String.Format(" Tip Kontrate nga {0} ne {1},", kovj.Kodi, kori.Kodi);
            }
            if (punesim.IdTitullPune != punesimfundit.IdTitullPune)
            {
                mesazh += " Pozicion pune,";
                clsProfesioneTitujPune prvj = new clsProfesioneTitujPune(punesimfundit.IdTitullPune, db);
                clsProfesioneTitujPune prri = new clsProfesioneTitujPune(punesim.IdTitullPune, db);
                fushatmod += String.Format(" Pozicion pune nga {0} ne {1},", prvj.Pershkrimi, prri.Pershkrimi);
            }
            if (punesim.IdKodeProfesione != punesimfundit.IdKodeProfesione)
            {
                mesazh += " Kode profesione,";
                clsKodeProfesione prvj = new clsKodeProfesione(punesimfundit.IdKodeProfesione, db);
                clsKodeProfesione prri = new clsKodeProfesione(punesim.IdKodeProfesione, db);
                fushatmod += String.Format(" Kode profesione pune nga {0} ne {1},", prvj.Kodi, prri.Kodi);
            }
            if (punesim.Larguar != punesimfundit.Larguar)
            {
                mesazh += " Larguar,";
                fushatmod += String.Format(" Larguar nga {0} ne {1},", punesimfundit.Larguar, punesim.Larguar);
            }
            if (punesim.MeKomisione != punesimfundit.MeKomisione)
            {
                mesazh += " Me Komisione,";
                fushatmod += String.Format(" Me Komisione nga {0} ne {1},", punesimfundit.Larguar, punesim.Larguar);
            }
            if (punesim.NdryshimPozicioni != punesimfundit.NdryshimPozicioni)
            {
                mesazh += " Ndryshim pozicioni,";
                string npvj = merrPershkrimNdryshimPozicioni(punesimfundit.NdryshimPozicioni, db);
                string npri = merrPershkrimNdryshimPozicioni(punesim.NdryshimPozicioni, db);
                if(npvj != npri)
                    fushatmod += String.Format(" Ndryshim pozicioni nga {0} ne {1},", npvj, npri);
            }
            if (punesim.NeProve != punesimfundit.NeProve)
            {
                mesazh += " Ne prove,";
                fushatmod += String.Format(" Ne prove nga {0} ne {1},", punesimfundit.NeProve, punesim.NeProve);
            }
            if (punesim.NrKontrate != punesimfundit.NrKontrate && (punesim.NrKontrate != "" || punesimfundit.NrKontrate != null))
            {
                mesazh += " Nr Kontrate,";
                fushatmod += String.Format(" Nr Kontrate nga {0} ne {1},", punesimfundit.NrKontrate, punesim.NrKontrate);
            }
            if (punesim.PeriudhaNjoftimi != punesimfundit.PeriudhaNjoftimi && (punesim.PeriudhaNjoftimi != "" || punesimfundit.PeriudhaNjoftimi != null))
            {
                mesazh += " Periudha Njoftimi,";
                fushatmod += String.Format(" Periudha Njoftimi nga {0} ne {1},", punesimfundit.PeriudhaNjoftimi, punesim.PeriudhaNjoftimi);
            }
            if (punesim.PeriudhaProve != punesimfundit.PeriudhaProve && (punesim.PeriudhaProve != "" || punesimfundit.PeriudhaProve != null))
            {
                mesazh += " Periudha porve,";
                fushatmod += String.Format(" Periudha porve nga {0} ne {1},", punesimfundit.PeriudhaProve, punesim.PeriudhaProve);
            }
            if (punesim.PunonjesGatishmeri != punesimfundit.PunonjesGatishmeri)
            {
                mesazh += " Punonjes gatishmeri,";
                fushatmod += String.Format(" Punonjes gatishmeri nga {0} ne {1},", punesimfundit.PunonjesGatishmeri, punesim.PunonjesGatishmeri);
            }
            if (punesim.PunonjesTurne != punesimfundit.PunonjesTurne)
            {
                mesazh += " Punonjes turne,";
                fushatmod += String.Format(" Punonjes turne nga {0} ne {1},", punesimfundit.PunonjesTurne, punesim.PunonjesTurne);
            }
            if (punesim.Shenime != punesimfundit.Shenime && (punesim.Shenime != "" || punesimfundit.Shenime != null))
            {
                mesazh += " Shenime,";
                fushatmod += String.Format(" Shenime nga {0} ne {1},", punesimfundit.Shenime, punesim.Shenime);
            }
            if (punesim.Statusi != punesimfundit.Statusi)
            {
                mesazh += " Roli,";
                string stvj, stri;
                if (punesimfundit.Statusi == 1)
                    stvj = "Shef departamenti";
                else stvj = "Punonjes";
                if (punesim.Statusi == 1)
                    stri = "Shef departamenti";
                else stri = "Punonjes";
                if(stvj != stri)
                    fushatmod += String.Format(" Roli nga {0} ne {1},", stvj, stri);
            }
            if (mesazh.Substring(mesazh.Length - 1, 1) == ",")
            {
                mesazh = mesazh.Substring(0, mesazh.Length - 1) + ".";
                fushatmod = fushatmod.Substring(0, fushatmod.Length - 1) + ".";
            }
            return mesazh;
        }
        public static string merrPershkrimNdryshimPozicioni(int id, clsDatabazeListPagesa db)
        {
            return db.merrNdryshimPozicioniSipasID(id);
        }
        public static int merrIdNdryshimPozicioni(string pershkrimi)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                return db.merrNdryshimPozicioniSipasPershkrimit(pershkrimi);
        }
        /// <summary>
        /// merr punesim sipas id
        /// </summary>
     
        public static bool ekzistonPunesimPerKetePunonjeMeKeteDateAktivizimi(string nrpersonal, DateTime dtaktivizimi, out int idpunesimi)
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                return data.ekzistonPunesimPerKetePunonjeMeKeteDateAktivizimi(nrpersonal, dtaktivizimi, out idpunesimi);
            }
        }
        public static DataTable merrArsye()
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                return data.ktheArsye();
            }
        }
        #endregion

        public bool merrPunesimFundit(int idpunonjes, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {

                db.kthePunesimSipasIdPuneonjesiTeFundit(idpunonjes, data, this);
            }
            return true;

        }
        public bool merrPunesimParaFundit(int idpunonjes, DateTime data)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {

                db.kthePunesimSipasIdPuneonjesiTeParaFundit(idpunonjes, data, this);
            }
            return true;

        }
        #region Metoda Internal


        public void Mbush(IDataRecord dbDataRowPunesim)
        {
            try
            {

                Converter.ParseExact(dbDataRowPunesim["IDPUNESIM"].ToString(), out idPunesim, "idpunesim");
                Converter.ParseExact(dbDataRowPunesim["IDPUNONJES"].ToString(), out idPunonjes, "idPunonjes");
                Converter.Parse(dbDataRowPunesim["IDDEPARTAMENT"].ToString(), out idDepartament, "idDepartament");
                Converter.Parse(dbDataRowPunesim["IDNENDEPARTAMENT"].ToString(), out idNenDepartament, "idNenDepartament");
                departament = dbDataRowPunesim["Departament"].ToString();
                nenDepartament = dbDataRowPunesim["NenDepartament"].ToString();
                detyra = dbDataRowPunesim["DETYRA"].ToString();
                nrKontrate = dbDataRowPunesim["NRKONTRATE"].ToString();
                Converter.Parse(dbDataRowPunesim["IDTIPKONTRATE"].ToString(), out idTipKontrate, "idTipKontrate");
                Converter.Parse(dbDataRowPunesim["DTFILLIMI"].ToString(), out dtFillimi, "dtFillimi");
                Converter.Parse(dbDataRowPunesim["DTPERFUNDIMI"].ToString(), out dtPerfundimi, "dtPerfundimi");
                Converter.Parse(dbDataRowPunesim["LARGUAR"].ToString(), out larguar, "larguar");
                DateTime dtLargimi;
                Converter.Parse(dbDataRowPunesim["DTLARGIMI"].ToString(), out dtLargimi, "dtLargimi");
                DtLargimi = dtLargimi;
                arsyeja = dbDataRowPunesim["ARSYEJA"].ToString();
                periudhaNjoftimi = dbDataRowPunesim["PERIUDHANJOFTIMI"].ToString();
                periudhaProve = dbDataRowPunesim["PERIUDHAPROVE"].ToString();
                Converter.Parse(dbDataRowPunesim["NEPROVE"].ToString(), out neProve, "neProve");
                tipKontrate = dbDataRowPunesim["TipKontrate"].ToString();
                Converter.Parse(dbDataRowPunesim["IDGRUPPUNONJESISH"].ToString(), out idgrupPunonjesish, "idgrupPunonjes");
                Converter.Parse(dbDataRowPunesim["PagaShtesa"].ToString(), out pagaShtesa, "pagaShtesa");
                Converter.Parse(dbDataRowPunesim["IDPROFESIONI"].ToString(), out idProfesioni, "idProfesioni");
                Converter.Parse(dbDataRowPunesim["IDTITULLPUNE"].ToString(), out idTitullPune, "idTitullPune");
                Converter.Parse(dbDataRowPunesim["PUNONJESTURNE"].ToString(), out punonjesTurne, "punonjesTurne");
                Converter.Parse(dbDataRowPunesim["PUNONJESGATISHMERI"].ToString(), out punonjesGatishmeri, "punonjesGatishmeri");
                Converter.Parse(dbDataRowPunesim["STATUSI"].ToString(), out statusi, "statusi");
                Converter.Parse(dbDataRowPunesim["NDRYSHIMPOZICIONI"].ToString(), out ndryshimPozicioni, "ndryshimPozicioni");
                Converter.Parse(dbDataRowPunesim["DTNENSHKRIMI"].ToString(), out dtNenshkrimi, "dtNenshkrimi");
                shenime = dbDataRowPunesim["SHENIME"].ToString();
                Converter.Parse(dbDataRowPunesim["DTAKTIVIZIMI"].ToString(), out dtAktivizimi, "dtAktivizimi");
                Converter.Parse(dbDataRowPunesim["IDPERDORUESI"].ToString(), out idPerdoruesi, "idPerdoruesi");
                Converter.Parse(dbDataRowPunesim["MEKOMISIONE"].ToString(), out meKomisione, "meKomisione");
                Converter.Parse(dbDataRowPunesim["IDKODEPROFESIONE"].ToString(), out idKodeProfesione, "idKodeProfesione");
                kodeProfesione = dbDataRowPunesim["KODEPROFESIONE"].ToString();

            }
            catch (MyWarnException warn)
            {
                logu.Warn("gabim ne mbushjen e punesimit per punonjesin :{0} {1}", IdPunonjes, warn.Message);
            }
            catch (MyException myex)
            {
                throw new MyException(logu, "gabim ne mbushjen e punesimit per punonjesin :{0} {1}", IdPunonjes, myex.Message);
            }
        }
        internal static clsPunesim Krijo(IDataRecord dbDataRowPunesim)
        {
            var punesim = new clsPunesim();
            punesim.Mbush(dbDataRowPunesim);
            return punesim;
        }

        internal static clsPunesim KrijoPerGride(IDataRecord record)
        {
            var punesim = Krijo(record);
            punesim.profesioni = record["Profesioni"].ToString();
            punesim.grupPunonjes = record["GrupPunonjesish"].ToString();
            punesim.titullPune = record["TitullPune"].ToString();
            return punesim;
        }

        #endregion
    }
}
