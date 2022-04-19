using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{  
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne dokumentat te ndryshem si dokument regjistrimi, magazine etj. 
    ///  (Te dhenat  merren  me ane te store procedurave dhe sherben per kerkimin e dokumentave ekzistues sipas ambjentit ku ndodhemi.
    /// </summary>
    public class clsDokumenti
    {
        #region Atribute

        private int idDokumenti;
        private int idNiveli;
        private string nrDokumenti;
        private int idKlientFurnitori;
        private string pershkrimi;
        private int idMonedha;
        private double kursi;
        private double vlefta;
        private double vleftaPaLikujduar;
        private int status;
        private DateTime dtDokumenti;
        private DateTime dtMaturimi;
        private double zbritja;
        private int idKushtPagese;
        private double vleftaLikuiduar;
        private double vleftaLikuiduarMon;
        private DateTime dtAzhornimi;
        private double kursAzhornimi;
        private int idKonfigAmbjente;
        private string emertimiKf;
        private DataRow rreshti;
        //private string ngjyra;
     

        #endregion

        #region Konstruktori

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsDokumenti() 
        {
        }
      
        public clsDokumenti(object niveli, object nrdok, object dtdok, object vleftapalikuiduar, object vlefta, object kursi, object monedha, int idndermarje, int idkf, object iddok,clsDatabaseRegjistrim dbregj)
        {
            DbAdmin.clsDatabaseAdmin dbadmin = new DbAdmin.clsDatabaseAdmin(dbregj );
                if (niveli != null && niveli.ToString() != string.Empty)
                {
                    //DbCore.DbRegjistrim.clsNivelRegjistrimi nivel = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                    //nivel.mbushNivelRegjistrimiSipasKodit(niveli.ToString(), idndermarje));
                    int idNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(niveli.ToString(),idndermarje,dbregj);
                    if (idNiveli >0)
                        this.idNiveli = idNiveli;
                }
                if (nrdok != null && nrdok.ToString() != "")
                   nrDokumenti = nrdok.ToString();
                if (iddok != null && iddok.ToString() != "")
                IdDokumenti = int.Parse(iddok.ToString());
                if (vleftapalikuiduar != null && vleftapalikuiduar.ToString() != "")
                    vleftaPaLikujduar = double.Parse(vleftapalikuiduar.ToString());
                if (vlefta != null && vlefta.ToString() != "")
                {
                   this.Vlefta = double.Parse(vlefta.ToString());                   
                }
                if (monedha != null && monedha.ToString() != "")
                {
                    DbAdmin.clsMonedha clsMon = new DbAdmin.clsMonedha();
                    clsMon.mbushMonedhen(monedha.ToString(), idndermarje,dbadmin);
                    IdMonedha = clsMon.IdMonedha;
                }
                if (kursi != null && kursi.ToString() != "")
                    Kursi = Double.Parse(kursi.ToString());
                if (dtdok != null && dtdok.ToString() != "")
                    DtDokumenti = DateTime.Parse(dtdok.ToString());

                if (niveli != null && niveli.ToString() != "")
                {
                    IdKlientFurnitori = idkf;
                }
                else 
                    this.idNiveli = -1;
           
        }
        public clsDokumenti(int idniveli, string nrdok, DateTime dtdok, double vleftapalikuiduar, double vlefta, double kursi, int idmonedha, int idkf, int iddok)
        {

            this.idNiveli = idniveli;
            nrDokumenti = nrdok;
            IdDokumenti = iddok;
            vleftaPaLikujduar = vleftapalikuiduar;
            this.Vlefta = vlefta;
            this.idMonedha = idmonedha;
            Kursi = kursi;
            DtDokumenti = dtdok;
            IdKlientFurnitori = idkf;


        }
        public clsDokumenti(int id)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            mbushDokument(dbDokumenti.ktheKategoriNivelDokSipasID(id));
            dbDokumenti.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtdok"> data e  dokumentit</param>
        /// <param name="dtmaturimi"> data e maturimit te dokumentit</param>
        /// <param name="iddok"> id e dokumentit</param>
        /// <param name="idKF"> id e Klient furnitorit me te cilin lidhet</param>
        /// <param name="idkushtpagese"> id kushtpageses me te cilin lidhet</param>
        /// <param name="idmon"> id e monedhes me te cilin lidhet</param>
        /// <param name="kurs">kursi i dokumentit</param>
        /// <param name="idniv"> id e nivelit te dokumentit</param>
        /// <param name="nrDok"> nr i dokumentit</param>
        /// <param name="pershk"> pershkrimi i dokumentit</param>
        /// <param name="stat"> gjendja e dokumentit </param>
        /// <param name="vleft"> vlefta e dokumentit </param>
        /// <param name="vlPaLik"> vlefta e palikuiduar e dokumentit</param>
        /// <param name="zb">zbritja e dokumentit</param>
        /// <param name="vleftalikuiduar"></param>
        /// <param name="vleftalikuiduarmon"></param>
        ///<param name="dtAzhornimi"></param>
        ///<paparam name="kursazhornimi"></paparam>
        ///<param name="idKonfigAmbjente"></param>
        public clsDokumenti(int iddok, int idniv, string nrDok, int idKF, string pershk, int idmon, double kurs, double vleft, double vlPaLik, int stat, DateTime dtdok, DateTime dtmaturimi, double zb, int idkushtpagese, double vleftalikuiduar, double vleftalikuiduarmon, DateTime dtAzhornimi, double kursazhornimi, int idKonfigAmbjente, string emertimiKf)
        {
            idDokumenti = iddok;
            idNiveli = idniv;
            nrDokumenti = nrDok;
            idKlientFurnitori = idKF;
            pershkrimi = pershk;
            idMonedha = idmon;
            kursi = kurs;
            vlefta = vleft;
            vleftaPaLikujduar = vlPaLik;
            status = stat;
            dtDokumenti = dtdok;
            dtMaturimi = dtmaturimi;
            zbritja = zb;
            idKushtPagese = idkushtpagese;
            vleftaLikuiduar = vleftalikuiduar;
            vleftaLikuiduarMon = vleftalikuiduarmon;
            this.dtAzhornimi = dtAzhornimi;
            this.kursAzhornimi = kursazhornimi;
            this.idKonfigAmbjente = idKonfigAmbjente;
            this.emertimiKf = emertimiKf;
        }

        public clsDokumenti(DataRow rreshti)
        {
            
            mbushDokument(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne  e dokumentit.
        /// </summary>
        public int IdDokumenti
        {
            get { return idDokumenti; }
            set { idDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit.
        /// </summary>
        public int IdNiveli
        {
            get { return idNiveli; }
            set { idNiveli = value; }
        }

        /// <summary>
        /// kthen/vendos id e konfigurimi
        /// </summary>
        public int IdKonfigAmbjente
        {
            get
            {
                return idKonfigAmbjente;
            }
            set
            {
                idKonfigAmbjente = value;
            }
        }
        //public string Ngjyra
        //{
        //    get
        //    {
        //        return ngjyra;
        //    }
        //    set
        //    {
        //        ngjyra = value;
        //    }
        //}
        /// <summary>
        /// Kthen/Vendos nr e dokumentit.
        /// </summary>
        public string NrDokumenti
        {
            get { return nrDokumenti; }
            set { nrDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e klient furnitorit me te cilen lidhet.
        /// </summary>
        public int IdKlientFurnitori
        {
            get { return idKlientFurnitori ; }
            set { idKlientFurnitori  = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i dokumentit.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes te dokumentit.
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha ; }
            set { idMonedha  = value; }
        }

        /// <summary>
        /// Kthen/Vendos kursin e sokumentit
        /// </summary>
        public double Kursi
        {
            get { return kursi; }
            set { kursi = value; }
        }

        /// <summary>
        /// Kthen/Vendos  vlefta e dokumentit.
        /// </summary>
        public double Vlefta
        {
            get { return vlefta  ; }
            set { vlefta =value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta e palikuiduar e dokumentit.
        /// </summary>
        public double VleftaPaLikujduar
        {
            get { return vleftaPaLikujduar ; }
            set { vleftaPaLikujduar = value; }
        }

        /// <summary>
        /// Kthen/Vendos gjendjen e dokumentit.
        /// <example > ruajtur, stornuar, fshire etj</example>
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e  dokumentit.
        /// </summary>
        public DateTime DtDokumenti
        {
            get { return dtDokumenti; }
            set { dtDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos  daten e maturimit te dokumentit.
        /// </summary>
        public DateTime DtMaturimi
        {
            get { return dtMaturimi; }
            set { dtMaturimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos  daten e azhornimit te dokumentit.
        /// </summary>
        public DateTime DtAzhornimi
        {
            get { return dtAzhornimi; }
            set { dtAzhornimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos zbritjen e dokumentit.
        /// </summary>
        public Double Zbritja
        {
            get { return zbritja; }
            set { zbritja = value; }
        }
           /// <summary>
        /// Kthen/Vendos kursin
        /// </summary>
        public Double KursAzhornimi
        {
            get { return kursAzhornimi; }
            set { kursAzhornimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e kusht pageses me te cilen lidhet dokumenti
        /// </summary>
        public int IdKushtPagese
        {
            get { return idKushtPagese; }
            set { idKushtPagese = value; }
        }
          /// <summary>
        /// Kthen/Vendos vleften likuiduar.
        /// </summary>
        public Double VleftaLikuiduar
        {
            get { return vleftaLikuiduar; }
        set { vleftaLikuiduar = value; }
        } 
        /// <summary>
        /// Kthen/Vendos vleften likuiduar ne monedhen baze.
        /// </summary>
        public Double VleftaLikuiduarMon
        {
            get { return vleftaLikuiduarMon; }
        set { vleftaLikuiduarMon =value; }
        }

        public String EmertimiKf
        {
            get { return emertimiKf; }
            set { emertimiKf = value; }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush dokumentin nga databaza
        /// </summary>
        /// <param name="dbDataRowDokumenti">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDokument(DataRow dbDataRowDokumenti)
        {
            if (dbDataRowDokumenti != null)
            {
                try
                {
                    idDokumenti = int.Parse(dbDataRowDokumenti["IDSHITJEKOKA"].ToString());
                    idNiveli = int.Parse(dbDataRowDokumenti["IDNIVEL"].ToString());
                    nrDokumenti = dbDataRowDokumenti["NRDOK"].ToString();
                    idKlientFurnitori = int.Parse(dbDataRowDokumenti["IDKLIENTFURNITOR"].ToString());
                    pershkrimi = dbDataRowDokumenti["Pershkrimi"].ToString();
                    idMonedha = int.Parse(dbDataRowDokumenti["IDMONEDHA"].ToString());
                    kursi = double.Parse(dbDataRowDokumenti["KURSI"].ToString());
                    vlefta = double.Parse(dbDataRowDokumenti["TOTALI"].ToString());
                    vleftaPaLikujduar = double.Parse(dbDataRowDokumenti["vleftaPalikujduar"].ToString());
                    status = int.Parse(dbDataRowDokumenti["IDSTATUSDOK"].ToString());
                    dtDokumenti = DateTime.Parse(dbDataRowDokumenti["DTDOK"].ToString());
                    dtMaturimi = DateTime.Parse(dbDataRowDokumenti["DTMATURIMI"].ToString());
                    zbritja = double.Parse(dbDataRowDokumenti["ZBRITJE"].ToString());
                     int.TryParse(dbDataRowDokumenti["IDKUSHTPAGESE"].ToString(), out idKushtPagese);
                    vleftaLikuiduar= double.Parse(dbDataRowDokumenti["vleftaPalikujduar"].ToString());
                    vleftaLikuiduarMon= double.Parse(dbDataRowDokumenti["vleftaPalikujduar"].ToString())*double.Parse(dbDataRowDokumenti["KURSI"].ToString());
                    dtAzhornimi = DateTime.Parse(dbDataRowDokumenti["DTAZHORNIMI"].ToString());
                    kursAzhornimi = double.Parse(dbDataRowDokumenti["KURSAZHORNIMI"].ToString());
                    idKonfigAmbjente = int.Parse(dbDataRowDokumenti["IDKONFIGAMBJENTE"].ToString());
                    emertimiKf = dbDataRowDokumenti["EmertimiKf"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se dokumentit nga db-ja");
                }
            }
            else
                return false;
        }
        internal bool mbushDokumentNgaBanka(DataRow dbDataRowDokumenti)
        {
            if (dbDataRowDokumenti != null)
            {
                try
                {
                    idDokumenti = int.Parse(dbDataRowDokumenti["IDKOKA"].ToString());
                    idNiveli = int.Parse(dbDataRowDokumenti["IDNIVEL"].ToString());
                    nrDokumenti = dbDataRowDokumenti["NRDOKUMENTI"].ToString();
                     idMonedha = int.Parse(dbDataRowDokumenti["IDMONEDHA"].ToString());
                    kursi = double.Parse(dbDataRowDokumenti["IDKURSI"].ToString());
                    vlefta = double.Parse(dbDataRowDokumenti["VLERA"].ToString());
                    vleftaPaLikujduar = double.Parse(dbDataRowDokumenti["VLERA"].ToString());
                    status = int.Parse(dbDataRowDokumenti["IDSTATUSDOK"].ToString());
                    dtDokumenti = DateTime.Parse(dbDataRowDokumenti["DATEDOKUMENTI"].ToString());
                    
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se dokumentit nga db-ja");
                }
            }
            else
                return false;
        }
        /// <summary>
        /// mbush dokumentin nga databaza
        /// </summary>
        /// <param name="dbDataRowDokumenti">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDokumentKerkimi(DataRow dbDataRowDokumenti)
        {
            if (dbDataRowDokumenti != null)
            {
                try
                {
                    idDokumenti = int.Parse(dbDataRowDokumenti["IdDokumenti"].ToString());
                    idNiveli = int.Parse(dbDataRowDokumenti["Niveli"].ToString());
                    nrDokumenti = dbDataRowDokumenti["NrDokumenti"].ToString();
                     int.TryParse(dbDataRowDokumenti["IdKlientFurnitori"].ToString(), out idKlientFurnitori);
                    pershkrimi = dbDataRowDokumenti["pershkrimi"].ToString();
                    idMonedha = int.Parse(dbDataRowDokumenti["idMonedha"].ToString());
                    vlefta = double.Parse(dbDataRowDokumenti["vlefta"].ToString());
                    status = int.Parse(dbDataRowDokumenti["Statusi"].ToString());
                    dtDokumenti = DateTime.Parse(dbDataRowDokumenti["DtDokumenti"].ToString());
                    idKonfigAmbjente = int.Parse(dbDataRowDokumenti["IdKonfigAmbjente"].ToString());
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se dokumentit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
