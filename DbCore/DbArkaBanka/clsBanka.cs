using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje banke ose nje arke
    ///  (Te dhenat  merren nga tabela : T_BANKA)
    /// </summary>
    public class clsBanka
    {
        #region Atribute

        private int idBanka;
        private string kodiBanka;
        private string emerBanka;
        private int idTipiBanka;
        private string nrLlogariBanka;
        private string iban;
        private int idGrupBanke;
        private string shenimeBanka;
        private bool aktivBanka;
        private int idLlogariKontabilizimi;
        private int idMonedhaBanka;
        private string rrugaBanka;
        private string qytetiBanka;
        private string shtetiBanka;
        private string zipKodBanka;
        private string telBanka;
        private string emerKontaktiBanka;
        private string mbiemerKontaktiBanka;
        private string telKontaktiBanka;
        private string faxKontaktiBanka;
        private string celKontaktiBanka;
        private string emailKontaktiBanka;
        private int idPerdoruesi;
        private int idNdermarje;
        private int komisioni;
        private string idNivelAutorizimi;
        private Boolean llojArkaBanka;
        private string adresaKontaktiBanka;
        private int idKonfig;
        private string nrGrupBanke;
        private string nrLlogari;
        private string kodMonedha;
        private string nrKomisioni;
        private int idDegeAdministrative;
        private int idStatusDok;
        private bool shfaqNeEinvoice;
       
        private string dega;
        private string nrKlienti;
        private string valuta;
        private string kodiLlogarise;
        private string tipi;
        private string kodiTCR;
        private string nrRendor;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colLidhjetAutorizim oColLidhjetAutorizim;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret
        public clsBanka(int idBanka) {
            mbushBanke(idBanka);
        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsBanka(int idBanka, string kodiBanka, string emerBanka, int idTipiBanka, string nrLlogariBanka, string iban, int idGrupBanke, string shenimeBanka, bool aktivBanka, int idLlogariKontabilizimi, int idMonedhaBanka, string rrugaBanka, string qytetiBanka, string shtetiBanka, string zipKodBanka, string telBanka, string emerKontaktiBanka, string mbiemerKontaktiBanka, string telKontaktiBanka, string faxKontaktiBanka, string celKontaktiBanka, string emailKontaktiBanka, int idPerdoruesi, int komis, int idnderm, bool llojarkabanka, string adresakontaktibanka, int idkonfig, int iddegeAdministrative, int idstatusdok, string dega, string valuta, string nrKlienti, string kodiLlogarise, string tipi, string kodiTCR, string nrRendor, bool shfaqeneeinvoice)
        {
            this.idBanka = idBanka;
            this.kodiBanka = kodiBanka;
            this.emerBanka = emerBanka;
            this.idTipiBanka = idTipiBanka;
            this.nrLlogariBanka = nrLlogariBanka;
            this.iban = iban;
            this.idGrupBanke = idGrupBanke;
            this.shenimeBanka = shenimeBanka;
            this.aktivBanka = aktivBanka;
            this.idLlogariKontabilizimi = idLlogariKontabilizimi;
            this.idMonedhaBanka = idMonedhaBanka;
            this.rrugaBanka = rrugaBanka;
            this.qytetiBanka = qytetiBanka;
            this.shtetiBanka = shtetiBanka;
            this.zipKodBanka = zipKodBanka;
            this.telBanka = telBanka;
            this.emerKontaktiBanka = emerKontaktiBanka;
            this.mbiemerKontaktiBanka = mbiemerKontaktiBanka;
            this.telKontaktiBanka = telKontaktiBanka;
            this.faxKontaktiBanka = faxKontaktiBanka;
            this.celKontaktiBanka = celKontaktiBanka;
            this.emailKontaktiBanka = emailKontaktiBanka;
            this.idPerdoruesi = idPerdoruesi;
            this.komisioni = komis;
            this.idNdermarje = idnderm;
            this.llojArkaBanka = llojarkabanka;
            this.adresaKontaktiBanka = adresakontaktibanka;
            this.idKonfig = idkonfig;
            this.idDegeAdministrative = iddegeAdministrative;
            this.idStatusDok = idstatusdok;
            this.nrKlienti = nrKlienti;
            this.kodiLlogarise = kodiLlogarise;
            this.dega = dega;
            this.tipi = tipi;
            this.valuta = valuta;
            this.kodiTCR = kodiTCR;
            this.nrRendor = nrRendor;
            this.shfaqNeEinvoice = shfaqeneeinvoice;
            oColLidhjetAutorizim = new colLidhjetAutorizim();
        }


         /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsBanka(int idBanka, string kodiBanka, string emerBanka, int idTipiBanka, string nrLlogariBanka, string iban, int idGrupBanke, string shenimeBanka, bool aktivBanka, int idLlogariKontabilizimi, int idMonedhaBanka, string rrugaBanka, string qytetiBanka, string shtetiBanka, string zipKodBanka, string telBanka, string emerKontaktiBanka, string mbiemerKontaktiBanka, string telKontaktiBanka, string faxKontaktiBanka, string celKontaktiBanka, string emailKontaktiBanka, int idPerdoruesi, int komis, int idnderm, bool llojarkabanka, string adresakontaktibanka, int idkonfig, int iddegeAdministrative, int idstatusdok, string dega, string valuta, string nrKlienti, string kodiLlogarise, string tipi, colLidhjetAutorizim collidhjesaut, string kodiTCR, string nrRendor, ResourceManager rm, CultureInfo ci,bool shfaqeNeEinvoice)
        {
            try
            {
                this.idBanka = idBanka;
                this.kodiBanka = kodiBanka;
                this.emerBanka = emerBanka;
                this.idTipiBanka = idTipiBanka;
                this.nrLlogariBanka = nrLlogariBanka;
                this.iban = iban;
                this.idGrupBanke = idGrupBanke;
                this.shenimeBanka = shenimeBanka;
                this.aktivBanka = aktivBanka;
                this.idLlogariKontabilizimi = idLlogariKontabilizimi;
                this.idMonedhaBanka = idMonedhaBanka;
                this.rrugaBanka = rrugaBanka;
                this.qytetiBanka = qytetiBanka;
                this.shtetiBanka = shtetiBanka;
                this.zipKodBanka = zipKodBanka;
                this.telBanka = telBanka;
                this.emerKontaktiBanka = emerKontaktiBanka;
                this.mbiemerKontaktiBanka = mbiemerKontaktiBanka;
                this.telKontaktiBanka = telKontaktiBanka;
                this.faxKontaktiBanka = faxKontaktiBanka;
                this.celKontaktiBanka = celKontaktiBanka;
                this.emailKontaktiBanka = emailKontaktiBanka;
                this.idPerdoruesi = idPerdoruesi;
                this.komisioni = komis;
                this.idNdermarje = idnderm;
                this.llojArkaBanka = llojarkabanka;
                this.adresaKontaktiBanka = adresakontaktibanka;
                this.idKonfig = idkonfig;
                this.idDegeAdministrative = iddegeAdministrative;
                this.idStatusDok = idstatusdok;
                this.nrKlienti = nrKlienti;
                this.kodiLlogarise = kodiLlogarise;
                this.dega = dega;
                this.tipi = tipi;
                this.valuta = valuta;
                oColLidhjetAutorizim = collidhjesaut;
                this.kodiTCR = kodiTCR;
                this.nrRendor = nrRendor;
                this.shfaqNeEinvoice = shfaqeNeEinvoice;
                clsMesazh mesazh = kontrolloBanka(rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsBanka()
        {
        }

        public clsBanka(DataRow rreshti)
        {
            mbushBank(rreshti);
        }
        public clsBanka(DataRow rreshti, bool tcr)
        {
            mbushBankPaTCR(rreshti);
        }
        public clsBanka(string Kodi, int IdNdermarrje, clsDatabaseArkaBanka db)
        {
            mbushBank(db.TransCache.getArkaBanka(IdNdermarrje, Kodi, db));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdBanka
        {
            get { return idBanka; }
            set { idBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e bankes.
        /// </summary>
        public string KodiBanka
        {
            get { return kodiBanka; }
            set { kodiBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e bankes.
        /// </summary>
        public string EmerBanka
        {
            get { return emerBanka; }
            set { emerBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e tipit te bankes.
        /// </summary>
        public int IdTipiBanka
        {
            get { return idTipiBanka; }
            set { this.idTipiBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e llogarise se bankes.
        /// </summary>
        public string NrLlogariBanka
        {
            get { return nrLlogariBanka; }
            set { nrLlogariBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos IBAN, nje lloj kodi nderkombetar qe identifikon llogarite bakare.
        /// </summary>
        public string IBAN
        {
            get { return iban; }
            set { iban = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e grupit te bankes.
        /// </summary>
        public int IdGrupBanke
        {
            get { return idGrupBanke; }
            set { idGrupBanke = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenime per banken.
        /// </summary>
        public string ShenimeBanka
        {
            get { return shenimeBanka; }
            set { shenimeBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren nese baka eshte aktive apo jo.
        /// </summary>
        public bool AktivBanka
        {
            get { return aktivBanka; }
            set { aktivBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne llogarise per kontabilitetin e bankes.
        /// </summary>
        public int IdLlogariKontabilizimi
        {
            get { return idLlogariKontabilizimi; }
            set { idLlogariKontabilizimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes se bankes.
        /// </summary>
        public int IdMonedhaBanka
        {
            get
            {
                return idMonedhaBanka;
            }
            set
            {
                idMonedhaBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos rrugen e bankes.
        /// </summary>
        public string RrugaBanka
        {
            get
            {
                return rrugaBanka;
            }
            set
            {
                rrugaBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos qytetin e bankes.
        /// </summary>
        public string QytetiBanka
        {
            get
            {
                return qytetiBanka;
            }
            set
            {
                qytetiBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos shtetin e bankes.
        /// </summary>
        public string ShtetiBanka
        {
            get
            {
                return shtetiBanka;
            }

            set
            {
                shtetiBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin zip.
        /// </summary>
        public string ZipKodBanka
        {
            get
            {
                return zipKodBanka;
            }
            set
            {
                zipKodBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos numrin e telefonit te bankes.
        /// </summary>
        public string TelBanka
        {
            get
            {
                return telBanka;
            }
            set
            {
                telBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje emer kontakti te bankes.
        /// </summary>
        public string EmerKontaktiBanka
        {
            get
            {
                return emerKontaktiBanka;
            }
            set
            {
                emerKontaktiBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje mbiemer kontakti te bankes.
        /// </summary>
        public string MbiemerKontaktiBanka
        {
            get
            {
                return mbiemerKontaktiBanka;
            }

            set
            {
                mbiemerKontaktiBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje numer telefoni kontakti te bankes.
        /// </summary>
        public string TelKontaktiBanka
        {
            get
            {
                return telKontaktiBanka;
            }
            set
            {
                telKontaktiBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje numer fax-i kontakti te bankes.
        /// </summary>
        public string FaxKontaktiBanka
        {
            get
            {
                return faxKontaktiBanka;
            }
            set
            {
                faxKontaktiBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje numer celulari kontakti te bankes.
        /// </summary>
        public string CelKontaktiBanka
        {
            get
            {
                return celKontaktiBanka;
            }
            set
            {
                celKontaktiBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje adrese e-mail kontakti te bankes.
        /// </summary>
        public string EmailKontaktiBanka
        {
            get
            {
                return emailKontaktiBanka;
            }
            set
            {
                emailKontaktiBanka = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nje adrese  kontakti te bankes.
        /// </summary>
        public string AdresaKontaktiBanka
        {
            get
            {
                return adresaKontaktiBanka;
            }
            set
            {
                adresaKontaktiBanka = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos llojin nese eshte arke apo banke.
        /// </summary>
        /// <example> true-banke, false-arke</example>
        public bool LlojArkaBanka
        {
            get
            {
                return llojArkaBanka;
            }
            set
            {
                llojArkaBanka = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe celi banken.
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
        /// Kthen/Vendos ID-ne e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get
            {
                return idKonfig;
            }
            set
            {
                idKonfig = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos llogarine e komisionit bankar.
        /// </summary>
        public int Komisioni
        {
            get
            {
                return komisioni;
            }
            set
            {
                komisioni = value;
            }
        }
        
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }
        public string IdNivelAutorizimi
        {
            get
            {
                return idNivelAutorizimi;
            }
            set
            {
                this.idNivelAutorizimi = value;
            }

        }
        /// <summary>
        /// Kthen/Vendos nr e grupit te bankes
        /// </summary>
        public string NrGrupBanke
        {
            get
            {
                return nrGrupBanke;
            }
            set
            {
                nrGrupBanke = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se kontabilitetit
        /// </summary>
        public string NrLlogari
        {
            get
            {
                return nrLlogari;
            }
            set
            {
                nrLlogari = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodin e monedhes
        /// </summary>
        public string KodMonedha
        {
            get
            {
                return kodMonedha;
            }
            set
            {
                kodMonedha = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se komisionit
        /// </summary>
        public string NrKomisioni
        {
            get
            {
                return nrKomisioni;
            }
            set
            {
                nrKomisioni = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos id e degesAdministrative
        /// </summary>
        public int IdDegeAdministrative
        {
            get
            {
                return idDegeAdministrative;
            }
            set
            {
                idDegeAdministrative = value;
            }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /*Dega, Numri i klientit, Valuta, Kodi i llogarise, Tipi*/
        public string Dega { get { return dega; } set { dega = value; } }
        public string NrKlienti { get { return nrKlienti; } set { nrKlienti = value; } }
        public string Valuta { get { return valuta; } set { valuta = value; } }
        public string KodiLlogarise { get { return kodiLlogarise; } set { kodiLlogarise = value; } }
        public string Tipi { get { return tipi; } set { tipi = value; } }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public DateTime DtModifikimi
        {
            get{return dtModifikimi;}
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsLidhjeAutorizim"/>
        /// </summary>
        public colLidhjetAutorizim OColLidhjetAutorizim
        {
            get { return oColLidhjetAutorizim; }
            set { oColLidhjetAutorizim = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e pajisjes së Arkës së tatimpaguesit, kod i gjeneruar nga CIS
        /// </summary>
        public string KodiTCR
        {
            get { return kodiTCR; }
            set { kodiTCR = value; }
        }
        
        public bool ShfaqNeEinvoice
        {
            get { return shfaqNeEinvoice; }
            set { shfaqNeEinvoice = value; }
        }

        /// <summary>
        /// Kthen/vendos numrin rendor të Arkës së tatimpaguesit
        /// </summary>
        public string NrRendor
        {
            get { return nrRendor; }
            set { value = nrRendor; }
        }

        #endregion

        #region Metoda Publike

        public static double ktheGjendjeBanke(int idBanka,bool llojiArkaBanka, int idPerdoruesi, int idndermarje, DateTime dateDokumenti)
        {
            clsDatabaseArkaBanka dbArkaBanka = new clsDatabaseArkaBanka();
            double gjendje = dbArkaBanka.ktheGjendjenBankes(idBanka,llojiArkaBanka, idPerdoruesi, idndermarje, dateDokumenti);
            dbArkaBanka.Dispose();
            return gjendje;
        }
        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsBanka dhe autorizimet.
        ///<param name="banka">Objekt i tipit clsBanka qe do te fshihet</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiBankeAndAutorizime(clsBanka banka)
        {//fshin llogarine dhe buxhetet perkatese
            colLidhjetAutorizim colLidhjeAutorizim = new colLidhjetAutorizim(banka.IdBanka, "Banka");
            clsDatabaseArkaBanka dbArkaBanka = new clsDatabaseArkaBanka();

            try
            {
                dbArkaBanka.beginTransaksion();
                clsMesazh mesazh;
                foreach (clsLidhjeAutorizim o in colLidhjeAutorizim)
                {
                    clsDatabaseAdmin data = new clsDatabaseAdmin(dbArkaBanka);
                    mesazh = data.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
                    if (!mesazh.Status)
                    {
                        dbArkaBanka.rollbackTransaksion();
                        return mesazh;
                    }
                }
                mesazh = dbArkaBanka.fshiBanke(banka.IdBanka);
                if (!mesazh.Status)
                {
                    dbArkaBanka.rollbackTransaksion();
                    return mesazh;
                }
                dbArkaBanka.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbArkaBanka.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsBanka dhe autorizimet.
        ///<param name="banka">Objekt i tipit clsBanka qe do te ruhet</param>
        ///Therret funksionin <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.ruajBanke"/>
        ///Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ruajBankeAndAutorizime(clsBanka banka)
        {        
            clsMesazh mesazh;        
            clsDatabaseArkaBanka dbArkaBanka = new clsDatabaseArkaBanka();

            try
            {
                dbArkaBanka.beginTransaksion();
                mesazh = dbArkaBanka.ruajBanke(out int idB, banka.KodiBanka, banka.EmerBanka, banka.IdTipiBanka, banka.NrLlogariBanka, banka.IBAN, banka.IdGrupBanke, banka.ShenimeBanka, banka.AktivBanka,
                    banka.IdLlogariKontabilizimi, banka.IdMonedhaBanka, banka.RrugaBanka, banka.QytetiBanka, banka.ShtetiBanka, banka.ZipKodBanka, banka.TelBanka, banka.EmerKontaktiBanka, banka.MbiemerKontaktiBanka,
                    banka.TelKontaktiBanka, banka.FaxKontaktiBanka, banka.CelKontaktiBanka, banka.EmailKontaktiBanka, banka.IdPerdoruesi, banka.Komisioni, banka.IdNdermarje, banka.LlojArkaBanka, banka.AdresaKontaktiBanka,
                    banka.IdKonfig, banka.idDegeAdministrative, banka.idStatusDok, banka.dega, banka.nrKlienti, banka.valuta, banka.kodiLlogarise, banka.tipi, banka.kodiTCR, banka.nrRendor,banka.shfaqNeEinvoice);
                if (!mesazh.Status)
                {
                    dbArkaBanka.rollbackTransaksion();
                    return mesazh;
                }
                banka.IdBanka = idB;
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbArkaBanka);
                foreach (clsLidhjeAutorizim o in banka.oColLidhjetAutorizim)
                {
                    o.IdLloji = DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Banka", dbKont);
                    o.IdLidhese = banka.IdBanka;
                    mesazh = new clsDatabaseAdmin(dbArkaBanka).ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                    {
                        dbArkaBanka.rollbackTransaksion();
                        return mesazh;
                    }
                }

                dbArkaBanka.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);                              
            }
            catch (Exception ce)
            {
                dbArkaBanka.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te modifikuar nje objekt clsBanka dhe autorizimet.
        ///<param name="banka">Objekt i tipit clsBanka qe do te modifikohet</param>
        ///Therret funksionin <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.modifikoBanke"/>
        ///Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>, <see cref="DbAdmin.clsDatabaseAdmin.modifikoLidhjeAutorizim"/>, <see cref="DbAdmin.clsDatabaseAdmin.fshiLidhjeAutorizim"/> sipas rastit nese jane shtuar apo hequr rreshta
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoBankeAndAutorizime(clsBanka banka)
        {
            colLidhjetAutorizim colLidhjetAutorizim = new colLidhjetAutorizim(banka.IdBanka, "Banka");
            clsDatabaseArkaBanka dbArkaBanka = new clsDatabaseArkaBanka();

            try
            {
                dbArkaBanka.beginTransaksion();
                clsMesazh mesazh = dbArkaBanka.modifikoBanke(banka.IdBanka, banka.KodiBanka, banka.EmerBanka, banka.IdTipiBanka, banka.NrLlogariBanka, banka.IBAN, banka.IdGrupBanke, banka.ShenimeBanka,
                    banka.AktivBanka, banka.IdLlogariKontabilizimi, banka.IdMonedhaBanka, banka.RrugaBanka, banka.QytetiBanka, banka.ShtetiBanka, banka.ZipKodBanka, banka.TelBanka, banka.EmerKontaktiBanka,
                    banka.MbiemerKontaktiBanka, banka.TelKontaktiBanka, banka.FaxKontaktiBanka, banka.CelKontaktiBanka, banka.EmailKontaktiBanka, banka.IdPerdoruesi, banka.Komisioni, banka.IdNdermarje, 
                    banka.LlojArkaBanka, banka.AdresaKontaktiBanka, banka.IdKonfig, banka.idDegeAdministrative, banka.idStatusDok, banka.dega, banka.nrKlienti, banka.valuta, banka.kodiLlogarise, banka.tipi,
                    banka.kodiTCR, banka.nrRendor, banka.shfaqNeEinvoice);
                if (!mesazh.Status)
                {
                    dbArkaBanka.rollbackTransaksion();
                    return mesazh;
                }
               
                clsDatabaseAdmin data = new clsDatabaseAdmin(dbArkaBanka);
                colLidhjetAutorizim oColLidhjetAutorizim = banka.oColLidhjetAutorizim;
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbArkaBanka );
                for (int i = 0; i < oColLidhjetAutorizim.Count; i++)
                {
                    if (mesazh.Status)
                    {
                        int idAutorizimKoka = oColLidhjetAutorizim[i].IdAutorizimeKoka;
                        if (idAutorizimKoka == -1)
                            continue;
                        oColLidhjetAutorizim[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Banka", dbKont);
                        oColLidhjetAutorizim[i].IdLidhese = banka.IdBanka;
                        clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                        if (lidhjeNjejte != null)
                        {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                            colLidhjetAutorizim.Remove(lidhjeNjejte);
                            continue;
                        }
                        mesazh = data.ruajLidhjeAutorizim(oColLidhjetAutorizim[i].IdLidhjeAutorizim, oColLidhjetAutorizim[i].IdLidhese, oColLidhjetAutorizim[i].IdLloji, oColLidhjetAutorizim[i].IdAutorizimeKoka, 1);
                    }
                    else
                    {
                        dbArkaBanka.rollbackTransaksion();
                        return mesazh;
                    }
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
              
                mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, IdPerdoruesi, data);
                if (!mesazh.Status)
                {
                    dbArkaBanka.rollbackTransaksion();
                    return mesazh;
                }

                dbArkaBanka.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            }
            catch (Exception ce)
            {
                dbArkaBanka.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ruan objektin e bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsBanka.ruajBankeAndAutorizime"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            //clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_ruajt = ruajBankeAndAutorizime(this);
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsBanka.modifikoBankeAndAutorizime"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            //clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_modifikua = modifikoBankeAndAutorizime(this);
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsBanka.fshiBankeAndAutorizime"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_fshi = data.fshiBankeStatus(this.idBanka, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        public clsBanka merr()
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e nje banke
            clsBanka data = new clsBanka();
            data.mbushBanke(this.IdBanka);
            return data;
        }

        /// <summary>
        /// Kthen nje objekt te tipit <see cref="DbCore.DbArkaBanka.clsBanka"/>, te cilin e merr nga databaza 
        /// sipas kodit qe i eshte caktuar objektit dhe ID-se se ndermarrjes.Thirret funksioni
        /// <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.ktheBankeSipasKodit"/> 
        /// </summary>
        public clsBanka merrBankeSipasKodit()
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e nje banke
            clsBanka data = new clsBanka();
            data.mbushBankeSipasKodit(this.kodiBanka, this.IdNdermarje);
            return data;
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbArkaBanka.clsBanka.cs"/> . Thirret funksioni
        /// <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.ktheGjitheBankatSipasAutorizimeve"/> 
        /// </summary>
        public colBankat merrGjitheBankat(int idnderm, int idperdoruesi)
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e te gjithave bankave
            colBankat data = new colBankat();
            data.mbushGjitheBankatSipasAutorizimeve(idnderm, idperdoruesi);
            return data;

        }

        /// <summary>
        /// Kthen nje objekt te tipit <see cref="DbCore.clsMesazh"/>, i cili tregon nese ekziston banka.Thirret funksioni
        /// <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.ekzistonBanke"/> 
        /// </summary>
        public clsMesazh ekzistonBanke()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh mesazhi = data.ekzistonBanke(this.KodiBanka, this.IdNdermarje);
            data.Dispose();
            return mesazhi;
        }
        
        public static clsMesazh ekziston(string kodi, int idndermarje)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh mesazhi = data.ekzistonBanke(kodi, idndermarje);
            data.Dispose();
            return mesazhi;
        }
        
        public static clsMesazh ekziston(string kodi, int idndermarje, clsDatabaseArkaBanka data)
        {
            return data.ekzistonBanke(kodi, idndermarje);
        }
        
        /// <summary>
        /// Kthen nje objekt te tipit <see cref="DbCore.DbKontabiliteti.clsLlogari"/>, te cilin e merr nga databaza 
        /// sipas kodit qe te nenllogarise dhe ID-se se llogarise.
        /// </summary>
        public DbCore.DbKontabiliteti.clsLlogari merrLlogariBanke(string kodnenllojllogarie)
        {
            DbCore.DbKontabiliteti.clsLlogari oLlogari = new DbCore.DbKontabiliteti.clsLlogari();
            switch (kodnenllojllogarie)
            {
                case "KBN":
                    oLlogari.IdLlogari = this.IdLlogariKontabilizimi;
                    oLlogari = oLlogari.merrLlogariSipasId();
                    break;
                case "KOBN":
                    oLlogari.IdLlogari = this.Komisioni;
                    oLlogari = oLlogari.merrLlogariSipasId();
                    break;
            }
            return oLlogari;
        }
        
        public DbCore.DbKontabiliteti.clsLlogari merrLlogariBanke(string kodnenllojllogarie, DbKontabiliteti.clsDatabaseKontabilitet db)
        {
            DbCore.DbKontabiliteti.clsLlogari oLlogari = new DbCore.DbKontabiliteti.clsLlogari();
            switch (kodnenllojllogarie)
            {
                case "KBN":
                    oLlogari = new DbKontabiliteti.clsLlogari(this.IdLlogariKontabilizimi, db);

                    break;
                case "KOBN":
                    oLlogari = new DbKontabiliteti.clsLlogari(this.Komisioni, db);

                    break;
            }
            return oLlogari;
        }

        /// <summary>
        /// mbush banken
        /// </summary>
        /// <param name="idBanka">id e bankes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public clsMesazh mbushBanke(int idBanka)
        {
            using (clsDatabaseArkaBanka data = new clsDatabaseArkaBanka())
            {
                clsMesazh mesazhi = mbushBank(data.merrBanke(idBanka));
                return mesazhi;
            }
        }

        /// <summary>
        /// mbush banken
        /// </summary>
        /// <param name="idBanka">id e bankes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public clsMesazh mbushBankeID(int idBanka, clsDatabaseArkaBanka data)
        {

            return mbushBank(data.merrBanke(idBanka));
        }

        /// <summary>
        /// mbush bankes sipas kodit
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public clsMesazh mbushBankeSipasKodit(string kodi, int idnderm)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh mesazhi = mbushBank(data.ktheBankeSipasKodit(kodi, idnderm));
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "Mbushja e bankes sipas kodit: " + kodi + "dhe idndermarrjes:" + idnderm + " u krye me sukses";
            else
                mesazhi.PershkrimMesazhi = "Nuk ekziston banka me kod: " + kodi + "dhe idndermarrje:" + idnderm;
            data.Dispose();
            return mesazhi;
        } 
        public clsMesazh mbushBankeSipasKoditMeAutorizime(string kodi, int idnderm, int idperdorues)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh mesazhi = mbushBank(data.ktheBankeSipasKoditMeAutorizime(kodi, idnderm, idperdorues));
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "Mbushja e bankes sipas kodit: " + kodi + "dhe idndermarrjes:" + idnderm + " u krye me sukses";
            else
                mesazhi.PershkrimMesazhi = "Nuk ekziston banka me kod: " + kodi + "dhe idndermarrje:" + idnderm;
            data.Dispose();
            return mesazhi;
        }

        
        public clsMesazh mbushBankeSipasKodit(string kodi, int idnderm, clsDatabaseArkaBanka data)
        {
            clsMesazh mesazhi = mbushBank(data.ktheBankeSipasKodit(kodi, idnderm));
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "Mbushja e bankes sipas kodit: " + kodi + "dhe idndermarrjes:" + idnderm + " u krye me sukses";
            else
                mesazhi.PershkrimMesazhi = "Nuk ekziston banka me kod: " + kodi + "dhe idndermarrje:" + idnderm;
            return mesazhi;
        }
        public clsMesazh mbushBankeSipasKoditMeAutorizime(string kodi, int idnderm,int idperdorues, clsDatabaseArkaBanka data)
        {

            clsMesazh mesazhi = mbushBank(data.ktheBankeSipasKoditMeAutorizime(kodi, idnderm, idperdorues));
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "Mbushja e bankes sipas kodit: " + kodi + "dhe idndermarrjes:" + idnderm + " u krye me sukses";
            else
                mesazhi.PershkrimMesazhi = "Nuk ekziston banka me kod: " + kodi + "dhe idndermarrje:" + idnderm;
            return mesazhi;
        }
        
        /// <summary>
        /// kthe nje datatable me arken/banken e marre nga db sipas id.
        /// </summary>
        /// <param name="idTransportues"></param>
        /// <returns>datatable</returns>
        public static DataTable ktheArkaBankaSipasIdDt(int idArkaBanka)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            DataTable dt = data.merrBankeSipasId(idArkaBanka);
            return dt;
        }

        /// <summary>
        /// kthen Id e bankes ne baze te kodit te bankes dhe id ndermarrjes
        /// </summary>
        /// <param name="kodBanka"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheIdBanka(string kodBanka, int idNdermarrje)
        {            
            using (clsDatabaseArkaBanka data = new clsDatabaseArkaBanka())
            {
                return data.ktheIdBankaSipasKodDheIdNderm(kodBanka, idNdermarrje);
            }
        }

        /// <summary>
        /// kthen Id e bankes ne baze te kodit te bankes dhe id ndermarrjes
        /// </summary>
        /// <param name="kodBanka"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheLlojBanka(string kodBanka, int idNdermarrje)
        {
            using (clsDatabaseArkaBanka data = new clsDatabaseArkaBanka())
            {
                return data.ktheLlojBankaSipasKodDheIdNderm(kodBanka, idNdermarrje);
            }
        }

        /// <summary>
        /// kthen Id e monedhes bankes ne baze te kodit te bankes dhe id ndermarrjes
        /// </summary>
        /// <param name="kodBanka"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheIdMonedhaBanka(string kodBanka, int idNdermarrje)
        {
            using (clsDatabaseArkaBanka data = new clsDatabaseArkaBanka())
            {
                return data.ktheIdMonedhaBankaSipasKodDheIdNderm(kodBanka, idNdermarrje);
            }
        }
        #endregion

        #region Metdoa Private

        private clsMesazh kontrolloBanka (ResourceManager rm, CultureInfo ci){
            if (kodiBanka == "")
                return new clsMesazh(false, "Plotesoni kodin e Bankes!");
            clsMesazh kontrollkodiBanka = clsFunksione.kontrolloKaraktereMeMesazh(kodiBanka, FusheKontrolli.Kodi, false);
            if (!kontrollkodiBanka.Status)
                return kontrollkodiBanka;

            if (emerBanka == "")
                return new clsMesazh(false, "Plotesoni emrin e Bankes!");
            clsMesazh kontrollemerBanka = clsFunksione.kontrolloKaraktereMeMesazh(emerBanka, FusheKontrolli.Emri, true);
            if (!kontrollemerBanka.Status)
                return kontrollemerBanka;

            return new clsMesazh(true, "Kontrollet e Bankes u kaluan me sukses");
        }
        #endregion

        #region Metoda Internal

        internal clsMesazh mbushBank(DataRow dbDataRowBank)
        {
            if (dbDataRowBank != null)
            {
                try
                {
                    int.TryParse(dbDataRowBank["IDBANKA"].ToString(), out idBanka);
                    kodiBanka = dbDataRowBank["KODIBANKA"].ToString();
                    emerBanka = dbDataRowBank["EMERBANKA"].ToString();
                    int.TryParse(dbDataRowBank["IDTIPIBANKA"].ToString(), out idTipiBanka);
                    nrLlogariBanka = dbDataRowBank["NRLLOGARIBANKA"].ToString();
                    iban = dbDataRowBank["IBAN"].ToString();
                    int.TryParse(dbDataRowBank["IDGRUPBANKE"].ToString(), out idGrupBanke);
                    shenimeBanka = dbDataRowBank["SHENIMEBANKA"].ToString();
                    Boolean.TryParse(dbDataRowBank["AKTIVBANKA"].ToString(), out aktivBanka);
                    int.TryParse(dbDataRowBank["IDLLOGARIKONTABILIZIMI"].ToString(), out idLlogariKontabilizimi);
                    int.TryParse(dbDataRowBank["IDMONEDHABANKA"].ToString(), out idMonedhaBanka);
                    rrugaBanka = dbDataRowBank["RRUGABANKA"].ToString();
                    qytetiBanka = dbDataRowBank["QYTETIBANKA"].ToString();
                    shtetiBanka = dbDataRowBank["SHTETIBANKA"].ToString();
                    zipKodBanka = dbDataRowBank["ZIPKODBANKA"].ToString();
                    telBanka = dbDataRowBank["TELBANKA"].ToString();
                    emerKontaktiBanka = dbDataRowBank["EMERKONTAKTIBANKA"].ToString();
                    mbiemerKontaktiBanka = dbDataRowBank["MBIEMERKONTAKTIBANKA"].ToString();
                    telKontaktiBanka = dbDataRowBank["TELKONTAKTIBANKA"].ToString();
                    faxKontaktiBanka = dbDataRowBank["FAXKONTAKTIBANKA"].ToString();
                    celKontaktiBanka = dbDataRowBank["CELKONTAKTIBANKA"].ToString();
                    emailKontaktiBanka = dbDataRowBank["EMAILKONTAKTIBANKA"].ToString();
                    int.TryParse(dbDataRowBank["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowBank["KOMISIONI"].ToString(), out komisioni);
                    int.TryParse(dbDataRowBank["IDNDERMARJE"].ToString(), out idNdermarje);
                    bool.TryParse(dbDataRowBank["LLOJARKABANKA"].ToString(), out llojArkaBanka);
                    adresaKontaktiBanka = dbDataRowBank["ADRESAKONTAKTIBANKA"].ToString();
                    int.TryParse(dbDataRowBank["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowBank["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(dbDataRowBank["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowBank["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowBank["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    nrGrupBanke = dbDataRowBank["NRGRUPBANKE"].ToString();
                    nrLlogari = dbDataRowBank["NrLlogari"].ToString();
                    kodMonedha = dbDataRowBank["MONEDHAKOD"].ToString();
                    nrKomisioni = dbDataRowBank["NrKomisioni"].ToString();

                    dega = dbDataRowBank["DEGA"].ToString();
                    valuta = dbDataRowBank["VALUTA"].ToString();
                    nrKlienti = dbDataRowBank["NRKLIENTI"].ToString();
                    kodiLlogarise = dbDataRowBank["KODILLOGARISE"].ToString();
                    tipi = dbDataRowBank["TIPI"].ToString();
                    kodiTCR = dbDataRowBank["KODITCR"].ToString();
                    nrRendor = dbDataRowBank["NUMRIRENDOR"].ToString();
                    if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV5())
                        bool.TryParse(dbDataRowBank["SHFAQNEEINVOICE"].ToString(), out shfaqNeEinvoice);
                    else
                        shfaqNeEinvoice = false;
                    return new clsMesazh(true, "Banka u mbush me sukses");
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se bankes nga db-ja");
                }
            }
            else
                return new clsMesazh(false, "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja"); ;
        }
        internal clsMesazh mbushBankPaTCR(DataRow dbDataRowBank)
        {
            if (dbDataRowBank != null)
            {
                try
                {
                    int.TryParse(dbDataRowBank["IDBANKA"].ToString(), out idBanka);
                    kodiBanka = dbDataRowBank["KODIBANKA"].ToString();
                    emerBanka = dbDataRowBank["EMERBANKA"].ToString();
                    int.TryParse(dbDataRowBank["IDTIPIBANKA"].ToString(), out idTipiBanka);
                    nrLlogariBanka = dbDataRowBank["NRLLOGARIBANKA"].ToString();
                    iban = dbDataRowBank["IBAN"].ToString();
                    int.TryParse(dbDataRowBank["IDGRUPBANKE"].ToString(), out idGrupBanke);
                    shenimeBanka = dbDataRowBank["SHENIMEBANKA"].ToString();
                    Boolean.TryParse(dbDataRowBank["AKTIVBANKA"].ToString(), out aktivBanka);
                    int.TryParse(dbDataRowBank["IDLLOGARIKONTABILIZIMI"].ToString(), out idLlogariKontabilizimi);
                    int.TryParse(dbDataRowBank["IDMONEDHABANKA"].ToString(), out idMonedhaBanka);
                    rrugaBanka = dbDataRowBank["RRUGABANKA"].ToString();
                    qytetiBanka = dbDataRowBank["QYTETIBANKA"].ToString();
                    shtetiBanka = dbDataRowBank["SHTETIBANKA"].ToString();
                    zipKodBanka = dbDataRowBank["ZIPKODBANKA"].ToString();
                    telBanka = dbDataRowBank["TELBANKA"].ToString();
                    emerKontaktiBanka = dbDataRowBank["EMERKONTAKTIBANKA"].ToString();
                    mbiemerKontaktiBanka = dbDataRowBank["MBIEMERKONTAKTIBANKA"].ToString();
                    telKontaktiBanka = dbDataRowBank["TELKONTAKTIBANKA"].ToString();
                    faxKontaktiBanka = dbDataRowBank["FAXKONTAKTIBANKA"].ToString();
                    celKontaktiBanka = dbDataRowBank["CELKONTAKTIBANKA"].ToString();
                    emailKontaktiBanka = dbDataRowBank["EMAILKONTAKTIBANKA"].ToString();
                    int.TryParse(dbDataRowBank["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowBank["KOMISIONI"].ToString(), out komisioni);
                    int.TryParse(dbDataRowBank["IDNDERMARJE"].ToString(), out idNdermarje);
                    bool.TryParse(dbDataRowBank["LLOJARKABANKA"].ToString(), out llojArkaBanka);
                    adresaKontaktiBanka = dbDataRowBank["ADRESAKONTAKTIBANKA"].ToString();
                    int.TryParse(dbDataRowBank["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowBank["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(dbDataRowBank["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowBank["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowBank["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    nrGrupBanke = dbDataRowBank["NRGRUPBANKE"].ToString();
                    nrLlogari = dbDataRowBank["NrLlogari"].ToString();
                    kodMonedha = dbDataRowBank["MONEDHAKOD"].ToString();
                    nrKomisioni = dbDataRowBank["NrKomisioni"].ToString();

                    dega = dbDataRowBank["DEGA"].ToString();
                    valuta = dbDataRowBank["VALUTA"].ToString();
                    nrKlienti = dbDataRowBank["NRKLIENTI"].ToString();
                    kodiLlogarise = dbDataRowBank["KODILLOGARISE"].ToString();
                    tipi = dbDataRowBank["TIPI"].ToString();
                    if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV5())
                        bool.TryParse(dbDataRowBank["SHFAQNEEINVOICE"].ToString(), out shfaqNeEinvoice);
                    else
                        shfaqNeEinvoice = false;
                    return new clsMesazh(true, "Banka u mbush me sukses");
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se bankes nga db-ja");
                }
            }
            else
                return new clsMesazh(false, "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja"); ;
        }

        public clsMesazh mbushBank(clsBanka arkaBanka)
        {
            IdBanka = arkaBanka.IdBanka;
            KodiBanka = arkaBanka.KodiBanka;
            EmerBanka = arkaBanka.EmerBanka;
            IdTipiBanka = arkaBanka.IdTipiBanka;
            NrLlogariBanka = arkaBanka.NrLlogariBanka;
            IBAN = arkaBanka.IBAN;
            IdGrupBanke = arkaBanka.IdGrupBanke;
            ShenimeBanka = arkaBanka.ShenimeBanka;
            AktivBanka = arkaBanka.AktivBanka;
            IdLlogariKontabilizimi = arkaBanka.IdLlogariKontabilizimi;
            IdMonedhaBanka = arkaBanka.IdMonedhaBanka;
            RrugaBanka = arkaBanka.RrugaBanka;
            QytetiBanka = arkaBanka.QytetiBanka;
            ShtetiBanka = arkaBanka.ShtetiBanka;
            ZipKodBanka = arkaBanka.zipKodBanka;
            TelBanka = arkaBanka.TelBanka;
            EmerKontaktiBanka = arkaBanka.EmerKontaktiBanka;
            MbiemerKontaktiBanka = arkaBanka.MbiemerKontaktiBanka;
            TelKontaktiBanka = arkaBanka.TelKontaktiBanka;
            FaxKontaktiBanka = arkaBanka.FaxKontaktiBanka;
            CelKontaktiBanka = arkaBanka.CelKontaktiBanka;
            EmailKontaktiBanka = arkaBanka.EmailKontaktiBanka;
            IdPerdoruesi = arkaBanka.IdPerdoruesi;
            Komisioni = arkaBanka.Komisioni;
            IdNdermarje = arkaBanka.IdNdermarje;
            LlojArkaBanka = arkaBanka.LlojArkaBanka;
            AdresaKontaktiBanka = arkaBanka.AdresaKontaktiBanka;
            IdKonfig = arkaBanka.IdKonfig;
            IdDegeAdministrative = arkaBanka.IdDegeAdministrative;
            IdStatusDok = arkaBanka.IdStatusDok;
            DtKrijimi = arkaBanka.DtKrijimi;
            DtModifikimi = arkaBanka.DtModifikimi;
            NrGrupBanke = arkaBanka.NrGrupBanke;
            NrLlogari = arkaBanka.NrLlogari;
            KodMonedha = arkaBanka.KodMonedha;
            NrKomisioni = arkaBanka.NrKomisioni;
            Dega = arkaBanka.Dega;
            Valuta = arkaBanka.Valuta;
            NrKlienti = arkaBanka.NrKlienti;
            KodiLlogarise = arkaBanka.KodiLlogarise;
            Tipi = arkaBanka.Tipi;
            KodiTCR = arkaBanka.KodiTCR;
            NrRendor = arkaBanka.NrRendor;
            ShfaqNeEinvoice = arkaBanka.ShfaqNeEinvoice;
            return new clsMesazh(true, $"Mbushja e arkes/bankes me kod {arkaBanka.KodiBanka} u krye me sukses!");
        }

        #endregion
    }
}
