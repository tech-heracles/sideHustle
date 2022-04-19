using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje grup banke
    ///  (Te dhenat  merren nga tabela : T_GRUPBANKE)
    /// </summary>
    public class clsGrupBanke
    {
        #region Atributet

        private int idGrupBanke;
        private string nrGrupBanke;
        private string pershkrimGrupBanke;
        private int idPerdoruesi;
        private bool llojArkaBanka;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupBanke(int idgrupbanke, string nrgrupbanke, string pershkrimgrupbanke, int idperdoruesi, bool llojarkabanka, int idndermarje, int idstatusdok)
        {
            idGrupBanke = idgrupbanke;
            nrGrupBanke = nrgrupbanke;
            pershkrimGrupBanke = pershkrimgrupbanke;
            idPerdoruesi = idperdoruesi;
            llojArkaBanka = llojarkabanka;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupBanke(string nrgrupbanke, string pershkrimgrupbanke, int idperdoruesi, bool llojarkabanka, int idndermarje, int idstatusdok)
        {
            nrGrupBanke = nrgrupbanke;
            pershkrimGrupBanke = pershkrimgrupbanke;
            idPerdoruesi = idperdoruesi;
            llojArkaBanka = llojarkabanka;
            idNdermarje = idndermarje;   
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i grupit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsGrupBanke(string kodi, int idnderm)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            mbushGrupBanke(data.ktheGrupBankeSipasKodit(kodi, idnderm));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsGrupBanke(int id)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            mbushGrupBanke(data.merrGrupBankeSipasId(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupBanke()
        {
        }

        public clsGrupBanke(DataRow rreshti)
        {
            
            mbushGrupBanke(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupBanke
        {
            get { return idGrupBanke; }
            set { idGrupBanke = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e grupit te bankes.
        /// </summary>
        public string NrGrupBanke
        {
            get { return nrGrupBanke; }
            set { nrGrupBanke = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit te bankes.
        /// </summary>
        public String PershkrimGrupBanke
        {
            get { return pershkrimGrupBanke; }
            set { pershkrimGrupBanke = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe celi grupin e bankes.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e arkes bankes.
        /// </summary>
        /// <example> true-banka, false-arka</example>
        public bool LlojArkaBanka
        {
            get { return llojArkaBanka; }
            set { llojArkaBanka = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
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
        /// Ruan objektin e grupit te bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.ruajGrupBanke"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_ruajt = data.ruajGrupBanke(this.IdGrupBanke, this.NrGrupBanke, this.PershkrimGrupBanke, this.IdPerdoruesi, this.LlojArkaBanka, this.IdNdermarje, this.idStatusDok);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e grupit te bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.modifikoGrupBanke"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_modifikua = data.modifikoGrupBanke(this.IdGrupBanke, this.NrGrupBanke, this.PershkrimGrupBanke, this.IdPerdoruesi, this.LlojArkaBanka, this.IdNdermarje, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e grupit te bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.fshiGrupBanke"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            clsMesazh u_fshi = data.fshiGrupBankeStatus(this.IdGrupBanke, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }
        
        /// <summary>
        /// Nuk perdoret.
        /// </summary>
        public void merr()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            data.merrGrupBanke(this.IdGrupBanke);
            data.Dispose();
        }

        

        #endregion

        #region Metoda Internal

        internal bool mbushGrupBanke(DataRow dbDataRowGrupBanke)
        {
            if (dbDataRowGrupBanke != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupBanke["IDGRUPBANKE"].ToString(), out idGrupBanke);
                    nrGrupBanke = dbDataRowGrupBanke["NRGRUPBANKE"].ToString();
                    pershkrimGrupBanke = dbDataRowGrupBanke["PERSHKRIMGRUPBANKE"].ToString();
                    int.TryParse(dbDataRowGrupBanke["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    bool.TryParse(dbDataRowGrupBanke["LLOJARKABANKA"].ToString(), out llojArkaBanka);
                    int.TryParse(dbDataRowGrupBanke["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowGrupBanke["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowGrupBanke["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowGrupBanke["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupit te bankes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion 
    }
}
