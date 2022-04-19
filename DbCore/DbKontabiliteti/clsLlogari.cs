using System;
using System.Data;
using DbCore.DbAdmin;
using System.Drawing;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje llogari
    ///  (Te dhenat  merren nga tabela : T_LLOGARI)
    /// </remarks>
    public class clsLlogari
    {
        #region Atribute

        private int idLlogari;
        private string nrLlogari;
        private string emerLlogari1;
        private string emerLlogari2;
        private string emerLlogariFr;
        private int qenderKosto;
        private int kpf1;
        private int kpf2;
        private int kpf3;
        private int nivelTakse;
        private int idMonedha;
        private int grupi;
        private int nengrupi;
        private int llogariKonsoliduese;
        private int llogariKoresponduese;
        private int idNdermarja;
        //private int idNderViti;
        private string pershkrimiGrupiLlogaria;
        private string pershkrimiNenGrupiLlogaria;
        private string kodiMonedha;
        private string kodiKPF1;
        private string kodiKPF2;
        private string kodiKPF3;
        private int idKonfig;
        private int idPerdoruesi;
        private string pershkrimiMonedha;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string pershkrimTaksa;
        private int idObjektivaKosto;
        private int idSkemaQendraKosto;
        private int llojQendre;
        private string objektiva;
        private string qendra;
        private int idKategoriShpenzimi;
        private string kategoriShpenzimi;
        private colLidhjetAutorizim oColLidhjeAutorizim;
        private colVleraFushaShtese oColVleratFushatShtese;
        private bool aktiv;
        private DataRow rreshti;
        private string shenime1;
        private string shenime2;
        private string shenime3;
        private string shenime4;
        private string shenime5;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases   
        /// </summary>
        /// <param name="idLlog">id e llogarise</param>
        /// <param name="nrLlog">nr i llogarise</param>
        /// <param name="emerLlog1">emri i pare i llogarise</param>
        /// <param name="emerLlog2">emri i dyte i llogarise </param>
        /// <param name="emerLlogFr">emri ne frengjisht i llogarise </param>
        /// <param name="qenderkosto">qendra e kostos</param>
        /// <param name="kpf1">kpf1</param>
        /// <param name="kpf2">kpf2</param>
        /// <param name="kpf3">kpf3</param>
        /// <param name="niveltakse">niveli i takses</param>
        /// <param name="mon">id e monedhes</param>
        /// <param name="grup">grupi qe ben pjese llogaria</param>
        /// <param name="nengrup">nengrupi qe ben pjese llogaria</param>
        /// <param name="llogkonsoliduese">llogaria konsoliduese</param>
        /// <param name="llogkoresponduese">llogaria korresponduese</param>
        /// <param name="idndermarja">id e ndermarrjes</param>        
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="idkategorishpenzimi"></param>
        /// <param name="idobjektivakosto"></param>
        /// <param name="idskemaqendrakosto"></param>
        /// <param name="idstatusdok"></param>
        /// <param name="kategorishpenzimi"></param>
        /// <param name="kodimonedha"></param>
        /// <param name="kodTaksa"></param>
        /// <param name="kpf1d"></param>
        /// <param name="kpf2d"></param>
        /// <param name="kpf3d"></param>
        /// <param name="llojqendre"></param>
        /// <param name="pershkrimigrupillogaria"></param>
        /// <param name="pershkriminengrupillogaria"></param>
        /// <param name="shenime1"></param> 
        /// <param name="shenime2"></param> 
        /// <param name="shenime3"></param> 
        /// <param name="shenime4"></param> 
        /// <param name="shenime5"></param> 
        public clsLlogari(int idLlog, string nrLlog, string emerLlog1, string emerLlog2, string emerLlogFr, int qenderkosto, int kpf1, int kpf2, int kpf3, int niveltakse, int mon, int grup, int nengrup, int llogkonsoliduese, int llogkoresponduese, int idndermarja, string pershkrimigrupillogaria, string pershkriminengrupillogaria, string kodimonedha, string kpf1d, string kpf2d, string kpf3d, int idperdoruesi, int idkonfig, string kodTaksa, int idstatusdok, int idobjektivakosto, int idskemaqendrakosto, int llojqendre, int idkategorishpenzimi, string kategorishpenzimi, bool aktiv , string shenime1 , string shenime2 , string shenime3 , string shenime4 , string shenime5)
        {
            idLlogari = idLlog;
            nrLlogari = nrLlog;
            emerLlogari1 = emerLlog1;
            emerLlogari2 = emerLlog2;
            emerLlogariFr = emerLlogFr;
            qenderKosto = qenderkosto;
            this.kpf1 = kpf1;
            this.kpf2 = kpf2;
            this.kpf3 = kpf3;
            nivelTakse = niveltakse;
            pershkrimTaksa = kodTaksa;
            idKategoriShpenzimi = idkategorishpenzimi;
            kategoriShpenzimi = kategorishpenzimi;
            idMonedha = mon;
            grupi = grup;
            nengrupi = nengrup;
            llogariKonsoliduese = llogkonsoliduese;
            llogariKoresponduese = llogkoresponduese;
            idNdermarja = idndermarja;
            pershkrimiNenGrupiLlogaria = pershkriminengrupillogaria;
            pershkrimiGrupiLlogaria = pershkrimigrupillogaria;
            kodiMonedha = kodimonedha;
            kodiKPF1 = kpf1d;
            kodiKPF2 = kpf2d;
            kodiKPF3 = kpf3d;
            idKonfig = idkonfig;
            idStatusDok = idstatusdok;
            idObjektivaKosto = idobjektivakosto;
            idSkemaQendraKosto = idskemaqendrakosto;
            llojQendre = llojqendre;
            idPerdoruesi = idperdoruesi;
            oColLidhjeAutorizim = new colLidhjetAutorizim();
            oColVleratFushatShtese = new colVleraFushaShtese();
            this.aktiv = aktiv;
            this.shenime1 = shenime1;
            this.shenime2 = shenime2;
            this.shenime3 = shenime3;
            this.shenime4 = shenime4;
            this.shenime5 = shenime5;
        }

        public clsLlogari(string nrLlog, string emerLlog1, string emerLlog2, string emerLlogFr, int idqenderkosto, string qendraKosto, int kpf1, int kpf2, int kpf3, int niveltakse, int mon, int grup, int nengrup, int llogkonsoliduese, int llogkoresponduese, int idndermarja, string pershkrimigrupillogaria, string pershkriminengrupillogaria, string kodimonedha, string kpf1d, string kpf2d, string kpf3d, int idperdoruesi, int idkonfig, string kodTaksa, int idstatusdok, int idobjektivakosto, string objektivaKosto, int idskemaqendrakosto, int llojqendre, int idkategorishpenzimi, string kategorishpenzimi, bool aktiv,  bool shtim, int idGjuha, ResourceManager rm, CultureInfo ci, colLidhjetAutorizim oColLidhjeAutorizim , string shenime1, string shenime2, string shenime3, string shenime4, string shenime5)
        {
            try
            {
                nrLlogari = nrLlog;
                emerLlogari1 = emerLlog1;
                emerLlogari2 = emerLlog2;
                emerLlogariFr = emerLlogFr;
                qenderKosto = idqenderkosto;
                qendra = qendraKosto;
                this.kpf1 = kpf1;
                this.kpf2 = kpf2;
                this.kpf3 = kpf3;
                nivelTakse = niveltakse;
                pershkrimTaksa = kodTaksa;
                idKategoriShpenzimi = idkategorishpenzimi;
                kategoriShpenzimi = kategorishpenzimi;
                idMonedha = mon;
                grupi = grup;
                nengrupi = nengrup;
                llogariKonsoliduese = llogkonsoliduese;
                llogariKoresponduese = llogkoresponduese;
                idNdermarja = idndermarja;
                pershkrimiNenGrupiLlogaria = pershkriminengrupillogaria;
                pershkrimiGrupiLlogaria = pershkrimigrupillogaria;
                kodiMonedha = kodimonedha;
                kodiKPF1 = kpf1d;
                kodiKPF2 = kpf2d;
                kodiKPF3 = kpf3d;
                idKonfig = idkonfig;
                idStatusDok = idstatusdok;
                objektiva = objektivaKosto;
                idObjektivaKosto = idobjektivakosto;
                idSkemaQendraKosto = idskemaqendrakosto;
                llojQendre = llojqendre;
                idPerdoruesi = idperdoruesi;
                this.oColLidhjeAutorizim = oColLidhjeAutorizim;
                oColVleratFushatShtese = new colVleraFushaShtese();
                this.aktiv = aktiv;
                this.shenime1 = shenime1;
                this.shenime2 = shenime2;
                this.shenime3 = shenime3;
                this.shenime4 = shenime4;
                this.shenime5 = shenime5;

                clsMesazh mesazh = kontrolloLlogari(shtim, idGjuha, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        private clsMesazh kontrolloLlogari(bool shtim, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            if (nrLlogari == "")
                return new clsMesazh(false, "Plotesoni numrin e llogarise!");
            clsMesazh kontrollnrLlogari = clsFunksione.kontrolloKaraktereMeMesazh(nrLlogari, FusheKontrolli.Kodi, false);
            if (!kontrollnrLlogari.Status)
                return kontrollnrLlogari;

            if (emerLlogari1 == "")
                return new clsMesazh(false, "Plotesoni emertimin e llogarise!");
            clsMesazh kontrollemerLlogari1 = clsFunksione.kontrolloKaraktereMeMesazh(emerLlogari1, FusheKontrolli.Emri, true);
            if (!kontrollemerLlogari1.Status)
                return kontrollemerLlogari1;
            clsMesazh kontrollemerLlogari2 = clsFunksione.kontrolloKaraktereMeMesazh(emerLlogari2, FusheKontrolli.Emri, true);
            if (!kontrollemerLlogari2.Status)
                return kontrollemerLlogari2;

            if (kodiMonedha == "")
                return new clsMesazh(false, "Plotesoni monedhen!");
            if (kodiKPF1 == "")
                return new clsMesazh(false, "Plotesoni strukturen e pare!");
            if (pershkrimiGrupiLlogaria == "")
                return new clsMesazh(false, "Plotesoni grupin!");
            if (pershkrimiNenGrupiLlogaria == "")
                return new clsMesazh(false, "Plotesoni nengrupin!");
            if (shtim && ekzistonLlogari(nrLlogari, idNdermarja))
                return new clsMesazh(false, "Ekziston nje llogari me kete numer. Ju lutem shenoni nje numer tjeter!");
            if (!clsMonedha.ekziston(kodiMonedha, idNdermarja))
                return new clsMesazh(false, "Monedha nuk ekziston!");
            if (kpf1 <= 0)
                return new clsMesazh(false, "Llogaria standarte e struktures se pare nuk ekziston!");
            else
            {
                clsKPF kpf = new clsKPF(kpf1);
                if (clsKPF.eshtePrind(kodiKPF1, idNdermarja, 1, kpf.NiveliKPF))
                {
                    return new clsMesazh(false, "Nuk mund te zgjidhni nje llogari standarte prind per strukturen e pare! Ju lutem zgjidhni nje llogari standarte tjeter");
                }
            }
            if (kodiKPF2 != "")
            {
                if (kpf2 <= 0)
                    return new clsMesazh(false, "Llogaria standarte e struktures se dyte nuk ekziston!");
                else
                {
                    clsKPF kpf = new clsKPF(kpf2);
                    if (clsKPF.eshtePrind(kodiKPF2, idNdermarja, 2, kpf.NiveliKPF))
                    {
                        return new clsMesazh(false, "Nuk mund te zgjidhni nje llogari standarte prind per strukturen e dyte! Ju lutem zgjidhni nje llogari standarte tjeter");
                    }
                }
            }
            if (kodiKPF3 != "")
            {
                if (kpf3 <= 0)
                    return new clsMesazh(false, "Llogaria standarte e struktures se trete nuk ekziston!");
                else
                {
                    clsKPF kpf = new clsKPF(kpf3);
                    if (clsKPF.eshtePrind(kodiKPF3, idNdermarja, 3, kpf.NiveliKPF))
                    {
                        return new clsMesazh(false, "Nuk mund te zgjidhni nje llogari standarte prind per strukturen e trete! Ju lutem zgjidhni nje llogari standarte tjeter");
                    }
                }
            }
            if (grupi == -1)
                return new clsMesazh(false, "Grupi nuk ekziston!");

            if (nengrupi == -1)
                return new clsMesazh(false, "Nengrupi nuk ekziston!");
            else
            {
                clsNenGrupiLlogaria nen = new clsNenGrupiLlogaria(nengrupi, idGjuha);
                if (nen.IdGrupiLlogaria != grupi)
                {
                    return new clsMesazh(false, "Ky nengrup nuk i perket ketij grupi!");
                }
            }

            if (objektiva != "")
            {
                if (idObjektivaKosto == -1)
                    return new clsMesazh(false, "Objektiva e kostos nuk ekziston!");
                else
                {
                    DbQendraKosto.clsObjektivaKosto obj = new DbQendraKosto.clsObjektivaKosto(objektiva, idNdermarja);
                    if (!obj.Aktiv)
                        return new clsMesazh(false, "Objektiva e kostos nuk eshte aktive!");
                }
            }
            if (kategoriShpenzimi != "" && idKategoriShpenzimi == -1)
                return new clsMesazh(false, "Kategoria e shpenzimit nuk ekziston!");
            if (qendra != "")
            {
                if (llojQendre == 1)
                {
                    if (qenderKosto == -1)
                        return new clsMesazh(false, "Qendra e kostos nuk ekziston!");
                    DbQendraKosto.clsQendraKosto qndr = new DbQendraKosto.clsQendraKosto(qendra, idNdermarja);
                    if (!qndr.Aktiv)
                        return new clsMesazh(false, "Qendra e kostos nuk eshte aktive!");
                    DbQendraKosto.colQendraKosto col = new DbQendraKosto.colQendraKosto();
                    col.mbushQendraSipasPrindit(qndr.Id);
                    if (col.Count > 0)
                    {
                        return new clsMesazh(false, "Qendra e kostos eshte qender prind dhe nuk mund te zgjidhet!");
                    }
                }
                else
                {
                    if (!DbQendraKosto.clsKokaSkemaQK.ekzistonSkeme(qendra, idNdermarja))
                    {
                        return new clsMesazh(false, "Skema me kod " + qendra + " nuk ekziston!");
                    }
                }
            }

            return new clsMesazh(true, "Kontrollet e llogarise u kaluan me sukses");
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="nr">nr i llogarise</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public clsLlogari(string nr, int idndermarje)
        {
            if (nr != null && nr != "")
                using (clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet())
                {
                    mbushLlogari(dbLlogari.ktheLlogariSipasKodit(nr, idndermarje));
                }
        }

        public clsLlogari(string nr, int idndermarje, clsDatabaseKontabilitet dbLlogari)
        {
            if (String.IsNullOrEmpty(nr))
                return;
            mbushLlogari(dbLlogari.TransCache.getLlogari(nr, idndermarje, dbLlogari));
            //mbushLlogari(dbLlogari.ktheLlogariSipasKodit(nr, idndermarje));
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idLlog">id e llogarise</param>
        public clsLlogari(int idLlog)
        {
            if (idLlog < 1)
            {
                idLlogari = 0;
                return;
            }
            using (clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet())
            {
                mbushLlogari(dbLlogari.merrLlogari(idLlog));
            }
        }

        public clsLlogari(int idLlog, clsDatabaseKontabilitet dbLlogari)
        {
            if (idLlog == 0 || idLlog == -1)
            {
                idLlogari = 0;
                return;
            }
            mbushLlogari(dbLlogari.TransCache.getLlogari(idLlog, dbLlogari));
            //mbushLlogari(dbLlogari.merrLlogari(idLlog));
        }

        public clsLlogari(int idNenLlojLlogarie, int idArtikulli, clsDatabaseKontabilitet dbLlogari)
        {
            if (idNenLlojLlogarie == 0 || idNenLlojLlogarie == -1 || idArtikulli == 0 || idArtikulli == -1)
                return;
            mbushLlogari(dbLlogari.merrLlogariSipasIdNenLlojLlog(idNenLlojLlogarie, idArtikulli));
        }

        public clsLlogari(int idNenLlojLlogarie, int idArtikulli)
        {
            if (idNenLlojLlogarie == 0 || idNenLlojLlogarie == -1 || idArtikulli == 0 || idArtikulli == -1)
                return;
            using (clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet())
            {
                mbushLlogari(dbLlogari.merrLlogariSipasIdNenLlojLlog(idNenLlojLlogarie, idArtikulli));
            }
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlogari()
        {
        }

        public clsLlogari(DataRow rreshti)
        {
            
            mbushLlogari(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdLlogari
        {
            get
            {
                return idLlogari;
            }
            set
            {
                idLlogari = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos numrin e llogarise
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
        /// Kthen/Vendos emrin e pare te llogarise
        /// </summary>
        public string EmerLlogari1
        {
            get
            {
                return emerLlogari1;
            }
            set
            {
                emerLlogari1 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos emrin e dyte te llogarise
        /// </summary>
        public string EmerLlogari2
        {
            get
            {
                return emerLlogari2;
            }
            set
            {
                emerLlogari2 = value;
            }

        }

        /// <summary>
        /// Kthen/Vendos emertimin ne frengjisht te llogarise
        /// </summary>
        public string EmerLlogariFr
        {
            get { return emerLlogariFr; }
            set { emerLlogariFr = value; }
        }

        /// <summary>
        /// Kthen/Vendos qendren e kostos
        /// </summary>
        public int QendraKostos
        {
            get
            {
                return qenderKosto;
            }

            set
            {
                qenderKosto = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos KPF-ne(Llogari standarte) e pare 
        /// </summary>
        public int KPF1
        {
            get
            {
                return kpf1;
            }

            set
            {
                kpf1 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos KPF-ne(Llogari standarte) e dyte 
        /// </summary>
        public int KPF2
        {
            get
            {
                return kpf2;
            }
            set
            {
                kpf2 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos KPF-ne(Llogari standarte) e trete 
        /// </summary>
        public int KPF3
        {
            get
            {
                return kpf3;
            }
            set
            {
                kpf3 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nivelin e takses
        /// </summary>
        public int NivelTakse
        {
            get
            {
                return nivelTakse;
            }
            set
            {
                nivelTakse = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e takses
        /// </summary>
        public string PershkrimTaksa
        {
            get
            {
                return pershkrimTaksa;
            }
            set
            {
                pershkrimTaksa = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos  ID-ne e monedhes
        /// </summary>
        public int IdMonedha
        {
            get
            {
                return idMonedha;
            }
            set
            {
                idMonedha = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos grupin e llogarise <seealso cref="DbCore.DbKontabiliteti.clsGrupiLlogaria"/>
        /// </summary>
        public int Grupi
        {
            get
            {
                return grupi;
            }
            set
            {
                grupi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nengrupin e llogarise <seealso cref="DbCore.DbKontabiliteti.clsNenGrupiLlogaria"/>
        /// </summary>
        public int Nengrupi
        {
            get
            {
                return nengrupi;
            }
            set
            {
                nengrupi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos llogarine konsoliduese
        /// </summary>
        public int LlogariKonsoliduese
        {
            get
            {
                return llogariKonsoliduese;
            }

            set
            {
                llogariKonsoliduese = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos llogarine korresponduese
        /// </summary>
        public int LlogariKoresponduese
        {
            get
            {
                return llogariKoresponduese;
            }
            set
            {
                llogariKoresponduese = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarja
        {
            get
            {
                return idNdermarja;
            }
            set
            {
                idNdermarja = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne lidhese ndermarrje - vit
        /// </summary>
        //public int IdNderViti {
        //    get
        //    { 
        //        return idNderViti; 
        //    }
        //    set
        //    {
        //        idNderViti = value;
        //    }
        //}

        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit te llogarise <seealso cref="DbCore.DbKontabiliteti.clsGrupiLlogaria"/>
        /// </summary>
        public string PershkrimiGrupiLlogaria
        {
            get
            {
                return pershkrimiGrupiLlogaria;
            }
            set
            {
                pershkrimiGrupiLlogaria = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e nengrupit te llogarise <seealso cref="DbCore.DbKontabiliteti.clsNenGrupiLlogaria"/>
        /// </summary>
        public string PershkrimiNenGrupiLlogaria
        {
            get
            {
                return pershkrimiNenGrupiLlogaria;
            }
            set
            {
                pershkrimiNenGrupiLlogaria = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos Kodin e monedhes
        /// </summary>
        public string KodiMonedha
        {
            get
            {
                return kodiMonedha;
            }
            set
            {
                kodiMonedha = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e monedhes
        /// </summary>
        public string PershkrimiMonedha
        {
            get
            {
                return pershkrimiMonedha;
            }
            set
            {
                pershkrimiMonedha = value;
            }
        }
        public string KodiKPF1
        {
            get
            {
                return kodiKPF1;
            }
            set
            {
                kodiKPF1 = value;
            }
        }

        public string KodiKPF2
        {
            get
            {
                return kodiKPF2;
            }
            set
            {
                kodiKPF2 = value;
            }
        }

        public string KodiKPF3
        {
            get
            {
                return kodiKPF3;
            }
            set
            {
                kodiKPF3 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po kryen veprimin
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
        ///  id e kategorise se shpenzimit
        /// </summary>
        public int IdKategoriShpenzimi
        {
            get
            {
                return idKategoriShpenzimi;
            }
            set
            {
                idKategoriShpenzimi = value;
            }
        }
        /// <summary>
        /// kodi i kategorise se shpenzimit
        /// </summary>
        public string KategoriShpenzimi
        {
            get
            {
                return kategoriShpenzimi;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit
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
        /// <summary>
        /// id e objektives se kostos
        /// </summary>
        public int IdObjektivaKosto
        {
            get
            {
                return idObjektivaKosto;
            }
            set
            {
                idObjektivaKosto = value;
            }
        }
        /// <summary>
        /// id e skemes se qendres se kostos
        /// </summary>
        public int IdSkemaQendraKosto
        {
            get
            {
                return idSkemaQendraKosto;
            }
            set
            {
                idSkemaQendraKosto = value;
            }
        }
        /// <summary>
        /// lloj qendre 1-qendre 2 skeme
        /// </summary>
        public int LlojQendre
        {
            get
            {
                return llojQendre;
            }
            set
            {
                llojQendre = value;
            }
        }
        /// <summary>
        /// objektiva
        /// </summary>
        public string Objektiva
        {
            get
            {
                return objektiva;
            }
        }
        /// <summary>
        /// qendra ose skema
        /// </summary>
        public string Qendra
        {
            get
            {
                return qendra;
            }
        }


        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbAdmin.clsVleraFushaShtese"/>
        /// </summary>
        public colVleraFushaShtese OColVleratFushatShtese
        {
            get { return oColVleratFushatShtese; }
            set { oColVleratFushatShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbAdmin.clsLidhjeAutorizim"/>
        /// </summary>
        public colLidhjetAutorizim OColLidhjeAutorizim
        {
            get { return oColLidhjeAutorizim; }
            set { oColLidhjeAutorizim = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese llogaria eshte aktive apo jo
        /// </summary>
        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos Shenime 1
        /// </summary>
        public string Shenime1
        {
            get
            {
                return shenime1;
            }
            set
            {
                shenime1 = value;
            }

        }

        /// <summary>
        /// Kthen/Vendos Shenime 2
        /// </summary>
        public string Shenime2
        {
            get
            {
                return shenime2;
            }
            set
            {
                shenime2 = value;
            }

        }

        /// <summary>
        /// Kthen/Vendos Shenime 3
        /// </summary>
        public string Shenime3
        {
            get
            {
                return shenime3;
            }
            set
            {
                shenime3 = value;
            }

        }

        /// <summary>
        /// Kthen/Vendos Shenime 4
        /// </summary>
        public string Shenime4
        {
            get
            {
                return shenime4;
            }
            set
            {
                shenime4 = value;
            }

        }

        /// <summary>
        /// Kthen/Vendos Shenime 5
        /// </summary>
        public string Shenime5
        {
            get
            {
                return shenime5;
            }
            set
            {
                shenime5 = value;
            }

        }




        #endregion

        #region Metoda Publike

        public clsLlogari krijoLlogariPerImport(string nrLlog, string emerLlog1, string emerLlog2, string emerLlogFr, string qenderkosto, string kpf1, string kpf2, string kpf3, string niveltakse, string mon, string grup, string nengrup, int idndermarja, int idperdoruesi, int idstatusdok, string objektivakosto, string llojqendre, string kategorishpenzimi, bool aktiv, int idGjuha, string shenime1, string shenime2, string shenime3, string shenime4, string shenime5 , ResourceManager rm, CultureInfo ci)
        {
            try
            {
                int idMonedha = 0;
                if (mon != "")
                {
                    clsMonedha monedha = new clsMonedha();
                    monedha.mbushMonedhen(mon, idndermarja);
                    idMonedha = monedha.IdMonedha;
                }

                int idGrupi = 0;
                if (grup != "")
                {
                    clsGrupiLlogaria grupi = new clsGrupiLlogaria(grup, idndermarja, idGjuha);
                    idGrupi = grupi.IdGrupiLlogaria;
                }

                int idNenGrupi = 0;
                if (nengrup != "")
                    idNenGrupi = clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(nengrup, idndermarja, idGjuha);

                int idkpf1 = 0;
                if (kpf1 != "")
                {
                    clsKPF struktura1 = new clsKPF(1, idndermarja, idperdoruesi, kpf1);
                    idkpf1 = struktura1.IdKPF;
                }

                int idkpf2 = 0;
                if (kpf2 != "")
                {
                    clsKPF struktura2 = new clsKPF(2, idndermarja, idperdoruesi, kpf2);
                    idkpf2 = struktura2.IdKPF;
                }

                int idkpf3 = 0;
                if (kpf3 != "")
                {
                    clsKPF struktura3 = new clsKPF(3, idndermarja, idperdoruesi, kpf3);
                    idkpf3 = struktura3.IdKPF;
                }

                int idKonfig = DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LL", idndermarja);

                int idkategorishpenz = 0;
                if (kategorishpenzimi != "")
                {
                    clsKategoriShpenzimi kat = new clsKategoriShpenzimi(kategorishpenzimi, idndermarja);
                    idkategorishpenz = kat.Id;
                }

                int idTaksa = 0;
                if (niveltakse != "")
                {
                    DbRegjistrim.clsTaksa takse = new DbRegjistrim.clsTaksa(niveltakse, idndermarja);
                    idTaksa = takse.IdTaksa;
                }

                int idQenderKosto = 0;

                if (!String.IsNullOrEmpty(qenderkosto) && llojqendre != "Qendra Kosto" && llojqendre != "Skema me Qendra Kosto")
                    throw new MyException("Percaktoni llojin e qendres");

                if (qenderkosto != "")
                    idQenderKosto = DbQendraKosto.clsQendraKosto.ktheIdQK(qenderkosto, idndermarja);

                int idObjektivaKosto = 0;
                if (objektivakosto != "")
                {
                    DbQendraKosto.clsObjektivaKosto objektiv = new DbQendraKosto.clsObjektivaKosto(objektivakosto, idndermarja);
                    idObjektivaKosto = objektiv.Id;
                }

                int idLlojQendre = 0;
                if (llojqendre == "Qendra Kosto")
                    idLlojQendre = 1;
                else if (llojqendre == "Skema me Qendra Kosto")
                    idLlojQendre = 2;

                return new clsLlogari(nrLlog, emerLlog1, emerLlog2, emerLlogFr, idQenderKosto, qenderkosto, idkpf1, idkpf2, idkpf3, idTaksa, idMonedha, idGrupi, idNenGrupi, 0, 0, idndermarja, grup, nengrup, mon, kpf1, kpf2, kpf3, idperdoruesi, idKonfig, niveltakse, idstatusdok, idObjektivaKosto, objektivakosto, 0, idLlojQendre, idkategorishpenz, kategorishpenzimi, aktiv, true, idGjuha, rm, ci, new colLidhjetAutorizim() , shenime1,shenime2,shenime3,shenime4,shenime5);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ComboListTvsh[] ktheTvsh(DbRegjistrim.KonfigurimTVSHGjateRregj llojtvsh, int idPerdoruesi, DbRegjistrim.clsTaksa takseDefaulNdermarrje, DbRegjistrim.colTaksa taksaNdermarrje,DbRegjistrim.clsTaksa taksaKF)
        {
            ComboListTvsh tvshSygjeruar = new ComboListTvsh();
            if (llojtvsh == DbRegjistrim.KonfigurimTVSHGjateRregj.Sipas_Klientit)
            {
                
                    if (taksaKF.Aktiv == true)
                    {
                        tvshSygjeruar = new ComboListTvsh();
                        tvshSygjeruar.value = taksaKF.IdTaksa;
                        tvshSygjeruar.text = taksaKF.KodTaksa;
                        tvshSygjeruar.norma = taksaKF.NormaPerqindje;
                        tvshSygjeruar.caktuar = "KF";
                    }
                
            }
            if ((tvshSygjeruar.value == 0&&llojtvsh == DbRegjistrim.KonfigurimTVSHGjateRregj.Sipas_Klientit) || llojtvsh == DbRegjistrim.KonfigurimTVSHGjateRregj.Sipas_Artikullit)
            {
                if (IdLlogari > 0 && NivelTakse > 0)
                {
                    DbRegjistrim.clsTaksa taksa = new DbRegjistrim.clsTaksa(NivelTakse);
                    if (taksa.Aktiv == true)
                    {
                        tvshSygjeruar = new ComboListTvsh();
                        tvshSygjeruar.value = taksa.IdTaksa;
                        tvshSygjeruar.text = taksa.KodTaksa;
                        tvshSygjeruar.norma = taksa.NormaPerqindje;
                        tvshSygjeruar.caktuar = "llog";
                    }
                }
            }

            if (tvshSygjeruar.value == 0 && llojtvsh != DbRegjistrim.KonfigurimTVSHGjateRregj.Pa_TVSH)
            {
                tvshSygjeruar = new ComboListTvsh();
                tvshSygjeruar.value = takseDefaulNdermarrje.IdTaksa;
                tvshSygjeruar.text = takseDefaulNdermarrje.KodTaksa;
                tvshSygjeruar.norma = takseDefaulNdermarrje.NormaPerqindje;
                tvshSygjeruar.caktuar = "nderm";
            }
            int sygjIndex = 0;

            ComboListTvsh[] listeTvsh = new ComboListTvsh[taksaNdermarrje.Count + 1];
            if (tvshSygjeruar.value != 0)
            {
                sygjIndex = 1;
                listeTvsh[0] = tvshSygjeruar;
            }
            ComboListTvsh tvshPaTakse = new ComboListTvsh();
            DbRegjistrim.clsTaksa taksePaTVSH = DbRegjistrim.clsTaksa.krijoTaksePaTVSH();
            tvshPaTakse.value = taksePaTVSH.IdTaksa;
            tvshPaTakse.text = taksePaTVSH.KodTaksa;
            tvshPaTakse.norma = taksePaTVSH.NormaPerqindje;
            tvshPaTakse.caktuar = "asnje";
            listeTvsh[sygjIndex] = tvshPaTakse;
            sygjIndex++;
            for (int i = 0; i < taksaNdermarrje.Count; i++)
            {
                if (tvshSygjeruar.value != 0 && taksaNdermarrje[i].IdTaksa == tvshSygjeruar.value)
                {
                    sygjIndex--;
                    continue;
                }
                ComboListTvsh tmpTvsh = new ComboListTvsh();
                tmpTvsh.value = taksaNdermarrje[i].IdTaksa;
                tmpTvsh.text = taksaNdermarrje[i].KodTaksa;
                tmpTvsh.norma = taksaNdermarrje[i].NormaPerqindje;
                tmpTvsh.caktuar = "asnje";
                listeTvsh[i + sygjIndex] = tmpTvsh;
            }
            return listeTvsh;
        }

        public clsMesazh ruaj(bool vjenNgaImportSQL, string idTemp, string emerTabele, string primaryKey, string ndermarrjeKey)
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            try
            {
                using (var myScope = new MyTransactionScope())
                {
                    clsMesazh ruajtur = ruaj(db);
                    if (!ruajtur) return ruajtur;

                    if (vjenNgaImportSQL)
                    {
                        //ruajtur = db.updateDokTabeleTemportal(idLlogImp, idNdermarja, 1, emerTabele, primaryKey, ndermarrjeKey);
                        DbImporte.clsDatabazeImporte dbImport = new DbImporte.clsDatabazeImporte(db);
                        ruajtur = DbImporte.colImportSQL.updateDokTabeleTemportal(idTemp, idNdermarja, 1, emerTabele, primaryKey, ndermarrjeKey, dbImport);
                        if (!ruajtur) return ruajtur;

                    }
                    myScope.Complete();
                    return ruajtur;
                }

            }
            catch (Exception)
            {
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        public clsMesazh ruaj(clsDatabaseKontabilitet db)
        {
            clsMesazh ruajtur = ruajLlogari(idLlogari, nrLlogari, emerLlogari1, emerLlogari2, emerLlogariFr, qenderKosto, kpf1, kpf2, kpf3, nivelTakse, idMonedha, grupi, nengrupi, llogariKonsoliduese, llogariKoresponduese, idNdermarja, idPerdoruesi, idKonfig, oColLidhjeAutorizim, oColVleratFushatShtese, idStatusDok, idObjektivaKosto, idSkemaQendraKosto, llojQendre, idKategoriShpenzimi, aktiv,shenime1 ,shenime2 ,shenime3 ,shenime4 ,shenime5 , db);
            return ruajtur;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            db.beginTransaksion();
            clsMesazh ruajtur = modifiko(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }

        public clsMesazh modifiko(clsDatabaseKontabilitet db)
        {
            clsMesazh ruajtur = modifikoLlogari(idLlogari, nrLlogari, emerLlogari1, emerLlogari2, emerLlogariFr, qenderKosto, kpf1, kpf2, kpf3, nivelTakse, idMonedha, grupi, nengrupi, llogariKonsoliduese, llogariKoresponduese, idNdermarja, idPerdoruesi, idKonfig, oColLidhjeAutorizim, oColVleratFushatShtese, idStatusDok, idObjektivaKosto, idSkemaQendraKosto, llojQendre, idKategoriShpenzimi, aktiv,shenime1 , shenime2 , shenime3 , shenime4 , shenime5 , db);
            return ruajtur;
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsLlogari dhe collection-in e buxheteve te lidhur me te.      
        /// </summary>
        /// <param name="idLlog">id e llogarise</param>
        /// <param name="nrLlog">nr i llogarise</param>
        /// <param name="emerLlog1">emri i pare i llogarise</param>
        /// <param name="emerLlog2">ermri i dyte i llogarise </param>
        /// <param name="qenderkosto">qendra e kostos</param>
        /// <param name="kpf1">kpf1</param>
        /// <param name="kpf2">kpf2</param>
        /// <param name="kpf3">kpf3</param>
        /// <param name="niveltakse">niveli i takses</param>
        /// <param name="mon">id e monedhes</param>
        /// <param name="grup">grupi qe ben pjese llogaria</param>
        /// <param name="nengrup">nengrupi qe ben pjese llogaria</param>
        /// <param name="llogkonsoliduese">llogaria konsoliduese</param>
        /// <param name="llogkoresponduese">llogaria korresponduese</param>
        /// <param name="idndermarja">id e ndermarrjes</param>        
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="aktiv"></param>
        /// <param name="dbKont"></param>
        /// <param name="idkategorishpenzimi"></param>
        /// <param name="idobjektivakosto"></param>
        /// <param name="idskemakosto"></param>
        /// <param name="idstatusdok"></param>
        /// <param name="llojqendre"></param>
        /// <param name="oColLidhjeAutorizim"></param>
        /// <param name="oColVleratFushatShtese"></param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        public clsMesazh ruajLlogari(int idLlog, string nrLlog, string emerLlog1, string emerLlog2, string emerLlogFr, int qenderkosto, int kpf1, int kpf2, int kpf3, int niveltakse, int mon, int grup, int nengrup, int llogkonsoliduese, int llogkoresponduese, int idndermarja, int idperdoruesi, int idkonfig, colLidhjetAutorizim oColLidhjeAutorizim, colVleraFushaShtese oColVleratFushatShtese, int idstatusdok, int idobjektivakosto, int idskemakosto, int llojqendre, int idkategorishpenzimi, bool aktiv,string shenime1 ,string shenime2 , string shenime3 , string shenime4 , string shenime5, clsDatabaseKontabilitet dbKont)
        {
            //ruan llogarine dhe buxhetet perkatese

            clsMesazh mesazh = new clsMesazh();
            try
            {
                idLlogari = dbKont.ruajLlog(idLlog, nrLlog, emerLlog1, emerLlog2, emerLlogFr, qenderkosto, kpf1, kpf2, kpf3, niveltakse, mon, grup, nengrup, llogkonsoliduese, llogkoresponduese, idndermarja, idperdoruesi, idkonfig, idstatusdok, idobjektivakosto, idskemakosto, llojqendre, idkategorishpenzimi, aktiv , shenime1 , shenime2 , shenime3 , shenime4, shenime5 );

                if (idLlogari != 0)
                {
                    mesazh = oColLidhjeAutorizim.ruajLidhjeAutorizim(idLlogari, dbKont);
                }

            }
            catch (Exception ce)
            {
                mesazh.PershkrimMesazhi = ce.Message;
            }
            return mesazh;
        }

        /// <summary>
        /// Modifikon llogarine dhe buxhetet ne tabelat perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <param name="idLlog">id e llogarise</param>
        /// <param name="nrLlog">nr i llogarise</param>
        /// <param name="emerLlog1">emri i pare i llogarise</param>
        /// <param name="emerLlog2">ermri i dyte i llogarise </param>
        /// <param name="qenderkosto">qendra e kostos</param>
        /// <param name="kpf1">kpf1</param>
        /// <param name="kpf2">kpf2</param>
        /// <param name="kpf3">kpf3</param>
        /// <param name="niveltakse">niveli i takses</param>
        /// <param name="mon">id e monedhes</param>
        /// <param name="grup">grupi qe ben pjese llogaria</param>
        /// <param name="nengrup">nengrupi qe ben pjese llogaria</param>
        /// <param name="llogkonsoliduese">llogaria konsoliduese</param>
        /// <param name="llogkoresponduese">llogaria korresponduese</param>
        /// <param name="idndermarja">id e ndermarrjes</param>
        /// <param name="idndermvit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="aktiv"></param>
        /// <param name="dbKont"></param>
        /// <param name="idkategorishpenzimi"></param>
        /// <param name="idobjektivakosto"></param>
        /// <param name="idskemakosto"></param>
        /// <param name="idstatusdok"></param>
        /// <param name="llojqendre"></param>
        /// <param name="oColLidhjeAutorizim"></param>
        /// <param name="oColVleratFushatShtese"></param>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifikoLlogari(int idLlog, string nrLlog, string emerLlog1, string emerLlog2, string emerLlogFr, int qenderkosto, int kpf1, int kpf2, int kpf3, int niveltakse, int mon, int grup, int nengrup, int llogkonsoliduese, int llogkoresponduese, int idndermarja, int idperdoruesi, int idkonfig, colLidhjetAutorizim oColLidhjeAutorizim, colVleraFushaShtese oColVleratFushatShtese, int idstatusdok, int idobjektivakosto, int idskemakosto, int llojqendre, int idkategorishpenzimi, bool aktiv, string shenime1, string shenime2, string shenime3, string shenime4, string shenime5, clsDatabaseKontabilitet dbKont)
        {
            try
            {
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbKont);
                colLidhjetAutorizim colLidhjetAutorizim = new colLidhjetAutorizim(idLlog, "Llogari", dbAdmin);
                //modifikon llogarine dhe buxhetet perkatese
                clsMesazh mesazh = new clsMesazh();

                mesazh = dbKont.modifikoLlogari(idLlog, nrLlog, emerLlog1, emerLlog2, emerLlogFr, qenderkosto, kpf1, kpf2, kpf3, niveltakse, mon, grup, nengrup, llogkonsoliduese, llogkoresponduese, idndermarja, idperdoruesi, idkonfig, idstatusdok, idobjektivakosto, idskemakosto, llojqendre, idkategorishpenzimi, aktiv,shenime1 , shenime2 , shenime3 , shenime4 , shenime5);

                if (!mesazh.Status)
                    return mesazh;

                mesazh = clsFunksione.modifikoLidhjeAutorizimSipasLlojitTeBuxhetit(oColLidhjeAutorizim, "Llogari", idLlog, colLidhjetAutorizim, dbKont, dbAdmin, false,idperdoruesi);

                if (mesazh.Status)
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["msgModifikimiMeSukses"];

                return mesazh;



            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsLlogari dhe collection-in e buxheteve te lidhur me te.
        /// <param name="idLlog">id e llogarise</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiLlogari(int idLlog)
        {//fshin llogarine dhe buxhetet perkatese
            clsMesazh mesazh = new clsMesazh(true);
            using (clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet())
            {
                try
                {
                    dbKont.beginTransaksion();
                    clsLlogari llogari = new clsLlogari(idLlog);
                    colLidhjetAutorizim colLidhjeAutorizim = new colLidhjetAutorizim(idLlog, "Llogari");

                    mesazh = colLidhjeAutorizim.fshiLidhjeAutorizim(dbKont);

                    if (!mesazh.Status)
                        throw new Exception(mesazh.PershkrimMesazhi);

                    mesazh = dbKont.fshiLlogari(llogari.IdLlogari);

                    if (!mesazh.Status)
                        throw new Exception(mesazh.PershkrimMesazhi);

                    dbKont.commitTransaksion();

                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                    return mesazh;



                }
                catch (Exception ce)
                {
                    dbKont.rollbackTransaksion();
                    return new clsMesazh(false, ce.Message);
                }

            }
        }

        //[Obsolete("Perdor nga klasa perkatese: clsMesazh fshiLlogariAndBuxhete(int idLlog)", true)]
        public clsMesazh fshiLlogari()
        {//metoda qe therret klasen clsDatabaseKontabilitet per fshirjen e nje llogarie
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_fshi = data.fshiLlogariStatus(idLlogari, idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr nje objekt llogarie duke filtruar sipas kodit. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheLlogariSipasKodit"/>
        /// </summary>
        public clsLlogari merrLlogari()
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e nje llogarie
            clsLlogari llog = new clsLlogari(NrLlogari, IdNdermarja);
            return llog;
        }

        /// <summary>
        /// Merr nje objekt llogarie duke filtruar sipas kodit. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheLlogariSipasKodit"/>
        /// </summary>
        public clsLlogari merrLlogariSipasKodit()
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e nje llogarie
            clsLlogari llog = new clsLlogari(NrLlogari, IdNdermarja);
            return llog;
        }

        /// <summary>
        /// Merr nje objekt llogarie duke filtruar sipas kodit. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheLlogariSipasKodit"/>
        /// </summary>
        public bool merrLlogariAktiveSipasKodit(string nrLlog, int idNdermarrje)
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e nje llogarie
            clsDatabaseKontabilitet dbKontab = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogari(dbKontab.ktheLlogariAktiveSipasKodit(nrLlog, idNdermarrje));
            dbKontab.Dispose();
            return sukses;
        }

        /// <summary>
        /// Merr nje objekt llogarie duke filtruar sipas ID-se. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrLlogari"/>
        /// </summary>
        public clsLlogari merrLlogariSipasId()
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e nje llogarie
            clsLlogari llog = new clsLlogari(IdLlogari);
            return llog;
        }

        /// <summary>
        /// Merr nje collection me objekte llogarie duke filtruar sipas ndermarrjes. Therret funksionin
        /// <see cref="DBKontabiliteti.colLlogarite.mbushLLogariteNdermarrjes"/>
        /// </summary>
        public colLlogarite merrGjitheLlogarite(int idndermarje)
        {//metoda qe therret klasen clsDatabaseKontabilitet per marrjen e te gjithave llogarive
            colLlogarite llog = new colLlogarite();
            llog.mbushLLogariteNdermarrjes(idndermarje);
            return llog;
        }


        /// <summary>
        /// Tregon nese nje llogari te caktuar eshte llogari klient apo jo.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.eshteLlogariKlienti"/>
        /// </summary>
        public clsMesazh eshteLlogariKlienti()
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            clsMesazh mesazhi = db.eshteLlogariKlienti(IdLlogari.ToString());
            db.Dispose();
            return mesazhi;
        }

        /// <summary>
        /// mbush llogarite sipas kodit
        /// </summary>
        /// <param name="nrLlogari">nr i llogarise</param>
        /// <param name="idNdermarja">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushLlogariSipasKodit(string nrLlogari, int idNdermarja)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogari(dbLlogari.ktheLlogariSipasKodit(nrLlogari, idNdermarja));
            dbLlogari.Dispose();
            return sukses;
        }

        public static double merrGjendjeLlogari(int idllogari, DateTime date, clsDatabaseKontabilitet db)
        {
            return db.merrGjendjeLlogari(idllogari, date);
        }

        public static double merrGjendjeLlogari(int idllogari, DateTime date)
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            double gjendje = merrGjendjeLlogari(idllogari, date, db);
            db.Dispose();
            return gjendje;
        }

        public static double merrGjendjeLlogariMonHuaj(int idllogari, DateTime date, clsDatabaseKontabilitet db)
        {
            return db.merrGjendjeLlogariMonHuaj(idllogari, date);
        }

        public static double merrGjendjeLlogariMonHuaj(int idllogari, DateTime date)
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            double gjendje = db.merrGjendjeLlogariMonHuaj(idllogari, date);
            db.Dispose();
            return gjendje;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e llogarise sipas nr te llogarise dhe idndermarrjes
        /// </summary>
        /// <param name="nrLlogari">nr i llogarise</param>
        /// <param name="idNdermarja">id e ndermarrjes</param>
        /// <returns>id e llogarise</returns>
        public static int mbushIDLlogariSipasKodit(string nrLlogari, int idNdermarja)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            int id = (dbLlogari.ktheIDLlogariSipasKodit(nrLlogari, idNdermarja));
            dbLlogari.Dispose();
            return id;
        }

        public static int ktheNivelTakse(int idLlogari)
        {
            using (clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet())
            {
                return dbLlogari.ktheNivelTakseSipasIdLlogari(idLlogari);
            }
        }

        public static int ktheNivelTakseSipasNrLlogari(string kodi, int idNdermarrje)
        {
            using (clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet())
            {
                return dbLlogari.ktheNivelTakseSipasNrLlogari(kodi, idNdermarrje);
            }
        }


        public void merrLlogFitimHumbjeKlientit(int idNdermarje, int idMonedha, int idLlogKlienti, string kodNenLlojLlogarie)
        {
            clsDatabaseKontabilitet dbKontab = new clsDatabaseKontabilitet();
            dbKontab.beginTransaksion();
            try
            {
                merrLlogFitimHumbjeKlientit(idNdermarje, idMonedha, idLlogKlienti, kodNenLlojLlogarie, dbKontab);
                dbKontab.commitTransaksion();
            }
            catch (Exception)
            {
                dbKontab.rollbackTransaksion();
                throw new MyException("Gabim gjate leximit te llogarive fitim humbje te klientit!");
            }
        }

        public void merrLlogFitimHumbjeKlientit(int idNdermarje, int idMonedha, int idLlogKlienti, string kodNenLlojLlogarie, clsDatabaseKontabilitet dbKontab)
        {
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin(dbKontab);
            DataTable dt = dbKontab.merrLlogFitimHumbjeKlientit(idNdermarje, idMonedha, idLlogKlienti);
            clsMonedha monedha = new clsMonedha(idMonedha, dbadmin);
            if (kodNenLlojLlogarie == "LLMH")
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rreshti in dt.Rows)
                    {
                        if (rreshti["idLlogariHumbje"].ToString() != "0")
                        {
                            mbushLlogari(dbKontab.merrLlogari(int.Parse(rreshti["idLlogariHumbje"].ToString())));
                        }
                        //oLlogari = dbKontab.ktheLlogari(int.Parse(rreshti[2].ToString()));
                        else //nese nuk ka llogari humbje te skema e azhornimit merret llog e humbjes e monedhes   
                            mbushLlogari(dbKontab.merrLlogari(monedha.IdLlogHumbje));
                        //oLlogari = new DbCore.DbKontabiliteti.clsLlogari(dbAdmin.ktheMonedhe(idmonedha)[0].IdLlogHumbje);
                        //oLlogari = dbKontab.ktheLlogari(dbAdmin.ktheMonedhe(idmonedha)[0].IdLlogHumbje);
                    }
                }
                else
                    mbushLlogari(dbKontab.merrLlogari(monedha.IdLlogHumbje));
                //oLlogari = new DbCore.DbKontabiliteti.clsLlogari(dbAdmin.ktheMonedhe(idmonedha)[0].IdLlogHumbje);
                //oLlogari = dbKontab.ktheLlogari(dbAdmin.ktheMonedhe(idmonedha)[0].IdLlogHumbje);
            }
            else if (kodNenLlojLlogarie == "LLMF")
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rreshti in dt.Rows)
                    {
                        if (rreshti["idLlogariFitim"].ToString() != "0")
                            mbushLlogari(dbKontab.merrLlogari(int.Parse(rreshti["idLlogariFitim"].ToString())));
                        //oLlogari = dbKontab.ktheLlogari(int.Parse(rreshti[1].ToString()));
                        else //nese nuk ka llogari fitimi te skema e azhornimit merret llog e fitimit e monedhes     
                            mbushLlogari(dbKontab.merrLlogari(monedha.IdLlogFitimi));
                        //oLlogari = new DbCore.DbKontabiliteti.clsLlogari(dbAdmin.ktheMonedhe(idmonedha)[0].IdLlogFitimi);
                        //oLlogari = dbKontab.ktheLlogari(dbAdmin.ktheMonedhe(idmonedha)[0].IdLlogFitimi);
                    }
                }
                else
                    mbushLlogari(dbKontab.merrLlogari(monedha.IdLlogFitimi));
            }
            dt.Dispose();
        }

        public static bool ekzistonLlogari(string nrLlog, int idNdermarje)
        {
            if (nrLlog == null || nrLlog == string.Empty)
                return false;
            using (clsDatabaseKontabilitet dbKontab = new clsDatabaseKontabilitet())
            {
                return dbKontab.ekzistonLlogari(nrLlog, idNdermarje);
            }
        }

        public static bool eshteLlogariAktive(string nrLlog, int idNdermarje)
        {
            using (clsDatabaseKontabilitet dbKontab = new clsDatabaseKontabilitet())
            {
                return dbKontab.eshteLlogariAktive(nrLlog, idNdermarje);
            }
        }

        public static bool ekzistonLlogari(string nrLlog, int idNdermarje, clsDatabaseKontabilitet dbKontab)
        {
            return dbKontab.TransCache.ekzistonLlogaria(nrLlog, idNdermarje, dbKontab);
        }

        public static bool ekzistonLlogariSipasID(int idLlog)
        {
            using (clsDatabaseKontabilitet dbKontab = new clsDatabaseKontabilitet())
            {
                return dbKontab.ekzistonLlogariSipasID(idLlog);
            }
        }

        public ListeVleraInfo merrInfoLlogSipasIdKokaDheVisibleVlera(DateTime data, int idInfo, int idNderVit, DateTime dt)
        {
            clsInfoKoka info = new clsInfoKoka(idInfo);
            ListeVleraInfo lista = new ListeVleraInfo();

            clsKategoriShpenzimi kategori = new clsKategoriShpenzimi(idKategoriShpenzimi);
            colInfoTrupi col = colInfoTrupi.merrInfoSipasIdKokaDheVisible(info.IdInfoKoka, true, idNdermarja);

            colBuxhetet colBuxh = colBuxhetet.KrijoBuxhetet(kategori.Id, "KategoriShpenzimi", idNderVit, dt, false, true);
            lista.colInfoTrupi = col;
            int shifraPasPresjes = info.IdFormatNumri;
            System.Collections.ArrayList vlerat = new System.Collections.ArrayList();
            if (IdLlogari > 0)
                foreach (clsInfoTrupi trup in col)
                {
                    switch (trup.EmerKolone)
                    {

                        case "Kategoria":
                            if (kategori.Id > 0) vlerat.Add(kategori.Kodi); else vlerat.Add("");
                            break;
                        case "Emertimi":
                            if (kategori.Id > 0) vlerat.Add(kategori.Pershkrimi); else vlerat.Add("");
                            break;
                        case "EmerLlogari":
                            vlerat.Add(EmerLlogari1);
                            break;
                        case "NrLlogari":
                            vlerat.Add(NrLlogari);
                            break;
                        case "BuxhetiMujor1Mbetur":
                            if (colBuxh.Count > 1)
                                vlerat.Add(colBuxh[data.Month].Diferenca_1.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(0.ToString("F" + shifraPasPresjes));
                            break;
                        case "BuxhetiVjetor1Mbetur":
                            if (colBuxh.Count > 1)
                                vlerat.Add(colBuxh[0].Diferenca_1.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(0.ToString("F" + shifraPasPresjes));
                            break;
                        case "BuxhetiMujor2Mbetur":
                            if (colBuxh.Count > 1)
                                vlerat.Add(colBuxh[data.Month].Diferenca_2.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(0.ToString("F" + shifraPasPresjes));
                            break;
                        case "BuxhetiVjetor2Mbetur":
                            if (colBuxh.Count > 1)
                                vlerat.Add(colBuxh[0].Diferenca_2.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(0.ToString("F" + shifraPasPresjes));
                            break;

                        default:
                            vlerat.Add("");
                            break;
                    }
                }
            lista.vlerat = vlerat;
            return lista;
        }

        public clsMesazh kontrollotransferim(clsLlogari kod, int idndermarje, clsDatabaseKontabilitet db, int idperdoruesi, int idGjuha)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonLlogari(kod.nrLlogari, idndermarje))
            {
                clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);
                clsMonedha mon = new clsMonedha(kod.idMonedha, dbadm);
                mesazh = mon.kontrollotransferim(mon, idndermarje, dbadm, idperdoruesi);
                if (!mesazh.Status)
                    return mesazh;
                kod.idMonedha = mon.IdMonedha;
                clsKPF kpf1 = new clsKPF(kod.kpf1, db);
                mesazh = kpf1.kontrollotransferim(kpf1, idndermarje, db, idperdoruesi);
                if (!mesazh.Status)
                    return mesazh;
                kod.kpf1 = kpf1.IdKPF;// clsKPF.mbushIDKPF(kpf1.KodiKPF, idndermarje, kpf1.GrupiKPF, db);
                if (kod.kpf2 > 0)
                {
                    clsKPF kpf2 = new clsKPF(kod.kpf2, db);
                    mesazh = kpf2.kontrollotransferim(kpf2, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                    kod.kpf2 = kpf2.IdKPF;// clsKPF.mbushIDKPF(kpf2.KodiKPF, idndermarje, kpf2.GrupiKPF, db);
                }
                if (kod.kpf3 > 0)
                {
                    clsKPF kpf3 = new clsKPF(kod.kpf3, db);
                    mesazh = kpf3.kontrollotransferim(kpf3, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                    kod.kpf3 = kpf3.IdKPF;// clsKPF.mbushIDKPF(kpf3.KodiKPF, idndermarje, kpf3.GrupiKPF, db);
                }
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db);
                konf.mbushKonfigAmbjSipasKod("LL", idndermarje, dbshare);
                clsGrupiLlogaria grupi = new clsGrupiLlogaria(kod.grupi, idGjuha);
                grupi = new clsGrupiLlogaria(grupi.PershkrimiGrupiLlogaria, idndermarje, idGjuha);
                clsNenGrupiLlogaria nengrupi = new clsNenGrupiLlogaria(kod.nengrupi, idGjuha);
                kod.nengrupi = clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(nengrupi.PershkrimiNenGrupiLlogaria, idndermarje, idGjuha);
                kod.grupi = grupi.IdGrupiLlogaria;
                kod.idKategoriShpenzimi = 0;
                kod.idObjektivaKosto = 0;
                kod.idSkemaQendraKosto = 0;
                kod.qenderKosto = 0;
                kod.nivelTakse = 0;//se mund te arrije ne cikel
                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarja = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {
                clsLlogari kodnderm = new clsLlogari(kod.nrLlogari, idndermarje, db);
                kod.IdLlogari = kodnderm.IdLlogari;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);
                    clsMonedha mon = new clsMonedha(kod.idMonedha, dbadm);
                    mesazh = mon.kontrollotransferim(mon, idndermarje, dbadm, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                    kod.idMonedha = mon.IdMonedha;
                    clsKPF kpf1 = new clsKPF(kod.kpf1, db);
                    mesazh = kpf1.kontrollotransferim(kpf1, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                    kod.kpf1 = kpf1.IdKPF;// clsKPF.mbushIDKPF(kpf1.KodiKPF, idndermarje, kpf1.GrupiKPF, db);
                    if (kod.kpf2 > 0)
                    {
                        clsKPF kpf2 = new clsKPF(kod.kpf2, db);
                        mesazh = kpf2.kontrollotransferim(kpf2, idndermarje, db, idperdoruesi);
                        if (!mesazh.Status)
                            return mesazh;
                        kod.kpf2 = kpf2.IdKPF;// clsKPF.mbushIDKPF(kpf2.KodiKPF, idndermarje, kpf2.GrupiKPF, db);
                    }
                    if (kod.kpf3 > 0)
                    {
                        clsKPF kpf3 = new clsKPF(kod.kpf3, db);
                        mesazh = kpf3.kontrollotransferim(kpf3, idndermarje, db, idperdoruesi);
                        if (!mesazh.Status)
                            return mesazh;
                        kod.kpf3 = kpf3.IdKPF;// clsKPF.mbushIDKPF(kpf3.KodiKPF, idndermarje, kpf3.GrupiKPF, db);
                    }
                    clsGrupiLlogaria grupi = new clsGrupiLlogaria(kod.grupi, idGjuha);
                    grupi = new clsGrupiLlogaria(grupi.PershkrimiGrupiLlogaria, idndermarje, idGjuha);
                    clsNenGrupiLlogaria nengrupi = new clsNenGrupiLlogaria(kod.nengrupi, idGjuha);
                    kod.nengrupi = clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(nengrupi.PershkrimiNenGrupiLlogaria, idndermarje, idGjuha);
                    kod.grupi = grupi.IdGrupiLlogaria;
                    kod.idKategoriShpenzimi = 0;
                    kod.idObjektivaKosto = 0;
                    kod.idSkemaQendraKosto = 0;
                    kod.qenderKosto = 0;
                    kod.nivelTakse = 0;//se mund te arrije ne cikel
                    kod.idPerdoruesi = idperdoruesi;
                    kod.idNdermarja = idndermarje;

                    kod.idKonfig = kodnderm.idKonfig;
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return mesazh;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llogari nga databaza
        /// </summary>
        /// <param name="dbDataRowLlogari">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLlogari(DataRow dbDataRowLlogari)
        {
            if (dbDataRowLlogari != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlogari["IDLLOGARI"].ToString(), out idLlogari);
                    nrLlogari = dbDataRowLlogari["NRLLOGARI"].ToString();
                    emerLlogari1 = dbDataRowLlogari["EMERLLOGARI_sq"].ToString();
                    emerLlogari2 = dbDataRowLlogari["EMERLLOGARI_en"].ToString();
                    int.TryParse(dbDataRowLlogari["QENDRA_KOSTOS"].ToString(), out qenderKosto);
                    int.TryParse(dbDataRowLlogari["KPF1"].ToString(), out kpf1);
                    int.TryParse(dbDataRowLlogari["KPF2"].ToString(), out kpf2);
                    int.TryParse(dbDataRowLlogari["KPF3"].ToString(), out kpf3);
                    int.TryParse(dbDataRowLlogari["NIVEL_TAKSE"].ToString(), out nivelTakse);
                    int.TryParse(dbDataRowLlogari["MONEDHA"].ToString(), out idMonedha);
                    int.TryParse(dbDataRowLlogari["GRUP"].ToString(), out grupi);
                    int.TryParse(dbDataRowLlogari["NENGRUP"].ToString(), out nengrupi);
                    int.TryParse(dbDataRowLlogari["LLOGARI_KOSOLIDUESE"].ToString(), out llogariKonsoliduese);
                    int.TryParse(dbDataRowLlogari["LLOGARI_KORESPONDUESE"].ToString(), out llogariKoresponduese);
                    int.TryParse(dbDataRowLlogari["IDNDERMARJE"].ToString(), out idNdermarja);
                    int.TryParse(dbDataRowLlogari["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowLlogari["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowLlogari["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowLlogari["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowLlogari["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    pershkrimiGrupiLlogaria = dbDataRowLlogari["PERSHKGRUPILLOGARIA_sq"].ToString();
                    pershkrimiNenGrupiLlogaria = dbDataRowLlogari["PERSHKNENGRUPILLOGARIA_sq"].ToString();
                    kodiMonedha = dbDataRowLlogari["MONEDHAKOD"].ToString();
                    pershkrimiMonedha = dbDataRowLlogari["MONEDHAPERSHK"].ToString();
                    kodiKPF1 = dbDataRowLlogari["KodiKPF1"].ToString();
                    kodiKPF2 = dbDataRowLlogari["KodiKPF2"].ToString();
                    kodiKPF3 = dbDataRowLlogari["KodiKPF3"].ToString();
                    pershkrimTaksa = dbDataRowLlogari["PERSHKRIMTAKSA"].ToString();
                    int.TryParse(dbDataRowLlogari["IDOBJEKTIVAKOSTO"].ToString(), out idObjektivaKosto);
                    objektiva = dbDataRowLlogari["OBJEKTIVA"].ToString();
                    int.TryParse(dbDataRowLlogari["IDSKEMAQENDRAKOSTO"].ToString(), out idSkemaQendraKosto);
                    qendra = dbDataRowLlogari["QENDRA"].ToString();
                    int.TryParse(dbDataRowLlogari["LLOJQENDRE"].ToString(), out llojQendre);
                    int.TryParse(dbDataRowLlogari["IDKATEGORISHPENZIMI"].ToString(), out idKategoriShpenzimi);
                    bool.TryParse(dbDataRowLlogari["AKTIV"].ToString(), out aktiv);
                    shenime1 = dbDataRowLlogari["LLOGSHENIME1"].ToString();
                    shenime2 = dbDataRowLlogari["LLOGSHENIME2"].ToString();
                    shenime3 = dbDataRowLlogari["LLOGSHENIME3"].ToString();
                    shenime4 = dbDataRowLlogari["LLOGSHENIME4"].ToString();
                    shenime5 = dbDataRowLlogari["LLOGSHENIME5"].ToString();

                    oColLidhjeAutorizim = new colLidhjetAutorizim();
                    oColVleratFushatShtese = new colVleraFushaShtese();
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se llogarive nga db - ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se llogarive nga db-ja");
                }
            }
            else
                return false;
        }
        public bool mbushLlogari(clsLlogari llogari)
        {
            this.idLlogari = llogari.idLlogari;
            nrLlogari = llogari.nrLlogari;
            emerLlogari1 = llogari.emerLlogari1;
            emerLlogari2 = llogari.emerLlogari2;
            qenderKosto = llogari.qenderKosto;
            kpf1 = llogari.kpf1;
            kpf2 = llogari.kpf2;
            kpf3 = llogari.kpf3;
            nivelTakse = llogari.nivelTakse;
            idMonedha = llogari.idMonedha;
            grupi = llogari.grupi;
            nengrupi = llogari.nengrupi;
            llogariKonsoliduese = llogari.llogariKonsoliduese;
            llogariKoresponduese = llogari.llogariKoresponduese;
            idNdermarja = llogari.idNdermarja;
            pershkrimiGrupiLlogaria = llogari.pershkrimiGrupiLlogaria;
            pershkrimiNenGrupiLlogaria = llogari.pershkrimiNenGrupiLlogaria;
            kodiMonedha = llogari.kodiMonedha;
            kodiKPF1 = llogari.kodiKPF1;
            kodiKPF2 = llogari.kodiKPF2;
            kodiKPF3 = llogari.kodiKPF3;
            idKonfig = llogari.idKonfig;
            idPerdoruesi = llogari.idPerdoruesi;
            pershkrimiMonedha = llogari.pershkrimiMonedha;
            idStatusDok = llogari.idStatusDok;
            dtKrijimi = llogari.dtKrijimi;
            dtModifikimi = llogari.dtModifikimi;
            pershkrimTaksa = llogari.pershkrimTaksa;
            idObjektivaKosto = llogari.idObjektivaKosto;
            idSkemaQendraKosto = llogari.idSkemaQendraKosto;
            llojQendre = llogari.llojQendre;
            objektiva = llogari.objektiva;
            qendra = llogari.qendra;
            idKategoriShpenzimi = llogari.idKategoriShpenzimi;
            kategoriShpenzimi = llogari.kategoriShpenzimi;
            oColLidhjeAutorizim = llogari.oColLidhjeAutorizim;
            oColVleratFushatShtese = llogari.oColVleratFushatShtese;
            aktiv = llogari.aktiv;
            rreshti = llogari.rreshti;
            shenime1 = llogari.shenime1;
            shenime2 = llogari.shenime2;
            shenime3 = llogari.shenime3;
            shenime4 = llogari.shenime4;
            shenime5 = llogari.shenime5;

            return true;
        }
        #endregion
    }
}