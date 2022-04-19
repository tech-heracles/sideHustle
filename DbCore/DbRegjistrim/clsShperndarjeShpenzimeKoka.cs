using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbAsete;
using System.Resources;
using System.Globalization;
using DbCore.IMBUtils.Messages;
using DbCore.DbShare;
using Newtonsoft.Json;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti   shperndarje shpenzimesh
    ///  (Te dhenat  merren nga tabela : T_SHPERNDARJESHPENZIMEKOKA)
    /// </summary>
    public class clsShperndarjeShpenzimeKoka
    {
        #region Atribute

        private int idKokaShperndarjeShpenz;
        private String nrDok;
        private DateTime dtDok;
        private DateTime dtRegjistrimi;
        private string shenime;
        private double vleraTotale;
        private int idStatusDok;
        private int idNdermarje;
        private int idNdermarjeVit;
        private int idPerdoruesi;
        private int idNivel;
        private int idKonfigAmbjente;
        private int idDokNga;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;

        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colShperndarjeShpenzimeTrupi oColTrupi;
        private colShperndarjeShpenzimeFaturat oColFaturat;
        public colKokatFletetKontabel oColFletetKontabel;
        public colShperndarjeShpenzimeLlogarite oColLlogarite;
        private colKokaMagazina oKokaMag;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit</param>
        /// <param name="dtRegj"> data e regjistrimit</param>
        /// <param name="id"> id ritese</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idstatusdok"> id e gjendjes se dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim">shenim</param>
        /// <param name="vl"> vlefta e dokumentit</param>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="idKonfigAmbjente">id e konfigurimit te ambjentit</param>
        /// <param name="idDokNga"> id e dok me ke eshte lidhur</param>
        /// <param name="idNivelGjenerues">id e nivelit gjenerues</param>
        /// <param name="idKonfigGjenerues">id e konfigrurimit gjenerues</param>
        /// <param name="idGjenerues"> id e gjeneruesit</param>
        public clsShperndarjeShpenzimeKoka(int id, string nrDk, DateTime dtDk, DateTime dtRegj, string shenim, double vl, int idstatusdok,
            int idnderm, int idndermvit, int idperdoruesi, int idNivel, int idKonfigAmbjente, int idDokNga, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues)
        {
            idKokaShperndarjeShpenz = id;
            nrDok = nrDk;
            dtDok = dtDk;
            dtRegjistrimi = dtRegj;
            shenime = shenim;
            vleraTotale = vl;
            idStatusDok = idstatusdok;
            idNdermarje = idnderm;
            idNdermarjeVit = idndermvit;
            idPerdoruesi = idperdoruesi;
            this.idNivel = idNivel;
            this.idKonfigAmbjente = idKonfigAmbjente;
            this.idDokNga = idDokNga;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;

            oColTrupi = new colShperndarjeShpenzimeTrupi();
            oColFaturat = new colShperndarjeShpenzimeFaturat();
            oColLlogarite = new colShperndarjeShpenzimeLlogarite();
        }

        /// <summary>
        /// nderton objektin nga db-ja me id
        /// </summary>
        /// <param name="idShperndarjeShpenzimeshKoka">id e kokes se shperndarjeve te shpenzimeve</param>
        public clsShperndarjeShpenzimeKoka(int idShperndarjeShpenzimeshKoka)
            : this(new clsDatabaseRegjistrim(), idShperndarjeShpenzimeshKoka)
        {
            //ne kete menyre therrasim konstruktorin tjeter po me clsDatabaseRegjistrim te inizializuar            
        }
        public clsShperndarjeShpenzimeKoka(clsDatabaseRegjistrim dbShperndShpenKoka, int idShperndarjeShpenzimeshKoka)
        {
            mbushShperndarjeShpenzKok(dbShperndShpenKoka.ktheShperndarjeShpenzimeshKokaSipasID(idShperndarjeShpenzimeshKoka));
        }
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsShperndarjeShpenzimeKoka()
        {
        }

        public clsShperndarjeShpenzimeKoka(DataRow rreshti)
        {
            
            mbushShperndarjeShpenzKok(rreshti);
        }

        #endregion


        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKokaShperndarjeShpenz
        {
            get { return idKokaShperndarjeShpenz; }
            set { idKokaShperndarjeShpenz = value; }
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
        /// Kthen/Vendos dt e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos dt e regjistrimit.
        /// </summary>
        public DateTime DtRegjistrimi
        {
            get { return dtRegjistrimi; }
            set { dtRegjistrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenime.
        /// </summary>
        public String Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlera totale e dokumentit.
        /// </summary>
        public double VleraTotale
        {
            get { return vleraTotale; }
            set { vleraTotale = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit.
        /// <example> ruajtur , draft etj</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit
        /// </summary>
        public int IdNdermarrjeVit
        {
            get { return idNdermarjeVit; }
            set { idNdermarjeVit = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe ka kryer veprimin.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e Konfigurimit te ambjentit
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe e ka gjeneruar ne rastet e modifikimit.
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit qe e ka gjeneruar.
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit qe e ka gjeneruar
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe e ka gjeneruar nga nje ambjent tjeter.
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        /// <summary>
        /// Kthen/Vendos koleksionin me trupin e dokumentit.
        /// </summary>
        public colShperndarjeShpenzimeTrupi OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos  koleksionin me faturat e shperndarje shpenzimesh.
        /// </summary>
        public colShperndarjeShpenzimeFaturat OColFaturat
        {
            get { return oColFaturat; }
            set { oColFaturat = value; }
        }

        /// <summary>
        /// Kthen/Vendos collectionin me flete kontabel  qe gjenerohet kur dokumenti kontabilizohet.
        /// </summary>
        public colKokatFletetKontabel OColFletetKontabel
        {
            get { return oColFletetKontabel; }
            set { oColFletetKontabel = value; }
        }
        /// <summary>
        /// Kthen/Vendos collectionin me flete kontabel  qe gjenerohet kur dokumenti kontabilizohet.
        /// </summary>
        public colKokaMagazina OColKokaMag
        {
            get { return oKokaMag; }
            set { oKokaMag = value; }
        }

        public colShperndarjeShpenzimeLlogarite OColLlogarite
        {
            get { return oColLlogarite; }
            set { oColLlogarite = value; }
        }
        #endregion

        #region Metoda Publike
        public clsMesazh krijoShperndarjeShpenzimesh(string nrDk, DateTime dtDk, DateTime dtRegj, string shenim, double vl, int idstatusdok,
            int idnderm, int idndermvit, int idperdoruesi, int idNivel, int idKonfigAmbjente, int idDokNga, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colShperndarjeShpenzimeTrupi trupi, colShperndarjeShpenzimeFaturat faturat, colShperndarjeShpenzimeLlogarite llog, DbShare.clsKonfigurimAmbjenti konfmag, bool meKontabilizim, out string shfaqmesazhapolupe, int idkokaeksistuese, bool gjithmone, ResourceManager rm, CultureInfo ci)
        {

            nrDok = nrDk;
            dtDok = dtDk;
            dtRegjistrimi = dtRegj;
            shenime = shenim;
            vleraTotale = vl;
            idStatusDok = idstatusdok;
            idNdermarje = idnderm;
            idNdermarjeVit = idndermvit;
            idPerdoruesi = idperdoruesi;
            this.idNivel = idNivel;
            this.idKonfigAmbjente = idKonfigAmbjente;
            this.idDokNga = idDokNga;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            oKokaMag = new colKokaMagazina();
            OColFletetKontabel = new colKokatFletetKontabel();
            oColTrupi = trupi;
            oColFaturat = faturat;
            oColLlogarite = llog;

            clsMesazh mesazh = krijoMagazineNgaShperndarjeShpenzimesh(this, konfmag, meKontabilizim, out shfaqmesazhapolupe, idkokaeksistuese, gjithmone);
            if (!mesazh.Status)
                return mesazh;


            return new clsMesazh(true, "Shpërndarja e shpenzimeve u krijua me sukses!");
        }



        /// <summary>
        /// Ruan nje objekt dokumenti shperndarje shpenzimesh sebashku me trupin,faturat  dhe kontabilitetin perkates
        /// Nje objekt koka dokumenti shperndarje shpenzimesh ka nje koleksion me trupin e dokumentit,faturat  dhe kontabilitetin perkates , 
        /// ruajtja e nje dokumenti shperndarje shpenzimesh imponon ruajtjen edhe te nje colection-i me trupin,faturat dhe kontabilitetin kur kontabilizohet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit shperndarje shpenzimesh bashke me trupin,faturat dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje  dokumenti shperndarje shpenzimesh sebashku me trupin,faturat dhe kontabilitetin perkates

        /// </summary>
        /// <param name="dtDk"> data e dokumentit</param>
        /// <param name="dtRegj"> data e regjistrimit</param>
        /// <param name="id"> id ritese</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idstatusdok"> id e gjendjes se dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim">shenim</param>
        /// <param name="vl"> vlefta e dokumentit</param>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="idKonfigAmbjente">id e konfigurimit te ambjentit</param>
        /// <param name="idDokNga"> id e dok me ke eshte lidhur</param>
        /// <param name="idNivelGjenerues">id e nivelit gjenerues</param>
        /// <param name="idKonfigGjenerues">id e konfigrurimit gjenerues</param>
        /// <param name="idGjenerues"> id e gjeneruesit</param>
        /// <param name="oColTrupi">koleksion trupi</param>
        /// <param name="oColFaturat">koleksion Faturash</param>
        /// <param name="oColFletetKontabel">koleksion Fletesh kontabel</param>
        ///<param name="meKontabilizim"> tregon nese dokumenti i shperndarje shpenzimesh do te kontabilizohet apo jo</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajShperndarjeShpenzimesh(out int id, string nrDk, DateTime dtDk, DateTime dtRegj, string shenim, double vl, int idstatusdok,
            int idnderm, int idndermvit, int idNivel, int idKonfigAmbjente, int idDokNga, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues,
            colShperndarjeShpenzimeTrupi oColTrupi, colShperndarjeShpenzimeFaturat oColFaturat, colKokatFletetKontabel oColFletetKontabel,
            colKokaMagazina oColKokaMag, bool meKontabilizim, bool modifikim, int idperdoruesi, colShperndarjeShpenzimeLlogarite oColLlogarite, DbShare.clsKonfigurimAmbjenti konfamortizimi, colAmortizimiKoka colAmortizimetEVjetra)
        {
            //transaksioni per te ruajtur             
            clsMesazh mesazh;
            id = 0;
            DbCore.clsMesazh kontMesazh;
            clsDatabaseRegjistrim dbRegj = new DbRegjistrim.clsDatabaseRegjistrim();
            DbAsete.clsDatabazeAsete dbasete = new DbAsete.clsDatabazeAsete( );
            try
            {
                if (!modifikim && ekzistonDokumentShperndarjeShpenzimi(nrDk, dtDk, idnderm, idndermvit, dbRegj))
                    return new clsMesazh(false, MessagesResource.Messages["msgEkziston1DokShperndarjeShpenzimeshMeKetoTeDhena"]);
                id = dbRegj.ruajShperndarjeShpenzimeshKoka(id, nrDk, dtDk, dtRegj, shenim, vl, idstatusdok, idnderm, idndermvit, idNivel, idKonfigAmbjente, idDokNga, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idperdoruesi);
                if (id == 0)
                    return new clsMesazh(false, MessagesResource.Messages["msgNdodhi1GabimGjateRuajtjesSseKokesSeDokTeShperndarjes"]);
                foreach (clsShperndarjeShpenzimeTrupi o in oColTrupi)
                {
                    o.IdKoka = id;
                    mesazh = o.ruajShperndarjeShpenzimeshTrupinDheFaturat(o.IdTrupi, o.IdKoka, o.IdFatura, o.NrDok, o.DtDok, o.LlojDok, o.OColTrupiFaturat, dbRegj);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                foreach (clsShperndarjeShpenzimeLlogarite o in oColLlogarite)
                {
                    o.IdKoka = id;
                    mesazh = dbRegj.ruajShperndarjeShpenzimeshLlogari(o.IdShperndarjeShpenzimeLLog, o.IdKoka, o.IdLlogari, o.Vlefta);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                foreach (clsShperndarjeShpenzimeFaturat o in oColFaturat)
                {
                    o.IdKoka = id;
                    int idT;

                    mesazh = dbRegj.ruajShperndarjeShpenzimeshFaturat(out idT, o.IdKoka, o.IdFatura);
                    o.IdTrupi = idT;
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }

                int index = 0;
                string shfaqmesazh = "jo";
                string mesazhmevonshem = "";
                foreach (clsKokaMagazina mag in oColKokaMag)
                {

                    mag.IdGjenerues = id;
                    clsShperndarjeShpenzimeFaturat shpernd = oColFaturat[index];
                    clsShperndarjeShpenzimeTrupi shperndtrup = oColTrupi[index];

                    mesazh = ruajMagazine(dbRegj, mag, shperndtrup);

                    index++;

                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                    if (konfamortizimi.IdKonfigAmbjente > 0)
                    {
                        List<clsShperndarjeShpenzimeTrupi> trupPerKeteMag = oColTrupi.FindAll(x => x.DtDok == mag.DtDok);
                        DbAsete.colAmortizimiKoka kokavjeter = new DbAsete.colAmortizimiKoka();
                        List<clsAmortizimiKoka> kam = colAmortizimetEVjetra.FindAll(x => x.IdDokGjenerues == mag.IdDokNga);
                        kokavjeter.AddRange(kam);
                        colAmortizimiKoka col = new colAmortizimiKoka();
                        colSerialetMagazine colseriale = new colSerialetMagazine();
                        mesazh = col.krijoAmortizimeKokaShperndarjeShpenzimesh(mag, konfamortizimi, trupPerKeteMag, kokavjeter, colseriale);
                        if (!mesazh.Status)
                            return mesazh;
                        if (col.Count > 0)
                        {
                            mesazh = col.ruajListAmortizime(mag.IdKokaMagazina, 0, out shfaqmesazh, kokavjeter, 0, 86, new colSerialetMagazine(), false, null, modifikim, out mesazhmevonshem);
                            if (!mesazh.Status)
                                return mesazh;
                        }
                        if (colseriale.Count > 0)
                        {
                            foreach (DbCore.DbAsete.clsSerialetMagazine s in colseriale)
                            {
                                s.IdDok = mag.IdKokaMagazina;
                                s.IdStatusDokumenti = mag.IdStatusDok;
                                s.IdKonfigAmbjenti = mag.IdKonfigAmbjente;

                                mesazh = s.ruaj();
                                if (!mesazh.Status)
                                {
                                    return mesazh;
                                }
                                clsAQTSeriale serial = new clsAQTSeriale();
                                serial.merrAQTSerialSipasID(s.IdAQTSeriali, dbasete);

                                if (serial.IdHistorikAktualPaSerial > 0)//nqs kemi seriale te ndashem kalojme id e dokumentit te magazines dhe sasine dhe cmimin e ketij seriali
                                {
                                    clsHistorikAQTSeriale historik = new clsHistorikAQTSeriale();
                                    historik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, mag.IdNdermarrje, dbasete);
                                    mesazh = dbasete.modifikoHistorikAQTSerial(serial.IdHistorikAktualPaSerial, mag.IdPerdoruesi, mag.IdKokaMagazina, historik.SasiaProgresive, s.Cmimi, s.Cmimi * historik.SasiaProgresive, 1);
                                    if (!mesazh.Status)
                                    {
                                        return mesazh;
                                    }
                                }
                            }
                        }
                    }
                }

                if (meKontabilizim == true)
                {
                    foreach (clsKokaFleteKontabel kokaFK in oColFletetKontabel)
                    {

                        kokaFK.IdGjenerues = id;
                        kontMesazh = kokaFK.Ruaj(new clsDatabaseKontabilitet(dbRegj));
                        if (!kontMesazh.Status)
                            return new clsMesazh(kontMesazh.Status, kontMesazh.PershkrimMesazhi);
                    }

                }
                return new clsMesazh(true, MessagesResource.Messages["msgRuajtjeMeSukses"]);
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Ruan nje objekt dokumenti magazine sebashku me trupin , dhe kontabilitetin perkates ne rastet kur ky dokument eshte i lidhur me nje dokument shitje
        /// Nje objekt koka dokumenti magazine ka nje koleksion me trupin e dokumentit  dhe kontabilitetin perkates , 
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin dhe kontabilitetin kur kontabilizohet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti magazine sebashku me trupin dhe kontabilitetin perkates
        /// ne rastet kur dokumenti i magazines eshte dokument dalje behet kontrolli nese magazina ka gjendje per kete dalje apo jo
        /// </summary>
        ///<param name="koka"> koka e dokumentit te magazines qe do ruhet</param>
        ///<param name="shitje_blerje"> tregon nese dokumenti eshte dokument shitje apo blerje</param>
        ///<example> true- shitje, false-blerje</example>
        ///<param name="eshteTrasferim"> tregon nese dokumenti eshte transferim midis magazinave</param>
        ///<param name="idkonfigurimshitje"> id e konfigurimit te shitjes e cila sherben per te marre id e konfigurimit te magazines te lidhur me kete konfigurim dokumenti shitje</param>
        ///<param name="kokashitje">koka e dokumentit te shitjes</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajMagazine(clsDatabaseRegjistrim dbRegj, clsKokaMagazina koka, clsShperndarjeShpenzimeTrupi clsTrupi)
        {

            clsMesazh mesazh;
            int idDokMagazineVjeter = clsTrupi.IdFatura;
            clsKokaMagazina kokaMagVjeter = new clsKokaMagazina();
            kokaMagVjeter.mbushKokaMagazinaSipasID(idDokMagazineVjeter, dbRegj);
            kokaMagVjeter.mbushTrupMagazine(dbRegj);
            //koka.IdRenditjes = dbRegj.vendosIdRenditjesSS(koka.IdRenditjes, koka.IdMagazina, koka.DtDok); si ishte
            koka.IdRenditjes = dbRegj.vendosIdRenditjesSS_SipasFH(koka.IdRenditjes, kokaMagVjeter.IdKokaMagazina);

            koka.Vlefta = 0;

            foreach (clsTrupiMagazina o in koka.OcolTrupiMagazina)
            {
                koka.Vlefta += o.Vlefta;
            }
            bool klientFiskalizuar = false;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                klientFiskalizuar = true;
            koka.IdKokaMagazina = dbRegj.ruajKokaMagazina(koka.IdKokaMagazina, koka.IdNivel, koka.IdKonfigAmbjente, koka.IdKlientFurnitor, koka.IdMagazina, koka.DtDok, koka.NrDok, koka.IdProjekt,
                koka.NrProjekt, koka.IdKategoria, koka.IdDokNga, koka.Vlefta, koka.IdStatusDok, koka.IdNdermarrje, koka.IdNdermarrjeVit, koka.IdPerdoruesi, koka.DtRegjistrimi,
                koka.IdLlojDokumentiMagazine, koka.Shenime, koka.IdRenditjes, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, koka.IdGjenerues, koka.IdDegeAdministrative, koka.IdLlogari, koka.IdNjesiVartese, koka.MeKonfirmim, koka.IdGrup1, koka.IdGrup2, koka.IdGrup3, "", "", "", koka.IdAutomjet, koka.IdRaportDesing, koka.IdKrijuesi, koka.DtTransporti, koka.Shoferi, koka.TargaShoferi,koka.IdKategoriSeriali, koka.NrSerial, koka.NIVFSH, koka.WTNIC, koka.IdOperator
                ,koka.MallraTeDjeghsme,koka.ShoqerimIKerkuar,koka.Transportuesi,klientFiskalizuar,koka.Tipi,koka.Transaksioni,clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
            if (koka.IdKokaMagazina == 0)
                return new clsMesazh(false, "Ndodhi një gabim gjatë ruajtes së kokës së magazinës");
            int indexTrupiVjeter = 0;
            foreach (clsTrupiMagazina o in koka.OcolTrupiMagazina)
            {//behet ruajtja e trupit te magazines
                o.IdKokaMagazina = koka.IdKokaMagazina;
                o.IdStatusDok = koka.IdStatusDok;
                int idTrupiMagazinaVjeter = o.IdTrupiMagazina;
                o.IdRenditjes = dbRegj.vendosIdRenditjesTrupiSS_New(0, idTrupiMagazinaVjeter, koka.IdNdermarrje, o.IdKokaMagazina);
                indexTrupiVjeter++;
                int idM;
                mesazh = dbRegj.ruajTrupiMagazina(out idM, o.IdKokaMagazina, o.IdLlojVeprimi, o.IdArtikulli, o.IdNjesia, o.Sasia, o.Vlefta, o.Koeficenti, o.Shenja,
                        o.SasiProgresive, o.VleftaProgresive, o.IdMag, o.Data, o.IdStatusDok, o.IdRenditjes, o.IdDetajimi, o.SasiProgresive, o.VlefteProgresiveDetajimi, o.IdDetajimi2, o.IdTrupiRezervimi, o.IdTrupiKonvertimFSH, o.IdTrupiKonvertimUSH, o.IdTrupiKonvertimUD, o.IdKthimi, o.IdTrupiShitjeGjenerimi, o.Shenime,o.IdArtikullSet, o.IdBarkodi);
                if (!mesazh.Status)
                    return mesazh;
            }
            return new clsMesazh(true, "Ruajtja përfundoi me sukses!");
        }

        public clsMesazh krijoMagazineNgaShperndarjeShpenzimesh(clsShperndarjeShpenzimeKoka koka, DbShare.clsKonfigurimAmbjenti konfmag, bool mekontabilizim, out string shfaqmesazhapolupe, int idkokaeksistuese, bool gjithmone)
        {
            shfaqmesazhapolupe = "jo";
            double shumallog = 0;
            foreach (clsShperndarjeShpenzimeLlogarite llog in koka.OColLlogarite)
            {
                shumallog += llog.Vlefta;
            }

            colKokaMagazina kokatMagGjeneruar = new colKokaMagazina();
            kokatMagGjeneruar.mbushKokaMagazinaSipasIDGjenerues(IdKonfigAmbjente, idkokaeksistuese);

            clsMesazh mesazh = new clsMesazh();
            DbAdmin.clsDatabaseAdmin data = new DbAdmin.clsDatabaseAdmin();
            foreach (clsShperndarjeShpenzimeTrupi t in koka.OColTrupi)
            {
                if (((IEnumerable<clsKokaMagazina>)from k in koka.OColKokaMag where k.DtDok == t.DtDok select k).Count() > 0)
                    continue;
                string shenime = "";
                if (koka.Shenime != String.Empty) shenime = koka.Shenime;
                else
                {
                    shenime = "Nga shpërndarje shpenzimesh";
                }
                colTrupiMagazina coltrupi;
                clsKokaMagazina magek = new clsKokaMagazina();
                magek.mbushKokaMagazinaSipasID(t.IdFatura);
                if (magek.IdKokaMagazina < 1)
                    return new MesazhGabimi($"Dokumenti me numer {t.NrDok} nuk ekziston.");

                coltrupi = ruajTrupinMagazines(koka.OColTrupi, t.DtDok);
                int magazina = 0;
                if (coltrupi.Count > 0)
                    magazina = coltrupi[0].IdMag;
                bool magnjejte = true; double shuma = 0;
                foreach (clsTrupiMagazina tt in coltrupi)
                {
                    shuma += tt.Vlefta;
                    if (magazina != tt.IdMag)
                        magnjejte = false;
                }
                if (!magnjejte)
                    magazina = 0;
                clsNjesiAdministrative njesi = new clsNjesiAdministrative(magazina);
                if (magazina == 0)
                    njesi.Kodi = "";
                clsDegeAdministrative dege = new clsDegeAdministrative(magek.IdDegeAdministrative);
                if (magek.IdDegeAdministrative == 0)
                    dege.Kodi = "";
                clsKokaMagazina kokaMagGjeneruar = kokatMagGjeneruar.FirstOrDefault(x => x.DtDok == t.DtDok);
                clsKokaMagazina kokamg = new clsKokaMagazina();//null
                mesazh = kokamg.krijoMagazine(false, kokaMagGjeneruar != null ? kokaMagGjeneruar.IdKokaMagazina : 0, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, "", magazina, njesi.Kodi, t.DtDok, koka.NrDok, 1, "", konfmag.IdKategori, 0, 0, koka.IdStatusDok, koka.IdNdermarrje, magek.IdNdermarrjeVit, koka.IdPerdoruesi, koka.DtRegjistrimi, 1, shenime, koka.idNivel, koka.idKonfigAmbjente, koka.idKokaShperndarjeShpenz, magek.IdDegeAdministrative, dege.Kodi, 0, "", 0, "", false, 0, 0, 0, "", "", "", coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), new clsKokaRezervime(), new clsDatabaseRegjistrim(), true, 0, "", 0, true, koka.IdPerdoruesi, koka.dtDok, 0, "", string.Empty, string.Empty, 0, null,false,false,"","",0);
                if (mesazh.Status)
                    koka.OColKokaMag.Add(kokamg);
                else return mesazh;

                string pershkrimi = "";
                if (koka.Shenime != String.Empty)
                {
                    pershkrimi = koka.Shenime;
                }
                else
                {
                    pershkrimi = "Nga shpërndarja e shpenzimeve";
                }
                if (mekontabilizim)
                {
                    DbAdmin.clsPeriudhaKontabel periudha = new DbAdmin.clsPeriudhaKontabel(kokamg.DtDok, kokamg.IdNdermarrje, data);
                    try
                    {
                        DbQendraKosto.colObjektivaKosto objektivat;
                        List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                        List<int> idllogobj;
                        string shfaqmesazh = "jo";
                        DbQendraKosto.clsKokaQendraKosto kokaqendra1 = new DbQendraKosto.clsKokaQendraKosto();
                        DbKontabiliteti.colKokatFletetKontabel kokaFleteKontabel = new colKokatFletetKontabel(idkokaeksistuese, 7);
                        for (int i = 0; i < kokaFleteKontabel.Count; i++)
                        {
                            if (kokaFleteKontabel[i].NrDukumentiKokaFleteKontabel == koka.nrDok)
                                kokaqendra1.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokaFleteKontabel[i].IdKokaFleteKontabel, kokaFleteKontabel[i].IdKonfigAmbjente);
                        }
                        koka.oColFletetKontabel.Add(DbKontabiliteti.clsKokaFleteKontabel.GjeneroKontabilizimMagazine(kokamg.IdKokaMagazina, koka.IdNivel, koka.IdKonfigAmbjente, kokamg.DtDok, koka.NrDok, shuma, koka.IdNdermarrje, koka.IdNdermarrjeVit, koka.IdPerdoruesi, koka.DtRegjistrimi, kokamg.OcolTrupiMagazina, pershkrimi, idDokNga, 39, periudha.IdPeriudha, 0, 0, false, koka.OColLlogarite, shumallog, 7, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, kokamg.IdDegeAdministrative, 0, 0, out shfaqmesazh, kokaqendra1.IdKoka, kokaqendra1.ColTrupi, new colTrupiMagazina(), new DbAsete.colAmortizimiKoka(), gjithmone, new clsDatabaseKontabilitet(), kokamg.IdMagazina));
                        if (shfaqmesazh != "jo")
                            shfaqmesazhapolupe = shfaqmesazh;
                    }
                    catch (Exception ex)
                    {
                        return new clsMesazh(false, ex.Message);
                    }

                }
            }
            data.Dispose();
            return mesazh;
        }


        private colTrupiMagazina ruajTrupinMagazines(colShperndarjeShpenzimeTrupi trupish, DateTime date)
        {

            colTrupiMagazina trupat = new colTrupiMagazina();
            clsTrupiMagazina trupi;

            for (int i = 0; i < trupish.Count; i++)
            {
                if (trupish[i].DtDok == date)
                {

                    for (int j = 0; j < trupish[i].OColTrupiFaturat.Count; j++)
                    {
                        trupi = new clsTrupiMagazina(trupish[i].OColTrupiFaturat[j].IdTrupiShitje);
                        trupi.Sasia = 0;
                        trupi.Vlefta = trupish[i].OColTrupiFaturat[j].Vlera;
                        DbCore.DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(trupi.IdArtikulli);
                        trupi.Element = art;
                        trupat.Add(trupi);
                    }
                }
            }
            return trupat;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te shperndarje shpenzimesh ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka.kontrolloShperndarjeShpenzimesh"/> 
        /// dhe pastaj funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka.ruajShperndarjeShpenzimesh"/> 
        /// </summary>
        /// <param name="meKontabilizim"></param>
        /// <param name="hfNrAutoregjistrime">hidden fieldi me vleren e nr automatik</param>
        /// <param name="db">objekt i klases clsDatabaseRegjistrim qe merr pjese ne transaksion</param>
        /// <returns></returns>
        public clsMesazh ruajMeNrAuto(bool meKontabilizim, DbShare.clsKonfigurimAmbjenti konfam)
        {
            using (var myscope = new MyTransactionScope())
            {
                bool kaNdryshimNumri;
                int id = 0;
                clsMesazh mesazhKontrolli = kontrolloShperndarjeShpenzimesh(out kaNdryshimNumri);
                if (!mesazhKontrolli.Status)
                    return mesazhKontrolli;

                clsMesazh u_ruajt = ruajShperndarjeShpenzimesh(out id, this.NrDok, this.DtDok, this.DtRegjistrimi, this.Shenime, this.VleraTotale, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdNivel, this.IdKonfigAmbjente, this.IdDokNga, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.OColTrupi, this.OColFaturat, this.OColFletetKontabel, this.OColKokaMag, meKontabilizim,  false, this.idPerdoruesi, this.oColLlogarite, konfam, new colAmortizimiKoka());
                this.idKokaShperndarjeShpenz = id;
                if (u_ruajt)
                    myscope.Complete();
                //db.commitTransaksion();
                if (kaNdryshimNumri)
                    return mesazhKontrolli;

                return u_ruajt;
            }

        }

        private clsMesazh kontrolloShperndarjeShpenzimesh(out bool kaNdryshimNumri)
        {
            kaNdryshimNumri = false;

            clsDatabaseAdmin db = new clsDatabaseAdmin( );
            //db.vendosManager(dbRegj );
            clsMesazh mes = new clsMesazh();
            if (nrDok == "")
                return new clsMesazh(false, "Kodi i nuk mund te jete bosh");

            mes = KontrolloLlogarite();
            if (!mes)
                return mes;

            mes = kontrolloNumerAutomatik("txtNrDok", 518, out kaNdryshimNumri);
            if (!mes)
                return mes;
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i magazinës u krye me sukses!");
        }


        private clsMesazh KontrolloLlogarite()
        {
            for (int i = 0; i < this.OColLlogarite.Count; i++)
            {
                if (!this.OColLlogarite[i].NrLlogari.StartsWith("6"))
                    return new MesazhGabimi(MessagesResource.Messages["msgLlogariKlasaGjashte"]);

                clsLlogari llogaria = new clsLlogari(this.OColLlogarite[i].NrLlogari, this.IdNdermarrje);
                if (llogaria.IdLlogari <= 0)
                    return new MesazhGabimi(MessagesResource.Messages["msgGrideLlogariNukEkzistojne"]);
                if (!llogaria.Aktiv)
                    return new MesazhGabimi(MessagesResource.Messages["msgGrideLlogariJoAktive"]);
            }
            return new MesazhSuksesi();
        }

        private clsMesazh kontrolloNumerAutomatik(string kodKontrolli, int idKomponente, out bool kaNdryshimNrAuto)
        {
            clsMesazh msg = new MesazhSuksesi();
            bool modifikim = (this.IdKokaShperndarjeShpenz > 0);
            kaNdryshimNrAuto = false;
            switch (modifikim)
            {
                case true:
                    break;
                default:
                    int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(this.IdKonfigAmbjente, kodKontrolli, idKomponente);
                    string nrDokAuto = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, (DateTime)this.DtDok);

                    if (nrDokAuto != this.NrDok)
                        break;

                    IDictionary<string, object> hidden = new Dictionary<string, object>();
                    if (!String.IsNullOrEmpty(nrDokAuto)) //nqs ka nr automatik
                    {
                        NrAuto nrdokshi = new NrAuto();
                        nrdokshi.kodKontrolli = kodKontrolli;
                        nrdokshi.idNrAuto = idnrautonrdok;
                        nrdokshi.vlereNrAuto = nrDokAuto;
                        hidden.Add(kodKontrolli, JsonConvert.SerializeObject(nrdokshi));
                        msg = kontrolloNrAutoShperndarjeShpenzimesh(out kaNdryshimNrAuto, hidden);
                        if (!msg.Status)
                            ImbLogger.LogWarningBuxhetimi(msg.PershkrimMesazhi);
                    }
                    break;
            }
            return msg;
        }

        private clsMesazh kontrolloNrAutoShperndarjeShpenzimesh(out bool kaNdryshimNumri, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin( );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNdermarje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument shperndarje shpenzimesh sebashku me te trupin,faturat dhe kontabilitetin
        /// Nje objekt dokument shperndarje shpenzimesh ka nje koleksion me trupin,faturat dhe kontabilitetin perkates , 
        /// modifikimi e nje dokumenti shperndarje shpenzimesh imponon modifikimin edhe te nje colection-i me trupin,faturat dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti  shperndarje shpenzimesh bashke me trupin,faturat dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje  dokumenti shperndarje shpenzimesh sebashku me trupin,faturat dhe kontabilitetin
        /// 1. merret dokumenti eksistues  i shperndarje shpenzimesh dhe kalohet ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i shperndarje shpenzimesh se bashku me trupin,faturat dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues  te shperndarje shpenzimesh
        /// </summary>
        /// <param name="dtDk"> data e dokumentit</param>
        /// <param name="dtRegj"> data e regjistrimit</param>
        /// <param name="id"> id ritese</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idndermvit">id e ndermarje vitit</param>
        /// <param name="idstatusdok"> id e gjendjes se dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim">shenim</param>
        /// <param name="vl"> vlefta e dokumentit</param>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="idKonfigAmbjente">id e konfigurimit te ambjentit</param>
        /// <param name="idDokNga"> id e dok me ke eshte lidhur</param>
        /// <param name="idNivelGjenerues">id e nivelit gjenerues</param>
        /// <param name="idKonfigGjenerues">id e konfigrurimit gjenerues</param>
        /// <param name="idGjenerues"> id e gjeneruesit</param>
        /// <param name="oColTrupi">koleksion trupi</param>
        /// <param name="oColFaturat">koleksion Faturash</param>
        /// <param name="oColFletetKontabel">koleksion Fletesh kontabel</param>
        ///<param name="meKontabilizim">nese dokumenti do kontabilizohet</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public clsMesazh modifikoShperndarjeShpenzimesh(int id, string nrDk, DateTime dtDk, DateTime dtRegj, string shenim, double vl, int idstatusdok,
            int idnderm, int idndermvit, int idNivel, int idKonfigAmbjente, int idDokNga, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues,
            colShperndarjeShpenzimeTrupi oColTrupi, colShperndarjeShpenzimeFaturat oColFaturat, colKokatFletetKontabel oColFletetKontabel, colKokaMagazina oColKokaMag, bool meKontabilizim, int idperdoruesi, colShperndarjeShpenzimeLlogarite oColLlogarite, out int idre, DbShare.clsKonfigurimAmbjenti konfam)
        {
            idre = 0;
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhKont;
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsShperndarjeShpenzimeKoka kokaEkzistuese = new clsShperndarjeShpenzimeKoka( id);
                clsDatabazeAsete dbasete = new clsDatabazeAsete( );
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto( );
                clsDatabaseRegjistrim dbRegj = new DbRegjistrim.clsDatabaseRegjistrim();

                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarHapeniPerseri"]);
                }

                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                kokaEkzistuese.OColFletetKontabel = new colKokatFletetKontabel();
                colKokaMagazina kokaEkzistueseMag = new colKokaMagazina();
                mesazh = dbRegj.modifikoShperndarjeShpenzimeshKoka(kokaEkzistuese.IdKokaShperndarjeShpenz, kokaEkzistuese.NrDok, kokaEkzistuese.DtDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.Shenime, kokaEkzistuese.VleraTotale, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idPerdoruesi);
                idDokNga = kokaEkzistuese.IdKokaShperndarjeShpenz;
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet( );

                if (!mesazh.Status)
                {


                    return mesazh;
                }
                kokaEkzistueseMag.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKokaShperndarjeShpenz, dbRegj);
                int i = 0;
                DbAsete.colAmortizimiKoka colAmortizimetEVjetra = new DbAsete.colAmortizimiKoka();
                foreach (clsKokaMagazina mag in kokaEkzistueseMag)
                {
                    if (mag.IdKokaMagazina != 0)
                    {
                        if (OColKokaMag.Count > i)
                        {
                            oColKokaMag[i].IdDokNga = mag.IdKokaMagazina;
                            oColKokaMag[i].IdKrijuesi = mag.IdKrijuesi;
                        }
                        i++;
                        DbAsete.colAmortizimiKoka kokavjeter = new DbAsete.colAmortizimiKoka();
                        kokavjeter.ktheAmortizimKokaSipasIdGjeneruesi(mag.IdKokaMagazina, mag.IdKonfigAmbjente);
                        colAmortizimetEVjetra.AddRange(kokavjeter);
                        bool kaveprimepas = false;
                        mesazh = mag.fshiMagazina(mag.IdKokaMagazina, idperdoruesi, dbRegj, false, false, false, new colSerialetMagazine(), out kaveprimepas, false);//meqe eshte shperndarje te mos behet 
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }


                    }
                }

                DbKontabiliteti.colKokatFletetKontabel kokaFleteKontabel = new colKokatFletetKontabel(kokaEkzistuese.IdKokaShperndarjeShpenz, 7);

                int j = 0;
                foreach (DbKontabiliteti.clsKokaFleteKontabel f in oColFletetKontabel)
                {
                    if (kokaFleteKontabel.Count > j)
                    {
                        f.IdDokNga = kokaFleteKontabel[j].IdKokaFleteKontabel;
                        DbQendraKosto.clsKokaQendraKosto kokaqendra1 = new DbQendraKosto.clsKokaQendraKosto();
                        kokaqendra1.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokaFleteKontabel[j].IdKokaFleteKontabel, kokaFleteKontabel[j].IdKonfigAmbjente, dbqendra);
                        if (kokaqendra1.IdKoka != 0 && kokaqendra1.IdKoka != -1)
                        {
                            kokaFleteKontabel[j].KokaQendraKosto = kokaqendra1;
                        }
                        else kokaFleteKontabel[j].KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                        f.KokaQendraKosto.IdDokNga = kokaFleteKontabel[j].KokaQendraKosto.IdKoka;
                    }
                    j++;
                }

                for (int k = 0; k < kokaFleteKontabel.Count; k++)
                {

                    clsKokaFleteKontabel kokaFk = kokaFleteKontabel[k];
                    if (kokaFk.IdKokaFleteKontabel != 0)
                    {
                        mesazhKont = kokaFk.ModifikoFleteKontabel(true, dbkontab);
                        if (!mesazhKont.Status)
                        {

                            return new clsMesazh(false, mesazhKont.PershkrimMesazhi);
                        }
                    }

                }
                mesazh = ruajShperndarjeShpenzimesh(out id, nrDk, dtDk, dtRegj, shenim, vl, idstatusdok, idnderm, idndermvit, idNivel, idKonfigAmbjente, idDokNga, idNivelGjenerues, idKonfigGjenerues, idGjenerues, oColTrupi, oColFaturat, oColFletetKontabel, oColKokaMag, meKontabilizim,  true, idperdoruesi, oColLlogarite, konfam, colAmortizimetEVjetra);
                idre = id;
                if (!mesazh.Status)
                {

                    return mesazh;
                }

                return new clsMesazh(true, MessagesResource.Messages["regjMagModifikimiPerfundoiMeSukses"]);
            }
            catch (Exception ce)
            {


                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te shperndarje shpenzimesh ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka.modifikoShperndarjeShpenzimesh"/> 
        /// </summary>
        /// <param name="meKontabilizim"> tregon nese dokumenti do kontabilizohet apo jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool meKontabilizim, bool lidhur, DbShare.clsKonfigurimAmbjenti konfam)
        {
            clsMesazh u_modifikua;
            using (var scope = new MyTransactionScope())
            {

                if (lidhur == false)
                {
                    int idre = 0;
                    u_modifikua = modifikoShperndarjeShpenzimesh(this.IdKokaShperndarjeShpenz, this.NrDok, this.DtDok, this.DtRegjistrimi, this.Shenime, this.VleraTotale, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdNivel, this.IdKonfigAmbjente, this.IdDokNga, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.OColTrupi, this.OColFaturat, this.OColFletetKontabel, this.OColKokaMag, meKontabilizim, this.idPerdoruesi, this.oColLlogarite, out idre, konfam);
                    this.idKokaShperndarjeShpenz = idre;
                }
                else
                {
                    clsDatabaseRegjistrim dbRegj = new DbRegjistrim.clsDatabaseRegjistrim();
                    u_modifikua = dbRegj.modifikoShperndarjeShpenzimeshKoka(this.IdKokaShperndarjeShpenz, this.NrDok, this.DtDok, this.DtRegjistrimi, this.Shenime, this.VleraTotale, this.IdStatusDok, this.idPerdoruesi);
                }
                if (u_modifikua)
                {
                    scope.Complete();
                }
                
                return u_modifikua;
            }
        }

        /// <summary>
        /// fshin nje objekt dokument shperndarje shpenzimesh sebashku me te trupin, faturat  dhe kontabilitetin perkates
        /// Nje objekt dokument shperndarje shpenzimesh ka nje koleksion me trupin,faturat dhe kontabilitetin , 
        /// fshirja e nje dokumenti shperndarje shpenzimesh imponon fshirjen edhe te nje colection-i me trupin, faturat dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i shperndarje shpenzimesh bashke me trupin,faturat dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje  dokumenti shperndarje shpenzimesh sebashku me trupin, faturat dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit shperndarje shpenzimesh dhe e stornon
        ///2. ben fshirjen e trupit, fatures dhe koken e dokumentit shperndarje shpenzimesh

        /// </summary>
        ///<param name="id"> koka e dokumentit te shperndarje shpenzimesh i cili do te fshihet</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh fshiShperndarjeShpenzimesh(int id)
        {

            using (var scope = new MyTransactionScope())
            {
                clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
                clsMesazh mesazh = new clsMesazh();
                DbCore.clsMesazh mesazhKont = new DbCore.clsMesazh(true);
                try
                {

                    clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj);
                    clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);

                    clsShperndarjeShpenzimeKoka kokaEkzistuese = new clsShperndarjeShpenzimeKoka(id);
                    //clsShperndarjeShpenzimeKoka kokaEkzistuese = this.merrShperndarjeShpenzimeshKokaSipasID(koka.IdKokaShperndarjeShpenz)[0];
                    colKokaMagazina kokaEkzistueseMag = new colKokaMagazina();

                    kokaEkzistueseMag.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKokaShperndarjeShpenz, dbRegj);

                    foreach (clsKokaMagazina mag in kokaEkzistueseMag)
                    {
                        if (mag.IdKokaMagazina != 0)
                        {
                            bool kaveprimepas = false;
                            mesazh = mag.fshiMagazina(mag.IdKokaMagazina, idPerdoruesi, dbRegj, false, false, false, new colSerialetMagazine(), out kaveprimepas, false);
                            if (!mesazh.Status)
                            {
                                return mesazh;
                            }
                        }

                    }




                    kokaEkzistuese.OColFletetKontabel = new colKokatFletetKontabel();
                    DbKontabiliteti.colKokatFletetKontabel kokaFleteKontabel = new colKokatFletetKontabel(kokaEkzistuese.IdKokaShperndarjeShpenz, 7);
                    //DbKontabiliteti.colKokatFletetKontabel kokaFleteKontabel = dbkontab.merrKokaFleteKontabelSipasIDDokAndIDLlojDok(kokaEkzistuese.IdKokaShperndarjeShpenz, 39);
                    for (int i = 0; i < kokaFleteKontabel.Count; i++)
                    {
                        if (mesazhKont.Status)
                        {
                            clsKokaFleteKontabel kokaFk = kokaFleteKontabel[i];

                            //clsKokaFleteKontabel kokaFk = dbkontab.merrKokaFleteKontabelSipasIDDokAndIDLlojDok(kokaEkzistuese.IdKokaShperndarjeShpenz, 39)[0];
                            if (kokaFk.IdKokaFleteKontabel != 0)
                            {
                                mesazh = kokaFk.fshiupd(dbkontab);
                                if (!mesazh.Status)
                                {
                                    return mesazh;
                                }

                            }
                        }
                        else
                        {

                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazhKont.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    if (mesazhKont.Status)
                    {
                        kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                        kokaEkzistuese.OColFletetKontabel = new colKokatFletetKontabel();
                        mesazh = dbRegj.modifikoShperndarjeShpenzimeshKoka(kokaEkzistuese.IdKokaShperndarjeShpenz, kokaEkzistuese.NrDok, kokaEkzistuese.DtDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.Shenime, kokaEkzistuese.VleraTotale, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idPerdoruesi);

                        if (mesazh.Status)
                        {
                            scope.Complete();

                            mesazh = new clsMesazh(true, "Fshirja përfundoi me sukses!");
                            
                        }
                       return mesazh;
                        
                    }
                    else
                    {

                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhKont.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                catch (Exception ce)
                {
                    IMBUtils.Logging.ImbLogger.Error(ce);
                    return new clsMesazh(false, ce.Message);
                }
            }
        }

        /// <summary>
        /// fshin objektin e  kokes se dokumentit te shperndarje shpenzimesh ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka.fshiShperndarjeShpenzimesh"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = fshiShperndarjeShpenzimesh(this.IdKokaShperndarjeShpenz);
            return u_fshi;
        }

        public static int ktheIdStatusDokSipasID(int idKoka)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheIdStatusDokSipasIdKoka(idKoka);
            }
        }

        public static DataTable MerrTrupDokumentShperndarjeShpenzimiFull(int idDokumenti)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.MerrTrupDokumentShperndarjeShpenzimiFull(idDokumenti);
        }
        public static DataTable MerrLlogariteShperndarjeShpenzimiFull(int idDokumenti)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.MerrLlogariteShperndarjeShpenzimiFull(idDokumenti);
        }

        public static bool ekzistonDokumentShperndarjeShpenzimi(string nrDk, DateTime dtDk, int idnderm, int idndermvit, clsDatabaseRegjistrim dbRegj)
        {
            return dbRegj.ekzistonShperndarjeShpenzimesh(nrDk, dtDk, idnderm, idndermvit);
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kokat e shperndarjes se shpenzimeve nga databaza
        /// </summary>
        /// <param name="dbDataRowShperndShpenzKok">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushShperndarjeShpenzKok(DataRow dbDataRowShperndShpenzKok)
        {
            if (dbDataRowShperndShpenzKok != null)
            {
                try
                {
                    int.TryParse(dbDataRowShperndShpenzKok["IDSHPERNDARJESHPENZ"].ToString(), out idKokaShperndarjeShpenz);
                    nrDok = dbDataRowShperndShpenzKok["NRDOK"].ToString();
                    DateTime.TryParse(dbDataRowShperndShpenzKok["DTDOK"].ToString(), out dtDok);
                    DateTime.TryParse(dbDataRowShperndShpenzKok["DTREGJISTRIMI"].ToString(), out dtRegjistrimi);
                    shenime = dbDataRowShperndShpenzKok["SHENIME"].ToString();
                    double.TryParse(dbDataRowShperndShpenzKok["VLERATOTALE"].ToString(), out vleraTotale);
                    int.TryParse(dbDataRowShperndShpenzKok["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowShperndShpenzKok["IDNDERM"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowShperndShpenzKok["IDNDERMVIT"].ToString(), out idNdermarjeVit);
                    int.TryParse(dbDataRowShperndShpenzKok["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowShperndShpenzKok["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(dbDataRowShperndShpenzKok["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(dbDataRowShperndShpenzKok["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(dbDataRowShperndShpenzKok["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(dbDataRowShperndShpenzKok["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(dbDataRowShperndShpenzKok["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowShperndShpenzKok["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowShperndShpenzKok["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    oColTrupi = new colShperndarjeShpenzimeTrupi();
                    oColFaturat = new colShperndarjeShpenzimeFaturat();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se shperndarjes se shpenzimeve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
