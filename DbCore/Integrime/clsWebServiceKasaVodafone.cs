using System;
using System.Collections.Generic;
using DbCore.KasaService;
using DbCore.DbInventari;
using DbCore.IMBUtils.Security;

namespace DbCore.Integrime
{
    public class clsWebServiceKasaVodafone
    {

        #region Atribute

        private string requestSystem;
        private string transID;
        private string ip;
        private int idShop;
        private string advertisement;
        private List<Item> artikujtKase;
        private string url;
        #endregion

        #region Properties

        public string RequestSystem
        {
            get
            {
                return requestSystem;
            }
            set
            {
                requestSystem = value;
            }
        }

        public string TransID
        {
            get
            {
                return transID;
            }
            set
            {
                transID = value;
            }
        }

        public string Ip
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
            }
        }

        public int IdShop
        {
            get
            {
                return idShop;
            }
            set
            {
                idShop = value;
            }
        }

        public string Advertisement
        {
            get
            {
                return advertisement;
            }
            set
            {
                advertisement = value;
            }
        }

        public List<Item> ArtikujtKase
        {
            get
            {
                return artikujtKase;
            }
            set
            {
                artikujtKase = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }

        #endregion

        #region Konstruktori

        public clsWebServiceKasaVodafone(string URL)
        {
            this.url = URL;
        }

        /// <summary>
        /// Konstruktori qe do te vij me parametra nga ruajtja e dokumentit
        /// </summary>
        /// <param name="transId">ID e transaksionit. Do te jete ID unike per cdo transaksion</param><example> username_yyyymmddhhiiss</example>
        /// <param name="ip">Ip e kases nga do printohet</param>
        /// <param name="idshop">Id e shop qe do te printoje ne kase</param>
        /// <param name="idNdermarrje">Id e ndermarrjes qe do te printoje ne kase</param>
        /// <param name="adv">Parameter Opsional. Ketu do te vije nr serial nese do te jete fature tatimore. Nese nuk vjen asnje nr atehere merr vlere default.</param>
        /// <param name="artikujKase">Do te jete trupi i shitjes, i cili do te permbaj artikujt qe do te printohen ne kase</param>
        //public clsWebServiceKasaVodafone(string transId, string ip, int idshop, int idNdermarrje, DbRegjistrim.colTrupiShitje artikujKase, string adv = "www.vodafone.al")
        //{
        //    //Do te jete gjithmone AlphaWeb pasi tregon se cili program po kerkon printimin.
        //    this.requestSystem = "AlphaWeb";
        //    this.transID = clsEnDecVodafone.enkriptoMesazh(transId);
        //    this.ip = ip;
        //    //this.ip =  "10.5.47.136";
        //    this.idShop = idshop;
        //    this.advertisement = adv;
        //    ArtikujtKase = new List<Item>();
        //    krijoArtikujKase(artikujKase, idNdermarrje);
        //}
        public clsWebServiceKasaVodafone(string URL, string transId, string ip, DbRegjistrim.clsKokaShitje fatura, string adv = "www.vodafone.al")
        {
            this.url = URL;
            //Do te jete gjithmone AlphaWeb pasi tregon se cili program po kerkon printimin.
            this.requestSystem = "AlphaWeb";
            this.transID = clsEnDecVodafone.enkriptoMesazh(transId);
            this.ip = ip;
            //this.ip =  "10.5.47.136";
            this.idShop = fatura.IdDegeAdministrative;
            this.advertisement = adv;
            ArtikujtKase = new List<Item>();
            krijoArtikujKase(fatura);
        }
        public clsWebServiceKasaVodafone(string URL, string transId, string ip, int idshop, int idNdermarrje, int idperdorues, DbArkaBanka.clsVeprimBankaKoka artikujKase, string adv = "www.vodafone.al")
        {
            this.url = URL;
            //Do te jete gjithmone AlphaWeb pasi tregon se cili program po kerkon printimin.
            this.requestSystem = "AlphaWeb";
            this.transID = clsEnDecVodafone.enkriptoMesazh(transId);
            this.ip = ip;
            //this.ip =  "10.5.47.136";
            this.idShop = idshop;
            this.advertisement = adv;
            ArtikujtKase = new List<Item>();
            krijoBankaKase(artikujKase, idNdermarrje, idperdorues);
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Metode publike qe printon ne kase veprimin si kupon Tatimor
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="idUser">Id e userit qe po ben printimin</param>
        /// <returns>Kthne true nese veprimi printohet, dhe anasjelltas</returns>
        public bool printoKuponTatimor(int idShitje, int idUser, bool shitje)
        {
            bool statusiPrintimit;
            DbInventari.clsPrintimeKase printimiKase;
            bool derguar = true;
            try
            {
                KasaWebService webServisi = new KasaWebService();
                webServisi.Url = this.url;
                CommandRequest printimi = new CommandRequest();
                krijoCommandRequest(printimi);
                statusiPrintimit = webServisi.PrintoKuponTatimor(printimi);
                derguar = true;
            }
            catch
            {
                statusiPrintimit = false;
                derguar = false;
            }

            //Pasi merr pergjigjen e printimit e ruan ne databaze
            if (statusiPrintimit)
                printimiKase = new DbInventari.clsPrintimeKase(shitje ? idShitje : 0, statusiPrintimit, derguar, this.ip, this.idShop, "U printua ne kase!", DateTime.Now, idUser, shitje ? 0 : idShitje);
            else
                printimiKase = new DbInventari.clsPrintimeKase(shitje ? idShitje : 0, statusiPrintimit, derguar, this.ip, this.idShop, "Nuk u printua ne kase!", DateTime.Now, idUser, shitje ? 0 : idShitje);

            //Nese ekziston idShitjes ne tabele, ath kryen thjesht modifikim te statusit te printimit.
            if ((shitje && !printimiKase.merrDergimeKaseSipasIdShitje(idShitje)) || (!shitje && !printimiKase.ktheDergimeKaseSipasIdBanka(idShitje)))
                printimiKase.ruajPrintimKase();
            else
                printimiKase.modifikoPrintimKase();

            return statusiPrintimit;
        }

        /// <summary>
        /// Metode publike qe printon ne kase veprimet si fatura tatimore, pra kuponi do te nevojitet jo tatimor.
        /// </summary>
        /// <param name="idShitje">Id e dokumentit te shitjes</param>
        /// <param name="idUser">Id e userit qe po ben printimin</param>
        /// <returns>Kthne true nese veprimi printohet, dhe anasjelltas</returns>
        public bool printoFatureTatimore(int idShitje, int idUser, bool shitje)
        {
            bool statusiPrintimit;
            bool derguar = true;
            try
            {
                KasaWebService webServisi = new KasaWebService
                {
                    Url = this.url
                };
                CommandRequest printimi = new CommandRequest();
                krijoCommandRequest(printimi);
                statusiPrintimit = webServisi.PrintoKuponJoTatimor(printimi);
                derguar = true;
            }
            catch
            {
                statusiPrintimit = false;
                derguar = false;
            }

            //Pasi merr pergjigjen e printimit e ruan ne databaze
            var printimiKase = statusiPrintimit ? new clsPrintimeKase(shitje ? idShitje : 0, true, derguar, this.ip, this.idShop, "U printua ne kase!", DateTime.Now, idUser, shitje ? 0 : idShitje) : new DbInventari.clsPrintimeKase(shitje ? idShitje : 0, false, derguar, this.ip, this.idShop, "Nuk u printua ne kase!", DateTime.Now, idUser, shitje ? 0 : idShitje);

            //Nese ekziston idShitjes ne tabele, ath kryen thjesht modifikim te statusit te printimit.
            if ((shitje && !printimiKase.merrDergimeKaseSipasIdShitje(idShitje)) || (!shitje && !printimiKase.ktheDergimeKaseSipasIdBanka(idShitje)))
                printimiKase.ruajPrintimKase();
            else
                printimiKase.modifikoPrintimKase();

            return statusiPrintimit;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metode private per mbushjen e elementeve qe kerkon kasa per te printuar
        /// </summary>
        /// <param name="printimi">Objekti qe do te mbushet per printim</param>
        private void krijoCommandRequest(CommandRequest printimi)
        {
            printimi.RequestSystem = RequestSystem;
            printimi.TransactionID = TransID;
            printimi.IP = Ip;
            printimi.IdShop = IdShop;
            printimi.Advertisement = Advertisement;
            printimi.Items = ArtikujtKase.ToArray();
        }

        /// <summary>
        /// Mbushja e trupit qe do te printohet ne kase nga trupi i shitjes
        /// </summary>
        /// <param name="artKase">Trupi i shitjes qe do te perdoret per tu printuar ne kase</param>
        /// <param name="idNdermarrje">Id e ndermarrjes qe do te printoje kuponin</param>
        private void krijoArtikujKase(DbRegjistrim.clsKokaShitje kokaShitje)
        {
            foreach (DbRegjistrim.clsTrupiShitje artikullShitje in kokaShitje.OColTrupiShitje)
            {
                //Default 20% ==> 3
                string kodiTakses = "3";
                Item artikulliKase = new Item();
                DbInventari.clsArtikulli art = new DbInventari.clsArtikulli();
                art.merrSipasKodArtikullit(artikullShitje.Kodi, kokaShitje.IdNdermarrje);
                if (art.IdTvsh > 0)
                    kodiTakses = DbRegjistrim.clsTaksa.ktheKodTakseMeId(art.IdTvsh);
                int departamenti = 3;
                int.TryParse(kodiTakses, out departamenti);
                artikulliKase.Departamenti = departamenti;
                artikulliKase.Emertimi = artikullShitje.Kodi;
                artikulliKase.Sasia = float.Parse(artikullShitje.Sasia.ToString());
                artikulliKase.Cmimi = float.Parse((artikullShitje.Cmimi - kokaShitje.Zbritje).ToString());
                ArtikujtKase.Add(artikulliKase);
            }
        }
        private void krijoBankaKase(DbArkaBanka.clsVeprimBankaKoka artKase, int idNdermarrje, int idperdorues)
        {

            //Default 0% ==> 1
            string kodiTakses = System.Web.Configuration.WebConfigurationManager.AppSettings["reparti"];
            Item artikulliKase = new Item();
            int departamenti = 1;
            int.TryParse(kodiTakses, out departamenti);
            artikulliKase.Departamenti = departamenti;
            artikulliKase.Emertimi = System.Web.Configuration.WebConfigurationManager.AppSettings["mesazhkasepagesa"];
            artikulliKase.Sasia = 1;
            artikulliKase.Cmimi = float.Parse(artKase.VleraMonedhaBaze.ToString());
            ArtikujtKase.Add(artikulliKase);

        }
        #endregion

    }
}
