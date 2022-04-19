using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbKontabiliteti;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  qendrat e kostos
    ///  (Te dhenat  merren nga tabela : T_QENDRAKOSTO)
    /// </summary>
    public class clsQendraKosto
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se qendrave te kostos nga db-ja";
        private const string gabimEkzistimi = "Ekziston nje qender kostoje me kete kod. Ju lutem shenoni nje tjeter!";
        private const string STR_PrindiNukEshteAktivNukLejohetTeShtohenBijTeNjePr = "Prindi nuk eshte aktiv! Nuk lejohet te shtohen bij te nje prindi inaktiv!";
        private const string STR_QendraEKostosBijDuhetTeKeteMonedhenEPrindit = "Qendra e kostos bij duhet te kete monedhen e prindit!";
        /// <summary>
        /// mesazh gabimi per mos plotesimin e kodit
        /// </summary>
        private const string STR_PlotesoniKodin = "Plotesoni kodin e qendres se kostos!";
        /// <summary>
        /// mesazh gabimi per mospletesimin e emertimit
        /// </summary>
        private const string STR_PlotesoniEmertimin = "Plotesoni pershkrimin e qendres se kostos!";
        /// <summary>
        /// mesazh gabimi per mos plotesimin e monedhes
        /// </summary>
        private const string STR_PlotesoniMonedhen = "Plotesoni monedhen!";
        ///<summary>
        /// mesazh gabimi kur prindi nuk ekziston
        /// </summary>
        private const string STR_PrindiNukEkziston = "Prindi nuk ekziston!";
        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletUKaluanMeSukses = "Kontrollet e qendrave te kostos u kaluan me sukses";
        #region Atribute

        private int id;
        private string kodi;
        private string pershkrimi;
        private int idMonedha;
        private bool aktiv;
        private int idKonfig;
        private int idPrindi;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string prindi;
        private string monedha;
        private int niveli;
        /// <summary>
        /// koleksioni me buxhetet
        /// </summary>
        private colBuxhetet oColBuxhete;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idmon"> monedha</param>
        /// <param name="idprindi">id e prindit</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="id"> id ritese </param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="kod">kodi i struktures administrative</param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="idKonfig"> shenime </param>
        /// <param name="pershkrimi"> emri i struktures</param>
        public clsQendraKosto(int id, string kod, string pershkrimi, int idmon, bool aktiv, int idKonfig, int idprindi, int nderm, int idPerd, int idstatusdok, int niveli, colBuxhetet colBuxhetet)
        {
            this.id = id;
            kodi = kod;
            this.pershkrimi = pershkrimi;
            this.idMonedha = idmon;
            this.aktiv = aktiv;
            this.idKonfig = idKonfig;
            idPrindi = idprindi;
            idNdermarje = nderm;
            idPerdoruesi = idPerd;
            idStatusDok = idstatusdok;
            this.niveli = niveli;
            this.oColBuxhete = colBuxhetet;
        }

        public clsQendraKosto(int id, string kod, string pershkrimi, int idmon, bool aktiv, int idKonfig, int idprindi, int nderm, int idPerd, int idstatusdok, string prind, string monedha, bool isshtim, int idGjuha, int niveli, colBuxhetet colBuxhetet)
        {
            try
            {
            this.id = id;
            kodi = kod;
            this.pershkrimi = pershkrimi;
            this.idMonedha = idmon;
            this.aktiv = aktiv;
            this.idKonfig = idKonfig;
            idPrindi = idprindi;
            idNdermarje = nderm;
            idPerdoruesi = idPerd;
            idStatusDok = idstatusdok;
            this.prindi = prind;
            this.monedha = monedha;
            this.niveli = niveli;
            this.oColBuxhete = colBuxhetet;
            clsMesazh mesazh = kontrolloQK(isshtim, idGjuha);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i struktures administrative</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsQendraKosto(string kodi, int idNderm)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                if (!mbushQendraKosto(db.ktheQenderKostoSipasKodit(kodi, idNderm)))
                    id = -1;
            }
        }
        public clsQendraKosto(string kodi, int idNderm, clsDatabaseQendraKosto db)
        {
            if (!mbushQendraKosto(db.ktheQenderKostoSipasKodit(kodi, idNderm)))
                id = -1;
        }


        public static clsQendraKosto getQendraKostoByCodeFromCache(string kodi, int idNderm, clsDatabaseQendraKosto db)
        {
            return db.TransCache.getQendraKostoByCode(kodi, idNderm, db);
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e qendres</param>
        public clsQendraKosto(int id)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                mbushQendraKosto(db.ktheQenderKosto(id));
            }
        }
        public clsQendraKosto(int id ,  clsDatabaseQendraKosto db)
        {
              mbushQendraKosto(db.TransCache.getQendraKosto(id,db));
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsQendraKosto()
        {
        }

        public clsQendraKosto(DataRow rreshti)
        {
            
            mbushQendraKosto(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodi .
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e prindit.
        /// </summary>
        public int IdPrindi
        {
            get { return idPrindi; }
            set { idPrindi = value; }
        }

        /// <summary>
        /// Kthen/Vendos aktiv
        /// </summary>
        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos  id e monedhes
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }
        /// <summary>
        /// kthen kodin e monedhes
        /// </summary>
        public string Monedha
        {
            get
            {
                return monedha;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe e ka kryer veprimin.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// kthen/vendos id e statusit te dokumentit
        /// <example>0- draft, 1-ruajtur, 2-fshire</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// kthen daten e krijimit te kesaj strukture
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        /// <summary>
        /// kthen daten e modifikimit te fundit te kesaj stukture
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }
        /// <summary>
        /// kthen kodin e prindit
        /// </summary>
        public string Prindi
        {
            get
            {
                return prindi;
            }
        }

        public int Niveli
        {
            get { return niveli; }
            set { niveli = value; }
        }
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsBuxheti"/>
        /// </summary>
        public colBuxhetet OColBuxhetet
        {
            get { return oColBuxhete; }
            set { oColBuxhete = value; }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kontroll nese objekti i qendra e kostos i ka te dhenat e sakta 
        /// </summary>
        /// <param name="shtim">tregon nese eshte shtim apo modifikim</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloQK(bool shtim, int idGjuha)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodin);
            if (pershkrimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertimin);
            if (idMonedha == 0)
                return new clsMesazh(false, STR_PlotesoniMonedhen);
            if (shtim && ekzistonQK(kodi, idNdermarje))
                return new clsMesazh(false, gabimEkzistimi);

            if (prindi != "")
            {
                if (!ekzistonQK(prindi, idNdermarje))
                    return new clsMesazh(false, STR_PrindiNukEkziston);
                clsQendraKosto pr = new clsQendraKosto(idPrindi);
                if(!pr.aktiv)
                    return new clsMesazh(false, STR_PrindiNukEshteAktivNukLejohetTeShtohenBijTeNjePr);
                if(pr.idMonedha!=idMonedha)
                    return new clsMesazh(false, STR_QendraEKostosBijDuhetTeKeteMonedhenEPrindit);
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = idKonfig };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente, idGjuha);
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(pr.id.ToString(), konf.IdNivel.ToString());
                clsDatabaseQendraKosto dbQK = new clsDatabaseQendraKosto();
                if(lidhur&& !dbQK.kaBijQendraKostoje(pr.id))
                    return new clsMesazh(false,"Qendra e kostos prind eshte perdorur ne veprime dhe nuk mund te detajohet!");
            }

            return new clsMesazh(true, STR_KontrolletUKaluanMeSukses);
        }
        
        /// <summary>
        /// ruan qendren e kostos
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo qendra</returns>
        public clsMesazh ruajQenderKosto(int idNderVit, bool pergjigje)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                clsMesazh mesazh = new clsMesazh();
                db.beginTransaksion();
                int idq = 0;
                if (db.ekzistonQenderKostoje(kodi, idNdermarje))
                    return new clsMesazh(false, gabimEkzistimi);
                mesazh = db.ruajQenderKosto(out idq, kodi, pershkrimi, idPrindi, idMonedha, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, niveli);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se qendres se kostos!");
                }
                id = idq;
                clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet(db );
                int idLlojBuxheti = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("QendraKosto", dbKont);
                //duhet te behet kontrolli i buxhetit total te kesaj qk me buxhetin total te qk prind, nese eshte me i madh duhet afishuar nje mesazh per perdoruesin
                if (idPrindi != 0)
                {
                    clsBuxheti buxhetTotal = oColBuxhete.KtheBuxhetinTotal();
                    clsQendraKosto qkPrind = new clsQendraKosto(idPrindi);
                    qkPrind.oColBuxhete = new colBuxhetet(idPrindi, idLlojBuxheti);
                    if (tejkalohetBuxheti(buxhetTotal, qkPrind) && !pergjigje)
                    {
                        db.rollbackTransaksion();
                        return new clsMesazh(TipMesazhi.Informim, "Buxhetit 1 e tejkalon buxhetin e zerit prind. Doni te vazhdoni?");
                    }

                }
                foreach (clsBuxheti o in oColBuxhete)
                {
                    o.IdLidhese = id;
                    int idB;
                    //decimal buxhet_1max=  colBuxhetet.ktheBuxhet_1Max(id, idNderVit);
                    //  decimal buxhet_2max = colBuxhetet.ktheBuxhet_1Max(id, idNderVit);
               
                        mesazh = dbKont.ruajBuxhet(out idB, idLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, idNderVit, o.DtAktivizimi,o.IdKonfigUrdherPagese,o.Shenime);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se buxheteve!");
                    }
                }
                if (mesazh.Status)
                {
                    mesazh = colBuxhetet.ktheGjitheVitetENdermPerBuxhetetSipasQendraKostos(23, idNdermarje, id, idNderVit, dbKont);
                    if (mesazh.Status)
                        db.commitTransaksion();
                    else
                        db.rollbackTransaksion();

                }
                else
                    db.rollbackTransaksion();
                return mesazh;
            }
        }
        /// <summary>
        /// kontrollon nese ekziston qender me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonQK(string kodi, int idndermarje)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
               return db.ekzistonQenderKostoje(kodi, idndermarje);
            }
        }
        public static bool ekzistonQK(string kodi, int idndermarje ,   clsDatabaseQendraKosto db)
        {
           return db.ekzistonQenderKostoje(kodi, idndermarje);
        }

        public static int ktheIdQK(string kodi, int idndermarje)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return ktheIdQK( kodi,  idndermarje,db);
            }
        }
        public static int ktheIdQK(string kodi, int idndermarje, clsDatabaseQendraKosto db)
        {
            return db.ktheIdQenderKostoje(kodi, idndermarje);
        }
        public void merrQKP(int indermarje, clsDatabaseQendraKosto db)
        {
            mbushQendraKosto( db.TransCache.getQendraQKP(indermarje, db));
        }
        public static int ktheIdMonedhenEQK(int idQk)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.ktheIdMonedhenEQK(idQk);
            }
        }
        public static int ktheIdMonedhenEQK(int idQk, clsDatabaseQendraKosto db)
        {
                return db.ktheIdMonedhenEQK(idQk);
        }
        /// <summary>
        /// modifikon qendren
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo qendra</returns>   
        public clsMesazh modifikoQenderKosto(int idNderVit, bool pergjigje)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            db.beginTransaksion();
            mesazh = db.modifikoQenderKosto(id, kodi, pershkrimi, idPrindi, idMonedha, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, niveli);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            if (idPrindi == 0)
            {
                //clsDatabaseQendraKosto.kaBijQendraKostoje(id) 
                DataTable dt = db.ktheQendraKostoSipasPrindit(id);

                //Ndryshimi Fillon Ketu
                int fillimi = 0;
                int fundi = dt.Rows.Count - 1;
                if (dt.Rows.Count > 0)
                {
                    while (fillimi != fundi)
                    {
                        DataRow r = dt.Rows[fillimi];
                        //.Rows(fillimi);
                        clsQendraKosto qk = new clsQendraKosto();
                        qk.mbushQendraKosto(r);
                        DataTable dtTemp = db.ktheQendraKostoSipasPrindit(qk.id);
                        foreach (DataRow rtmp in dtTemp.Rows)
                        {
                            dt.Rows.Add(rtmp.ItemArray);
                            fundi++;
                        }
                        fillimi++;
                    }
                }
                //end while
                //Ndryshim mbaron ketu

                foreach (DataRow dr in dt.Rows)
                {
                    clsQendraKosto qk = new clsQendraKosto();
                    qk.mbushQendraKosto(dr);
                    mesazh = db.modifikoQenderKosto(qk.id, qk.kodi, qk.pershkrimi, qk.idPrindi, qk.idMonedha, aktiv, qk.idKonfig, idPerdoruesi, qk.idNdermarje, qk.idStatusDok, qk.niveli);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return mesazh;
                    }
                }
            }
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet(db );
            int idLlojBuxheti = clsLlojBuxheti.mbushIDLlojBuxheti("QendraKosto", dbKont);
            if (oColBuxhete.Count != 0)
            {
                if (!clsBuxheti.ekzistonProjektBuxhetiPerKategorine(id, idLlojBuxheti, idNderVit, false, oColBuxhete[0].DtAktivizimi, dbKont))
                {
                    foreach (clsBuxheti o in oColBuxhete)
                    {
                        o.IdLidhese = id;
                        o.IdLlojBuxheti = idLlojBuxheti;
                        int idB;
                        mesazh = dbKont.ruajBuxhet(out idB, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, idNderVit, o.DtAktivizimi, o.IdKonfigUrdherPagese, o.Shenime);
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te buxheteve!");
                        }
                    }
                }
                else if (!clsBuxheti.ekzistonBuxhetPerDaten(id, idLlojBuxheti, idNderVit, oColBuxhete[0].DtAktivizimi,  dbKont))
                {
                    foreach (clsBuxheti o in oColBuxhete)
                    {
                        o.IdLidhese = id;
                        o.IdLlojBuxheti = idLlojBuxheti;
                        int idB;
                        mesazh = dbKont.ruajBuxhet(out idB, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, idNderVit, o.DtAktivizimi, o.IdKonfigUrdherPagese, o.Shenime);
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te buxheteve!");
                        }
                    }
                }
                else
                {

                    //duhet te behet kontrolli i buxhetit total te kesaj qk me buxhetin total te qk prind, nese eshte me i madh duhet afishuar nje mesazh per perdoruesin

                    if (idPrindi != 0)
                    {
                        clsBuxheti buxhetTotal = oColBuxhete.KtheBuxhetinTotal();
                        clsQendraKosto qkPrind = new clsQendraKosto(idPrindi);
                        qkPrind.oColBuxhete = new colBuxhetet(idPrindi, idLlojBuxheti);
                        if (tejkalohetBuxheti(buxhetTotal, qkPrind) && !pergjigje)
                        {
                            db.rollbackTransaksion();
                            return new clsMesazh(TipMesazhi.Informim, "Buxhetit 1 e tejkalon buxhetin e zerit prind. Doni te vazhdoni?");
                        }
                    }
                    foreach (clsBuxheti o in oColBuxhete)
                    {

                        mesazh = dbKont.modifikoBuxhet(o.IdBuxheti, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, idNderVit, o.DtAktivizimi, o.IdKonfigUrdherPagese, o.Shenime);
                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te buxheteve!");
                        }
                    }
                }
            }
            db.commitTransaksion();
            return mesazh;
        }

        public bool tejkalohetBuxheti(clsBuxheti buxheti, clsQendraKosto qkPrind)
        {
            clsBuxheti buxhetTotal = oColBuxhete.KtheBuxhetinTotal();
            decimal buxhet_1max = buxheti.Buxheti_1;
            decimal buxhet_2max = buxheti.Buxheti_2;
          
            decimal buxhet_1prindmax = qkPrind.oColBuxhete.KtheBuxhetinTotal().Buxheti_1;
            decimal buxhet_2prindmax = qkPrind.oColBuxhete.KtheBuxhetinTotal().Buxheti_2;
            if (buxhet_1max > buxhet_1prindmax) return true;
            else return false;

        }



        /// <summary>
        /// Fshin objektin qendren ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            clsMesazh u_fshi = db.fshiQenderKostoStatus(id, idPerdoruesi);
            db.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin qender kosto nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        public void merr()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            db.ktheQenderKosto(id);
            db.Dispose();
        }

       
        /// <summary>
        /// Merr datatable qendrat  te nje ndermarjenga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe qendrat te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            DataTable dt = db.merrQendraKostoDT(IdNdermarje);
            db.Dispose();
            return dt;            
        }

        public static bool eshtePrindQendraKosto(int idQk, clsDatabaseQendraKosto db)
        {
            return db.TransCache.eshtePrindQendraKosto(idQk, db);
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush qendrat nga databaza
        /// </summary>
        /// <param name="db">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushQendraKosto(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["ID"].ToString(), out id);
                    kodi = db["KODI"].ToString();
                    pershkrimi = db["PERSHKRIMI"].ToString(); 
                    monedha = db["MONEDHA"].ToString(); 
                    prindi = db["PRINDI"].ToString();
                   int.TryParse( db["IDMONEDHA"].ToString() , out idMonedha);
                    int.TryParse(db["IDKONFIG"].ToString(), out idKonfig);
                    bool.TryParse(db["AKTIV"].ToString(), out aktiv);
                    int.TryParse(db["IDPRINDI"].ToString(), out idPrindi);
                    int.TryParse(db["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(db["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(db["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(db["NIVELI"].ToString(), out niveli);
                    DateTime.TryParse(db["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(db["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return false;
        }
        internal void mbushQendraKosto(clsQendraKosto qk)
        {

            id = qk.id;
            kodi = qk.kodi;
            pershkrimi = qk.pershkrimi;
            monedha = qk.monedha;
            prindi = qk.prindi;
            IdMonedha = qk.IdMonedha;
            idKonfig = qk.idKonfig;
            aktiv = qk.aktiv;
            IdPrindi = qk.IdPrindi;
            idNdermarje = qk.idNdermarje;
            idPerdoruesi = qk.idPerdoruesi;
            IdStatusDok = qk.IdStatusDok;
            niveli = qk.niveli;
            dtKrijimi = qk.dtKrijimi;
            dtModifikimi = qk.dtModifikimi;
                    


        }
        #endregion


    }
}
