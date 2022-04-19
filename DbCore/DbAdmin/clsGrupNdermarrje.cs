using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje grup ndermarrje
    ///  (Te dhenat  merren nga tabela : T_GRUPNDERMARRJE)
    /// </summary>
    public class clsGrupNdermarrje
    {
        #region Atributet

        private int id;
        private string kodi;
        private string pershkrimi;
        private int idPerdoruesi;
        private int idKrijuesi;
        private int idLicenca;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupNdermarrje(int id, string kodi, string pershkrimi, int idperdoruesi, int idkrijuesi, int idlicenca, int idstatusdok)
        {
            this.id = id;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.idPerdoruesi = idperdoruesi;
            this.idKrijuesi = idkrijuesi;
            this.idLicenca = idlicenca;
            
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupNdermarrje(string kodi, string pershkrimi, int idperdoruesi, int idkrijuesi, int idlicenca, int idstatusdok)
        {
            
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.idPerdoruesi = idperdoruesi;
            this.idKrijuesi = idkrijuesi;
            this.idLicenca = idlicenca;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i grupit</param>
        /// <param name="idlicenca">id e ndermarrjes</param>
        public clsGrupNdermarrje(string kodi, int idlicenca)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushGrupNdermarrje(data.ktheGrupNdermarrjeSipasKodit(kodi, idlicenca));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsGrupNdermarrje(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushGrupNdermarrje(data.merrGrupNdermarrjeSipasId(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupNdermarrje()
        {
        }

        public clsGrupNdermarrje(DataRow rreshti)
        {
            
            mbushGrupNdermarrje(rreshti);
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
        /// Kthen/Vendos kodin e grupit .
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit .
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe celi .
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// perdoruesi qe e krijoi
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e licences.
        /// </summary>
        public int IdLicenca
        {
            get { return idLicenca; }
            set { idLicenca = value; }
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
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e grupit te ndermarjes ne tabelen perkatese ne databaze
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajGrupNdermarrje(this.id, this.kodi, this.pershkrimi, this.IdPerdoruesi, this.idKrijuesi, this.idLicenca, this.idStatusDok);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e grupit te ndermarjes ne tabelen perkatese ne databaze.Therret funksionin 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoGrupNdermarrje(this.id, this.kodi, this.pershkrimi, this.IdPerdoruesi, this.idKrijuesi, this.idLicenca, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e grupit te ndermarjes ne tabelen perkatese ne databaze
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiGrupNdermarrjeStatus(this.id, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Nuk perdoret.
        /// </summary>
        public void merr()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.merrGrupNdermarrje(this.Id);
            data.Dispose();
        }



        #endregion

        #region Metoda Internal

        internal bool mbushGrupNdermarrje(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    kodi = dbDataRow["KODI"].ToString();
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRow["IDLICENCA"].ToString(), out idLicenca);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupit te ndermarjes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
