using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;
using DbCore.Integrime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbOTC
{
    /// <summary>
    /// 
    /// </summary>
    public class OTCPagesa : OTCLogData, IDataBaseReader
    {
        #region Atributet
        private bool merrSmsNjoftuese;
        private DateTime dtPagese;
        private decimal vleraPaguar;
        private OTCLlojSherbimi llojPagese;
        private string transactionID;
        private decimal komisioni;
        private string nrKontakti;
        private string nrSerial;
        private int pagesaID;
        private int idTransferta;
        private int idBalanca;
        private int paguesKomisioni;
        private string paguesi;
        private int idDokAnullues;
        private bool dokAnullimi;
        private OTCMPESATransferMoney transferta;
        private OTCMPESACheckBalance balanca;
        #endregion

        #region Properties
        /// <summary>
        /// kthen/vendos id e pageses
        /// </summary>
        public int PagesaID
        {
            get { return pagesaID; }
            set { pagesaID = value; }
        }
        /// <summary>
        /// kthen/vendos numrin serial
        /// </summary>
        public string NrSerial
        {
            get { return nrSerial; }
            set { nrSerial = value; }
        }
        /// <summary>
        /// kthen/vendos numrin e kontaktit
        /// </summary>
        public string NrKontakti
        {
            get { return nrKontakti; }
            set { nrKontakti = value; }
        }

        public int IdTtransferta
        {
            get { return idTransferta; }
            set { idTransferta = value; }
        }
        public int IdBalanca
        {
            get { return idBalanca; }
            set { idBalanca = value; }
        }
        /// <summary>
        /// kthen/vendos mesazhin njoftues
        /// </summary>
        public bool MerrSmsNjoftuese
        {
            get { return merrSmsNjoftuese; }
            set { merrSmsNjoftuese = value; }
        }
        /// <summary>
        /// kthen/vendos daten e pageses
        /// </summary>
        public DateTime DtPagese
        {
            get { return dtPagese; }
            set { dtPagese = value; }
        }



        /// <summary>
        /// kthen/vendos komisionin
        /// </summary>
        public decimal Komisioni
        {
            get { return komisioni; }
            set { komisioni = value; }
        }
        public int PaguesKomisioni
        {
            get { return paguesKomisioni; }
            set { paguesKomisioni = value; }
        }
        /// <summary>
        /// kthen/vendos vleren e paguar
        /// </summary>
        public decimal VleraPaguar
        {
            get { return vleraPaguar; }
            set { vleraPaguar = value; }
        }
        /// <summary>
        /// kthen/vendos llojin e pageses
        /// </summary>
        public OTCLlojSherbimi LlojPagese
        {
            get { return llojPagese; }
            set { llojPagese = value; }
        }
        /// <summary>
        /// kthen/vendos id e transaksionit
        /// </summary>
        public string TransactionID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }
        /// <summary>
        /// kthen/vendos id e dokumentit qe ka anulluar kete dokument
        /// </summary>
        public int IdDokAnullues
        {
            get { return idDokAnullues; }
            set { idDokAnullues = value; }
        }

        public bool DokAnullimi
        {
            get { return dokAnullimi; }
            set { dokAnullimi = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public colOTCFatura Faturat { get; set; } = new colOTCFatura();

        public OTCMPESATransferMoney Transferta
        {
            get { return transferta; }
            set
            {
                transferta = value;
                if (transferta != null) idTransferta = transferta.Id;
            }
        }

        public OTCMPESACheckBalance Balanca
        {
            get { return balanca; }
            set
            {
                balanca = value;
                if (balanca != null) idBalanca = balanca.Id;
            }

        }

        public string Paguesi
        {
            get
            {
                return paguesi;
            }

            set
            {
                paguesi = value;
            }
        }

        #endregion


        public OTCPagesa()
        {

        }

        public OTCPagesa(IDataRecord record)
        {
            Mbush(record);
        }

        public OTCPagesa(int PagesaId)
        {
            MerrSipasId(PagesaId);
        }

        private void MerrSipasId(int PagesaId)
        {
            using (clsDatabaseOTC dbOTC = new clsDatabaseOTC())
            {
                dbOTC.MerrPageseSipasId(PagesaId, this);
                Faturat = new colOTCFatura(PagesaID);
                Transferta = new OTCMPESATransferMoney
                {
                    Id = idTransferta
                };
                Transferta.LexoTransferte(dbOTC);
                Transferta.NrFature = Faturat[0].NrFature;
            }
        }
        public decimal MerrTotalTePaguarPerSMS()
        {

            switch (LlojPagese)
            {
                case OTCLlojSherbimi.OSHEE:
                    return VleraPaguar;
                case OTCLlojSherbimi.UKT:
                    return VleraPaguar + Komisioni;
                default:
                    throw new MyException($"Lloji i sherbimit {LlojPagese} nuk eshte i percaktuar per te derguar SMS");
            }

        }
        public override clsMesazh Ruaj()
        {
            try
            {
                using (var scope = new MyTransactionScope())
                {
                    var mesazh = new clsMesazh();
                    var idDokOrigjinal = PagesaID;
                    if (!DokAnullimi)
                    {
                        mesazh = Transferta.Ruaj();
                        if (!mesazh) return mesazh;
                    }
                    nrSerial = vendosNrAutomatik(200 + Convert.ToInt32(DokAnullimi));
                    mesazh = RuajKoken();
                    if (!mesazh) return mesazh;
                    Faturat.ForEach(f => f.IdPagesa = PagesaID);

                    mesazh = Faturat.Ruaj();

                    if (mesazh.Status && DokAnullimi)
                        mesazh = UpdateIdDokAnullues(idDokOrigjinal, PagesaID);

                    if (mesazh) scope.Complete();
                    return mesazh;
                }
            }
            catch (MyException myEx)
            {
                ImbLogger.LogOTC($"Deshtoi ruajtja e pageses {myEx.ToString()} => ", this);
                return new clsMesazh(false, myEx.Message);
            }
            catch (Exception ex)
            {
                ImbLogger.LogOTC($"Deshtoi ruajtja e pageses {ex.ToString()} => ", this);
                return new clsMesazh(false, "Deshtoi ruajtja e pageses ne db!");
            }
        }

        public clsMesazh Modifiko()
        {
            using (var db = new clsDatabaseOTC())
            {
                db.ModifikoPagese(PagesaID, NrSerial, NrKontakti, MerrSmsNjoftuese, DtPagese, IdNdermarrje, IdPerdoruesiAlphaWeb, IdPerdoruesiMpesa, Komisioni, VleraPaguar, LlojPagese, TransactionID, MesazhTransaksioni, StatusTransaksioni, IdTtransferta, IdBalanca, DtKrijimi, DtModifikimi);
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        private string vendosNrAutomatik(int idKategori)
        {
            int idNdermarrjeMeme = clsNdermarrje.ktheIdNdermarrjeMeme();
            colNrAutom colNrAuto = new colNrAutom();
            colNrAuto.mbushGjitheNumratAutomatikeSipasKategorise(idNdermarrjeMeme, idKategori);
            clsNrAutom NrAutom = colNrAuto.FirstOrDefault();
            if (NrAutom == null)
                throw new MyException("Nuk ekziston nje numer automatik per kete kategori dokumenti!");

            string NrSerial = NrAutom.ktheVlerenParsardheseNrAutomatik(DateTime.Now);
            clsNrAutomatikFundit nrFundit = NrAutom.ktheNrAutomatikFundit(DateTime.Now);

            clsMesazh mesazh;
            if (nrFundit != null)
            {
                nrFundit.Vlera = NrSerial;
                nrFundit.Data = DateTime.Now;
                nrFundit.IdPerdoruesi = IdPerdoruesiAlphaWeb;
                mesazh = nrFundit.modifiko();
            }
            else
            {
                nrFundit = new clsNrAutomatikFundit();
                nrFundit.IdNrAutom = NrAutom.IdNrAutom;
                nrFundit.Vlera = NrSerial;
                nrFundit.Data = DateTime.Now;
                nrFundit.IdPerdoruesi = IdPerdoruesiAlphaWeb;
                nrFundit.IdStatusDok = 1;
                nrFundit.IdNdermarje = IdNdermarrje;
                mesazh = nrFundit.ruaj();
            }
            if (!mesazh)
                throw new MyException(mesazh.PershkrimMesazhi);

            return NrSerial;
        }

        public clsMesazh Anullo(int idPerdoruesMPESA, int idPerdoruesiAlphaWeb)
        {
            DokAnullimi = true;
            VleraPaguar = -VleraPaguar;
            komisioni = 0;
            DtPagese = DateTime.Now;
            IdPerdoruesiMpesa = idPerdoruesMPESA;
            IdPerdoruesiAlphaWeb = idPerdoruesiAlphaWeb;
            idTransferta = 0;
            Balanca = null;
            Transferta = null;
            StatusTransaksioni = StatusOTC.Undefined;
            Faturat.ForEach(fature =>
            {
                fature.Interesi = -fature.Interesi;
                fature.VleraFillestareFatures = -fature.VleraFillestareFatures;
            });
            return Ruaj();
        }

        public clsMesazh UpdateIdTransferimi()
        {
            idTransferta = Transferta.Id;
            using (clsDatabaseOTC db = new clsDatabaseOTC())
                return db.updateIdTransferta(pagesaID, idTransferta);
        }
        public static clsMesazh UpdateIdDokAnullues(int PagesaId, int idDokAnullues)
        {

            using (clsDatabaseOTC db = new clsDatabaseOTC())
                return db.UpdateIdDokAnullues(PagesaId, idDokAnullues);
        }
        /// <summary>
        /// Ruan fushat e ketij objekti ne db
        /// </summary>
        /// <returns></returns>
        private clsMesazh RuajKoken()
        {
            using (clsDatabaseOTC db = new clsDatabaseOTC())
            {
                //fushat mesazh transaksioni dhe statusPagese lidhen me pergjigjen qe vjen nga sistemi i pagesave
                PagesaID = db.RuajPagese(NrSerial, NrKontakti, MerrSmsNjoftuese, DtPagese, IdNdermarrje, IdPerdoruesiAlphaWeb, IdPerdoruesiMpesa, Komisioni, VleraPaguar, LlojPagese, TransactionID, MesazhTransaksioni, StatusTransaksioni, Transferta == null ? 0 : Transferta.Id, Balanca == null ? 0 : Balanca.Id, Paguesi, paguesKomisioni, DokAnullimi, IdDokAnullues);
                idTransferta = Transferta == null ? 0 : Transferta.Id;
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        public clsMesazh Valido()
        {
            if (KonfigurimeStatikeIntegrimi.FakeResponse) return new clsMesazh(true);

            if (Faturat == null || Faturat.Count == 0) return new clsMesazh(false, "Nuk eshte zgjedhur asnje fature per tu paguar!");

            foreach (var fatura in Faturat)
            {
                if (fatura.EkzistonFaturaSipasNrSerial()) return new clsMesazh(false, $"Fatura me nr {fatura.NrFature} rezulton te jete paguar njehere!");
            }
            return new clsMesazh(true, "Validimi kaloi me sukses!");
        }

        public clsMesazh RiruajOperacionTransferimi()
        {
            using (var scope = new MyTransactionScope())
            {
                Transferta.StatusTransaksioni = StatusOTC.Pending;

                var mesazh = Transferta.Ruaj();
                if (!mesazh) return mesazh;

                mesazh = UpdateIdTransferimi();
                if (!mesazh) return mesazh;

                scope.Complete();
                return mesazh;
            }
        }


        public void Mbush(IDataRecord record)
        {

            nrSerial = record["NrSerial"]?.ToString();
            nrKontakti = record["NrKontakti"]?.ToString();
            bool.TryParse(record["MerrSmsNjoftuese"]?.ToString(), out merrSmsNjoftuese);
            DateTime.TryParse(record["DtPagese"]?.ToString(), out dtPagese);
            int.TryParse(record["IdNdermarrje"]?.ToString(), out idNdermarrje);
            int.TryParse(record["IdPerdoruesiALphaWeb"]?.ToString(), out idPerdoruesiAlphaWeb);
            int.TryParse(record["IdPerdoruesiMPESA"]?.ToString(), out idPerdoruesiMPESA);
            int.TryParse(record["PagesaId"]?.ToString(), out pagesaID);
            int.TryParse(record["IdTransferta"]?.ToString(), out idTransferta);
            decimal.TryParse(record["Komisioni"]?.ToString(), out vleraPaguar);
            decimal.TryParse(record["VleraPaguar"]?.ToString(), out vleraPaguar);
            int.TryParse(record["PAGUES_KOMISIONI"]?.ToString(), out paguesKomisioni);
            int.TryParse(record["ID_DOK_ANULLUES"]?.ToString(), out idDokAnullues);
            bool.TryParse(record["DokAnullimi"]?.ToString(), out dokAnullimi);
            int llojPagese = 0;
            if (int.TryParse(record["LlojPagese"]?.ToString(), out llojPagese))
                LlojPagese = (OTCLlojSherbimi)llojPagese;
            transactionID = record["TransactionID"]?.ToString();
            mesazhTransaksioni = record["MesazhiNgaUtiliteti"]?.ToString();
            Enum.TryParse(record["StatusiPageses"]?.ToString(), out _statusTransaksioni);
            paguesi = record["Paguesi"]?.ToString();



        }
    }
}
