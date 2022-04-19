using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.DbAdmin;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using DbCore.DbShare;

namespace DbCore.DbListPagesat
{   /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  struktura administrative
    ///  (Te dhenat  merren nga tabela : T_STRUKTURAADMINISTRATIVE)
    /// </summary>
    public class clsStrukturaAdministrative : IDataBase
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se njesise administrative nga db-ja";
        private const string gabimEkzistimi = "ekziston nje Strukture Administrative me kete kod. Ju lutem shenoni nje tjeter!";
        private const string prindiJoAktiv = "Prindi nuk eshte aktiv! Nuk lejohet te shtohen bij te nje prindi inaktiv!";
        #region Atribute

        private int idStukturaAdm;
        private string kodi;
        private string emri;
        private string personi;
        private string nrTel;
        private string shenime;
        private int idPrindi;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int qenderKosto;
        private int idSkemaQendraKosto;
        private int llojQendre;
        private string qendra;
        private int niveli;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="personi"> personi i kontaktit</param>
        /// <param name="idprindi">id e prindit</param>
        /// <param name="nrtel">nr telefoni</param>
        /// <param name="idstrukutra"> id ritese e struktures  administrative</param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="kod">kodi i struktures administrative</param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="shenime"> shenime </param>
        /// <param name="emri"> emri i struktures</param>
        /// <param name="aktive"> emri i struktures</param>
        public clsStrukturaAdministrative(int idstrukutra, string kod, string emri, string personi, string nrtel, string shenime, int idprindi, int nderm, int idPerd, int idstatusdok, int qenderkosto, int idskemakosto, int llojqendre, bool aktive)
        {
            idStukturaAdm = idstrukutra;
            kodi = kod;
            this.emri = emri;
            this.personi = personi;
            nrTel = nrtel;
            this.shenime = shenime;
            idPrindi = idprindi;
            idNdermarje = nderm;
            idPerdoruesi = idPerd;
            idStatusDok = idstatusdok;
            this.qenderKosto = qenderkosto;
            this.idSkemaQendraKosto = idskemakosto;
            this.llojQendre = llojqendre;
            this.Aktive = aktive;

        }
    
