using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e skemes
    ///  (Te dhenat  merren nga tabela : T_TRUPISKEMAWORKFLOW)
    /// </summary>
    public class clsTrupiSkemaWorkFlow
    {
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private int idPerdRol;
        private int niveli;
        private double vleraLimit;
        private bool modifiko;
        private int lloji;
        private string email;
        private int idDelegimi;
        private string perdoruesi;
        private string delegimi;
        private int niveliApr;
        private int llojGrupiKf;
        private string grupeKF;
        private decimal dite;
        private string pershkrimi;
        private DbKontabiliteti.colGrupeKF colGrupeKF;
        private DataRow rreshti;
        #endregion

        #region Properties

        /// <summary>
        /// dite limiti per te kaluar aprovimi ne nivelin tjeter
        /// </summary>
        public decimal Dite
        {
            get
            {
                return dite;
            }
            set
            {
                dite = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e kokes 
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit delegues.
        /// </summary>
        public int IdDelegimi
        {
            get { return idDelegimi; }
            set { idDelegimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e perdoruesit ose rolit.
        /// </summary>
        public int IdPerdRol
        {
            get
            { return idPerdRol; }
            set
            {
                idPerdRol = value;
            }
        }
        /// <summary>
        /// kthen/vendos llojin e grupit kf 1,2,3
        /// </summary>
        public int LlojGrupiKf
        {
            get
            {
                return llojGrupiKf;
            }
            set
            {
                llojGrupiKf = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nivelit.
        /// </summary>
        public int Niveli
        {
            get
            { return niveli; }
            set
            {
                niveli = value;
            }
        }
        /// <summary>
        /// kthen vendos nivelin e aprovimit
        /// </summary>
        public int NiveliApr
        {
            get
            {
                return niveliApr;
            }
            set
            {
                niveliApr = value;
            }
        }
        /// <summary>
        /// kthen merr emrin perdoruesin ose rolin
        /// </summary>
        public string Perdoruesi
        {
            get
            {
                return perdoruesi;
            }
            set
            {
                perdoruesi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos vleren limit.
        /// </summary>
        public double VleraLimit
        {
            get
            {
                return vleraLimit;
            }
            set
            {
                vleraLimit = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nese pedoruesi ka te drejta modifikimi.
        /// </summary>
        public bool Modifiko
        {
            get
            {
                return modifiko;
            }
            set
            {
                modifiko = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos llojin.
        /// </summary>
        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }
        /// <summary>
        /// Kthen/Vendos email.
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        /// <summary>
        /// emri i deleguesit
        /// </summary>
        public string Delegimi
        {
            get
            {
                return delegimi;
            }
            set
            {
                delegimi = value;
            }
        }
        public string GrupeKF
        {
            get
            {
                return grupeKF;
            }
            set
            {
                grupeKF = value;
            }
        }
        /// <summary>
        /// Pershkrimi
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }
        public DbKontabiliteti.colGrupeKF ColGrupeKF
        {
            get
            {
                return colGrupeKF;
            }
            set
            {
                colGrupeKF = value;
            }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idTrupi"> id ritese e trupit </param>
        /// <param name="idKoka"> id e kokes</param>
        ///<param name="email">emaili</param>
        ///<param name="iddeleguesi">iddeleguesi</param>
        ///<param name="idperdrol">id e perdoruesi ose rolit</param>
        ///<param name="lloji">lloji</param>
        ///<param name="niveli">niveli</param>
        ///<param name="modifiko">modifiko</param>
        ///<param name="vleraLimit">vlera limit</param>
        public clsTrupiSkemaWorkFlow(int idTrupi, int idKoka, int idperdrol, int niveli, double vleraLimit, bool modifiko, int lloji, string email, int iddeleguesi, int nivelapr, int llojgrupikf, DbKontabiliteti.colGrupeKF grupekf, decimal dite , string pershkrimi)
        {
            this.idTrupi = idTrupi;

            this.idKoka = idKoka;
            this.idPerdRol = idperdrol;
            this.niveli = niveli;
            this.vleraLimit = vleraLimit;
            this.modifiko = modifiko;
            this.lloji = lloji;
            this.email = email;
            this.idDelegimi = iddeleguesi;
            this.niveliApr = nivelapr;
            this.llojGrupiKf = llojgrupikf;
            this.colGrupeKF = grupekf;
            this.dite = dite;
            this.pershkrimi = pershkrimi;
        }
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idKoka"> id e kokes</param>
        ///<param name="email">emaili</param>
        ///<param name="iddeleguesi">iddeleguesi</param>
        ///<param name="idperdrol">id e perdoruesi ose rolit</param>
        ///<param name="lloji">lloji</param>
        ///<param name="niveli">niveli</param>
        ///<param name="modifiko">modifiko</param>
        ///<param name="vleraLimit">vlera limit</param>
        public clsTrupiSkemaWorkFlow(int idKoka, int idperdrol, int niveli, double vleraLimit, bool modifiko, int lloji, string email, int iddeleguesi, int nivelapr, int llojgrupikf, decimal dite , string pershkrimi)
        {

            this.idKoka = idKoka;
            this.idPerdRol = idperdrol;
            this.niveli = niveli;
            this.vleraLimit = vleraLimit;
            this.modifiko = modifiko;
            this.lloji = lloji;
            this.email = email;
            this.idDelegimi = iddeleguesi;
            this.niveliApr = nivelapr;
            this.llojGrupiKf = llojgrupikf;
            this.dite = dite;
            this.pershkrimi = pershkrimi;
            this.ColGrupeKF = new DbKontabiliteti.colGrupeKF();
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idtrupi">id e trupit te katerise se zbritjes</param>
        public clsTrupiSkemaWorkFlow(int idtrupi)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            mbushTrupiSkeme(db.merrTrupiSkemaWorkFlow(idtrupi));
            db.Dispose();
        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTrupiSkemaWorkFlow()
        {
            this.ColGrupeKF = new DbKontabiliteti.colGrupeKF();
        }

        public clsTrupiSkemaWorkFlow(DataRow rreshti)
        {
            
            mbushTrupiSkeme(rreshti);
        }

        #endregion
        public bool merrTrupSipasKokesDhePerdoruesit(int idkoka, int idperdoruesi)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return merrTrupSipasKokesDhePerdoruesit(idkoka, idperdoruesi, db);
            }
        }


        public bool merrTrupSipasKokesDhePerdoruesit(int idkoka, int idperdoruesi, clsDatabaseAdmin db)
        {
            return mbushTrupiSkeme(db.ktheTrupiSkemaWorkFlowSipasKokesDhePerdoruesit(idkoka, idperdoruesi));
        }
        public bool merrTrupSipasKokesDhePerdoruesitDheNivelit(int idkoka, int idperdoruesi, int niveli, clsDatabaseAdmin db)
        {
            return mbushTrupiSkeme(db.ktheTrupiSkemaWorkFlowSipasKokesDhePerdoruesitDheNivelit(idkoka, idperdoruesi, niveli));
        }
        public bool merrTrupSipasKokesDheDeleguesi(int idkoka, int idperdoruesi, clsDatabaseAdmin db)
        {
            return mbushTrupiSkeme(db.ktheTrupiSkemaWorkFlowSipasKokesDheDeleguesi(idkoka, idperdoruesi));
        }
        #region Metoda Internal

        /// <summary>
        /// mbushja e trupit te skemes nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushTrupiSkeme(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {

                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["IDPERDROL"].ToString(), out idPerdRol);
                    int.TryParse(dbDataRow["NIVELI"].ToString(), out niveli);
                    int.TryParse(dbDataRow["LLOJGRUPIKF"].ToString(), out llojGrupiKf);
                    int.TryParse(dbDataRow["NIVELIAPROV"].ToString(), out niveliApr);
                    double.TryParse(dbDataRow["VLERALIMIT"].ToString(), out vleraLimit);
                    bool.TryParse(dbDataRow["MODIFIKO"].ToString(), out modifiko);
                    int.TryParse(dbDataRow["LLOJI"].ToString(), out lloji);
                    email = dbDataRow["EMAIL"].ToString();
                    grupeKF = dbDataRow["GRUPEKF"].ToString();
                    perdoruesi = dbDataRow["PERDORUESI"].ToString();
                    delegimi = dbDataRow["DELEGIMI"].ToString();
                    int.TryParse(dbDataRow["IDDELEGIMI"].ToString(), out idDelegimi);
                    decimal.TryParse(dbDataRow["DITE"].ToString(), out dite);
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    colGrupeKF = new DbKontabiliteti.colGrupeKF();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te skemes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