            /// <summary>
            /// konstruktor me 2 parametra
            /// </summary>
            /// <param name="kodi">kodi i struktures administrative</param>
            /// <param name="idNderm">id e ndermarrjes</param>
            public clsStrukturaAdministrative(string kodi, int idNderm)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheStruktureAdmSipasKodit(kodi, idNderm, this);
            }
        }
     
        public clsStrukturaAdministrative(int idNderm, string pershkrimi)
        {
            this.merrSipasEmrit(pershkrimi, idNderm);
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idStrukturaAdm">id e struktura administrative</param>
        public clsStrukturaAdministrative(int idStrukturaAdm) : this(idStrukturaAdm, new clsDatabazeListPagesa())
        {

        }
        public clsStrukturaAdministrative(int idStrukturaAdm, clsDatabazeListPagesa db)
        {
            if (idStrukturaAdm > 0) mbushStruktureAdministrative(db.TransCache.getStrukturaAdministrative(idStrukturaAdm, db));

        }
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsStrukturaAdministrative()
        {
        }
        public clsStrukturaAdministrative(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }
        #endregion
     
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdStrukturaAdm
        {
            get { return idStukturaAdm; }
            set { idStukturaAdm = value; }
        }
        /// <summary>
        /// niveli i struktures ne hierarki
        /// </summary>
        public int Niveli
        {
            get { return niveli; }
            set { niveli = value; }
        }
        /// <summary>
        /// Kthen/Vendos shenime.
        /// </summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodi i struktures administrative.
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos emri i strukutres administrative.
        /// </summary>
        public string Emri
        {
            get { return emri; }
            set { emri = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e prindit.
        /// </summary>
        public int IdPrindi
        {
            get { return idPrindi; }
            set { idPrindi = value; }
        }
        public Boolean Aktive;
            
        /// <summary>
        /// Kthen/Vendos nr e telefonit
        /// </summary>
        public String NrTel
        {
            get { return nrTel; }
            set { nrTel = value; }
        }

        /// <summary>
        /// Kthen/Vendos  personi i kontaktit
        /// </summary>
        public String Personi
        {
            get { return personi; }
            set { personi = value; }
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
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// kthen daten e modifikimit te fundit te kesaj stukture
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// id e qendres se kostos
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
        /// id e skemes se kostos
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
        /// lloji i qendres 1-qender 2- skeme
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
        /// kodi i qendres ose i skemes
        /// </summary>
        public string Qendra
        {
            get
            {
                return qendra;
            }
            set { qendra = value; }
        }
        #endregion

        #region Metoda Publike

        public static clsStrukturaAdministrative Krijo(IDataRecord record)
        {
            clsStrukturaAdministrative strukturaAdmin = new clsStrukturaAdministrative();
            strukturaAdmin.Mbush(record);

            return strukturaAdmin;
        }

        /// <summary>
        /// ruan strukturen administrative
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo struktura</returns>
        public clsMesazh ruajStruktureAdm(IDictionary<string, object> hfNrAutoKF, IDictionary<string, object> HfArkiva)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.beginTransaksion();
            int idStruktura = 0;
            clsMesazh mesazhkont = kontrolloStruktureAdministrative(out kaNdryshimNumri, db, hfNrAutoKF, false);
            if (!mesazhkont.Status)
            {
                db.rollbackTransaksion();
                return mesazhkont;
            }

            mesazh = db.ruajStruktureAdm(out idStruktura, kodi, emri, idPrindi, personi, nrTel, shenime, idPerdoruesi, idNdermarje, idStatusDok, qenderKosto, idSkemaQendraKosto, llojQendre,Aktive);
            mesazh = colArkiva.RuajArkiven(idStruktura, 105, idPerdoruesi, idNdermarje, HfArkiva);
            idStukturaAdm = idStruktura;
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            db.commitTransaksion();

            if (kaNdryshimNumri)
                return mesazhkont;
            return mesazh;

        }
        private clsMesazh kontrolloStruktureAdministrative(out bool kaNdryshimNrAuto, clsDatabazeListPagesa db, IDictionary<string, object> hfNrAutoKF, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (kodi == "")
                return new clsMesazh(false, "Kodi nuk mund te jete bosh");
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh();
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoArt(out kaNdryshimNrAuto, db, hfNrAutoKF);
                    if (!mes.Status)
                        return mes;
                }

                if (db.EshteBijaStruktura(idPrindi))
                 return new clsMesazh(false, prindiJoAktiv);
                
                if (db.ekzistonStruktureAdministrative(kodi, idNdermarje))
                    return new clsMesazh(false, gabimEkzistimi);
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
               
            }
            else
            {
                clsStrukturaAdministrative pr = new clsStrukturaAdministrative(idPrindi);
                if (DbCore.DbListPagesat.clsStrukturaAdministrative.eshteBija(idPrindi))
                {
                    string mesazh = string.Format(MessagesResource.Messages["msgdepartamenti"], kodi, pr.Kodi);
                    return new clsMesazh(false, mesazh);
                }
            }
                return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }
        private clsMesazh kontrolloNrAutoArt(out bool kaNdryshimNumri, clsDatabazeListPagesa db, IDictionary<string, object> hfNrAutoKf)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);

            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "Kodi") != "")
                this.Kodi = NrAuto.ktheVlerenEre(list, "Kodi");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.idPerdoruesi, this.idNdermarje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);

        }
        /// <summary>
        /// kontrollon nese ekziston nje strukture administrative me kete emer
        /// </summary>
        /// <param name="emri"> emri i struktures</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonEmri(string emri, int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.ekzistonStruktureAdministrativeEmri(emri, idndermarje);
            db.Dispose();
            return ekziston;
        }

        /// <summary>
        /// modifikon strukturen administrative
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo struktura</returns>   
        public clsMesazh Modifiko()
        {
            bool kaNdryshimNumri;
            clsMesazh mesazh = new clsMesazh();
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                

                int idStruktura = 0;
                IDictionary<string, object> hfNrAutoKF = null;
                clsMesazh mesazhkont = kontrolloStruktureAdministrative(out kaNdryshimNumri, db, hfNrAutoKF, true);
                if (!mesazhkont.Status)
                {

                    return mesazhkont;
                }
              
             return db.modifikoStruktureAdm(idStukturaAdm, kodi, emri, idPrindi, personi, nrTel, shenime, idPerdoruesi, idNdermarje, idStatusDok, qenderKosto, idSkemaQendraKosto, llojQendre, Aktive);
            }
        }

        /// <summary>
        /// modifikon prindin e stuktura administrative
        /// </summary>
        /// <param name="idprind">id prindi</param>
        /// <param name="idperdorues">id perdoruesi</param>
        /// <returns></returns>
        public clsMesazh modifikoStrukturaAdm(int idprind, int idperdorues)
        {
            IdPrindi = idprind;
            IdPerdoruesi = idperdorues;
            return Modifiko();
        }

        /// <summary>
        /// Fshin objektin struktura administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Fshi()
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                return db.fshiStruktureAdmStatus(idStukturaAdm, idPerdoruesi);
        }

        /// <summary>
        /// merr strukturen administrative sipas emrit
        /// </summary>
        /// <param name="emri">emri</param>
        /// <param name="idndermarje">idndermarje</param>
        public void merrSipasEmrit(string emri, int idndermarje)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                db.ktheStruktureAdmSipasEmrit(emri, idndermarje, this);

        }
        /// <summary>
        /// kontrollon nese struktura administrative eshte e lidhur
        /// </summary>
        /// <param name="idstrukt">id e struktures administrative</param>
        /// <returns> true ose false</returns>
        public static bool eshteILidhur(int idstrukt)
        {
            clsDatabazeListPagesa dbLP = new clsDatabazeListPagesa();
            bool pergjigje = dbLP.eshteILidhurStrukturaAdministrative(idstrukt);
            dbLP.Dispose();
            return pergjigje;
        }

        public static bool eshteBija(int idprindi)
        {
            clsDatabazeListPagesa dbLP = new clsDatabazeListPagesa();
            bool pergjigje = dbLP.EshteBijaStruktura(idprindi);
            dbLP.Dispose();
            return pergjigje;
        }
        public void Mbush(IDataRecord record)
        {

            int.TryParse(record["IDSTRUKTURAADM"].ToString(), out idStukturaAdm);
            kodi = record["KODI"].ToString();
            emri = record["EMRI"].ToString();
            personi = record["PERSONI"].ToString();
            shenime = record["SHENIME"].ToString();
            nrTel = record["NRTEL"].ToString();
            int.TryParse(record["IDPRINDI"].ToString(), out idPrindi);
            int.TryParse(record["IDNDERMARJE"].ToString(), out idNdermarje);
            int.TryParse(record["IDPERDORUESI"].ToString(), out idPerdoruesi);
            int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
            DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
            DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            int.TryParse(record["QENDRAKOSTOS"].ToString(), out qenderKosto);
            int.TryParse(record["IDSKEMAQENDRAKOSTO"].ToString(), out idSkemaQendraKosto);
            qendra = record["QENDRA"].ToString();
            int.TryParse(record["LLOJQENDRE"].ToString(), out llojQendre);
            int.TryParse(record["NIVELI"].ToString(), out niveli);
            Boolean.TryParse(record["AKTIV"].ToString(), out Aktive);
        }
       
        #endregion

        #region Metoda Internal

        internal void mbushStruktureAdministrative(clsStrukturaAdministrative st)
        {  
                this.IdStrukturaAdm = st.IdStrukturaAdm;
                this.kodi = st.kodi;
                this.emri = st.emri;
                this.personi = st.personi;
                this.shenime = st.shenime;
                this.nrTel = st.nrTel;
                this.IdPrindi = st.IdPrindi;
                this.IdNdermarje = st.IdNdermarje;
                this.idPerdoruesi = st.idPerdoruesi;
                this.dtKrijimi = st.dtKrijimi;
                this.dtModifikimi = st.dtModifikimi;
                this.qenderKosto = st.qenderKosto;
                this.IdSkemaQendraKosto = st.IdSkemaQendraKosto;
                this.qendra = st.qendra;
                this.llojQendre = st.llojQendre;
                this.niveli = st.niveli;
                this.IdStatusDok = st.IdStatusDok;
                this.Aktive = st.Aktive;
        
        }

        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
